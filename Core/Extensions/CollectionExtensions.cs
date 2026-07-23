using System;
using System.Collections.Generic;
using System.Linq;

namespace EBayFinancesApi.Core.Extensions;

internal static class CollectionExtensions
{
    public static IReadOnlyList<TResult> Map<TSource, TResult>(
        this IReadOnlyList<TSource> source, Func<TSource, TResult> selector) =>
        source.Select(selector).ToList();

    public static IReadOnlyDictionary<string, TResult> Map<TSource, TResult>(
        this IReadOnlyDictionary<string, TSource> source, Func<TSource, TResult> selector) =>
        source.ToDictionary(kv => kv.Key, kv => selector(kv.Value));

    public static IReadOnlyList<IReadOnlyDictionary<string, TResult>> Map<TSource, TResult>(
        this IReadOnlyList<IReadOnlyDictionary<string, TSource>> source, Func<TSource, TResult> selector) =>
        source.Select(d => d.Map(selector)).ToList();

    public static IReadOnlyDictionary<string, IReadOnlyList<TResult>> Map<TSource, TResult>(
        this IReadOnlyDictionary<string, IReadOnlyList<TSource>> source, Func<TSource, TResult> selector) =>
        source.ToDictionary(kv => kv.Key, kv => kv.Value.Map(selector));
}