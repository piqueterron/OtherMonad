namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Execute <see cref="Func{TSource, TResult}"><![CDATA[Func<]]><typeparamref name="TSource"/>, <typeparamref name="TResult"/><![CDATA[>]]></see> 
    /// if success otherwise execute <see cref="Func{TResult}"><![CDATA[Func<]]><typeparamref name="TResult"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <typeparam name="TResult">Generic type response</typeparam>
    /// <param name="source">List of <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></param>
    /// <param name="left">Execute <see cref="Delegate"/> when <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> has value</param>
    /// <param name="right">Execute <see cref="Delegate"/> when <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> has not value</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    /// <exception cref="ArgumentNullException">Left or right condition is null</exception>
    public static TResult Match<TSource, TResult>(this Maybe<TSource> source, Func<TSource, TResult> left, Func<TResult> right)
    {
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(right, nameof(right));

        if (source.HasValue)
        {
            return left(source.Value);
        }

        return right();
    }

    /// <summary>
    /// <para>Execute <see cref="Func{TSource, TResult}"><![CDATA[Func<]]><typeparamref name="TSource"/>, <typeparamref name="TResult"/><![CDATA[>]]></see> 
    /// if success otherwise execute <see cref="Func{TResult}"><![CDATA[Func<]]><typeparamref name="TResult"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <typeparam name="TResult">Generic type response</typeparam>
    /// <param name="source">List of <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></param>
    /// <param name="left">Execute <see cref="Delegate"/> when <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> has value</param>
    /// <param name="right">Execute <see cref="Delegate"/> when <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> has not value</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Task{TResult}"/></returns>
    /// <exception cref="ArgumentNullException">Left or right condition is null</exception>
    public static async Task<TResult> Match<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, Task<TResult>> left, Func<CancellationToken, Task<TResult>> right, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(right, nameof(right));

        if (source.HasValue)
        {
            return await left(source.Value, cancellation).ConfigureAwait(false);
        }

        return await right(cancellation).ConfigureAwait(false);
    }

    /// <summary>
    /// <para>Execute <see cref="Func{TSource, TResult}"><![CDATA[Func<]]><typeparamref name="TSource"/>, <typeparamref name="TResult"/><![CDATA[>]]></see> 
    /// if success otherwise execute <see cref="Func{TResult}"><![CDATA[Func<]]><typeparamref name="TResult"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <typeparam name="TResult">Generic type response</typeparam>
    /// <param name="source">List of <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></param>
    /// <param name="left">Execute <see cref="Delegate"/> when <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> has value</param>
    /// <param name="right">Execute <see cref="Delegate"/> when <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> has not value</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Task{TResult}"/></returns>
    /// <exception cref="ArgumentNullException">Left or right condition is null</exception>
    public static async ValueTask<TResult> Match<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> left, Func<CancellationToken, ValueTask<TResult>> right, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(right, nameof(right));

        if (source.HasValue)
        {
            return await left(source.Value, cancellation).ConfigureAwait(false);
        }

        return await right(cancellation).ConfigureAwait(false);
    }
}