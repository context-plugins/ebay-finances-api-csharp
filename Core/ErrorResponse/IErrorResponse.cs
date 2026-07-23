using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EBayFinancesApi.Core.ErrorResponse;

public interface IErrorResponse<TError>
{
    Task<TError> Map(HttpResponseMessage response, CancellationToken ct);
}