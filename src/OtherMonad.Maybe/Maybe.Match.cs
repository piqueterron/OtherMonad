namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// Evaluates the <see cref="Maybe{TSource}"/> by executing <paramref name="some"/> if a value is present,
    /// or <paramref name="none"/> if the value is absent.
    /// </summary>
    /// <typeparam name="TSource">The type of the source value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> instance to evaluate.</param>
    /// <param name="some">A function to execute when a value is present.</param>
    /// <param name="none">A function to execute when no value is present.</param>
    /// <returns>The value returned by the invoked function.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="some"/> or <paramref name="none"/> is <see langword="null"/>.</exception>
    public static TResult Match<TSource, TResult>(this Maybe<TSource> source, Func<TSource, TResult> some, Func<TResult> none)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);

        if (source.HasValue)
        {
            return some(source.Value);
        }

        return none();
    }

    /// <summary>
    /// Asynchronously evaluates the <see cref="Maybe{TSource}"/> by executing <paramref name="some"/> if a value is present,
    /// or <paramref name="none"/> if the value is absent.
    /// </summary>
    /// <typeparam name="TSource">The type of the source value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> instance to evaluate.</param>
    /// <param name="some">An asynchronous function to execute when a value is present.</param>
    /// <param name="none">An asynchronous function to execute when no value is present.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by the invoked function.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="some"/> or <paramref name="none"/> is <see langword="null"/>.</exception>
    public static async Task<TResult> Match<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, Task<TResult>> some, Func<CancellationToken, Task<TResult>> none, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);

        if (source.HasValue)
        {
            return await some(source.Value, cancellation).ConfigureAwait(false);
        }

        return await none(cancellation).ConfigureAwait(false);
    }

    /// <summary>
    /// Evaluates a deferred <see cref="Maybe{TSource}"/> by executing <paramref name="some"/> if the resolved value is present,
    /// or <paramref name="none"/> if the resolved value is absent.
    /// </summary>
    /// <typeparam name="TSource">The type of the source value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">A deferred computation that produces a <see cref="Maybe{TSource}"/>.</param>
    /// <param name="some">A function to execute when the resolved value is present.</param>
    /// <param name="none">A function to execute when the resolved value is absent.</param>
    /// <returns>The value returned by the invoked function.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="some"/> or <paramref name="none"/> is <see langword="null"/>.</exception>
    public static TResult Match<TSource, TResult>(this Deferred<Maybe<TSource>> source, Func<TSource, TResult> some, Func<TResult> none)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);

        var src = source();

        if (src.HasValue)
        {
            return some(src.Value);
        }

        return none();
    }

    /// <summary>
    /// Asynchronously evaluates a deferred <see cref="Maybe{TSource}"/> by executing <paramref name="some"/> if the resolved value is present,
    /// or <paramref name="none"/> if the resolved value is absent.
    /// </summary>
    /// <typeparam name="TSource">The type of the source value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">An asynchronous deferred computation that produces a <see cref="Maybe{TSource}"/>.</param>
    /// <param name="some">A function to execute when the resolved value is present.</param>
    /// <param name="none">A function to execute when the resolved value is absent.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the value returned by the invoked function.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="some"/> or <paramref name="none"/> is <see langword="null"/>.</exception>
    public static async Task<TResult> Match<TSource, TResult>(this DeferredTask<Maybe<TSource>> source, Func<TSource, TResult> some, Func<TResult> none)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);

        var src = await source().ConfigureAwait(false);

        if (src.HasValue)
        {
            return some(src.Value);
        }

        return none();
    }
}