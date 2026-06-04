namespace OtherMonad;

/// <summary>
/// LINQ query-comprehension support for <see cref="Either{TLeft,TRight}"/>.
/// These methods enable the C# query syntax (<c>from ... select ...</c>) over the Right (success) value.
/// A Left (failure) value short-circuits and is propagated unchanged.
/// </summary>
public static partial class Either
{
    /// <summary>
    /// Projects the Right (success) value into a new form. Equivalent to <see cref="Map{TLeft,TRight,TResult}(Either{TLeft,TRight},Func{TRight,TResult})"/>.
    /// Enables the <c>select</c> clause of the C# query syntax.
    /// </summary>
    /// <typeparam name="TLeft">The failure/error type.</typeparam>
    /// <typeparam name="TRight">The success type of the source.</typeparam>
    /// <typeparam name="TResult">The success type of the result.</typeparam>
    /// <param name="source">The Either instance to project.</param>
    /// <param name="selector">A projection function applied to the Right value.</param>
    /// <returns><see cref="Either{TLeft,TResult}"/> with the projected Right value, or the original Left value unchanged.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static Either<TLeft, TResult> Select<TLeft, TRight, TResult>(
        this Either<TLeft, TRight> source,
        Func<TRight, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return source.Map(selector);
    }

    /// <summary>
    /// Projects the Right (success) value into a new <see cref="Either{TLeft,TResult}"/> and flattens the result.
    /// Equivalent to <see cref="Bind{TLeft,TRight,TResult}(Either{TLeft,TRight},Func{TRight,Either{TLeft,TResult}})"/>.
    /// </summary>
    /// <typeparam name="TLeft">The failure/error type.</typeparam>
    /// <typeparam name="TRight">The success type of the source.</typeparam>
    /// <typeparam name="TResult">The success type of the result.</typeparam>
    /// <param name="source">The Either instance to project.</param>
    /// <param name="selector">A projection function that returns an <see cref="Either{TLeft,TResult}"/>.</param>
    /// <returns>The <see cref="Either{TLeft,TResult}"/> returned by <paramref name="selector"/>, or the original Left value unchanged.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static Either<TLeft, TResult> SelectMany<TLeft, TRight, TResult>(
        this Either<TLeft, TRight> source,
        Func<TRight, Either<TLeft, TResult>> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return source.Bind(selector);
    }

    /// <summary>
    /// Projects the Right (success) value into an intermediate <see cref="Either{TLeft,TCollection}"/> and then combines the
    /// original and intermediate values into a final result. This is the overload required by the C# query syntax when multiple
    /// <c>from</c> clauses are used. A Left value in either step short-circuits and is propagated unchanged.
    /// </summary>
    /// <typeparam name="TLeft">The failure/error type.</typeparam>
    /// <typeparam name="TRight">The success type of the source.</typeparam>
    /// <typeparam name="TCollection">The success type of the intermediate value.</typeparam>
    /// <typeparam name="TResult">The success type of the final result.</typeparam>
    /// <param name="source">The Either instance to project.</param>
    /// <param name="collectionSelector">A function that returns an intermediate <see cref="Either{TLeft,TCollection}"/> from the Right value.</param>
    /// <param name="resultSelector">A function that combines the source and intermediate Right values into the final result.</param>
    /// <returns>An <see cref="Either{TLeft,TResult}"/> with the combined Right value, or the first Left value encountered.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="collectionSelector"/> or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
    public static Either<TLeft, TResult> SelectMany<TLeft, TRight, TCollection, TResult>(
        this Either<TLeft, TRight> source,
        Func<TRight, Either<TLeft, TCollection>> collectionSelector,
        Func<TRight, TCollection, TResult> resultSelector)
    {
        ArgumentNullException.ThrowIfNull(collectionSelector);
        ArgumentNullException.ThrowIfNull(resultSelector);

        return source.Bind(right => collectionSelector(right).Map(collection => resultSelector(right, collection)));
    }
}
