namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <inheritdoc cref="Enumerable.FirstOrDefault"/>
    public static Maybe<TSource> FirstOrDefault<TSource>(this IEnumerable<Maybe<TSource>> source)
    {
        foreach (var src in source)
        {
            if (src.HasValue)
            {
                return src;
            }
        }

        return Maybe<TSource>.None;
    }

    /// <inheritdoc cref="Enumerable.FirstOrDefault"/>
    public static Maybe<TSource> FirstOrDefault<TSource>(this IEnumerable<Maybe<TSource>> source, Func<TSource, bool> predicate)
    {
        foreach (var src in source)
        {
            if (src.HasValue)
            {
                if (predicate(src.Value))
                {
                    return src;
                }
            }
        }

        return Maybe<TSource>.None;
    }

    /// <inheritdoc cref="Enumerable.FirstOrDefault"/>
    public static Maybe<TSource> FirstOrDefault<TSource>(this IEnumerable<Maybe<TSource>> source, Func<TSource, bool> predicate, TSource defaultValue)
    {
        var result = source.FirstOrDefault(predicate);

        return result.HasValue ? result : defaultValue;
    }
}