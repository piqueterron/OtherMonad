namespace OtherMonad;

/// <summary>
/// LINQ query-comprehension support for <see cref="Maybe{TSource}"/>.
/// These methods enable the C# query syntax (<c>from ... select ...</c>) over <see cref="Maybe{TSource}"/>.
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// Projects the value of a <see cref="Maybe{TSource}"/> into a new form. Equivalent to <see cref="Map{TSource,TResult}(Maybe{TSource},Func{TSource,TResult})"/>.
    /// Enables the <c>select</c> clause of the C# query syntax.
    /// </summary>
    /// <typeparam name="TSource">The type of the source value.</typeparam>
    /// <typeparam name="TResult">The type of the projected value.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> to project.</param>
    /// <param name="selector">A projection function applied to the contained value.</param>
    /// <returns>A <see cref="Maybe{TResult}"/> containing the projected value, or <see cref="Maybe{TResult}.None"/> if the source has no value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static Maybe<TResult> Select<TSource, TResult>(this Maybe<TSource> source, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return source.Map(selector);
    }

    /// <summary>
    /// Projects the value of a <see cref="Maybe{TSource}"/> into a new <see cref="Maybe{TResult}"/> and flattens the result.
    /// Equivalent to <see cref="Bind{TSource,TResult}(Maybe{TSource},Func{TSource,Maybe{TResult}})"/>.
    /// </summary>
    /// <typeparam name="TSource">The type of the source value.</typeparam>
    /// <typeparam name="TResult">The type of the projected value.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> to project.</param>
    /// <param name="selector">A projection function that returns a <see cref="Maybe{TResult}"/>.</param>
    /// <returns>The <see cref="Maybe{TResult}"/> returned by <paramref name="selector"/>, or <see cref="Maybe{TResult}.None"/> if the source has no value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static Maybe<TResult> SelectMany<TSource, TResult>(this Maybe<TSource> source, Func<TSource, Maybe<TResult>> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return source.Bind(selector);
    }

    /// <summary>
    /// Projects the value of a <see cref="Maybe{TSource}"/> into an intermediate <see cref="Maybe{TCollection}"/> and then combines the
    /// original and intermediate values into a final result. This is the overload required by the C# query syntax when multiple
    /// <c>from</c> clauses are used.
    /// </summary>
    /// <typeparam name="TSource">The type of the source value.</typeparam>
    /// <typeparam name="TCollection">The type of the intermediate value.</typeparam>
    /// <typeparam name="TResult">The type of the final projected value.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> to project.</param>
    /// <param name="collectionSelector">A function that returns an intermediate <see cref="Maybe{TCollection}"/> from the source value.</param>
    /// <param name="resultSelector">A function that combines the source and intermediate values into the final result.</param>
    /// <returns>A <see cref="Maybe{TResult}"/> containing the combined value, or <see cref="Maybe{TResult}.None"/> if any step has no value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="collectionSelector"/> or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
    public static Maybe<TResult> SelectMany<TSource, TCollection, TResult>(
        this Maybe<TSource> source,
        Func<TSource, Maybe<TCollection>> collectionSelector,
        Func<TSource, TCollection, TResult> resultSelector)
    {
        ArgumentNullException.ThrowIfNull(collectionSelector);
        ArgumentNullException.ThrowIfNull(resultSelector);

        return source.Bind(value => collectionSelector(value).Map(collection => resultSelector(value, collection)));
    }
}
