using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core.ErrorResponse;
using EBayFinancesApi.Core.Models;

namespace EBayFinancesApi.Errors;

public sealed class GetTransactionSummaryError : ApiError
{
    private readonly Optional<RawError> _noContentValue;

    private GetTransactionSummaryError(Optional<RawError> noContentValue, Optional<RawError> fallback) : base(fallback)
    {
        _noContentValue = noContentValue;
    }

    private static GetTransactionSummaryError AsNoContent(RawError value) =>
        new(Optional<RawError>.Some(value), default);

    private static GetTransactionSummaryError AsFallback(RawError value) =>
        new(default, Optional<RawError>.Some(value));

    public bool TryGetNoContent(out RawError value) => _noContentValue.TryGetValue(out value);

    internal static Task<GetTransactionSummaryError> Create(HttpResponseMessage response, CancellationToken ct) =>
        (int)response.StatusCode switch
        {
            400 or 500 => FromRawBody(response, ct).As(AsNoContent),
            _ => FromRawBody(response, ct).As(AsFallback)
        };
}

internal sealed class GetTransactionSummaryErrorResponse : IErrorResponse<GetTransactionSummaryError>
{
    public static GetTransactionSummaryErrorResponse Instance { get; } = new();

    private GetTransactionSummaryErrorResponse()
    {
    }

    public Task<GetTransactionSummaryError> Map(HttpResponseMessage response, CancellationToken ct) =>
        GetTransactionSummaryError.Create(response, ct);
}
