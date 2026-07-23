using System.Net.Http;
using EBayFinancesApi.Api;
using EBayFinancesApi.Core;
using EBayFinancesApi.Core.Models;

namespace EBayFinancesApi;

/// <summary>
/// This API is used to retrieve seller payouts and monetary transaction details related to those payouts.
/// </summary>
public sealed class EBayFinancesApiClient
{
    public EBayFinancesApiClient(HttpClient httpClient, EBayFinancesApiClientOptions options)
    {
        var server = new Server(options.Environment, options.Server);
        var queryParameterFactory = new QueryParameterFactory([]);
        var templateParamsFactory = new TemplateParamsFactory([]);
        var urlFactory = new UriFactory(queryParameterFactory, templateParamsFactory);
        var httpStatusPolicy = new HttpStatusPolicy([]);
        var headersFactory =
            new HeadersFactory([new HeaderParam("User-Agent", "EBayFinancesApiClient/v1.15.0 CSharp"),
                    new HeaderParam("X-APIMatic-Lang", "CSharp"),
                    new HeaderParam("X-APIMatic-Package-Version", "v1.15.0"),
                    new HeaderParam("X-APIMatic-Gen-Version", "4.0.0"),
                    new HeaderParam("X-APIMatic-OS", RuntimeEnvironment.Os),
                    new HeaderParam("X-APIMatic-Runtime", RuntimeEnvironment.Runtime)]);
        var resiliencePipelineFactory = new ResiliencePipelineFactory(options.Retry);
        var rawClient =
            new RawClient(httpClient, urlFactory, httpStatusPolicy, headersFactory, resiliencePipelineFactory);
        var auth = new AuthSchemes(options, server, rawClient, urlFactory);
        PayoutModel = new PayoutModel(rawClient, server, auth);
        SellerFundsSummary = new SellerFundsSummary(rawClient, server, auth);
        TransactionModel = new TransactionModel(rawClient, server, auth);
        TransferModel = new TransferModel(rawClient, server, auth);
    }

    public PayoutModel PayoutModel { get; }

    public SellerFundsSummary SellerFundsSummary { get; }

    public TransactionModel TransactionModel { get; }

    public TransferModel TransferModel { get; }
}
