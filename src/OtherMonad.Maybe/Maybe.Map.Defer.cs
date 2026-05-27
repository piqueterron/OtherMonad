namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>If the element has a value, apply <see cref="Func{TSource, TResult}"/> to create a new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see>; otherwise return <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see>.</para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A value to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to source element</param>
    /// <returns><see cref="Deferred{Maybe}"><![CDATA[ Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static Deferred<Maybe<TResult>> MapDefer<TSource, TResult>(this Maybe<TSource> source, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return () => source.Map(selector);
    }

    /// <summary>
    /// <para>If the element has a value, apply <see cref="Func{TSource, TResult}"/> to create a new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see>; otherwise return <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see>.</para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A value to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to source element</param>
    /// <returns><see cref="Deferred{Maybe}"><![CDATA[ Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static Deferred<Maybe<TResult>> MapDefer<TSource, TResult>(this Deferred<Maybe<TSource>> source, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return () =>
        {
            var src = source();
            return src.Map(selector);
        };
    }

    /// <summary>
    /// <para>If the element has a value, apply an asynchronous <see cref="Func{TSource, CancellationToken, Task}"/> to create a new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see>; otherwise return <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see>.</para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A value to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to source element</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="DeferredTask{Maybe}"><![CDATA[ DeferredTask<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static DeferredTask<Maybe<TResult>> MapDefer<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return async () => await source.Map(selector, cancellation);
    }

    /// <summary>
    /// <para>If the element has a value, apply an asynchronous <see cref="Func{TSource, CancellationToken, Task}"/> to create a new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see>; otherwise return <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see>.</para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A value to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to source element</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="DeferredTask{Maybe}"><![CDATA[ DeferredTask<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static DeferredTask<Maybe<TResult>> MapDefer<TSource, TResult>(this DeferredTask<Maybe<TSource>> source, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return async () =>
        {
            var src = await source();
            return await src.Map(selector, cancellation);
        };
    }

    /// <summary>
    /// <para>Projects each element of a sequence with value into a new <see cref="Maybe{TResult}"><![CDATA[ IEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="sources">A sequence of values to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to each source element</param>
    /// <returns>The type of the value returned <see cref="Maybe{TSource}"><![CDATA[Deferred<IEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static Deferred<IEnumerable<Maybe<TResult>>> MapDefer<TSource, TResult>(this Deferred<IEnumerable<Maybe<TSource>>> sources, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return () =>
        {
            var list = new List<Maybe<TResult>>();

            foreach (var source in sources())
            {
                var data = source.Map(selector);

                list.Add(data);
            }

            return list;
        };
    }

    /// <summary>
    /// <para>Projects each element of a sequence with value into a new <see cref="Maybe{TResult}"><![CDATA[ IEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="sources">A sequence of values to invoke a transform function on</param>
    /// <param name="selector">A transform function to apply to each source element</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns>The type of the value returned <see cref="Maybe{TSource}"><![CDATA[DeferredTask<IEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static DeferredTask<IEnumerable<Maybe<TResult>>> MapDefer<TSource, TResult>(this DeferredTask<IEnumerable<Maybe<TSource>>> sources, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return async () =>
        {
            var list = new List<Maybe<TResult>>();
            var src = await sources();

            foreach (var source in src)
            {
                var deferred = source.MapDefer(selector, cancellation);
                var item = await deferred();

                list.Add(item);
            }

            return list;
        };
    }
}
