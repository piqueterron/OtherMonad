namespace OtherMonad;

public static partial class Maybe
{
    public static Deferred<IEnumerable<Maybe<TResult>>> Map<TSource, TResult>(this IEnumerable<Deferred<Maybe<TSource>>> sources, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return () =>
        {
            var res = new List<Maybe<TResult>>();

            foreach (var source in sources)
            {
                var deferred = source.BindDefer(src => selector(src));

                res.Add(deferred());
            }

            return res;
        };
    }

    public static DeferredTask<IEnumerable<Maybe<TResult>>> Map<TSource, TResult>(this IEnumerable<DeferredTask<Maybe<TSource>>> sources, Func<TSource, CancellationToken, Task<TResult>> selector, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        return async () =>
        {
            var res = new List<Maybe<TResult>>();

            foreach (var source in sources)
            {
                var deferred = source.BindDefer((src, ct) => selector(src, ct), cancellation);
                var item = await deferred();

                res.Add(item);
            }

            return res;
        };
    }
}
