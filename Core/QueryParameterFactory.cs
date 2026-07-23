using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EBayFinancesApi.Core.Models;

namespace EBayFinancesApi.Core;

internal sealed class QueryParameterFactory
{
    private readonly IReadOnlyCollection<Param> _defaultQueryParams;

    public QueryParameterFactory(IReadOnlyCollection<Param> defaultQueryParams) =>
        _defaultQueryParams = defaultQueryParams;

    public string Serialize(IReadOnlyCollection<Param> queryParams)
    {
        var totalParams = _defaultQueryParams.Concat(queryParams);
        var parts = GenerateParts(totalParams);
        return string.Join("&", parts);
    }

    private static IEnumerable<string> GenerateParts(IEnumerable<Param> queryParams)
    {
        foreach (var queryParam in queryParams)
        {
            var flattened = ParameterFlattener.Flatten(queryParam);

            foreach (var f in flattened) yield return $"{Uri.EscapeDataString(f.Key)}={Uri.EscapeDataString(f.Value)}";
        }
    }
}

internal static class QueryParamExtensions
{
    public static bool TryGetQueryParam<T>(string url, string paramName, out T value)
    {
        value = default!;

        if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(paramName))
            return false;

        var uri = url.StartsWith("http", StringComparison.OrdinalIgnoreCase)
            ? new Uri(url)
            : new Uri("http://dummy" + url);

        var raw = GetQueryParamRaw(uri.Query, paramName);
        return raw is not null && TryParsePrimitive(raw, out value);
    }

    private static string? GetQueryParamRaw(string query, string paramName)
    {
        var raw = query.TrimStart('?');
        if (string.IsNullOrEmpty(raw)) return null;

        foreach (var pair in raw.Split('&'))
        {
            var idx = pair.IndexOf('=');
            if (idx < 0) continue;
            var key = Uri.UnescapeDataString(pair[..idx]);
            if (string.Equals(key, paramName, StringComparison.Ordinal))
                return Uri.UnescapeDataString(pair[(idx + 1)..]);
        }
        return null;
    }

    private static bool TryParsePrimitive<T>(string raw, out T value)
    {
        value = default!;

        var type = typeof(T);

        if (type == typeof(string))
        {
            value = (T)(object)raw;
            return true;
        }

        if (type == typeof(int))
        {
            if (int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
            {
                value = (T)(object)result;
                return true;
            }
            return false;
        }

        if (type == typeof(long))
        {
            if (long.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
            {
                value = (T)(object)result;
                return true;
            }
            return false;
        }

        if (type == typeof(double))
        {
            if (double.TryParse(raw, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var result))
            {
                value = (T)(object)result;
                return true;
            }
            return false;
        }

        if (type == typeof(decimal))
        {
            if (decimal.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
            {
                value = (T)(object)result;
                return true;
            }
            return false;
        }

        if (type == typeof(bool))
        {
            if (bool.TryParse(raw, out var result))
            {
                value = (T)(object)result;
                return true;
            }
            return false;
        }

        if (type == typeof(Guid))
        {
            if (Guid.TryParse(raw, out var result))
            {
                value = (T)(object)result;
                return true;
            }
            return false;
        }

        // Unsupported type
        return false;
    }
}