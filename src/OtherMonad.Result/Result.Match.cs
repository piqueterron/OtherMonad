namespace OtherMonad;

/// <summary>
/// Extension methods for <see cref="Result{T}"/>.
/// </summary>
public static partial class Result
{
    /// <summary>
    /// <para>Evaluates the Result and returns a value by applying the corresponding function.
    /// Executes <paramref name="onOk"/> when in the Ok (success) state,
    /// or <paramref name="onErr"/> when in the Err (failure) state.</para>
    /// <para>Delegates to <see cref="Either.Match{TLeft,TRight,TResult}(IEither{TLeft,TRight},Func{TLeft,TResult},Func{TRight,TResult})"/>.</para>
    /// </summary>
    /// <typeparam name="T">The success type.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the selector.</typeparam>
    /// <param name="source">The Result instance to evaluate.</param>
    /// <param name="onErr">Function invoked when in the Err (failure) state.</param>
    /// <param name="onOk">Function invoked when in the Ok (success) state.</param>
    /// <returns><typeparamref name="TResult"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="onErr"/> or <paramref name="onOk"/> is <see langword="null"/>.</exception>
    public static TResult Match<T, TResult>(
        this IResult<T> source,
        Func<Exception, TResult> onErr,
        Func<T, TResult> onOk)
    {
        return ((IEither<Exception, T>)source).Match(onErr, onOk);
    }

    /// <summary>
    /// <para>Evaluates the Result asynchronously and returns a value by applying the corresponding function.
    /// Executes <paramref name="onOk"/> when in the Ok (success) state,
    /// or <paramref name="onErr"/> when in the Err (failure) state.</para>
    /// </summary>
    /// <typeparam name="T">The success type.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the selector.</typeparam>
    /// <param name="source">The Result instance to evaluate.</param>
    /// <param name="onErr">Async function invoked when in the Err (failure) state.</param>
    /// <param name="onOk">Async function invoked when in the Ok (success) state.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns><see cref="Task{TResult}"><![CDATA[Task<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="onErr"/> or <paramref name="onOk"/> is <see langword="null"/>.</exception>
    public static Task<TResult> Match<T, TResult>(
        this IResult<T> source,
        Func<Exception, CancellationToken, Task<TResult>> onErr,
        Func<T, CancellationToken, Task<TResult>> onOk,
        CancellationToken cancellation = default)
    {
        return ((IEither<Exception, T>)source).Match(onErr, onOk, cancellation);
    }

    /// <summary>
    /// <para>Same as <see cref="Match{T,TResult}(IResult{T},Func{Exception,TResult},Func{T,TResult})"/>
    /// but silently returns <paramref name="default"/> if either function is <see langword="null"/> or throws.</para>
    /// </summary>
    /// <typeparam name="T">The success type.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the selector.</typeparam>
    /// <param name="source">The Result instance to evaluate.</param>
    /// <param name="onErr">Function invoked when in the Err (failure) state.</param>
    /// <param name="onOk">Function invoked when in the Ok (success) state.</param>
    /// <param name="default">Value returned when a function is <see langword="null"/> or throws.</param>
    /// <returns><typeparamref name="TResult"/></returns>
    public static TResult TryMatch<T, TResult>(
        this IResult<T> source,
        Func<Exception, TResult> onErr,
        Func<T, TResult> onOk,
        TResult @default = default!)
    {
        return ((IEither<Exception, T>)source).TryMatch(onErr, onOk, @default);
    }

    /// <summary>
    /// <para>Same as <see cref="Match{T,TResult}(IResult{T},Func{Exception,CancellationToken,Task{TResult}},Func{T,CancellationToken,Task{TResult}},CancellationToken)"/>
    /// but silently returns <paramref name="default"/> if either function is <see langword="null"/> or throws.</para>
    /// </summary>
    /// <typeparam name="T">The success type.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the selector.</typeparam>
    /// <param name="source">The Result instance to evaluate.</param>
    /// <param name="onErr">Async function invoked when in the Err (failure) state.</param>
    /// <param name="onOk">Async function invoked when in the Ok (success) state.</param>
    /// <param name="default">Value returned when a function is <see langword="null"/> or throws.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns><see cref="Task{TResult}"><![CDATA[Task<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    public static Task<TResult> TryMatch<T, TResult>(
        this IResult<T> source,
        Func<Exception, CancellationToken, Task<TResult>> onErr,
        Func<T, CancellationToken, Task<TResult>> onOk,
        TResult @default = default!,
        CancellationToken cancellation = default)
    {
        return ((IEither<Exception, T>)source).TryMatch(onErr, onOk, @default, cancellation);
    }
}
