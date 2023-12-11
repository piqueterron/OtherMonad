namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>If has value apply <see cref="Delegate"/> to create new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> 
    /// with value, else <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> of None</para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source">List of <see cref="Maybe{TSource}"/> where <c>TSource</c> is a <typeparamref name="TSource"/></param>
    /// <param name="selector">Filter</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> or <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> of None</returns>
    public static Maybe<TResult> Bind<TSource, TResult>(this Maybe<TSource> source, Func<TSource, TResult> selector) =>
        source.HasValue ? selector(source.Value) : Maybe<TResult>.None;

    /// <summary>
    /// <para>If has value apply <see cref="Delegate"/> async to create new <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> 
    /// with value, else <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> of None</para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <typeparam name="TResult">Generic type</typeparam>
    /// <param name="source">List of <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see></param>
    /// <param name="selector">Filter</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see> or <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see> of None</returns>
    public static async Task<Maybe<TResult>> Bind<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default) =>
        source.HasValue ? await selector(source.Value, cancellation).ConfigureAwait(false) : Maybe<TResult>.None;

    /// <summary>
    /// <para>If has value apply <see cref="Delegate"/> async to create new <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> 
    /// with value, else <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> of None</para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <typeparam name="TResult">Generic type</typeparam>
    /// <param name="source">List of <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see></param>
    /// <param name="selector">Filter</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see> or <see cref="Maybe{TResult}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see> of None</returns>
    public static async ValueTask<Maybe<TResult>> Bind<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellation = default) =>
        source.HasValue ? await selector(source.Value, cancellation).ConfigureAwait(false) : Maybe<TResult>.None;
}