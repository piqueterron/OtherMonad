namespace OtherMonad;

/// <summary>
/// Extension methods to Either Monad
/// </summary>
public static partial class Either
{
    /// <summary>
    /// <para>Execute <see cref="Func{TLeft, TResult}"></see> 
    /// if success otherwise execute <see cref="Func{TRight, TResult}"></see></para>
    /// </summary>
    /// <typeparam name="TLeft">Function call when struct state is success</typeparam>
    /// <typeparam name="TRight">Function call when struct state is fail</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">The type of the element of source</param>
    /// <param name="left">Execute success <see cref="Func{TLeft, TResult}"/> when <typeparamref name="TLeft"/></param>
    /// <param name="right">Execute fail <see cref="Func{TRight, TResult}"/> when <typeparamref name="TRight"/></param>
    /// <returns><typeparamref name="TResult"/></returns>
    /// <exception cref="ArgumentNullException">Left or right condition is null</exception>
    public static TResult Match<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, TResult> left, Func<TRight, TResult> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return source.IsLeft ? left(source.Left) : right(source.Right);
    }

    /// <summary>
    /// <para>Execute <see cref="Func{TLeft, CancellationToken, TResult}"></see> 
    /// if success otherwise execute <see cref="Func{TRight, CancellationToken, TResult}"></see></para>
    /// </summary>
    /// <typeparam name="TLeft">Function call when struct state is success</typeparam>
    /// <typeparam name="TRight">Function call when struct state is fail</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">The type of the element of source</param>
    /// <param name="left">Execute success <see cref="Func{TLeft, TResult}"/> when <typeparamref name="TLeft"/></param>
    /// <param name="right">Execute fail <see cref="Func{TRight, TResult}"/> when <typeparamref name="TRight"/></param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Task{TResult}"><![CDATA[Task<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    /// <exception cref="ArgumentNullException">Left or right condition is null</exception>
    public static async Task<TResult> Match<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, CancellationToken, Task<TResult>> left, Func<TRight, CancellationToken, Task<TResult>> right, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return source.IsLeft ? await left(source.Left, cancellation).ConfigureAwait(false) : await right(source.Right, cancellation).ConfigureAwait(false);
    }

    /// <summary>
    /// <para>Execute <see cref="Func{TLeft, CancellationToken, TResult}"></see> 
    /// if success otherwise execute <see cref="Func{TRight, CancellationToken, TResult}"></see>. 
    /// If any path it throws an exception, obfuscate the exception and return the default value</para>
    /// </summary>
    /// <typeparam name="TLeft">Function call when struct state is success</typeparam>
    /// <typeparam name="TRight">Function call when struct state is fail</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">The type of the element of source</param>
    /// <param name="left">Execute success <see cref="Func{TLeft, TResult}"/> when <typeparamref name="TLeft"/></param>
    /// <param name="right">Execute fail <see cref="Func{TRight, TResult}"/> when <typeparamref name="TRight"/></param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Task{TResult}"><![CDATA[Task<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    public static async Task<TResult> TryMatch<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, CancellationToken, Task<TResult>> left, Func<TRight, CancellationToken, Task<TResult>> right, TResult @default = default, CancellationToken cancellation = default)
    {
        if (left is null || right is null)
        {
            return @default;
        }

        try
        {
            return await source.Match(left, right, cancellation);
        }
        catch
        {
            return @default;
        }
    }

    /// <summary>
    /// <para>Execute <see cref="Func{TLeft, TResult}"></see> 
    /// if success otherwise execute <see cref="Func{TRight, TResult}"></see>
    /// If any path it throws an exception, obfuscate the exception and return the default value</para>
    /// </summary>
    /// <typeparam name="TLeft">Function call when struct state is success</typeparam>
    /// <typeparam name="TRight">Function call when struct state is fail</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">The type of the element of source</param>
    /// <param name="left">Execute success <see cref="Func{TLeft, TResult}"/> when <typeparamref name="TLeft"/></param>
    /// <param name="right">Execute fail <see cref="Func{TRight, TResult}"/> when <typeparamref name="TRight"/></param>
    /// <param name="default">Value by default type of <typeparamref name="TResult"/></param>
    /// <returns><typeparamref name="TResult"/></returns>
    public static TResult TryMatch<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, TResult> left, Func<TRight, TResult> right, TResult @default = default)
    {
        if (left is null || right is null)
        {
            return @default;
        }

        try
        {
            return source.Match(left, right);
        }
        catch
        {
            return @default;
        }
    }
}