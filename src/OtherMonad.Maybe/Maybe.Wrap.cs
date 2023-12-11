﻿namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary> 
    /// <para>Wraps an object of type <typeparamref name="TSource"/> in a structure of type <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <param name="source"><typeparamref name="TSource"/> to convert</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></returns>
    public static Maybe<TSource> Wrap<TSource>(this TSource source) => source;

    /// <summary>
    /// <para>Unwraps the <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> type structure to an object of type <typeparamref name="TSource"/></para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <param name="source"><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></param>
    /// <returns><typeparamref name="TSource"/></returns>
    public static TSource Unwrap<TSource>(this Maybe<TSource> source) => source.Value;

    /// <summary>
    /// <para>Unwraps the <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> type structure to an object of type <typeparamref name="TSource"/> otherwise return default <typeparamref name="TSource"/></para>
    /// </summary>
    /// <typeparam name="TSource">Generic type</typeparam>
    /// <param name="source"><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></param>
    /// <param name="default"><typeparamref name="TSource"/> default value</param>
    /// <returns><typeparamref name="TSource"/></returns>
    public static TSource Unwrap<TSource>(this Maybe<TSource> source, TSource @default)
    {
        if (source.HasValue)
        {
            return source.Unwrap();
        }

        return @default;
    }
}