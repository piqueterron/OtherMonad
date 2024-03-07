namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Combine two <see cref="Maybe{TResult}"/> apply <see cref="Func{TSource, TResult}"/> to create new 
    /// <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TCombine">The type of the element of combine</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A left value to invoke a combine</param>
    /// <param name="other">A right value to invoke a combine</param>
    /// <param name="select">A combine function to apply to source element with other</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see> or default value</returns>
    public static Deferred<Maybe<TResult>> Combine<TSource, TCombine, TResult>(this Deferred<Maybe<TSource>> source, Maybe<TCombine> other, Func<TSource, TCombine, TResult> select)
    {
        return () =>
        {
            return source.BindDefer(src => select(src, other.Value))
                .Match(res => res, () => Maybe<TResult>.None);
        };
    }

    /// <summary>
    /// <para>Combine two <see cref="Maybe{TResult}"/> apply <see cref="Func{TSource, TResult}"/> to create new 
    /// <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TCombine">The type of the element of combine</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A left value to invoke a combine</param>
    /// <param name="other">A right value to invoke a combine</param>
    /// <param name="select">A combine function to apply to source element with other</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see> or default value</returns>
    public static Deferred<Maybe<TResult>> Combine<TSource, TCombine, TResult>(this Deferred<Maybe<TSource>> source, Deferred<Maybe<TCombine>> other, Func<TSource, TCombine, TResult> select)
    {
        return () =>
        {
            var data = other();

            return source.BindDefer(src => select(src, data.Value))
                .Match(res => res, () => Maybe<TResult>.None);
        };
    }

    /// <summary>
    /// <para>Combine two <see cref="Maybe{TResult}"/> apply <see cref="Func{TSource, TResult}"/> to create new 
    /// <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TCombine">The type of the element of combine</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A left value to invoke a combine</param>
    /// <param name="other">A right value to invoke a combine</param>
    /// <param name="select">A combine function to apply to source element with other</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see> or default value</returns>
    public static DeferredTask<Maybe<TResult>> Combine<TSource, TCombine, TResult>(this DeferredTask<Maybe<TSource>> source, Maybe<TCombine> other, Func<TSource, TCombine, CancellationToken, Task<TResult>> select, CancellationToken cancellation = default)
    {
        return async () =>
        {
            return await source.BindDefer((src, ct) => select(src, other.Value, ct), cancellation)
                .Match(res => res, () => Maybe<TResult>.None);
        };
    }

    /// <summary>
    /// <para>Combine two <see cref="Maybe{TResult}"/> apply <see cref="Func{TSource, TResult}"/> to create new 
    /// <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TCombine">The type of the element of combine</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A left value to invoke a combine</param>
    /// <param name="other">A right value to invoke a combine</param>
    /// <param name="select">A combine function to apply to source element with other</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see> or default value</returns>
    public static DeferredTask<Maybe<TResult>> Combine<TSource, TCombine, TResult>(this DeferredTask<Maybe<TSource>> source, DeferredTask<Maybe<TCombine>> other, Func<TSource, TCombine, CancellationToken, Task<TResult>> select, CancellationToken cancellation = default)
    {
        return async () =>
        {
            var data = await other();

            return await source.BindDefer((src, ct) => select(src, data.Value, ct), cancellation)
                .Match(res => res, () => Maybe<TResult>.None);
        };
    }

    /// <summary>
    /// <para>Combine two <see cref="Maybe{TResult}"/> apply <see cref="Func{TSource, TResult}"/> to create new 
    /// <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TCombine">The type of the element of combine</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A left value to invoke a combine</param>
    /// <param name="other">A right value to invoke a combine</param>
    /// <param name="select">A combine function to apply to source element with other</param>
    /// <param name="defaultValueFactory">Default value in case no match</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see> or default value</returns>
    public static Deferred<Maybe<TResult>> TryCombine<TSource, TCombine, TResult>(this Deferred<Maybe<TSource>> source, Maybe<TCombine> other, Func<TSource, TCombine, TResult> select, Func<TResult> defaultValueFactory)
    {
        return () =>
        {
            try
            {
                var comb = source.Combine(other, select);

                return comb();
            }
            catch
            {
                return defaultValueFactory();
            }
        };
    }

    /// <summary>
    /// <para>Combine two <see cref="Maybe{TResult}"/> apply <see cref="Func{TSource, TResult}"/> to create new 
    /// <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TCombine">The type of the element of combine</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A left value to invoke a combine</param>
    /// <param name="other">A right value to invoke a combine</param>
    /// <param name="select">A combine function to apply to source element with other</param>
    /// <param name="defaultValueFactory">Default value in case no match</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see> or default value</returns>
    public static Deferred<Maybe<TResult>> TryCombine<TSource, TCombine, TResult>(this Deferred<Maybe<TSource>> source, Deferred<Maybe<TCombine>> other, Func<TSource, TCombine, TResult> select, Func<TResult> defaultValueFactory)
    {
        return () =>
        {
            try
            {
                var comb = source.Combine(other, select);

                return comb();
            }
            catch
            {
                return defaultValueFactory();
            }
        };
    }

    /// <summary>
    /// <para>Combine two <see cref="Maybe{TResult}"/> apply <see cref="Func{TSource, TResult}"/> to create new 
    /// <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TCombine">The type of the element of combine</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A left value to invoke a combine</param>
    /// <param name="other">A right value to invoke a combine</param>
    /// <param name="select">A combine function to apply to source element with other</param>
    /// <param name="defaultValueFactory">Default value in case no match</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ DeferredTask<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see> or default value</returns>
    public static DeferredTask<Maybe<TResult>> TryCombine<TSource, TCombine, TResult>(this DeferredTask<Maybe<TSource>> source, Maybe<TCombine> other, Func<TSource, TCombine, CancellationToken, Task<TResult>> select, Func<Task<TResult>> defaultValueFactory, CancellationToken cancellation = default)
    {
        return async () =>
        {
            try
            {
                var comb = source.Combine(other, select, cancellation);

                return await comb();
            }
            catch
            {
                return await defaultValueFactory();
            }
        };
    }

    /// <summary>
    /// <para>Combine two <see cref="Maybe{TResult}"/> apply <see cref="Func{TSource, TResult}"/> to create new 
    /// <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TCombine">The type of the element of combine</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A left value to invoke a combine</param>
    /// <param name="other">A right value to invoke a combine</param>
    /// <param name="select">A combine function to apply to source element with other</param>
    /// <param name="defaultValueFactory">Default value in case no match</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ DeferredTask<Maybe<]]><typeparamref name="TResult"/><![CDATA[>> ]]></see> or default value</returns>
    public static DeferredTask<Maybe<TResult>> TryCombine<TSource, TCombine, TResult>(this DeferredTask<Maybe<TSource>> source, DeferredTask<Maybe<TCombine>> other, Func<TSource, TCombine, CancellationToken, Task<TResult>> select, Func<Task<TResult>> defaultValueFactory, CancellationToken cancellation = default)
    {
        return async () =>
        {
            try
            {
                var comb = source.Combine(other, select, cancellation);

                return await comb();
            }
            catch
            {
                return await defaultValueFactory();
            }
        };
    }
}