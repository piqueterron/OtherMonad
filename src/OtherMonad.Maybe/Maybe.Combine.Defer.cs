namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    public static Deferred<Maybe<TResult>> Combine<TSource, TCombine, TResult>(this Deferred<Maybe<TSource>> source, Maybe<TCombine> other, Func<TSource, TCombine, TResult> select)
    {
        return () =>
        {
            return source.BindDefer(src => select(src, other.Value))
                .Match(res => res, () => Maybe<TResult>.None);
        };
    }

    public static Deferred<Maybe<TResult>> Combine<TSource, TCombine, TResult>(this Deferred<Maybe<TSource>> source, Deferred<Maybe<TCombine>> other, Func<TSource, TCombine, TResult> select)
    {
        return () =>
        {
            var data = other();

            return source.BindDefer(src => select(src, data.Value))
                .Match(res => res, () => Maybe<TResult>.None);
        };
    }

    public static DeferredTask<Maybe<TResult>> Combine<TSource, TCombine, TResult>(this DeferredTask<Maybe<TSource>> source, Maybe<TCombine> other, Func<TSource, TCombine, CancellationToken, Task<TResult>> select, CancellationToken cancellationToken = default)
    {
        return async () =>
        {
            return await source.BindDefer((src, ct) => select(src, other.Value, ct), cancellationToken)
                .Match(res => res, () => Maybe<TResult>.None);
        };
    }

    public static DeferredTask<Maybe<TResult>> Combine<TSource, TCombine, TResult>(this DeferredTask<Maybe<TSource>> source, DeferredTask<Maybe<TCombine>> other, Func<TSource, TCombine, CancellationToken, Task<TResult>> select, CancellationToken cancellationToken = default)
    {
        return async () =>
        {
            var data = await other();

            return await source.BindDefer((src, ct) => select(src, data.Value, ct), cancellationToken)
                .Match(res => res, () => Maybe<TResult>.None);
        };
    }

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

    public static DeferredTask<Maybe<TResult>> TryCombine<TSource, TCombine, TResult>(this DeferredTask<Maybe<TSource>> source, Maybe<TCombine> other, Func<TSource, TCombine, CancellationToken, Task<TResult>> select, Func<Task<TResult>> defaultValueFactory, CancellationToken cancellationToken = default)
    {
        return async () =>
        {
            try
            {
                var comb = source.Combine(other, select, cancellationToken);

                return await comb();
            }
            catch
            {
                return await defaultValueFactory();
            }
        };
    }

    public static DeferredTask<Maybe<TResult>> TryCombine<TSource, TCombine, TResult>(this DeferredTask<Maybe<TSource>> source, DeferredTask<Maybe<TCombine>> other, Func<TSource, TCombine, CancellationToken, Task<TResult>> select, Func<Task<TResult>> defaultValueFactory, CancellationToken cancellationToken = default)
    {
        return async () =>
        {
            try
            {
                var comb = source.Combine(other, select, cancellationToken);

                return await comb();
            }
            catch
            {
                return await defaultValueFactory();
            }
        };
    }
}