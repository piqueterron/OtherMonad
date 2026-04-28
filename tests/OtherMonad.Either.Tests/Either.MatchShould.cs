namespace Monads.Tests.Either;

using OtherMonad;

[Trait("Either", "Match")]
public class EitherMatchShould
{
    [Fact]
    public void GivenEitherWhenApplyMatchWithLeftNullConditionThrowArgumentnullexception()
    {
        var either = Either<Exception, string>.Create.Right("test");

        Assert.Throws<ArgumentNullException>(() =>
        {
            either.Match(null!, c => "");
        });
    }

    [Fact]
    public void GivenEitherWhenApplyMatchWithRightNullConditionThrowArgumentnullexception()
    {
        var either = Either<Exception, string>.Create.Right("test");

        Assert.Throws<ArgumentNullException>(() =>
        {
            either.Match(c => "", null!);
        });
    }

    [Fact]
    public void GivenEitherWhenApplyMatchWithSuccessStateExecuteRightCondition()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = either.Match(c => "fail", c => "success");

        Assert.Equal("success", result);
    }

    [Fact]
    public void GivenEitherWhenApplyMatchWithErrorStateExecuteLeftCondition()
    {
        var either = Either<Exception, string>.Create.Left(new Exception());

        var result = either.Match(c => "fail", c => "success");

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyMatchWithLeftNullConditionThrowArgumentnullexception()
    {
        var either = Either<Exception, string>.Create.Right("test");

        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () =>
        {
            await either.Match(null!, (c, ct) => Task.FromResult(""), CancellationToken.None);
        });
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyMatchWithRightNullConditionThrowArgumentnullexception()
    {
        var either = Either<Exception, string>.Create.Right("test");

        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () =>
        {
            await either.Match((c, ct) => Task.FromResult(""), null!, CancellationToken.None);
        });
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyMatchWithSuccessStateExecuteRightCondition()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.Match((c, ct) => Task.FromResult("fail"), (c, ct) => Task.FromResult("success"), CancellationToken.None);

        Assert.Equal("success", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyMatchWithErrorStateExecuteLeftCondition()
    {
        var either = Either<Exception, string>.Create.Left(new Exception());

        var result = await either.Match((c, ct) => Task.FromResult("fail"), (c, ct) => Task.FromResult("success"), CancellationToken.None);

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchWithLeftConditionNullReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.TryMatch(null!, (c, ct) => Task.FromResult(""), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchWithRightConditionNullReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.TryMatch((c, ct) => Task.FromResult(""), null!, "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchRightConditionReturnRightValue()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.TryMatch((c, ct) => Task.FromResult("fail"), (c, ct) => Task.FromResult("success"), "default", CancellationToken.None);

        Assert.Equal("success", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchLeftConditionReturnLeftValue()
    {
        var either = Either<Exception, string>.Create.Left(new Exception());

        var result = await either.TryMatch((c, ct) => Task.FromResult("fail"), (c, ct) => Task.FromResult("success"), "default", CancellationToken.None);

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchRightConditionThrowExceptionReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.TryMatch((c, ct) => Task.FromResult("fail"), (c, ct) => throw new Exception(), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchLeftConditionThrowExceptionReturnDefault()
    {
        var either = Either<Exception, string>.Create.Left(new Exception());

        var result = await either.TryMatch((c, ct) => throw new Exception(), (c, ct) => Task.FromResult("success"), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public void GivenEitherWhenApplyTryMatchWithRightAndLeftConditionNullReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = either.TryMatch(null!, null!, true);

        Assert.True(result);
    }

    [Fact]
    public void GivenEitherWhenApplyTryMatchWithLeftConditionNullReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = either.TryMatch(null!, c => false, true);

        Assert.True(result);
    }

    [Fact]
    public void GivenEitherWhenApplyTryMatchWithRightConditionNullReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = either.TryMatch(c => false, null!, true);

        Assert.True(result);
    }

    [Fact]
    public void GivenEitherWhenApplyTryMatchRightConditionThrowExceptionReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = either.TryMatch(c => false, c => throw new Exception(), true);

        Assert.True(result);
    }

    [Fact]
    public void GivenEitherWhenApplyTryMatchLeftConditionThrowExceptionReturnDefault()
    {
        var either = Either<Exception, string>.Create.Left(new Exception());

        var result = either.TryMatch(c => throw new Exception(), c => false, true);

        Assert.True(result);
    }
}