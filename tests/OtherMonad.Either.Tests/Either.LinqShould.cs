namespace Monads.Either.Tests;

using OtherMonad;

[Trait("Either", "Linq")]
public class EitherLinqShould
{
    [Fact]
    public void GivenRightWhenUsingSelectReturnProjectedRight()
    {
        var source = Either<string, int>.Create.Right(21);

        var result = from value in source
                     select value * 2;

        Assert.True(result.IsRight);
        Assert.Equal(42, result.Right);
    }

    [Fact]
    public void GivenLeftWhenUsingSelectPropagateLeft()
    {
        var source = Either<string, int>.Create.Left("error");

        var result = from value in source
                     select value * 2;

        Assert.True(result.IsLeft);
        Assert.Equal("error", result.Left);
    }

    [Fact]
    public void GivenTwoRightWhenUsingMultipleFromReturnCombinedRight()
    {
        var a = Either<string, int>.Create.Right(2);
        var b = Either<string, int>.Create.Right(3);

        var result = from x in a
                     from y in b
                     select x + y;

        Assert.True(result.IsRight);
        Assert.Equal(5, result.Right);
    }

    [Fact]
    public void GivenSecondLeftWhenUsingMultipleFromPropagateLeftAndDoNotInvokeResultSelector()
    {
        var a = Either<string, int>.Create.Right(2);
        var b = Either<string, int>.Create.Left("boom");
        var resultSelectorInvoked = false;

        var result = from x in a
                     from y in b
                     select Track(x, y, ref resultSelectorInvoked);

        Assert.True(result.IsLeft);
        Assert.Equal("boom", result.Left);
        Assert.False(resultSelectorInvoked);
    }

    [Fact]
    public void GivenFirstLeftWhenUsingMultipleFromDoNotInvokeCollectionSelector()
    {
        var a = Either<string, int>.Create.Left("boom");
        var collectionSelectorInvoked = false;

        var result = a.SelectMany(
            x => { collectionSelectorInvoked = true; return Either<string, int>.Create.Right(x + 1); },
            (x, y) => x + y);

        Assert.True(result.IsLeft);
        Assert.Equal("boom", result.Left);
        Assert.False(collectionSelectorInvoked);
    }

    [Fact]
    public void GivenNullSelectorWhenUsingSelectThrowArgumentNullException()
    {
        var source = Either<string, int>.Create.Right(1);

        Assert.Throws<ArgumentNullException>(() => source.Select<string, int, int>(null!));
    }

    [Fact]
    public void GivenNullSelectorWhenUsingSelectManyThrowArgumentNullException()
    {
        var source = Either<string, int>.Create.Right(1);

        Assert.Throws<ArgumentNullException>(() => source.SelectMany<string, int, int>(null!));
    }

    [Fact]
    public void GivenNullCollectionSelectorWhenUsingSelectManyThrowArgumentNullException()
    {
        var source = Either<string, int>.Create.Right(1);

        Assert.Throws<ArgumentNullException>(() => source.SelectMany<string, int, int, int>(null!, (x, y) => x + y));
    }

    [Fact]
    public void GivenNullResultSelectorWhenUsingSelectManyThrowArgumentNullException()
    {
        var source = Either<string, int>.Create.Right(1);

        Assert.Throws<ArgumentNullException>(() => source.SelectMany<string, int, int, int>(x => Either<string, int>.Create.Right(x + 1), null!));
    }

    private static int Track(int x, int y, ref bool invoked)
    {
        invoked = true;
        return x + y;
    }
}
