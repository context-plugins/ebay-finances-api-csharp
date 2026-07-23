using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core.Exceptions;

namespace EBayFinancesApi.Core.Authentication;

/// <summary>
/// Represents multiple alternative schemes (OR logic).
/// The first scheme that succeeds wins; if all fail, throws <see cref="AuthSchemeException"/>.
/// </summary>
internal sealed class AuthSchemeAny : IRevocableAuthScheme
{
    private readonly IReadOnlyList<IAuthScheme> _schemes;

    public AuthSchemeAny(params IAuthScheme[] schemes)
    {
        if (schemes is null || schemes.Length == 0)
            throw new ArgumentException("Must provide at least one scheme.", nameof(schemes));
        if (schemes.Any(s => s is null))
            throw new ArgumentException("All schemes must be non-null.", nameof(schemes));
        _schemes = schemes;
    }

    public async ValueTask Apply(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var failures = new List<Exception>(_schemes.Count);

        foreach (var scheme in _schemes)
        {
            try
            {
                await scheme.Apply(request, cancellationToken).ConfigureAwait(false);
                return;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                failures.Add(ex);
            }
        }

        throw new AuthSchemeException("No authentication scheme succeeded.", failures);
    }

    // We don't track which inner scheme won the last Apply, so on a 401 we invalidate every
    // revocable inner scheme. Over-invalidation is safe — at worst the next request through an
    // unrelated scheme pays for one extra credential fetch.
    public void Invalidate() => _schemes.InvalidateRevocable();
}
