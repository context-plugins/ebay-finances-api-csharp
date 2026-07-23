using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EBayFinancesApi;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddEBayFinancesApiClient(Action<EBayFinancesApiClientOptions>? configure = null)
        {
            var options = new EBayFinancesApiClientOptions();
            configure?.Invoke(options);
            services.AddHttpClient();
            services.AddSingleton(sp =>
                {
                    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient();
                    return new EBayFinancesApiClient(httpClient, options);
                });
            return services;
        }
    }
}
