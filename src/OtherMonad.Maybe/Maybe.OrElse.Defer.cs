namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Check if has value and return value otherwise return default <typeparamref name="TSource"/></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <param name="source">A value to invoke to check if has value</param>
    /// <param name="default">Default value to return if dont has value</param>
    /// <returns><see cref="Deferred{Maybe}"><![CDATA[Deferred<Maybe<]]><typeparamref name="TSource"/><![CDATA[>>]]></see></returns>
    public static Deferred<Maybe<TSource>> OrElseDefer<TSource>(this Maybe<TSource> source, TSource @default)
    {
        return () => source.OrElse(@default);
    }

    /// <summary>
    /// <para>Check if has value and return value otherwise return default <typeparamref name="TSource"/></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <param name="source">A value to invoke to check if has value</param>
    /// <param name="default">Default value to return if dont has value</param>
    /// <returns><see cref="Deferred{Maybe}"><![CDATA[Deferred<Maybe<]]><typeparamref name="TSource"/><![CDATA[>>]]></see></returns>
    public static Deferred<Maybe<TSource>> OrElseDefer<TSource>(this Deferred<Maybe<TSource>> source, TSource @default)
    {
        return () =>
        {
            var src = source();
            return src.OrElse(@default);
        };
    }

    /// <summary>
    /// <para>Check if has value and return value otherwise return default <typeparamref name="TSource"/></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <param name="source">A value to invoke to check if has value</param>
    /// <param name="default">Default value to return if dont has value</param>
    /// <returns><see cref="DeferredTask{Maybe}"><![CDATA[DeferredTask<Maybe<]]><typeparamref name="TSource"/><![CDATA[>>]]></see></returns>
    public static DeferredTask<Maybe<TSource>> OrElseDefer<TSource>(this DeferredTask<Maybe<TSource>> source, TSource @default)
    {
        return async () =>
        {
            var src = await source();
            return src.OrElse(@default);
        };
    }
}