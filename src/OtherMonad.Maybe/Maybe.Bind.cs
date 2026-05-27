namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// Applies a transformation function to the value if present, otherwise returns <see cref="Maybe{TResult}.None"/>.
    /// </summary>
    /// <typeparam name="TSource">The type of the source value.</typeparam>
    /// <typeparam name="TResult">The type of the result value.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> to transform.</param>
    /// <param name="selector">A transformation function to apply to the contained value.</param>
    /// <returns>A new <see cref="Maybe{TResult}"/> containing the transformation result, or <see cref="Maybe{TResult}.None"/> if the source has no value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static Maybe<TResult> Bind<TSource, TResult>(this Maybe<TSource> source, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return source.HasValue ? selector(source.Value) : Maybe<TResult>.None;
    }

    /// <summary>
    /// Applies an asynchronous transformation function to the value if present, otherwise returns <see cref="Maybe{TResult}.None"/>.
    /// </summary>
    /// <typeparam name="TSource">The type of the source value.</typeparam>
    /// <typeparam name="TResult">The type of the result value.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> to transform.</param>
    /// <param name="selector">An asynchronous transformation function to apply to the contained value.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a new <see cref="Maybe{TResult}"/> with the transformation result, or <see cref="Maybe{TResult}.None"/> if the source has no value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static async Task<Maybe<TResult>> Bind<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return source.HasValue ? await selector(source.Value, cancellation).ConfigureAwait(false) : Maybe<TResult>.None;
    }
}