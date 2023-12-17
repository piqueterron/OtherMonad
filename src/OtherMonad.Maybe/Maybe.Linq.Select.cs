namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <inheritdoc cref="Enumerable.Select"/>
    public static IEnumerable<Maybe<TResult>> Select<TSource, TResult>(this IEnumerable<Maybe<TSource>> source, Func<TSource, TResult> selector)
    {
        foreach (var src in source)
        {
            if (src.HasValue)
            {
                yield return selector(src.Value);
            }
        }
    }

    /// <inheritdoc cref="Enumerable.Select"/>
    public static IEnumerable<Maybe<TResult>> Select<TSource, TResult>(this IEnumerable<Maybe<TSource>> source, Func<TSource, int, TResult> selector)
    {
        var index = -1;
        using var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var element = enumerator.Current;

            if (element.HasValue)
            {
                yield return selector(element.Value, ++index);
            }
        }
    }
}