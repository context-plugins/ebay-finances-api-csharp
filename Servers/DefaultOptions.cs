using EBayFinancesApi.Core.Models;

namespace EBayFinancesApi.Servers;

public class DefaultOptions
{
    public ProductionOptions Production { get; set; } = new();

    internal UrlTemplate Resolve(ServerEnvironment environment, string path) =>
        environment.Match(() => new UrlTemplate(Production.BaseUrl,
                path,
                [TemplateParam.ForServer("basePath", Production.BasePath)]));

    public class ProductionOptions
    {
        public string BaseUrl { get; set; } = "https://apiz.ebay.com{basePath}";
        public string BasePath { get; set; } = "/sell/finances/v1";
    }
}
