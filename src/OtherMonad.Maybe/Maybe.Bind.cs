namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>If element has value apply <see cref="Func{TSource, TResult}"/> to create new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> otherwise <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A value to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to source element</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> or <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static Maybe<TResult> Bind<TSource, TResult>(this Maybe<TSource> source, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        return source.HasValue ? selector(source.Value) : Maybe<TResult>.None;
    }

    /// <summary>
    /// <para>If element has value apply <see cref="Func{TSource, CancellationToken, TResult}"/> to create new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> otherwise <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A value to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to source element</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see> or <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static async Task<Maybe<TResult>> Bind<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        return source.HasValue ? await selector(source.Value, cancellation).ConfigureAwait(false) : Maybe<TResult>.None;
    }

    /// <summary>
    /// <para>If element has value apply <see cref="Func{TSource, CancellationToken, TResult}"/> to create new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> otherwise <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A value to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to source element</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see> or <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static async ValueTask<Maybe<TResult>> Bind<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        return source.HasValue ? await selector(source.Value, cancellation).ConfigureAwait(false) : Maybe<TResult>.None;
    }
}