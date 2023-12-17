namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <inheritdoc cref="Enumerable.SingleOrDefault"/>
    public static Maybe<TSource> SingleOrDefault<TSource>(this IEnumerable<Maybe<TSource>> source)
    {
        var result = Maybe<TSource>.None;

        foreach (var src in source)
        {
            if (src.HasValue)
            {
                if (!result.HasValue)
                {
                    result = src;
                }
                else
                {
                    throw new InvalidOperationException("Sequence contains more than one matching element");
                }
            }
        }

        return result;
    }

    /// <inheritdoc cref="Enumerable.SingleOrDefault"/>
    public static Maybe<TSource> SingleOrDefault<TSource>(this IEnumerable<Maybe<TSource>> source, TSource defaultValue)
    {
        var result = source.SingleOrDefault();
        return result.HasValue ? result : defaultValue;
    }

    /// <inheritdoc cref="Enumerable.SingleOrDefault"/>
    public static Maybe<TSource> SingleOrDefault<TSource>(this IEnumerable<Maybe<TSource>> sources, Func<TSource, bool> predicate)
    {
        var result = Maybe<TSource>.None;

        foreach (var source in sources)
        {
            if (source.HasValue)
            {
                if (predicate(source.Value))
                {
                    if (!result.HasValue)
                    {
                        result = source;
                    }
                    else
                    {
                        throw new InvalidOperationException("Sequence contains more than one matching element");
                    }
                }
            }
        }

        return result;
    }

    /// <inheritdoc cref="Enumerable.SingleOrDefault"/>
    public static Maybe<TSource> SingleOrDefault<TSource>(this IEnumerable<Maybe<TSource>> source, Func<TSource, bool> predicate, TSource defaultValue)
    {
        var result = source.SingleOrDefault(predicate);
        return result.HasValue ? result : defaultValue;
    }
}