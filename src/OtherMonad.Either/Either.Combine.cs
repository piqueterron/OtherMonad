namespace OtherMonad;

/// <summary>
/// Extension methods to Either Monad
/// </summary>
public static partial class Either
{
    public static Either<TLeft, TRight> Combine<TLeft1, TRight1, TLeft2, TRight2, TLeft, TRight>(
        this IEither<TLeft1, TRight1> source,
        IEither<TLeft2, TRight2> other,
        Func<TLeft1, TLeft2, TLeft> leftFunc,
        Func<TRight1?, TRight2?, TRight> rightFunc)
    {
        if (source.IsLeft && other.IsLeft)
        {
            return leftFunc(source.Left, other.Left);
        }

        return rightFunc(source.Right, other.Right);
    }
}