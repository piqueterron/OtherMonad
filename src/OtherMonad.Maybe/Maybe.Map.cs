namespace OtherMonad;

using System.Runtime.CompilerServices;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Iterate all <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> with value, and apply filter</para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <typeparam name="TResult">Generic result type</typeparam>
    /// <param name="sources"><see cref="Maybe{TSource}"><![CDATA[IEnumerable<Maybe<]]><typeparamref name="TSource"/><![CDATA[>>]]></see></param>
    /// <param name="selector">Filter</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[IEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    public static IEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IEnumerable<Maybe<TSource>> sources, Func<TSource, TResult> selector)
    {
        foreach (var source in sources)
        {
            if (source.HasValue)
            {
                yield return selector(source.Value);
            }
        }
    }

    /// <summary>
    /// <para>Iterate all <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> with value, and apply filter</para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <typeparam name="TResult">Generic result type</typeparam>
    /// <param name="sources"><see cref="Maybe{TSource}"><![CDATA[IEnumerable<Maybe<]]><typeparamref name="TSource"/><![CDATA[>>]]></see></param>
    /// <param name="selector">Filter</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[IAsyncEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    public static async IAsyncEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IEnumerable<Maybe<TSource>> sources, Func<TSource, CancellationToken, Task<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        foreach (var source in sources)
        {
            if (source.HasValue)
            {
                yield return await selector(source.Value, cancellation).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// <para>Iterate all <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> with value, and apply filter</para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <typeparam name="TResult">Generic result type</typeparam>
    /// <param name="sources"><see cref="Maybe{TSource}"><![CDATA[IAsyncEnumerable<Maybe<]]><typeparamref name="TSource"/><![CDATA[>>]]></see></param>
    /// <param name="selector">Filter</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[IAsyncEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    public static async IAsyncEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IAsyncEnumerable<Maybe<TSource>> sources, Func<TSource, CancellationToken, Task<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        await foreach (var source in sources)
        {
            if (source.HasValue)
            {
                yield return await selector(source.Value, cancellation).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// <para>Iterate all <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> with value, and apply filter</para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <typeparam name="TResult">Generic result type</typeparam>
    /// <param name="sources"><see cref="Maybe{TSource}"><![CDATA[IEnumerable<Maybe<]]><typeparamref name="TSource"/><![CDATA[>>]]></see></param>
    /// <param name="selector">Filter</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[IAsyncEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    public static async IAsyncEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IEnumerable<Maybe<TSource>> sources, Func<TSource, CancellationToken, ValueTask<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        foreach (var source in sources)
        {
            if (source.HasValue)
            {
                yield return await selector(source.Value, cancellation).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// <para>Iterate all <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> with value, and apply filter</para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <typeparam name="TResult">Generic result type</typeparam>
    /// <param name="sources"><see cref="Maybe{TSource}"><![CDATA[IAsyncEnumerable<Maybe<]]><typeparamref name="TSource"/><![CDATA[>>]]></see></param>
    /// <param name="selector">Filter</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[IAsyncEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    public static async IAsyncEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IAsyncEnumerable<Maybe<TSource>> sources, Func<TSource, CancellationToken, ValueTask<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        await foreach (var source in sources)
        {
            if (source.HasValue)
            {
                yield return await selector(source.Value, cancellation).ConfigureAwait(false);
            }
        }
    }
}