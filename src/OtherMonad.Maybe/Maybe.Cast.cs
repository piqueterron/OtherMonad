namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Cast from <see cref="object"/> to <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <param name="object">Object to convert</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></returns>
    /// <exception cref="Exception"/>
    public static Maybe<TSource> Cast<TSource>(this object @object) => (TSource)@object;

    /// <summary>
    /// <para>Cast from <see cref="object"/> to <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <param name="object">Object to convert</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></returns>
    public static Maybe<TSource> TryCast<TSource>(this object @object)
    {
        try
        {
            return (TSource)@object;
        }
        catch
        {
            return Maybe<TSource>.None;
        }
    }
}