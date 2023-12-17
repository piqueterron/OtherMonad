namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <inheritdoc cref="Enumerable.SelectMany"/>
    public static IEnumerable<Maybe<TResult>> SelectMany<TSource, TCollection, TResult>(this IEnumerable<Maybe<TSource>> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
    {
        foreach (var src in source)
        {
            var maybe = src;

            if (maybe.HasValue)
            {
                foreach (var element in collectionSelector(maybe.Value))
                {
                    yield return resultSelector(maybe.Value, element);
                }
            }
        }
    }

    /// <inheritdoc cref="Enumerable.SelectMany"/>
    public static IEnumerable<Maybe<TResult>> SelectMany<TSource, TCollection, TResult>(this IEnumerable<Maybe<TSource>> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
    {
        var index = -1;
        using var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;

            if (current.HasValue)
            {
                foreach (var element in collectionSelector(current.Value, ++index))
                {
                    yield return resultSelector(current.Value, element);
                }
            }
        }
    }

    /// <inheritdoc cref="Enumerable.SelectMany"/>
    public static IEnumerable<Maybe<TResult>> SelectMany<TSource, TResult>(this IEnumerable<Maybe<TSource>> source, Func<TSource, IEnumerable<TResult>> selector)
    {
        foreach (var src in source)
        {
            if (src.HasValue)
            {
                foreach (var element in selector(src.Value))
                {
                    yield return element;
                }
            }
        }
    }

    /// <inheritdoc cref="Enumerable.SelectMany"/>
    public static IEnumerable<Maybe<TResult>> SelectMany<TSource, TResult>(this IEnumerable<Maybe<TSource>> source, Func<TSource, int, IEnumerable<TResult>> selector)
    {
        var index = -1;
        using var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;

            if (current.HasValue)
            {
                foreach (var element in selector(current.Value, ++index))
                {
                    yield return element;
                }
            }
        }
    }
}