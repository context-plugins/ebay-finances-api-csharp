using System.Net.Http;

namespace EBayFinancesApi.Core.Request;

internal interface IRequest
{
    HttpContent Get();

    bool CanRetry { get; }
}