namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary> 
    /// <para>Wraps an object of type <typeparamref name="TSource"/> in a struct of type <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <param name="source">A value to wrap</param>
    /// <returns><see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see></returns>
    public static Maybe<TSource> Wrap<TSource>(this TSource source)
    {
        return source;
    }

    /// <summary>
    /// <para>Unwraps the <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> type struct to an object of type <typeparamref name="TSource"/></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <param name="source">A value to unwrap</param>
    /// <returns><typeparamref name="TSource"/></returns>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="source"/> has no value (<see cref="Maybe{TSource}.HasValue"/> is <see langword="false"/>). Use <see cref="Unwrap{TSource}(Maybe{TSource}, TSource)"/> to provide a fallback value.</exception>
    public static TSource Unwrap<TSource>(this Maybe<TSource> source)
    {
        if (!source.HasValue)
            throw new InvalidOperationException("Maybe has no value. Use Unwrap(defaultValue) to provide a fallback.");

        return source.Value;
    }

    /// <summary>
    /// <para>Unwraps the <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see> type structure to an object of type <typeparamref name="TSource"/> otherwise return default <typeparamref name="TSource"/></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <param name="source">A value to unwrap</param>
    /// <param name="default">Default value to return if dont has value</param>
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