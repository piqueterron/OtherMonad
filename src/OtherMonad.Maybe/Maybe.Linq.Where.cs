namespace OtherMonad;

public static partial class Maybe
{
    /// <inheritdoc cref="Enumerable.Where"/>
    public static IEnumerable<Maybe<TSource>> Where<TSource>(this IEnumerable<Maybe<TSource>> source, Func<TSource, bool> predicate)
    {
        foreach (var src in source)
        {
            if (src.HasValue)
            {
                if (predicate(src.Value))
                {
                    yield return src;
                }
            }
        }
    }

    /// <inheritdoc cref="Enumerable.Where"/>
    public static IEnumerable<Maybe<TSource>> Where<TSource>(this IEnumerable<Maybe<TSource>> source, Func<TSource, int, bool> predicate)
    {
        var index = -1;
        using var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var element = enumerator.Current;

            if (element.HasValue)
            {
                if (predicate(element.Value, ++index))
                {
                    yield return element;
                }
            }
        }
    }
}
