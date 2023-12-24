namespace OtherMonad;

/// <summary>
/// Extension methods to Either Monad
/// </summary>
public static partial class Either
{
    /// <summary>
    /// <para>Combine <see cref="IEither{TSourceLeft,TSourceRight}"/> with <see cref="IEither{TOtherLeft,TOtherRight}"/> apply <see cref="Func{TSourceLeft, TOtherLeft, TLeft}"/> if both has left value, otherwise apply <see cref="Func{TSourceRight, TOtherRight, TRight}"/></para>
    /// </summary>
    /// <typeparam name="TSourceLeft">The type of the left element of source</typeparam>
    /// <typeparam name="TSourceRight">The type of the right element of source</typeparam>
    /// <typeparam name="TOtherLeft">The type of the left element of combine</typeparam>
    /// <typeparam name="TOtherRight">The type of the right element of combine</typeparam>
    /// <typeparam name="TLeft">The type of the left value returned by selector</typeparam>
    /// <typeparam name="TRight">The type of the right value returned by selector</typeparam>
    /// <param name="source">A left value to invoke a combine</param>
    /// <param name="other">A right value to invoke a combine</param>
    /// <param name="selectorLeft">A combine function to apply to source left element with other</param>
    /// <param name="selectorRight">A combine function to apply to source right element with other</param>
    /// <returns><see cref="Either{TLeft,TRight}"/></returns>
    /// <exception cref="ArgumentNullException">Left selector is null</exception>
    /// <exception cref="ArgumentNullException">Right selector is null</exception>
    public static Either<TLeft, TRight> Combine<TSourceLeft, TSourceRight, TOtherLeft, TOtherRight, TLeft, TRight>(
        this IEither<TSourceLeft, TSourceRight> source,
        IEither<TOtherLeft, TOtherRight> other,
        Func<TSourceLeft, TOtherLeft, TLeft> selectorLeft,
        Func<TSourceRight?, TOtherRight?, TRight> selectorRight)
    {
        ArgumentNullException.ThrowIfNull(selectorLeft, nameof(selectorLeft));
        ArgumentNullException.ThrowIfNull(selectorRight, nameof(selectorRight));

        if (source.IsLeft && other.IsLeft)
        {
            return selectorLeft(source.Left, other.Left);
        }

        return selectorRight(source.Right, other.Right);
    }
}