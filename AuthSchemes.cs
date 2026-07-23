using EBayFinancesApi.Core;
using EBayFinancesApi.Core.Authentication;
using EBayFinancesApi.Core.Authentication.OAuth2;
using EBayFinancesApi.Core.Authentication.OAuth2.AuthorizationCode;

namespace EBayFinancesApi;

internal sealed class AuthSchemes
{
    public IAuthScheme ApiAuth { get; }

    public AuthSchemes(EBayFinancesApiClientOptions options,
        Server server,
        RawClient rawClient,
        UriFactory urlFactory)
    {
        ApiAuth =
            OAuth2RefreshableScheme<OAuth2AuthorizationCodeCredentials>.Create(options.ApiAuth,
                options.ApiAuthTokenStrategy ??
                    OAuth2AuthorizationCodeStrategy.ForBasicAuthRequest(server.AuthServer("/authorize"),
                        server.AccessTokenServer("/token"),
                        rawClient,
                        urlFactory));
    }
}
