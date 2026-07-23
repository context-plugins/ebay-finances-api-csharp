using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core.ErrorResponse;
using EBayFinancesApi.Core.Models;

namespace EBayFinancesApi.Errors;

public sealed class GetTransferError : ApiError
{
    private readonly Optional<RawError> _noContentValue;

    private GetTransferError(Optional<RawError> noContentValue, Optional<RawError> fallback) : base(fallback)
    {
        _noContentValue = noContentValue;
    }

    private static GetTransferError AsNoContent(RawError value) =>
        new(Optional<RawError>.Some(value), default);

    private static GetTransferError AsFallback(RawError value) =>
        new(default, Optional<RawError>.Some(value));

    public bool TryGetNoContent(out RawError value) => _noContentValue.TryGetValue(out value);

    internal static Task<GetTransferError> Create(HttpResponseMessage response, CancellationToken ct) =>
        (int)response.StatusCode switch
        {
            400 or 404 or 500 => FromRawBody(response, ct).As(AsNoContent),
            _ => FromRawBody(response, ct).As(AsFallback)
        };
}

internal sealed class GetTransferErrorResponse : IErrorResponse<GetTransferError>
{
    public static GetTransferErrorResponse Instance { get; } = new();

    private GetTransferErrorResponse()
    {
    }

    public Task<GetTransferError> Map(HttpResponseMessage response, CancellationToken ct) =>
        GetTransferError.Create(response, ct);
}
