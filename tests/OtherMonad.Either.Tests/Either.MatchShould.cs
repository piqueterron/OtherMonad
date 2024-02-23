namespace Monads.Tests.Either;

using OtherMonad;

[Trait("Either", "Match")]
public class EitherMatchShould
{
    [Fact]
    public void GivenEitherTaskWhenApplyMatchWithLeftNullConditionThrowArgumentnullexception()
    {
        Either<string, Exception> either = "test";

        Assert.Throws<ArgumentNullException>(() =>
        {
            either.Match(null, c => "");
        });
    }

    [Fact]
    public void GivenEitherTaskWhenApplyMatchWithRightNullConditionThrowArgumentnullexception()
    {
        Either<string, Exception> either = "test";

        Assert.Throws<ArgumentNullException>(() =>
        {
            either.Match(c => "", null);
        });
    }

    [Fact]
    public void GivenEitherTaskWhenApplyMatchWithSuccesssStateExecuteLeftCondition()
    {
        Either<string, Exception> either = "test";

        var result = either.Match(c => "success", c => "fail");

        Assert.Equal("success", result);
    }

    [Fact]
    public void GivenEitherTaskWhenApplyMatchWithErrorStateExecuteRightCondition()
    {
        Either<string, Exception> either = new Exception();

        var result = either.Match(c => "success", c => "fail");

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task GivenEitherTaskasyncWhenApplyMatchWithLeftNullConditionThrowArgumentnullexception()
    {
        Either<string, Exception> either = "test";

        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () =>
        {
            await either.Match(null, (c, ct) => Task.FromResult(""), CancellationToken.None);
        });
    }

    [Fact]
    public async Task GivenEitherTaskasyncWhenApplyMatchWithRightNullConditionThrowArgumentnullexception()
    {
        Either<string, Exception> either = "test";

        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () =>
        {
            await either.Match((c, ct) => Task.FromResult(""), null, CancellationToken.None);
        });
    }

    [Fact]
    public async Task GivenEitherTaskasyncWhenApplyMatchWithSuccesssStateExecuteLeftCondition()
    {
        Either<string, Exception> either = "test";

        var result = await either.Match((c, ct) => Task.FromResult("success"), (c, ct) => Task.FromResult("fail"), CancellationToken.None);

        Assert.Equal("success", result);
    }

    [Fact]
    public async Task GivenEitherTaskasyncWhenApplyMatchWithErrorStateExecuteRightCondition()
    {
        Either<string, Exception> either = new Exception();

        var result = await either.Match((c, ct) => Task.FromResult("success"), (c, ct) => Task.FromResult("fail"), CancellationToken.None);

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task GivenEitherTaskasyncWhenApplyTrymatchWithLeftConditionNullReturnDefault()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch(null, (c, ct) => Task.FromResult(""), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task GivenEitherTaskasyncWhenApplyTrymatchWithRightConditionNullReturnDefault()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch((c, ct) => Task.FromResult(""), null, "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task GivenEitherTaskasyncWhenApplyTrymatchLeftConditionReturnLeftValue()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch((c, ct) => Task.FromResult("success"), (c, ct) => Task.FromResult("fail"), "default", CancellationToken.None);

        Assert.Equal("success", result);
    }

    [Fact]
    public async Task GivenEitherTaskasyncWhenApplyTrymatchRightConditionReturnRightValue()
    {
        Either<string, Exception> either = new Exception();

        var result = await either.TryMatch((c, ct) => Task.FromResult("success"), (c, ct) => Task.FromResult("fail"), "default", CancellationToken.None);

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task GivenEitherTaskasyncWhenApplyTrymatchLeftConditionThrowExceptionReturnDefault()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch((c, ct) => throw new Exception(), (c, ct) => Task.FromResult("fail"), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task GivenEitherTaskasyncWhenApplyTrymatchRightConditionThrowExceptionReturnDefault()
    {
        Either<string, Exception> either = new Exception();

        var result = await either.TryMatch((c, ct) => Task.FromResult("success"), (c, ct) => throw new Exception(), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public void GivenEitherWhenApplyTrymatchWithRightAndLeftConditionNullReturnDefault()
    {
        Either<string, Exception> either = "test";

        var result = either.TryMatch(null, null, true);

        Assert.True(result);
    }

    [Fact]
    public void GivenEitherWhenApplyTrymatchWithLeftConditionNullReturnDefault()
    {
        Either<string, Exception> either = "test";

        var result = either.TryMatch(null, c => false, true);

        Assert.True(result);
    }

    [Fact]
    public void GivenEitherWhenApplyTrymatchWithRightConditionNullReturnDefault()
    {
        Either<string, Exception> either = "test";

        var result = either.TryMatch(c => false, null, true);

        Assert.True(result);
    }

    [Fact]
    public void GivenEitherWhenApplyTrymatchLeftConditionThrowExceptionReturnDefault()
    {
        Either<string, Exception> either = "test";

        var result = either.TryMatch(c => throw new Exception(), c => false, true);

        Assert.True(result);
    }

    [Fact]
    public void GivenEitherWhenApplyTrymatchRightConditionThrowExceptionReturnDefault()
    {
        Either<string, Exception> either = new Exception();

        var result = either.TryMatch(c => false, c => throw new Exception(), true);

        Assert.True(result);
    }
}