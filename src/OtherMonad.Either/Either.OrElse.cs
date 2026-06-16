namespace OtherMonad;

using System.Runtime.CompilerServices;

/// <summary>
/// Extension methods to Either Monad
/// </summary>
public static partial class Either
{
    /// <summary>
    /// Returns the provided fallback <see cref="IEither{TLeft,TRight}"/> if in the Left (failure) state.
    /// If in the Right (success) state, returns the current instance unchanged.
    /// </summary>
    /// <typeparam name="TLeft">The failure/error type.</typeparam>
    /// <typeparam name="TRight">The success type.</typeparam>
    /// <param name="source">The Either instance to evaluate.</param>
    /// <param name="fallback">The fallback Either returned when in the Left (failure) state.</param>
    /// <returns><see cref="Either{TLeft,TRight}"/></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<TLeft, TRight> OrElse<TLeft, TRight>(
        this Either<TLeft, TRight> source,
        Either<TLeft, TRight> fallback)
    {
        return source.IsRight ? source : fallback;
    }

    /// <summary>
    /// Asynchronously obtains a fallback <see cref="Either{TLeft,TRight}"/> if in the Left (failure) state.
    /// If in the Right (success) state, returns the current instance unchanged.
    /// </summary>
    /// <typeparam name="TLeft">The failure/error type.</typeparam>
    /// <typeparam name="TRight">The success type.</typeparam>
    /// <param name="source">The Either instance to evaluate.</param>
    /// <param name="fallbackFactory">A factory invoked when in the Left (failure) state.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the current instance or the fallback.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="fallbackFactory"/> is <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Either<TLeft, TRight>> OrElse<TLeft, TRight>(
        this Either<TLeft, TRight> source,
        Func<CancellationToken, Task<Either<TLeft, TRight>>> fallbackFactory,
        CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(fallbackFactory);

        return source.IsRight
            ? source
            : await fallbackFactory(cancellation).ConfigureAwait(false);
    }
}
