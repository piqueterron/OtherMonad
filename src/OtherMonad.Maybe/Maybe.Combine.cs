namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// Combines two <see cref="Maybe{T}"/> instances using a combining function. If the combination fails, returns a default value.
    /// </summary>
    /// <typeparam name="TSource">The type of the first value.</typeparam>
    /// <typeparam name="TCombine">The type of the second value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The first <see cref="Maybe{TSource}"/>.</param>
    /// <param name="other">The second <see cref="Maybe{TCombine}"/>.</param>
    /// <param name="select">A function to combine both values.</param>
    /// <param name="defaultValueFactory">A function that produces a default value if combination fails.</param>
    /// <returns>A <see cref="Maybe{TResult}"/> with the combined value, or the result of <paramref name="defaultValueFactory"/> if combination fails.</returns>
    public static Maybe<TResult> TryCombine<TSource, TCombine, TResult>(this Maybe<TSource> source, Maybe<TCombine> other, Func<TSource, TCombine, TResult> select, Func<TResult> defaultValueFactory)
    {
        try
        {
            return source.Combine(other, select);
        }
        catch (Exception)
        {
            return defaultValueFactory();
        }
    }

    /// <summary>
    /// Combines two <see cref="Maybe{T}"/> instances using a combining function if both have values.
    /// </summary>
    /// <typeparam name="TSource">The type of the first value.</typeparam>
    /// <typeparam name="TCombine">The type of the second value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The first <see cref="Maybe{TSource}"/>.</param>
    /// <param name="other">The second <see cref="Maybe{TCombine}"/>.</param>
    /// <param name="select">A function to combine both values.</param>
    /// <returns>A <see cref="Maybe{TResult}"/> with the combined value if both inputs have values; otherwise, <see cref="Maybe{TResult}.None"/>.</returns>
    public static Maybe<TResult> Combine<TSource, TCombine, TResult>(this Maybe<TSource> source, Maybe<TCombine> other, Func<TSource, TCombine, TResult> select)
    {
        if (!source.HasValue || !other.HasValue)
            return Maybe<TResult>.None;

        return select(source.Value, other.Value);
    }
}