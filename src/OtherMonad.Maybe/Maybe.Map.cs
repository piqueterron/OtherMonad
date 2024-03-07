namespace OtherMonad;

using System.Runtime.CompilerServices;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Projects each element of a sequence with value into a new <see cref="Maybe{TResult}"><![CDATA[ IEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="sources">A sequence of values to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to each source element</param>
    /// <returns>The type of the value returned <see cref="Maybe{TSource}"><![CDATA[IEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static IEnumerable<Maybe<TResult>> Map<TSource, TResult>(this IEnumerable<Maybe<TSource>> sources, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        foreach (var source in sources)
        {
            yield return source.Bind(src => selector(src));
        }
    }

    /// <summary>
    /// <para>Projects each element of a sequence with value into a new <see cref="Maybe{TResult}"><![CDATA[ IEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see></para>
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
        ArgumentNullException.ThrowIfNull(selector);

        foreach (var source in sources)
        {
            yield return await source.Bind((src, ct) => selector(src, ct), cancellation).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// <para>Projects each element of a sequence with value into a new <see cref="Maybe{TResult}"><![CDATA[ IEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see></para>
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
        ArgumentNullException.ThrowIfNull(selector);

        await foreach (var source in sources)
        {
            yield return await source.Bind((src, ct) => selector(src, ct), cancellation).ConfigureAwait(false);
        }
    }
}