namespace OtherMonad;

public static partial class Maybe
{
    public static Deferred<Maybe<TResult>> BindDeferred<TSource, TResult>(this Maybe<TSource> source, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        return () => source.Bind(selector);
    }

    public static Deferred<Maybe<TResult>> BindDeferred<TSource, TResult>(this Deferred<Maybe<TSource>> source, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        return () =>
        {
            var src = source();
            return src.Bind(selector);
        };
    }

    public static DeferredTask<Maybe<TResult>> BindDeferred<TSource, TResult>(this Maybe<TSource> source, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        return async () => await source.Bind(selector, cancellation);
    }

    public static DeferredTask<Maybe<TResult>> BindDeferred<TSource, TResult>(this DeferredTask<Maybe<TSource>> source, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));

        return async () =>
        {
            var src = await source();
            return await src.Bind(selector, cancellation);
        };
    }
}
