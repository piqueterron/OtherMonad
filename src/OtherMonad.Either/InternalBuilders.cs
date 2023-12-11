namespace OtherMonad;
internal static class InternalBuilders
{
    public static Result<TLeft> Create<TLeft>(TLeft left) =>
        left;

    public static Result<TLeft> Create<TLeft>(Exception right) =>
        right;

    public static Either<(TLeft1, TLeft2), (TRight1, TRight2)> CreateLeft<TLeft1, TRight1, TLeft2, TRight2>(TLeft1 left1, TLeft2 left2) =>
        (left1, left2);

    public static Either<(TLeft1, TLeft2), (TRight1, TRight2)> CreateRight<TLeft1, TRight1, TLeft2, TRight2>(TRight1 right1, TRight2 right2) =>
        (right1, right2);

    public static Either<(TLeft1, TLeft2, TLeft3), (TRight1, TRight2, TRight3)> CreateLeft<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3>(TLeft1 left1, TLeft2 left2, TLeft3 left3) =>
        (left1, left2, left3);

    public static Either<(TLeft1, TLeft2, TLeft3), (TRight1, TRight2, TRight3)> CreateRight<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3>(TRight1 right1, TRight2 right2, TRight3 right3) =>
        (right1, right2, right3);

    public static Either<(TLeft1, TLeft2, TLeft3, TLeft4), (TRight1, TRight2, TRight3, TRight4)> CreateLeft<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4>(TLeft1 left1, TLeft2 left2, TLeft3 left3, TLeft4 left4) =>
        (left1, left2, left3, left4);

    public static Either<(TLeft1, TLeft2, TLeft3, TLeft4), (TRight1, TRight2, TRight3, TRight4)> CreateRight<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4>(TRight1 right1, TRight2 right2, TRight3 right3, TRight4 right4) =>
        (right1, right2, right3, right4);

    public static Either<(TLeft1, TLeft2, TLeft3, TLeft4, TLeft5), (TRight1, TRight2, TRight3, TRight4, TRight5)> CreateLeft<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4, TLeft5, TRight5>(TLeft1 left1, TLeft2 left2, TLeft3 left3, TLeft4 left4, TLeft5 left5) =>
        (left1, left2, left3, left4, left5);

    public static Either<(TLeft1, TLeft2, TLeft3, TLeft4, TLeft5), (TRight1, TRight2, TRight3, TRight4, TRight5)> CreateRight<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4, TLeft5, TRight5>(TRight1 right1, TRight2 right2, TRight3 right3, TRight4 right4, TRight5 right5) =>
        (right1, right2, right3, right4, right5);

    public static Either<(TLeft1, TLeft2, TLeft3, TLeft4, TLeft5, TLeft6), (TRight1, TRight2, TRight3, TRight4, TRight5, TRight6)> CreateLeft<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4, TLeft5, TRight5, TLeft6, TRight6>(TLeft1 left1, TLeft2 left2, TLeft3 left3, TLeft4 left4, TLeft5 left5, TLeft6 left6) =>
        (left1, left2, left3, left4, left5, left6);

    public static Either<(TLeft1, TLeft2, TLeft3, TLeft4, TLeft5, TLeft6), (TRight1, TRight2, TRight3, TRight4, TRight5, TRight6)> CreateRight<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4, TLeft5, TRight5, TLeft6, TRight6>(TRight1 right1, TRight2 right2, TRight3 right3, TRight4 right4, TRight5 right5, TRight6 right6) =>
        (right1, right2, right3, right4, right5, right6);
}