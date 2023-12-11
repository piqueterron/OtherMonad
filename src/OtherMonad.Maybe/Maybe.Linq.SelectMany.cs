namespace OtherMonad;

public static partial class Maybe
{
    /// <inheritdoc cref="Enumerable.SelectMany"/>
    public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
    {
        foreach (var src in source)
        {
            var maybe = (Maybe<TSource>)src;

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
    public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
    {
        var index = -1;
        using var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var current = (Maybe<TSource>)enumerator.Current;

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
    public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
    {
        foreach (var src in source)
        {
            var maybe = (Maybe<TSource>)src;

            if (maybe.HasValue)
            {
                foreach (var element in selector(maybe.Value))
                {
                    yield return element;
                }
            }
        }
    }

    /// <inheritdoc cref="Enumerable.SelectMany"/>
    public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
    {
        var index = -1;
        using var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var current = (Maybe<TSource>)enumerator.Current;

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