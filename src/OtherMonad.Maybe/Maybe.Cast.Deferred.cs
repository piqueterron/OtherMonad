namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    public static Deferred<Maybe<TResult>> CastDeferred<TResult>(this object source) => () => (TResult)source;

    public static Deferred<Maybe<TSource>> TryCastDeferred<TSource>(this object source)
    {
        return () =>
        {
            try
            {
                return source.Cast<TSource>();
            }
            catch
            {
                return Maybe<TSource>.None;
            }
        };
    }
}