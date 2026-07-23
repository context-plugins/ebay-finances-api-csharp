using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core.ErrorResponse;
using EBayFinancesApi.Core.Models;

namespace EBayFinancesApi.Errors;

public sealed class GetPayoutSummaryError : ApiError
{
    private readonly Optional<RawError> _noContentValue;

    private GetPayoutSummaryError(Optional<RawError> noContentValue, Optional<RawError> fallback) : base(fallback)
    {
        _noContentValue = noContentValue;
    }

    private static GetPayoutSummaryError AsNoContent(RawError value) =>
        new(Optional<RawError>.Some(value), default);

    private static GetPayoutSummaryError AsFallback(RawError value) =>
        new(default, Optional<RawError>.Some(value));

    public bool TryGetNoContent(out RawError value) => _noContentValue.TryGetValue(out value);

    internal static Task<GetPayoutSummaryError> Create(HttpResponseMessage response, CancellationToken ct) =>
        (int)response.StatusCode switch
        {
            400 or 500 => FromRawBody(response, ct).As(AsNoContent),
            _ => FromRawBody(response, ct).As(AsFallback)
        };
}

internal sealed class GetPayoutSummaryErrorResponse : IErrorResponse<GetPayoutSummaryError>
{
    public static GetPayoutSummaryErrorResponse Instance { get; } = new();

    private GetPayoutSummaryErrorResponse()
    {
    }

    public Task<GetPayoutSummaryError> Map(HttpResponseMessage response, CancellationToken ct) =>
        GetPayoutSummaryError.Create(response, ct);
}
