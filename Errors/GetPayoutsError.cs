using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core.ErrorResponse;
using EBayFinancesApi.Core.Models;

namespace EBayFinancesApi.Errors;

public sealed class GetPayoutsError : ApiError
{
    private readonly Optional<RawError> _noContentValue;

    private GetPayoutsError(Optional<RawError> noContentValue, Optional<RawError> fallback) : base(fallback)
    {
        _noContentValue = noContentValue;
    }

    private static GetPayoutsError AsNoContent(RawError value) =>
        new(Optional<RawError>.Some(value), default);

    private static GetPayoutsError AsFallback(RawError value) =>
        new(default, Optional<RawError>.Some(value));

    public bool TryGetNoContent(out RawError value) => _noContentValue.TryGetValue(out value);

    internal static Task<GetPayoutsError> Create(HttpResponseMessage response, CancellationToken ct) =>
        (int)response.StatusCode switch
        {
            400 or 500 => FromRawBody(response, ct).As(AsNoContent),
            _ => FromRawBody(response, ct).As(AsFallback)
        };
}

internal sealed class GetPayoutsErrorResponse : IErrorResponse<GetPayoutsError>
{
    public static GetPayoutsErrorResponse Instance { get; } = new();

    private GetPayoutsErrorResponse()
    {
    }

    public Task<GetPayoutsError> Map(HttpResponseMessage response, CancellationToken ct) =>
        GetPayoutsError.Create(response, ct);
}
