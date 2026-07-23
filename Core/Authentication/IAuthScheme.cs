using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EBayFinancesApi.Core.Authentication;

public interface IAuthScheme
{
    ValueTask Apply(HttpRequestMessage request, CancellationToken cancellationToken);
}

internal static class AuthSchemeExtensions
{
    extension(IEnumerable<IAuthScheme> authSchemes)
    {
        public async ValueTask Apply(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
        {
            foreach (var authScheme in authSchemes)
            {
                await authScheme.Apply(httpRequest, cancellationToken).ConfigureAwait(false);
            }
        }

        public void InvalidateRevocable()
        {
            foreach (var scheme in authSchemes.OfType<IRevocableAuthScheme>())
                scheme.Invalidate();
        }
    }
}
