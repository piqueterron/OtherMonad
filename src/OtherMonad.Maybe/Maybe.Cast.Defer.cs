namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Casts the element of <see cref="object"/> to <see cref="Deferred{Maybe}"><![CDATA[Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></para>
    /// </summary>
    /// <typeparam name="TResult">The type to cast the element of source to</typeparam>
    /// <param name="source">Contains the element to be cast to type <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see></param>
    /// <returns><see cref="Deferred{Maybe}"><![CDATA[Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    /// <exception cref="InvalidCastException">invalid cast object.</exception>
    public static Deferred<Maybe<TResult>> CastDefer<TResult>(this object source)
    {
        return () => (TResult)source;
    }

    /// <summary>
    /// <para>Casts the element of <see cref="object"/> to <see cref="Deferred{Maybe}"><![CDATA[Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></para>
    /// </summary>
    /// <typeparam name="TResult">The type to cast the element of source to</typeparam>
    /// <param name="source">Contains the element to be cast to type <see cref="Maybe{TSource}"><![CDATA[Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></param>
    /// <returns><see cref="Deferred{Maybe}"><![CDATA[Deferred<Maybe<]]><typeparamref name="TResult"/><![CDATA[>>]]></see></returns>
    public static Deferred<Maybe<TSource>> TryCastDefer<TSource>(this object source)
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