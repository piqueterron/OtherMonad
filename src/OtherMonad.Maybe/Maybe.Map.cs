namespace OtherMonad;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Projects each element of a sequence with value into a new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="sources">A sequence of values to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to each source element</param>
    /// <returns>The type of the value returned <see cref="Maybe{TSource}"><![CDATA[IEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static IEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IEnumerable<Maybe<TSource>> sources, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        foreach (var source in sources)
        {
            if (source.HasValue)
            {
                yield return selector(source.Value);
            }
        }
    }

    /// <summary>
    /// <para>Projects each element of a sequence with value into a new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="sources">A sequence of values to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to each source element</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns>The type of the value returned <see cref="Maybe{TSource}"><![CDATA[IAsyncEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static async IAsyncEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IEnumerable<Maybe<TSource>> sources, Func<TSource, CancellationToken, Task<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        foreach (var source in sources)
        {
            if (source.HasValue)
            {
                yield return await selector(source.Value, cancellation).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// <para>Projects each element of a sequence with value into a new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="sources">A sequence of values to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to each source element</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns>The type of the value returned <see cref="Maybe{TSource}"><![CDATA[IAsyncEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static async IAsyncEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IAsyncEnumerable<Maybe<TSource>> sources, Func<TSource, CancellationToken, Task<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        await foreach (var source in sources)
        {
            if (source.HasValue)
            {
                yield return await selector(source.Value, cancellation).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// <para>Projects each element of a sequence with value into a new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="sources">A sequence of values to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to each source element</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns>The type of the value returned <see cref="Maybe{TSource}"><![CDATA[IAsyncEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static async IAsyncEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IEnumerable<Maybe<TSource>> sources, Func<TSource, CancellationToken, ValueTask<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        foreach (var source in sources)
        {
            if (source.HasValue)
            {
                yield return await selector(source.Value, cancellation).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// <para>Projects each element of a sequence with value into a new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="sources">A sequence of values to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to each source element</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns>The type of the value returned <see cref="Maybe{TSource}"><![CDATA[IAsyncEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static async IAsyncEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IAsyncEnumerable<Maybe<TSource>> sources, Func<TSource, CancellationToken, ValueTask<TResult>> selector, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        await foreach (var source in sources)
        {
            if (source.HasValue)
            {
                yield return await selector(source.Value, cancellation).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// <para>Projects each element of a sequence with value into a new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="sources">A sequence of values to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to each source element</param>
    /// <returns>The type of the value returned <see cref="Maybe{TSource}"><![CDATA[Span<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static Span<Maybe<TResult>> Map<TSource, TResult>(this Span<Maybe<TSource>> sources, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        var items = new List<Maybe<TResult>>();

        foreach (var source in sources)
        {
            if (source.HasValue)
            {
                items.Add(selector(source.Value));
            }
        }

        return CollectionsMarshal.AsSpan(items);
    }
















    public static TResult Combine<TSource, TCombine, TResult>(
        this Maybe<TSource> source,
        Maybe<TCombine> combine,
        Func<TSource, TCombine, TResult> select,
        Func<TResult> defaultValueFactory) =>
            source.Bind(a => select(a, combine.Value)).Match(x => x, defaultValueFactory);
}