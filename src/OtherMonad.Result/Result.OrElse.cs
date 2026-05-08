namespace OtherMonad;

/// <summary>
/// Extension methods for <see cref="Result{T}"/>.
/// </summary>
public static partial class Result
{
    /// <summary>
    /// <para>If in the Err (failure) state, returns the provided <paramref name="fallback"/> Result.
    /// If in the Ok (success) state, returns the current instance unchanged.</para>
    /// <para>Delegates to <see cref="Either.OrElse{TLeft,TRight}(Either{TLeft,TRight},Either{TLeft,TRight})"/>.</para>
    /// </summary>
    /// <typeparam name="T">The success type.</typeparam>
    /// <param name="source">The Result instance to evaluate.</param>
    /// <param name="fallback">The fallback Result returned when in the Err (failure) state.</param>
    /// <returns><see cref="Result{T}"/></returns>
    public static Result<T> OrElse<T>(this Result<T> source, Result<T> fallback)
    {
        Either<Exception, T> result = ((Either<Exception, T>)source).OrElse((Either<Exception, T>)fallback);

        return result;
    }

    /// <summary>
    /// <para>If in the Err (failure) state, invokes <paramref name="fallbackFactory"/> to obtain a fallback Result.
    /// If in the Ok (success) state, returns the current instance unchanged.</para>
    /// </summary>
    /// <typeparam name="T">The success type.</typeparam>
    /// <param name="source">The Result instance to evaluate.</param>
    /// <param name="fallbackFactory">An async factory invoked when in the Err (failure) state.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns><see cref="Task{T}"><![CDATA[Task<]]><see cref="Result{T}"/><![CDATA[>]]></see></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="fallbackFactory"/> is <see langword="null"/>.</exception>
    public static async Task<Result<T>> OrElse<T>(
        this Result<T> source,
        Func<CancellationToken, Task<Result<T>>> fallbackFactory,
        CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(fallbackFactory);

        Either<Exception, T> result = await ((Either<Exception, T>)source).OrElse(
            async ct => (Either<Exception, T>)await fallbackFactory(ct).ConfigureAwait(false),
            cancellation).ConfigureAwait(false);

        return result;
    }

    /// <summary>
    /// Returns the success value if the Result is in the Ok state; otherwise returns <paramref name="default"/>.
    /// </summary>
    /// <typeparam name="T">The success type.</typeparam>
    /// <param name="source">The Result instance to evaluate.</param>
    /// <param name="default">The value to return when in the Err state.</param>
    /// <returns>The success value or <paramref name="default"/>.</returns>
    public static T GetValueOrDefault<T>(this Result<T> source, T @default = default!)
    {
        return source.IsOk ? source.Value : @default;
    }
}
