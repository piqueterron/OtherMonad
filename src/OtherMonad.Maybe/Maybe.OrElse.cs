namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Check if has value and return value otherwise return default <typeparamref name="TSource"/></para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <param name="source"><typeparamref name="TSource"/></param>
    /// <param name="default"><typeparamref name="TSource"/> default value</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></returns>
    public static Maybe<TSource> OrElse<TSource>(this Maybe<TSource> source, TSource @default)
    {
        if (source.HasValue)
        {
            return source;
        }

        return @default;
    }

    /// <summary>
    /// <para>Check if has value and return value otherwise return default <typeparamref name="TSource"/></para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <param name="source"><typeparamref name="TSource"/></param>
    /// <param name="default"><typeparamref name="TSource"/> default value</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></returns>
    public static async Task<Maybe<TSource>> OrElse<TSource>(this Task<Maybe<TSource>> source, TSource @default)
    {
        var maybe = await source;

        if (maybe.HasValue)
        {
            return maybe;
        }

        return @default;
    }

    /// <summary>
    /// <para>Check if has value and return value otherwise return default <typeparamref name="TSource"/></para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <param name="source"><typeparamref name="TSource"/></param>
    /// <param name="default"><typeparamref name="TSource"/> default value</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></returns>
    public static async ValueTask<Maybe<TSource>> OrElse<TSource>(this ValueTask<Maybe<TSource>> source, TSource @default)
    {
        var maybe = await source;

        if (maybe.HasValue)
        {
            return maybe;
        }

        return @default;
    }
}