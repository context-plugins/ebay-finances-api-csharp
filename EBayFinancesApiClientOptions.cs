using EBayFinancesApi.Core.Authentication.OAuth2;
using EBayFinancesApi.Core.Authentication.OAuth2.AuthorizationCode;
using EBayFinancesApi.Core.Configuration;
using EBayFinancesApi.Servers;

namespace EBayFinancesApi;

public class EBayFinancesApiClientOptions
{
    public ServerEnvironment Environment { get; set; } = ServerEnvironment.Default();
    public RetryOptions Retry { get; set; } = RetryOptions.Default();
    public ServerOptions Server { get; set; } = new();
    /// <summary>
    /// The security definitions for this API. Please check individual operations for applicable scopes.
    /// </summary>
    public OAuth2AuthorizationCodeCredentials? ApiAuth { get; set; }
    public IOAuth2RefreshableTokenStrategy<OAuth2AuthorizationCodeCredentials>? ApiAuthTokenStrategy { get; set; }
}
