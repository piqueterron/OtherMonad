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
    /// <returns><see cref="Deferred{Maybe}"><![CDATA[ Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static Deferred<Maybe<TResult>> BindDefer<TSource, TResult>(this Maybe<TSource> source, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return () => source.Bind(selector);
    }

    /// <summary>
    /// <para>If element has value apply <see cref="Func{TSource, TResult}"/> to create new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> otherwise <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A value to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to source element</param>
    /// <returns><see cref="Deferred{Maybe}"><![CDATA[ Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static Deferred<Maybe<TResult>> BindDefer<TSource, TResult>(this Deferred<Maybe<TSource>> source, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return () =>
        {
            var src = source();
            return src.Bind(selector);
        };
    }

    /// <summary>
    /// <para>If element has value apply <see cref="Func{TSource, TResult}"/> to create new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> otherwise <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A value to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to source element</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="DeferredTask{Maybe}"><![CDATA[ DeferredTask<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static DeferredTask<Maybe<TResult>> BindDefer<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return async () => await source.Bind(selector, cancellation);
    }

    /// <summary>
    /// <para>If element has value apply <see cref="Func{TSource, TResult}"/> to create new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> otherwise <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A value to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to source element</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="DeferredTask{Maybe}"><![CDATA[ DeferredTask<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static DeferredTask<Maybe<TResult>> BindDefer<TSource, TResult>(this DeferredTask<Maybe<TSource>> source, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return async () =>
        {
            var src = await source();
            return await src.Bind(selector, cancellation);
        };
    }
}
