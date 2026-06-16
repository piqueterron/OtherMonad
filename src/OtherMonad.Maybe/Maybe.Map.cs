namespace OtherMonad;

using System.Runtime.CompilerServices;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<TResult> Map<TSource, TResult>(this Maybe<TSource> source, Func<TSource, TResult> selector)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Maybe<TResult>> Map<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return source.HasValue ? await selector(source.Value, cancellation).ConfigureAwait(false) : Maybe<TResult>.None;
    }

    /// <summary>
    /// Applies a transformation function to each <see cref="Maybe{TSource}"/> in the sequence that has a value.
    /// </summary>
    /// <typeparam name="TSource">The type of the source values.</typeparam>
    /// <typeparam name="TResult">The type of the result values.</typeparam>
    /// <param name="sources">A sequence of <see cref="Maybe{TSource}"/> values to transform.</param>
    /// <param name="selector">A transformation function to apply to each contained value.</param>
    /// <returns>A sequence of <see cref="Maybe{TResult}"/> with transformed values, or <see cref="Maybe{TResult}.None"/> for empty sources.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IEnumerable<Maybe<TSource>> sources, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        foreach (var source in sources)
        {
            yield return source.Map(selector);
        }
    }

    /// <summary>
    /// Asynchronously applies a transformation function to each <see cref="Maybe{TSource}"/> in the sequence that has a value.
    /// </summary>
    /// <typeparam name="TSource">The type of the source values.</typeparam>
    /// <typeparam name="TResult">The type of the result values.</typeparam>
    /// <param name="sources">A sequence of <see cref="Maybe{TSource}"/> values to transform.</param>
    /// <param name="selector">An asynchronous transformation function to apply to each contained value.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for each task to complete.</param>
    /// <returns>An asynchronous sequence of <see cref="Maybe{TResult}"/> with transformed values, or <see cref="Maybe{TResult}.None"/> for empty sources.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async IAsyncEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IEnumerable<Maybe<TSource>> sources, Func<TSource, CancellationToken, Task<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        foreach (var source in sources)
        {
            yield return await source.Map(selector, cancellation).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Asynchronously applies a transformation function to each <see cref="Maybe{TSource}"/> in the asynchronous sequence that has a value.
    /// </summary>
    /// <typeparam name="TSource">The type of the source values.</typeparam>
    /// <typeparam name="TResult">The type of the result values.</typeparam>
    /// <param name="sources">An asynchronous sequence of <see cref="Maybe{TSource}"/> values to transform.</param>
    /// <param name="selector">An asynchronous transformation function to apply to each contained value.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for each task to complete.</param>
    /// <returns>An asynchronous sequence of <see cref="Maybe{TResult}"/> with transformed values, or <see cref="Maybe{TResult}.None"/> for empty sources.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async IAsyncEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IAsyncEnumerable<Maybe<TSource>> sources, Func<TSource, CancellationToken, Task<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        await foreach (var source in sources)
        {
            yield return await source.Map(selector, cancellation).ConfigureAwait(false);
        }
    }
}