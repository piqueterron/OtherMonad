namespace OtherMonad;

/// <summary>
/// Extension methods to Either Monad
/// </summary>
public static partial class Either
{
    /// <summary>
    /// <para>Combines <see cref="IEither{TSourceLeft,TSourceRight}"/> with <see cref="IEither{TOtherLeft,TOtherRight}"/>:
    /// applies <paramref name="selectorRight"/> when both are in the Right (success) state,
    /// applies <paramref name="selectorLeft"/> when both are in the Left (failure) state,
    /// or returns a Left (failure) when states are mixed.</para>
    /// <para>When states are mixed (one Left, one Right) the result is always a failure.
    /// <paramref name="selectorLeft"/> is called with the available Left value and
    /// <c>default</c> for the missing one; the selector must handle <c>null</c> inputs gracefully.</para>
    /// </summary>
    /// <typeparam name="TSourceLeft">The failure/error type of source.</typeparam>
    /// <typeparam name="TSourceRight">The success type of source.</typeparam>
    /// <typeparam name="TOtherLeft">The failure/error type of other.</typeparam>
    /// <typeparam name="TOtherRight">The success type of other.</typeparam>
    /// <typeparam name="TLeft">The failure/error type of the result.</typeparam>
    /// <typeparam name="TRight">The success type of the result.</typeparam>
    /// <param name="source">The first Either to combine.</param>
    /// <param name="other">The second Either to combine.</param>
    /// <param name="selectorLeft">Combines two failure values into one failure result. May receive <c>null</c> for the missing side in mixed states.</param>
    /// <param name="selectorRight">Combines two success values into one success result.</param>
    /// <returns><see cref="Either{TLeft,TRight}"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selectorLeft"/> or <paramref name="selectorRight"/> is <see langword="null"/>.</exception>
    public static Either<TLeft, TRight> Combine<TSourceLeft, TSourceRight, TOtherLeft, TOtherRight, TLeft, TRight>(
        this IEither<TSourceLeft, TSourceRight> source,
        IEither<TOtherLeft, TOtherRight> other,
        Func<TSourceLeft?, TOtherLeft?, TLeft> selectorLeft,
        Func<TSourceRight, TOtherRight, TRight> selectorRight)
    {
        ArgumentNullException.ThrowIfNull(selectorLeft);
        ArgumentNullException.ThrowIfNull(selectorRight);

        if (source.IsRight && other.IsRight)
        {
            return Either<TLeft, TRight>.Create.Right(selectorRight(source.Right, other.Right));
        }

        if (source.IsLeft && other.IsLeft)
        {
            return Either<TLeft, TRight>.Create.Left(selectorLeft(source.Left, other.Left));
        }

        return source.IsLeft
            ? Either<TLeft, TRight>.Create.Left(selectorLeft(source.Left, default))
            : Either<TLeft, TRight>.Create.Left(selectorLeft(default, other.Left));
    }
}