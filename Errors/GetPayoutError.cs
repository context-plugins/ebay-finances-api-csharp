using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core.ErrorResponse;
using EBayFinancesApi.Core.Models;

namespace EBayFinancesApi.Errors;

public sealed class GetPayoutError : ApiError
{
    private readonly Optional<RawError> _noContentValue;

    private GetPayoutError(Optional<RawError> noContentValue, Optional<RawError> fallback) : base(fallback)
    {
        _noContentValue = noContentValue;
    }

    private static GetPayoutError AsNoContent(RawError value) =>
        new(Optional<RawError>.Some(value), default);

    private static GetPayoutError AsFallback(RawError value) =>
        new(default, Optional<RawError>.Some(value));

    public bool TryGetNoContent(out RawError value) => _noContentValue.TryGetValue(out value);

    internal static Task<GetPayoutError> Create(HttpResponseMessage response, CancellationToken ct) =>
        (int)response.StatusCode switch
        {
            400 or 404 or 500 => FromRawBody(response, ct).As(AsNoContent),
            _ => FromRawBody(response, ct).As(AsFallback)
        };
}

internal sealed class GetPayoutErrorResponse : IErrorResponse<GetPayoutError>
{
    public static GetPayoutErrorResponse Instance { get; } = new();

    private GetPayoutErrorResponse()
    {
    }

    public Task<GetPayoutError> Map(HttpResponseMessage response, CancellationToken ct) =>
        GetPayoutError.Create(response, ct);
}
