namespace OtherMonad;

public static partial class Maybe
{
    /// <inheritdoc cref="Enumerable.Single"/>
    public static Maybe<TSource> Single<TSource>(this IEnumerable<Maybe<TSource>> source, Func<TSource, bool> predicate)
    {
        var result = Maybe<TSource>.None;

        foreach (var src in source)
        {
            if (src.HasValue)
            {
                if (predicate(src.Value))
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
        }

        if (!result.HasValue)
        {
            throw new InvalidOperationException("Sequence contains no matching element");
        }

        return result;
    }

    /// <inheritdoc cref="Enumerable.Single"/>
    public static Maybe<TSource> Single<TSource>(this IEnumerable<Maybe<TSource>> source)
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

        if (!result.HasValue)
        {
            throw new InvalidOperationException("Sequence contains no matching element");
        }

        return result;
    }
}