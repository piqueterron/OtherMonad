namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    public static Deferred<Maybe<TSource>> OrElseDeferred<TSource>(this Maybe<TSource> source, TSource @default)
    {
        return () => source.OrElse(@default);
    }

    public static Deferred<Maybe<TSource>> OrElseDeferred<TSource>(this Deferred<Maybe<TSource>> source, TSource @default)
    {
        return () =>
        {
            var src = source();
            return src.OrElse(@default);
        };
    }

    public static DeferredTask<Maybe<TSource>> OrElseDeferred<TSource>(this Task<Maybe<TSource>> source, TSource @default)
    {
        return async () => await source.OrElse(@default);
    }

    public static DeferredTask<Maybe<TSource>> OrElseDeferred<TSource>(this DeferredTask<Maybe<TSource>> source, TSource @default)
    {
        return async () =>
        {
            var src = await source();
            return src.OrElse(@default);
        };
    }
}