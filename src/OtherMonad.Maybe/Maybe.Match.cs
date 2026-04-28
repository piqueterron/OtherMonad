namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Execute <see cref="Func{TSource, TResult}"><![CDATA[Func<]]><typeparamref name="TSource"/>, <typeparamref name="TResult"/><![CDATA[>]]></see> 
    /// if a value is present (<em>some</em>), otherwise execute <see cref="Func{TResult}"><![CDATA[Func<]]><typeparamref name="TResult"/><![CDATA[>]]></see> (<em>none</em>).</para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> instance to evaluate</param>
    /// <param name="some">Execute <see cref="Func{TSource, TResult}"/> when <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> has a value</param>
    /// <param name="none">Execute <see cref="Func{TResult}"/> when <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> has no value</param>
    /// <returns>The value returned by the invoked function (<typeparamref name="TResult"/>)</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="some"/> or <paramref name="none"/> is <see langword="null"/></exception>
    public static TResult Match<TSource, TResult>(this Maybe<TSource> source, Func<TSource, TResult> some, Func<TResult> none)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);

        if (source.HasValue)
        {
            return some(source.Value);
        }

        return none();
    }

    /// <summary>
    /// <para>Asynchronously execute <see cref="Func{TSource, CancellationToken, Task{TResult}}"/> if a value is present (<em>some</em>),
    /// otherwise execute <see cref="Func{CancellationToken, Task{TResult}}"/> (<em>none</em>).</para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> instance to evaluate</param>
    /// <param name="some">Async function invoked when <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> has a value</param>
    /// <param name="none">Async function invoked when <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> has no value</param>
    /// <param name="cancellation">A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects</param>
    /// <returns><see cref="Task{TResult}"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="some"/> or <paramref name="none"/> is <see langword="null"/></exception>
    public static async Task<TResult> Match<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, Task<TResult>> some, Func<CancellationToken, Task<TResult>> none, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);

        if (source.HasValue)
        {
            return await some(source.Value, cancellation).ConfigureAwait(false);
        }

        return await none(cancellation).ConfigureAwait(false);
    }

    /// <summary>
    /// <para>Evaluates a deferred <see cref="Maybe{TSource}"/> and executes <paramref name="some"/> if a value is present,
    /// otherwise executes <paramref name="none"/>.</para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A <see cref="Deferred{Maybe}"><![CDATA[Deferred<Maybe<]]><typeparamref name="TSource"/><![CDATA[>>]]></see> delegate to evaluate</param>
    /// <param name="some">Execute <see cref="Func{TSource, TResult}"/> when the resolved <see cref="Maybe{TSource}"/> has a value</param>
    /// <param name="none">Execute <see cref="Func{TResult}"/> when the resolved <see cref="Maybe{TSource}"/> has no value</param>
    /// <returns>The value returned by the invoked function (<typeparamref name="TResult"/>)</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="some"/> or <paramref name="none"/> is <see langword="null"/></exception>
    public static TResult Match<TSource, TResult>(this Deferred<Maybe<TSource>> source, Func<TSource, TResult> some, Func<TResult> none)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);

        var src = source();

        if (src.HasValue)
        {
            return some(src.Value);
        }

        return none();
    }

    /// <summary>
    /// <para>Asynchronously evaluates a <see cref="DeferredTask{Maybe}"/> and executes <paramref name="some"/> if a value is present,
    /// otherwise executes <paramref name="none"/>.</para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A <see cref="DeferredTask{Maybe}"><![CDATA[DeferredTask<Maybe<]]><typeparamref name="TSource"/><![CDATA[>>]]></see> delegate to evaluate</param>
    /// <param name="some">Execute <see cref="Func{TSource, TResult}"/> when the resolved <see cref="Maybe{TSource}"/> has a value</param>
    /// <param name="none">Execute <see cref="Func{TResult}"/> when the resolved <see cref="Maybe{TSource}"/> has no value</param>
    /// <returns><see cref="Task{TResult}"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="some"/> or <paramref name="none"/> is <see langword="null"/></exception>
    public static async Task<TResult> Match<TSource, TResult>(this DeferredTask<Maybe<TSource>> source, Func<TSource, TResult> some, Func<TResult> none)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);

        var src = await source().ConfigureAwait(false);

        if (src.HasValue)
        {
            return some(src.Value);
        }

        return none();
    }
}