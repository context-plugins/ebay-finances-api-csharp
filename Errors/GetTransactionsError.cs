using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core.ErrorResponse;
using EBayFinancesApi.Core.Models;

namespace EBayFinancesApi.Errors;

public sealed class GetTransactionsError : ApiError
{
    private readonly Optional<RawError> _noContentValue;

    private GetTransactionsError(Optional<RawError> noContentValue, Optional<RawError> fallback) : base(fallback)
    {
        _noContentValue = noContentValue;
    }

    private static GetTransactionsError AsNoContent(RawError value) =>
        new(Optional<RawError>.Some(value), default);

    private static GetTransactionsError AsFallback(RawError value) =>
        new(default, Optional<RawError>.Some(value));

    public bool TryGetNoContent(out RawError value) => _noContentValue.TryGetValue(out value);

    internal static Task<GetTransactionsError> Create(HttpResponseMessage response, CancellationToken ct) =>
        (int)response.StatusCode switch
        {
            400 or 500 => FromRawBody(response, ct).As(AsNoContent),
            _ => FromRawBody(response, ct).As(AsFallback)
        };
}

internal sealed class GetTransactionsErrorResponse : IErrorResponse<GetTransactionsError>
{
    public static GetTransactionsErrorResponse Instance { get; } = new();

    private GetTransactionsErrorResponse()
    {
    }

    public Task<GetTransactionsError> Map(HttpResponseMessage response, CancellationToken ct) =>
        GetTransactionsError.Create(response, ct);
}
