namespace OtherMonad;

/// <summary>
/// Extension methods to Either Monad
/// </summary>
public static partial class Either
{
    /// <summary>
    /// <para>Evaluates the Either and returns a result by applying the corresponding function.
    /// Executes <paramref name="right"/> when in the Right (success) state,
    /// or <paramref name="left"/> when in the Left (failure) state.</para>
    /// </summary>
    /// <typeparam name="TLeft">The failure/error type.</typeparam>
    /// <typeparam name="TRight">The success type.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the selector.</typeparam>
    /// <param name="source">The Either instance to evaluate.</param>
    /// <param name="left">Function invoked when in the Left (failure) state.</param>
    /// <param name="right">Function invoked when in the Right (success) state.</param>
    /// <returns><typeparamref name="TResult"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
    public static TResult Match<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, TResult> left, Func<TRight, TResult> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return source.IsLeft ? left(source.Left) : right(source.Right);
    }

    /// <summary>
    /// <para>Evaluates the Either asynchronously and returns a result by applying the corresponding function.
    /// Executes <paramref name="right"/> when in the Right (success) state,
    /// or <paramref name="left"/> when in the Left (failure) state.</para>
    /// </summary>
    /// <typeparam name="TLeft">The failure/error type.</typeparam>
    /// <typeparam name="TRight">The success type.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the selector.</typeparam>
    /// <param name="source">The Either instance to evaluate.</param>
    /// <param name="left">Async function invoked when in the Left (failure) state.</param>
    /// <param name="right">Async function invoked when in the Right (success) state.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns><see cref="Task{TResult}"><![CDATA[Task<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
    public static async Task<TResult> Match<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, CancellationToken, Task<TResult>> left, Func<TRight, CancellationToken, Task<TResult>> right, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return source.IsLeft ? await left(source.Left, cancellation).ConfigureAwait(false) : await right(source.Right, cancellation).ConfigureAwait(false);
    }

    /// <summary>
    /// <para>Same as <see cref="Match{TLeft,TRight,TResult}(IEither{TLeft,TRight},Func{TLeft,CancellationToken,Task{TResult}},Func{TRight,CancellationToken,Task{TResult}},CancellationToken)"/>
    /// but silently returns <paramref name="default"/> if either function is <see langword="null"/> or throws.</para>
    /// </summary>
    /// <typeparam name="TLeft">The failure/error type.</typeparam>
    /// <typeparam name="TRight">The success type.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the selector.</typeparam>
    /// <param name="source">The Either instance to evaluate.</param>
    /// <param name="left">Async function invoked when in the Left (failure) state.</param>
    /// <param name="right">Async function invoked when in the Right (success) state.</param>
    /// <param name="default">Value returned when a function is <see langword="null"/> or throws.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns><see cref="Task{TResult}"><![CDATA[Task<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    public static async Task<TResult> TryMatch<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, CancellationToken, Task<TResult>> left, Func<TRight, CancellationToken, Task<TResult>> right, TResult @default = default!, CancellationToken cancellation = default)
    {
        if (left is null || right is null)
        {
            return @default;
        }

        try
        {
            return await source.Match(left, right, cancellation);
        }
        catch (Exception)
        {
            return @default;
        }
    }

    /// <summary>
    /// <para>Same as <see cref="Match{TLeft,TRight,TResult}(IEither{TLeft,TRight},Func{TLeft,TResult},Func{TRight,TResult})"/>
    /// but silently returns <paramref name="default"/> if either function is <see langword="null"/> or throws.</para>
    /// </summary>
    /// <typeparam name="TLeft">The failure/error type.</typeparam>
    /// <typeparam name="TRight">The success type.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the selector.</typeparam>
    /// <param name="source">The Either instance to evaluate.</param>
    /// <param name="left">Function invoked when in the Left (failure) state.</param>
    /// <param name="right">Function invoked when in the Right (success) state.</param>
    /// <param name="default">Value returned when a function is <see langword="null"/> or throws.</param>
    /// <returns><typeparamref name="TResult"/></returns>
    public static TResult TryMatch<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, TResult> left, Func<TRight, TResult> right, TResult @default = default!)
    {
        if (left is null || right is null)
        {
            return @default;
        }

        try
        {
            return source.Match(left, right);
        }
        catch (Exception)
        {
            return @default;
        }
    }
}