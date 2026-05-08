namespace OtherMonad;

/// <summary>
/// Extension methods for <see cref="Result{T}"/>.
/// </summary>
public static partial class Result
{
    /// <summary>
    /// <para>If in the Ok (success) state, applies <paramref name="selector"/> to the value
    /// to obtain a new <see cref="Result{TResult}"/>. If in the Err (failure) state, propagates
    /// the exception unchanged.</para>
    /// <para>Delegates to <see cref="Either.Bind{TLeft,TRight,TResult}(Either{TLeft,TRight},Func{TRight,Either{TLeft,TResult}})"/>.</para>
    /// </summary>
    /// <typeparam name="T">The success type of the source.</typeparam>
    /// <typeparam name="TResult">The success type of the result.</typeparam>
    /// <param name="source">The Result instance to bind.</param>
    /// <param name="selector">A function applied to the value when in the Ok state.</param>
    /// <returns><see cref="Result{TResult}"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static Result<TResult> Bind<T, TResult>(
        this Result<T> source,
        Func<T, Result<TResult>> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return ((Either<Exception, T>)source).Bind(v => (Either<Exception, TResult>)selector(v));
    }

    /// <summary>
    /// <para>If in the Ok (success) state, applies <paramref name="selector"/> asynchronously to the value
    /// to obtain a new <see cref="Result{TResult}"/>. If in the Err (failure) state, propagates
    /// the exception unchanged.</para>
    /// </summary>
    /// <typeparam name="T">The success type of the source.</typeparam>
    /// <typeparam name="TResult">The success type of the result.</typeparam>
    /// <param name="source">The Result instance to bind.</param>
    /// <param name="selector">An async function applied to the value when in the Ok state.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns><see cref="Task{T}"><![CDATA[Task<]]><see cref="Result{TResult}"/><![CDATA[>]]></see></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static async Task<Result<TResult>> Bind<T, TResult>(
        this Result<T> source,
        Func<T, CancellationToken, Task<Result<TResult>>> selector,
        CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        Either<Exception, TResult> result = await ((Either<Exception, T>)source).Bind(
            async (v, ct) => (Either<Exception, TResult>)await selector(v, ct).ConfigureAwait(false),
            cancellation).ConfigureAwait(false);

        return result;
    }

    /// <summary>
    /// <para>If in the Ok (success) state, applies <paramref name="selector"/> to transform the value,
    /// wrapping the result in a new <see cref="Result{TResult}"/>. If in the Err (failure) state,
    /// propagates the exception unchanged.</para>
    /// <para>Delegates to <see cref="Either.Map{TLeft,TRight,TResult}(Either{TLeft,TRight},Func{TRight,TResult})"/>.</para>
    /// </summary>
    /// <typeparam name="T">The success type of the source.</typeparam>
    /// <typeparam name="TResult">The success type of the result.</typeparam>
    /// <param name="source">The Result instance to map.</param>
    /// <param name="selector">A function applied to the value when in the Ok state.</param>
    /// <returns><see cref="Result{TResult}"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static Result<TResult> Map<T, TResult>(
        this Result<T> source,
        Func<T, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return ((Either<Exception, T>)source).Map(selector);
    }

    /// <summary>
    /// <para>If in the Ok (success) state, applies <paramref name="selector"/> asynchronously to transform
    /// the value, wrapping the result in a new <see cref="Result{TResult}"/>. If in the Err (failure) state,
    /// propagates the exception unchanged.</para>
    /// </summary>
    /// <typeparam name="T">The success type of the source.</typeparam>
    /// <typeparam name="TResult">The success type of the result.</typeparam>
    /// <param name="source">The Result instance to map.</param>
    /// <param name="selector">An async function applied to the value when in the Ok state.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns><see cref="Task{T}"><![CDATA[Task<]]><see cref="Result{TResult}"/><![CDATA[>]]></see></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static async Task<Result<TResult>> Map<T, TResult>(
        this Result<T> source,
        Func<T, CancellationToken, Task<TResult>> selector,
        CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        Either<Exception, TResult> result = await ((Either<Exception, T>)source)
            .Map(selector, cancellation).ConfigureAwait(false);

        return result;
    }
}
