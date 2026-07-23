using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core.ErrorResponse;
using EBayFinancesApi.Core.Models;

namespace EBayFinancesApi.Errors;

public sealed class GetSellerFundsSummaryError : ApiError
{
    private readonly Optional<RawError> _noContentValue;

    private GetSellerFundsSummaryError(Optional<RawError> noContentValue, Optional<RawError> fallback) : base(fallback)
    {
        _noContentValue = noContentValue;
    }

    private static GetSellerFundsSummaryError AsNoContent(RawError value) =>
        new(Optional<RawError>.Some(value), default);

    private static GetSellerFundsSummaryError AsFallback(RawError value) =>
        new(default, Optional<RawError>.Some(value));

    public bool TryGetNoContent(out RawError value) => _noContentValue.TryGetValue(out value);

    internal static Task<GetSellerFundsSummaryError> Create(HttpResponseMessage response, CancellationToken ct) =>
        (int)response.StatusCode switch
        {
            500 => FromRawBody(response, ct).As(AsNoContent),
            _ => FromRawBody(response, ct).As(AsFallback)
        };
}

internal sealed class GetSellerFundsSummaryErrorResponse : IErrorResponse<GetSellerFundsSummaryError>
{
    public static GetSellerFundsSummaryErrorResponse Instance { get; } = new();

    private GetSellerFundsSummaryErrorResponse()
    {
    }

    public Task<GetSellerFundsSummaryError> Map(HttpResponseMessage response, CancellationToken ct) =>
        GetSellerFundsSummaryError.Create(response, ct);
}
