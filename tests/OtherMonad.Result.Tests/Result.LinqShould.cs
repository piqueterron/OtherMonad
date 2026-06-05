namespace Monads.Result.Tests;

using OtherMonad;

[Trait("Result", "Linq")]
public class ResultLinqShould
{
    [Fact]
    public void GivenOkWhenUsingSelectReturnProjectedOk()
    {
        var source = Result<int>.Create.Ok(21);

        var result = from value in source
                     select value * 2;

        Assert.True(result.IsOk);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void GivenErrWhenUsingSelectPropagateError()
    {
        var error = new InvalidOperationException("boom");
        var source = Result<int>.Create.Err(error);

        var result = from value in source
                     select value * 2;

        Assert.True(result.IsErr);
        Assert.Same(error, result.Error);
    }

    [Fact]
    public void GivenTwoOkWhenUsingMultipleFromReturnCombinedOk()
    {
        var a = Result<int>.Create.Ok(2);
        var b = Result<int>.Create.Ok(3);

        var result = from x in a
                     from y in b
                     select x + y;

        Assert.True(result.IsOk);
        Assert.Equal(5, result.Value);
    }

    [Fact]
    public void GivenSecondErrWhenUsingMultipleFromPropagateErrorAndDoNotInvokeResultSelector()
    {
        var a = Result<int>.Create.Ok(2);
        var error = new InvalidOperationException("boom");
        var b = Result<int>.Create.Err(error);
        var resultSelectorInvoked = false;

        var result = from x in a
                     from y in b
                     select Track(x, y, ref resultSelectorInvoked);

        Assert.True(result.IsErr);
        Assert.Same(error, result.Error);
        Assert.False(resultSelectorInvoked);
    }

    [Fact]
    public void GivenFirstErrWhenUsingMultipleFromDoNotInvokeCollectionSelector()
    {
        var error = new InvalidOperationException("boom");
        var a = Result<int>.Create.Err(error);
        var collectionSelectorInvoked = false;

        var result = a.SelectMany(
            x => { collectionSelectorInvoked = true; return Result<int>.Create.Ok(x + 1); },
            (x, y) => x + y);

        Assert.True(result.IsErr);
        Assert.Same(error, result.Error);
        Assert.False(collectionSelectorInvoked);
    }

    [Fact]
    public void GivenNullSelectorWhenUsingSelectThrowArgumentNullException()
    {
        var source = Result<int>.Create.Ok(1);

        Assert.Throws<ArgumentNullException>(() => source.Select<int, int>(null!));
    }

    [Fact]
    public void GivenNullSelectorWhenUsingSelectManyThrowArgumentNullException()
    {
        var source = Result<int>.Create.Ok(1);

        Assert.Throws<ArgumentNullException>(() => source.SelectMany<int, int>(null!));
    }

    [Fact]
    public void GivenNullCollectionSelectorWhenUsingSelectManyThrowArgumentNullException()
    {
        var source = Result<int>.Create.Ok(1);

        Assert.Throws<ArgumentNullException>(() => source.SelectMany<int, int, int>(null!, (x, y) => x + y));
    }

    [Fact]
    public void GivenNullResultSelectorWhenUsingSelectManyThrowArgumentNullException()
    {
        var source = Result<int>.Create.Ok(1);

        Assert.Throws<ArgumentNullException>(() => source.SelectMany<int, int, int>(x => Result<int>.Create.Ok(x + 1), null!));
    }

    private static int Track(int x, int y, ref bool invoked)
    {
        invoked = true;
        return x + y;
    }
}
