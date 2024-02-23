namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    public static Maybe<TResult> Combine<TSource, TCombine, TResult>(this Deferred<Maybe<TSource>> source, Maybe<TCombine> other, Func<TSource, TCombine, TResult> select)
    {
        return source.BindDefer(src => select(src, other.Value))
            .Match(res => res, () => Maybe<TResult>.None);
    }

    public static async Task<Maybe<TResult>> Combine<TSource, TCombine, TResult>(this DeferredTask<Maybe<TSource>> source, Maybe<TCombine> other, Func<TSource, TCombine, CancellationToken, Task<TResult>> select, CancellationToken cancellationToken = default)
    {
        return await source.BindDefer((src, ct) => select(src, other.Value, ct), cancellationToken)
            .Match(res => res, () => Maybe<TResult>.None);
    }

    public static Maybe<TResult> TryCombine<TSource, TCombine, TResult>(this Deferred<Maybe<TSource>> source, Maybe<TCombine> other, Func<TSource, TCombine, TResult> select, Func<TResult> defaultValueFactory)
    {
        try
        {
            return source.Combine(other, select);
        }
        catch
        {
            return defaultValueFactory();
        }
    }

    public static async Task<Maybe<TResult>> TryCombine<TSource, TCombine, TResult>(this DeferredTask<Maybe<TSource>> source, Maybe<TCombine> other, Func<TSource, TCombine, CancellationToken, Task<TResult>> select, Func<Task<TResult>> defaultValueFactory, CancellationToken cancellationToken = default)
    {
        try
        {
            return await source.Combine(other, select, cancellationToken);
        }
        catch
        {
            return await defaultValueFactory();
        }
    }
}