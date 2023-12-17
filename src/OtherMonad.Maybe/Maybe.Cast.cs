namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Casts the element of <see cref="object"/> to <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TResult">The type to cast the element of source to</typeparam>
    /// <param name="source">Contains the element to be cast to type <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see></param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    /// <exception cref="InvalidCastException">invalid cast object.</exception>
    public static Maybe<TResult> Cast<TResult>(this object source) => (TResult)source;

    /// <summary>
    /// <para>Casts the element of <see cref="object"/> to <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TResult">The type to cast the element of source to</typeparam>
    /// <param name="source">Contains the element to be cast to type <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see></param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TResult"/><![CDATA[>]]></see></returns>
    public static Maybe<TSource> TryCast<TSource>(this object source)
    {
        try
        {
            return (TSource)source;
        }
        catch
        {
            return Maybe<TSource>.None;
        }
    }
}