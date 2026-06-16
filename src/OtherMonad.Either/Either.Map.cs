namespace OtherMonad;

using System.Runtime.CompilerServices;

/// <summary>
/// Extension methods to Either Monad
/// </summary>
public static partial class Either
{
    /// <summary>
    /// Applies a transformation function to the Right value if in the Right (success) state,
    /// wrapping the result in a new <see cref="Either{TLeft,TResult}"/>. If in the Left (failure) state,
    /// propagates the Left value unchanged.
    /// </summary>
    /// <typeparam name="TLeft">The failure/error type.</typeparam>
    /// <typeparam name="TRight">The success type of the source.</typeparam>
    /// <typeparam name="TResult">The success type of the result.</typeparam>
    /// <param name="source">The Either instance to map.</param>
    /// <param name="selector">A function applied to the Right value when in the Right state.</param>
    /// <returns><see cref="Either{TLeft,TResult}"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<TLeft, TResult> Map<TLeft, TRight, TResult>(
        this Either<TLeft, TRight> source,
        Func<TRight, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return source.IsRight
            ? Either<TLeft, TResult>.Create.Right(selector(source.Right))
            : Either<TLeft, TResult>.Create.Left(source.Left);
    }

    /// <summary>
    /// Asynchronously applies a transformation function to the Right value if in the Right (success) state,
    /// wrapping the result in a new <see cref="Either{TLeft,TResult}"/>. If in the Left (failure) state,
    /// propagates the Left value unchanged.
    /// </summary>
    /// <typeparam name="TLeft">The failure/error type.</typeparam>
    /// <typeparam name="TRight">The success type of the source.</typeparam>
    /// <typeparam name="TResult">The success type of the result.</typeparam>
    /// <param name="source">The Either instance to map.</param>
    /// <param name="selector">An async function applied to the Right value when in the Right state.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the mapped <see cref="Either{TLeft,TResult}"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Either<TLeft, TResult>> Map<TLeft, TRight, TResult>(
        this Either<TLeft, TRight> source,
        Func<TRight, CancellationToken, Task<TResult>> selector,
        CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return source.IsRight
            ? Either<TLeft, TResult>.Create.Right(await selector(source.Right, cancellation).ConfigureAwait(false))
            : Either<TLeft, TResult>.Create.Left(source.Left);
    }
}
