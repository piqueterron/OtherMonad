namespace OtherMonad;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// <para>Combine two <see cref="Maybe{TResult}"/> apply <see cref="Func{TSource, TResult}"/> to create new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> otherwise default value</para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TCombine">The type of the element of combine</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A left value to invoke a combine</param>
    /// <param name="other">A right value to invoke a combine</param>
    /// <param name="select">A combine function to apply to source element with other</param>
    /// <param name="defaultValueFactory">Default value in case no match</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> or default value</returns>
    public static Maybe<TResult> TryCombine<TSource, TCombine, TResult>(this Maybe<TSource> source, Maybe<TCombine> other, Func<TSource, TCombine, TResult> select, Func<TResult> defaultValueFactory)
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

    /// <summary>
    /// <para>Combine two <see cref="Maybe{TResult}"/> apply <see cref="Func{TSource, TResult}"/> to create new <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see></para>
    /// </summary>
    /// <typeparam name="TSource">The type of the element of source</typeparam>
    /// <typeparam name="TCombine">The type of the element of combine</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector</typeparam>
    /// <param name="source">A left value to invoke a combine</param>
    /// <param name="other">A right value to invoke a combine</param>
    /// <param name="select">A combine function to apply to source element with other</param>
    /// <returns><see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[> ]]></see> or <see cref="Maybe{TResult}"><![CDATA[ Maybe<]]><typeparamref name="TResult"/><![CDATA[>.None ]]></see></returns>
    public static Maybe<TResult> Combine<TSource, TCombine, TResult>(this Maybe<TSource> source, Maybe<TCombine> other, Func<TSource, TCombine, TResult> select) =>
        source.Bind(src => select(src, other.Value))
            .Match(res => res, () => Maybe<TResult>.None);
}