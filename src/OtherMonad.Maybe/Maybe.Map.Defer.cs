namespace OtherMonad;

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
    /// <returns>The type of the value returned <see cref="Maybe{TSource}"><![CDATA[Deferred<IEnumerable<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>>]]></see></returns>
    /// <exception cref="ArgumentNullException">selector is null</exception>
    public static Deferred<IEnumerable<Maybe<TResult>>> Map<TSource, TResult>(this Deferred<IEnumerable<Maybe<TSource>>> sources, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return () =>
        {
            var list = new List<Maybe<TResult>>();

            foreach (var source in sources())
            {
                var data = source.Bind(src => selector(src));

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
    public static DeferredTask<IEnumerable<Maybe<TResult>>> Map<TSource, TResult>(this DeferredTask<IEnumerable<Maybe<TSource>>> sources, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return async () =>
        {
            var list = new List<Maybe<TResult>>();
            var src = await sources();

            foreach (var source in src)
            {
                var deferred = source.BindDefer((src, ct) => selector(src, ct), cancellation);
                var item = await deferred();

                list.Add(item);
            }

            return list;
        };
    }
}
