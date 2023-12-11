namespace OtherMonad;

/// <summary>
/// Extension methods to Either Monad
/// </summary>
public static partial class Either
{

    /// <summary>
    /// <para>Execute <see cref="Func{TLeft, TResult}"><![CDATA[Func<]]><typeparamref name="TLeft"/>, <typeparamref name="TResult"/><![CDATA[>]]></see> 
    /// if success otherwise execute <see cref="Func{TRight, TResult}"><![CDATA[Func<]]><typeparamref name="TRight"/>, <typeparamref name="TResult"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TResult">Generic type</typeparam>
    /// <param name="left">Execute success <see cref="Delegate"/> when <typeparamref name="TLeft"/></param>
    /// <param name="right">Execute fail <see cref="Delegate"/> when <typeparamref name="TRight"/></param>
    /// <returns><typeparamref name="TResult"/></returns>
    /// <exception cref="ArgumentNullException">Left or right condition is null</exception>
    public static TResult Match<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, TResult> left, Func<TRight, TResult> right)
    {
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(right, nameof(right));

        return source.IsLeft ? left(source.Left) : right(source.Right);
    }

    /// <summary>
    /// <para>Execute <see cref="Func{TLeft, TResult}"><![CDATA[Func<]]><typeparamref name="TLeft"/>, <typeparamref name="TResult"/><![CDATA[>]]></see> 
    /// if success otherwise execute <see cref="Func{TRight, TResult}"><![CDATA[Func<]]><typeparamref name="TRight"/>, <typeparamref name="TResult"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="left">Execute success <see cref="Delegate"/> when <typeparamref name="TLeft"/></param>
    /// <param name="right">Execute fail <see cref="Delegate"/> when <typeparamref name="TRight"/></param>
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

    /// <summary>
    /// <para>Execute <see cref="Func{TLeft, TResult}"><![CDATA[Func<]]><typeparamref name="TLeft"/>, <typeparamref name="TResult"/><![CDATA[>]]></see> 
    /// if success otherwise execute <see cref="Func{TRight, TResult}"><![CDATA[Func<]]><typeparamref name="TRight"/>, <typeparamref name="TResult"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="left">Execute success <see cref="Delegate"/> when <typeparamref name="TLeft"/></param>
    /// <param name="right">Execute fail <see cref="Delegate"/> when <typeparamref name="TRight"/></param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Task{TResult}"><![CDATA[Task<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    /// <exception cref="ArgumentNullException">Left or right condition is null</exception>
    public static async Task<TResult> Match<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, CancellationToken, Task<TResult>> left, Func<TRight, CancellationToken, Task<TResult>> right, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(right, nameof(right));

        return source.IsLeft ? await left(source.Left, cancellation).ConfigureAwait(false) : await right(source.Right, cancellation).ConfigureAwait(false);
    }

    /// <summary>
    /// <para>Execute <see cref="Func{TLeft, TResult}"><![CDATA[Func<]]><typeparamref name="TLeft"/>, <typeparamref name="TResult"/><![CDATA[>]]></see> 
    /// if success otherwise execute <see cref="Func{TRight, TResult}"><![CDATA[Func<]]><typeparamref name="TRight"/>, <typeparamref name="TResult"/><![CDATA[>]]></see>. 
    /// If on any path it throws an exception, we obfuscate the exception and return the default value</para>
    /// </summary>
    /// <typeparam name="TResult">Generic type</typeparam>
    /// <param name="left">Execute success <see cref="Delegate"/> when <typeparamref name="TLeft"/></param>
    /// <param name="right">Execute fail <see cref="Delegate"/> when <typeparamref name="TRight"/></param>
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
    /// <para>Execute <see cref="Func{TLeft, TResult}"><![CDATA[Func<]]><typeparamref name="TLeft"/>, <typeparamref name="TResult"/><![CDATA[>]]></see> 
    /// if success otherwise execute <see cref="Func{TRight, TResult}"><![CDATA[Func<]]><typeparamref name="TRight"/>, <typeparamref name="TResult"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TLeft">Generic type</typeparam>
    /// <typeparam name="TRight">Generic type</typeparam>
    /// <typeparam name="TResult">Generic type</typeparam>
    /// <param name="source"></param>
    /// <param name="left">Execute success <see cref="Delegate"/> when <typeparamref name="TLeft"/></param>
    /// <param name="right">Execute fail <see cref="Delegate"/> when <typeparamref name="TRight"/></param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="ValueTask{TResult}"><![CDATA[ValueTask<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    /// <exception cref="ArgumentNullException">Left or right condition is null</exception>
    public static async ValueTask<TResult> Match<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, CancellationToken, ValueTask<TResult>> left, Func<TRight, CancellationToken, ValueTask<TResult>> right, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(right, nameof(right));

        return source.IsLeft ? await left(source.Left, cancellation).ConfigureAwait(false) : await right(source.Right, cancellation).ConfigureAwait(false);
    }

    /// <summary>
    /// <para>Execute <see cref="Func{TLeft, TResult}"><![CDATA[Func<]]><typeparamref name="TLeft"/>, <typeparamref name="TResult"/><![CDATA[>]]></see> 
    /// if success otherwise execute <see cref="Func{TRight, TResult}"><![CDATA[Func<]]><typeparamref name="TRight"/>, <typeparamref name="TResult"/><![CDATA[>]]></see>. 
    /// If on any path it throws an exception, we obfuscate the exception and return the default value</para>
    /// </summary>
    /// <typeparam name="TLeft">Generic type</typeparam>
    /// <typeparam name="TRight">Generic type</typeparam>
    /// <typeparam name="TResult">Generic type</typeparam>
    /// <param name="source"></param>
    /// <param name="left">Execute success <see cref="Delegate"/> when <typeparamref name="TLeft"/></param>
    /// <param name="right">Execute fail <see cref="Delegate"/> when <typeparamref name="TRight"/></param>
    /// <param name="default">Value by default type of <typeparamref name="TResult"/></param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="ValueTask{TResult}"><![CDATA[ValueTask<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    public static async ValueTask<TResult> TryMatch<TLeft, TRight, TResult>(this IEither<TLeft, TRight> source, Func<TLeft, CancellationToken, ValueTask<TResult>> left, Func<TRight, CancellationToken, ValueTask<TResult>> right, TResult @default = default, CancellationToken cancellation = default)
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
}