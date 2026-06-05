namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Linq")]
public class MaybeLinqShould
{
    [Fact]
    public void GivenSomeWhenUsingSelectReturnProjectedSome()
    {
        Maybe<int> source = 21;

        var result = from value in source
                     select value * 2;

        Assert.True(result.HasValue);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void GivenNoneWhenUsingSelectReturnNone()
    {
        var source = Maybe<int>.None;

        var result = from value in source
                     select value * 2;

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<int>.None, result);
    }

    [Fact]
    public void GivenTwoSomeWhenUsingMultipleFromReturnCombinedSome()
    {
        Maybe<int> a = 2;
        Maybe<int> b = 3;

        var result = from x in a
                     from y in b
                     select x + y;

        Assert.True(result.HasValue);
        Assert.Equal(5, result.Value);
    }

    [Fact]
    public void GivenSecondNoneWhenUsingMultipleFromReturnNoneAndDoNotInvokeResultSelector()
    {
        Maybe<int> a = 2;
        var b = Maybe<int>.None;
        var resultSelectorInvoked = false;

        var result = from x in a
                     from y in b
                     select Track(x, y, ref resultSelectorInvoked);

        Assert.False(result.HasValue);
        Assert.False(resultSelectorInvoked);
    }

    [Fact]
    public void GivenFirstNoneWhenUsingMultipleFromReturnNoneAndDoNotInvokeCollectionSelector()
    {
        var a = Maybe<int>.None;
        var collectionSelectorInvoked = false;

        var result = a.SelectMany(
            x => { collectionSelectorInvoked = true; return (Maybe<int>)(x + 1); },
            (x, y) => x + y);

        Assert.False(result.HasValue);
        Assert.False(collectionSelectorInvoked);
    }

    [Fact]
    public void GivenNullSelectorWhenUsingSelectThrowArgumentNullException()
    {
        Maybe<int> source = 1;

        Assert.Throws<ArgumentNullException>(() => source.Select<int, int>(null!));
    }

    [Fact]
    public void GivenNullSelectorWhenUsingSelectManyThrowArgumentNullException()
    {
        Maybe<int> source = 1;

        Assert.Throws<ArgumentNullException>(() => source.SelectMany<int, int>(null!));
    }

    [Fact]
    public void GivenNullCollectionSelectorWhenUsingSelectManyThrowArgumentNullException()
    {
        Maybe<int> source = 1;

        Assert.Throws<ArgumentNullException>(() => source.SelectMany<int, int, int>(null!, (x, y) => x + y));
    }

    [Fact]
    public void GivenNullResultSelectorWhenUsingSelectManyThrowArgumentNullException()
    {
        Maybe<int> source = 1;

        Assert.Throws<ArgumentNullException>(() => source.SelectMany<int, int, int>(x => x + 1, null!));
    }

    private static int Track(int x, int y, ref bool invoked)
    {
        invoked = true;
        return x + y;
    }
}
