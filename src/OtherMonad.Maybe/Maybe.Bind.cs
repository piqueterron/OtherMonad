namespace OtherMonad;

using System.Runtime.CompilerServices;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// Applies a transformation function that already returns a <see cref="Maybe{TResult}"/> to the value if present,
    /// otherwise returns <see cref="Maybe{TResult}.None"/>.
    /// </summary>
    /// <typeparam name="TSource">The type of the source value.</typeparam>
    /// <typeparam name="TResult">The type of the result value.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> to transform.</param>
    /// <param name="selector">A transformation function to apply to the contained value.</param>
    /// <returns>The <see cref="Maybe{TResult}"/> returned by <paramref name="selector"/>, or <see cref="Maybe{TResult}.None"/> if the source has no value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<TResult> Bind<TSource, TResult>(this Maybe<TSource> source, Func<TSource, Maybe<TResult>> selector)
    {
        if(!source.HasValue)
        {
            return default;
        }
        ArgumentNullException.ThrowIfNull(selector);

        return selector(source.Value);
    }

    /// <summary>
    /// Applies an asynchronous transformation function that already returns a <see cref="Maybe{TResult}"/>
    /// to the value if present, otherwise returns <see cref="Maybe{TResult}.None"/>.
    /// </summary>
    /// <typeparam name="TSource">The type of the source value.</typeparam>
    /// <typeparam name="TResult">The type of the result value.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> to transform.</param>
    /// <param name="selector">An asynchronous transformation function to apply to the contained value.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Maybe{TResult}"/> returned by <paramref name="selector"/>, or <see cref="Maybe{TResult}.None"/> if the source has no value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Maybe<TResult>> Bind<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, Task<Maybe<TResult>>> selector, CancellationToken cancellation = default)
    {
        if (!source.HasValue)
        {
            return default;
        }
        ArgumentNullException.ThrowIfNull(selector);

        return await selector(source.Value, cancellation).ConfigureAwait(false);
    }
}