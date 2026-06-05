namespace OtherMonad;

/// <summary>
/// LINQ query-comprehension support for <see cref="Result{T}"/>.
/// These methods enable the C# query syntax (<c>from ... select ...</c>) over the Ok (success) value.
/// An Err (failure) value short-circuits and propagates the underlying <see cref="System.Exception"/> unchanged.
/// </summary>
public static partial class Result
{
    /// <summary>
    /// Projects the Ok (success) value into a new form. Equivalent to <see cref="Map{T,TResult}(Result{T},Func{T,TResult})"/>.
    /// Enables the <c>select</c> clause of the C# query syntax.
    /// </summary>
    /// <typeparam name="T">The success type of the source.</typeparam>
    /// <typeparam name="TResult">The success type of the result.</typeparam>
    /// <param name="source">The Result instance to project.</param>
    /// <param name="selector">A projection function applied to the value when in the Ok state.</param>
    /// <returns><see cref="Result{TResult}"/> with the projected value, or the original error unchanged.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static Result<TResult> Select<T, TResult>(this Result<T> source, Func<T, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return source.Map(selector);
    }

    /// <summary>
    /// Projects the Ok (success) value into a new <see cref="Result{TResult}"/> and flattens the result.
    /// Equivalent to <see cref="Bind{T,TResult}(Result{T},Func{T,Result{TResult}})"/>.
    /// </summary>
    /// <typeparam name="T">The success type of the source.</typeparam>
    /// <typeparam name="TResult">The success type of the result.</typeparam>
    /// <param name="source">The Result instance to project.</param>
    /// <param name="selector">A projection function that returns a <see cref="Result{TResult}"/>.</param>
    /// <returns>The <see cref="Result{TResult}"/> returned by <paramref name="selector"/>, or the original error unchanged.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static Result<TResult> SelectMany<T, TResult>(this Result<T> source, Func<T, Result<TResult>> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return source.Bind(selector);
    }

    /// <summary>
    /// Projects the Ok (success) value into an intermediate <see cref="Result{TCollection}"/> and then combines the
    /// original and intermediate values into a final result. This is the overload required by the C# query syntax when multiple
    /// <c>from</c> clauses are used. An Err value in either step short-circuits and propagates the exception unchanged.
    /// </summary>
    /// <typeparam name="T">The success type of the source.</typeparam>
    /// <typeparam name="TCollection">The success type of the intermediate value.</typeparam>
    /// <typeparam name="TResult">The success type of the final result.</typeparam>
    /// <param name="source">The Result instance to project.</param>
    /// <param name="collectionSelector">A function that returns an intermediate <see cref="Result{TCollection}"/> from the value.</param>
    /// <param name="resultSelector">A function that combines the source and intermediate values into the final result.</param>
    /// <returns>A <see cref="Result{TResult}"/> with the combined value, or the first error encountered.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="collectionSelector"/> or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
    public static Result<TResult> SelectMany<T, TCollection, TResult>(
        this Result<T> source,
        Func<T, Result<TCollection>> collectionSelector,
        Func<T, TCollection, TResult> resultSelector)
    {
        ArgumentNullException.ThrowIfNull(collectionSelector);
        ArgumentNullException.ThrowIfNull(resultSelector);

        return source.Bind(value => collectionSelector(value).Map(collection => resultSelector(value, collection)));
    }
}
