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
            await either.Match(null!, (c, ct) => Task.FromResult(""), TestContext.Current.CancellationToken);
        });
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyMatchWithRightNullConditionThrowArgumentnullexception()
    {
        var either = Either<Exception, string>.Create.Right("test");

        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () =>
        {
            await either.Match((c, ct) => Task.FromResult(""), null!, TestContext.Current.CancellationToken);
        });
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyMatchWithSuccessStateExecuteRightCondition()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.Match((c, ct) => Task.FromResult("fail"), (c, ct) => Task.FromResult("success"), TestContext.Current.CancellationToken);

        Assert.Equal("success", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyMatchWithErrorStateExecuteLeftCondition()
    {
        var either = Either<Exception, string>.Create.Left(new Exception());

        var result = await either.Match((c, ct) => Task.FromResult("fail"), (c, ct) => Task.FromResult("success"), TestContext.Current.CancellationToken);

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchWithLeftConditionNullReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.TryMatch(null!, (c, ct) => Task.FromResult(""), "default", TestContext.Current.CancellationToken);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchWithRightConditionNullReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.TryMatch((c, ct) => Task.FromResult(""), null!, "default", TestContext.Current.CancellationToken);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchRightConditionReturnRightValue()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.TryMatch((c, ct) => Task.FromResult("fail"), (c, ct) => Task.FromResult("success"), "default", TestContext.Current.CancellationToken);

        Assert.Equal("success", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchLeftConditionReturnLeftValue()
    {
        var either = Either<Exception, string>.Create.Left(new Exception());

        var result = await either.TryMatch((c, ct) => Task.FromResult("fail"), (c, ct) => Task.FromResult("success"), "default", TestContext.Current.CancellationToken);

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchRightConditionThrowExceptionReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.TryMatch((c, ct) => Task.FromResult("fail"), (c, ct) => throw new Exception(), "default", TestContext.Current.CancellationToken);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task GivenEitherAsyncWhenApplyTryMatchLeftConditionThrowExceptionReturnDefault()
    {
        var either = Either<Exception, string>.Create.Left(new Exception());

        var result = await either.TryMatch((c, ct) => throw new Exception(), (c, ct) => Task.FromResult("success"), "default", TestContext.Current.CancellationToken);

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

    [Fact]
    public void GivenEitherRightWithValueTypeWhenMatchReturnRightResult()
    {
        var either = Either<string, int>.Create.Right(42);

        var result = either.Match(
            left => $"Error: {left}",
            right => $"Success: {right}");

        Assert.Equal("Success: 42", result);
    }

    [Fact]
    public void GivenEitherLeftWithValueTypeWhenMatchReturnLeftResult()
    {
        var either = Either<string, int>.Create.Left("error occurred");

        var result = either.Match(
            left => $"Error: {left}",
            right => $"Success: {right}");

        Assert.Equal("Error: error occurred", result);
    }

    [Fact]
    public void GivenEitherRightWithStructWhenMatchReturnStructData()
    {
        var either = Either<string, DummyStruct>.Create.Right(new DummyStruct { Id = 1, Name = "Test" });

        var result = either.Match(
            left => $"Error: {left}",
            right => $"Id: {right.Id}, Name: {right.Name}");

        Assert.Equal("Id: 1, Name: Test", result);
    }

    [Fact]
    public void GivenEitherWhenMatchTransformsToComplexObjectReturnCorrectObject()
    {
        var either = Either<Exception, int>.Create.Right(100);

        var result = either.Match(
            left => new { Status = "Error", Value = 0 },
            right => new { Status = "Success", Value = right });

        Assert.Equal("Success", result.Status);
        Assert.Equal(100, result.Value);
    }

    [Fact]
    public void GivenEitherLeftWhenMatchTransformsToComplexObjectReturnCorrectObject()
    {
        var either = Either<Exception, int>.Create.Left(new Exception("fail"));

        var result = either.Match(
            left => new { Status = "Error", Message = left.Message },
            right => new { Status = "Success", Message = "OK" });

        Assert.Equal("Error", result.Status);
        Assert.Equal("fail", result.Message);
    }

    [Fact]
    public async Task GivenEitherRightWhenMatchAsyncWithTransformationReturnCorrectResult()
    {
        var either = Either<string, int>.Create.Right(10);

        var result = await either.Match(
            (left, ct) => Task.FromResult($"Left: {left}"),
            (right, ct) => Task.FromResult($"Right: {right * 2}"),
            TestContext.Current.CancellationToken);

        Assert.Equal("Right: 20", result);
    }

    [Fact]
    public async Task GivenEitherLeftWhenMatchAsyncWithTransformationReturnCorrectResult()
    {
        var either = Either<string, int>.Create.Left("error");

        var result = await either.Match(
            (left, ct) => Task.FromResult($"Left: {left.ToUpperInvariant()}"),
            (right, ct) => Task.FromResult($"Right: {right}"),
            TestContext.Current.CancellationToken);

        Assert.Equal("Left: ERROR", result);
    }

    [Fact]
    public async Task GivenEitherRightWhenMatchAsyncWithCancellationTokenUsesToken()
    {
        var cts = new CancellationTokenSource();
        var either = Either<string, int>.Create.Right(42);
        var tokenPassed = false;

        var result = await either.Match(
            (left, ct) => Task.FromResult("left"),
            (right, ct) =>
            {
                tokenPassed = ct == cts.Token;
                return Task.FromResult("right");
            },
            cts.Token);

        Assert.True(tokenPassed);
        Assert.Equal("right", result);
    }

    [Fact]
    public async Task GivenEitherLeftWhenMatchAsyncWithCancellationTokenUsesToken()
    {
        var cts = new CancellationTokenSource();
        var either = Either<string, int>.Create.Left("error");
        var tokenPassed = false;

        var result = await either.Match(
            (left, ct) =>
            {
                tokenPassed = ct == cts.Token;
                return Task.FromResult("left");
            },
            (right, ct) => Task.FromResult("right"),
            cts.Token);

        Assert.True(tokenPassed);
        Assert.Equal("left", result);
    }

    [Fact]
    public void GivenEitherRightWhenTryMatchWithSuccessReturnRightResult()
    {
        var either = Either<Exception, string>.Create.Right("success");

        var result = either.TryMatch(
            left => "error",
            right => right.ToUpperInvariant(),
            "default");

        Assert.Equal("SUCCESS", result);
    }

    [Fact]
    public void GivenEitherLeftWhenTryMatchWithSuccessReturnLeftResult()
    {
        var either = Either<Exception, string>.Create.Left(new Exception("failed"));

        var result = either.TryMatch(
            left => left.Message.ToUpperInvariant(),
            right => right,
            "default");

        Assert.Equal("FAILED", result);
    }

    [Fact]
    public async Task GivenEitherRightWhenTryMatchAsyncWithDelayReturnRightResult()
    {
        var either = Either<string, int>.Create.Right(5);

        var result = await either.TryMatch(
            (left, ct) => Task.FromResult(-1),
            async (right, ct) =>
            {
                await Task.Delay(1, ct);
                return right * 10;
            },
            0,
            TestContext.Current.CancellationToken);

        Assert.Equal(50, result);
    }

    [Fact]
    public async Task GivenEitherWhenTryMatchBothConditionsNullAndNoExceptionReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.TryMatch(
            null!,
            null!,
            "fallback",
            TestContext.Current.CancellationToken);

        Assert.Equal("fallback", result);
    }

    [Fact]
    public void GivenEitherRightWithBoolWhenMatchReturnCorrectBranch()
    {
        var either = Either<string, bool>.Create.Right(true);

        var result = either.Match(
            left => false,
            right => right);

        Assert.True(result);
    }

    [Fact]
    public void GivenEitherLeftWithBoolWhenMatchReturnCorrectBranch()
    {
        var either = Either<bool, string>.Create.Left(true);

        var result = either.Match(
            left => left,
            right => false);

        Assert.True(result);
    }

    [Fact]
    public void GivenEitherRightWithZeroWhenMatchReturnCorrectValue()
    {
        var either = Either<string, int>.Create.Right(0);

        var result = either.Match(
            left => -1,
            right => right);

        Assert.Equal(0, result);
    }

    [Fact]
    public void GivenEitherRightWithEmptyStringWhenMatchReturnEmptyString()
    {
        var either = Either<Exception, string>.Create.Right("");

        var result = either.Match(
            left => "error",
            right => right);

        Assert.Equal("", result);
    }

    [Fact]
    public async Task GivenEitherWhenTryMatchRightThrowsExceptionReturnDefault()
    {
        var either = Either<Exception, string>.Create.Right("test");

        var result = await either.TryMatch(
            (left, ct) => Task.FromResult("left"),
            (right, ct) => throw new InvalidOperationException("boom"),
            "default",
            TestContext.Current.CancellationToken);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task GivenEitherWhenTryMatchLeftThrowsExceptionReturnDefault()
    {
        var either = Either<Exception, string>.Create.Left(new Exception("error"));

        var result = await either.TryMatch(
            (left, ct) => throw new InvalidOperationException("boom"),
            (right, ct) => Task.FromResult("right"),
            "default",
            TestContext.Current.CancellationToken);

        Assert.Equal("default", result);
    }

    [Fact]
    public void GivenEitherWhenMatchWithSameBranchResultTypesReturnCorrectValue()
    {
        var eitherRight = Either<int, int>.Create.Right(100);
        var eitherLeft = Either<int, int>.Create.Left(200);

        var resultRight = eitherRight.Match(left => left * 2, right => right * 2);
        var resultLeft = eitherLeft.Match(left => left * 2, right => right * 2);

        Assert.Equal(200, resultRight);
        Assert.Equal(400, resultLeft);
    }

    public struct DummyStruct
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}