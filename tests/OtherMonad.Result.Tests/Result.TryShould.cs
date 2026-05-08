namespace OtherMonad.Tests;

[Trait("Result", "Try")]
public class ResultTryShould
{
    [Fact]
    public void GivenNonThrowingFactoryWhenTryReturnOk()
    {
        var result = Result.Try(() => 42);

        Assert.True(result.IsOk);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void GivenThrowingFactoryWhenTryReturnErr()
    {
        var result = Result.Try<int>(() => throw new InvalidOperationException("boom"));

        Assert.True(result.IsErr);
        Assert.IsType<InvalidOperationException>(result.Error);
        Assert.Equal("boom", result.Error.Message);
    }

    [Fact]
    public void GivenNullFactoryWhenTryThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Result.Try((Func<int>)null!));
    }

    [Fact]
    public void GivenFactoryReturningStringWhenTryReturnOkWithString()
    {
        var result = Result.Try(() => "hello");

        Assert.True(result.IsOk);
        Assert.Equal("hello", result.Value);
    }

    [Fact]
    public void GivenParsingSucceedsWhenTryReturnOkWithParsedValue()
    {
        var result = Result.Try(() => int.Parse("123"));

        Assert.True(result.IsOk);
        Assert.Equal(123, result.Value);
    }

    [Fact]
    public void GivenParsingFailsWhenTryReturnErrWithFormatException()
    {
        var result = Result.Try(() => int.Parse("not-a-number"));

        Assert.True(result.IsErr);
        Assert.IsType<FormatException>(result.Error);
    }

    [Fact]
    public void GivenFactoryThrowsArgumentExceptionWhenTryReturnErrWithSameException()
    {
        var error = new ArgumentException("bad arg");

        var result = Result.Try<string>(() => throw error);

        Assert.True(result.IsErr);
        Assert.Same(error, result.Error);
    }

    [Fact]
    public async Task GivenNonThrowingAsyncFactoryWhenTryReturnOk()
    {
        var result = await Result.Try((ct) => Task.FromResult(99));

        Assert.True(result.IsOk);
        Assert.Equal(99, result.Value);
    }

    [Fact]
    public async Task GivenThrowingAsyncFactoryWhenTryReturnErr()
    {
        var result = await Result.Try<int>(async (ct) =>
        {
            await Task.Delay(1, ct);
            throw new InvalidOperationException("async boom");
        });

        Assert.True(result.IsErr);
        Assert.IsType<InvalidOperationException>(result.Error);
        Assert.Equal("async boom", result.Error.Message);
    }

    [Fact]
    public async Task GivenNullAsyncFactoryWhenTryThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            Result.Try((Func<CancellationToken, Task<int>>)null!));
    }

    [Fact]
    public async Task GivenAsyncFactoryWhenTryWithCancellationTokenPassesToken()
    {
        var cts = new CancellationTokenSource();
        var tokenPassed = false;

        var result = await Result.Try(ct =>
        {
            tokenPassed = ct == cts.Token;
            return Task.FromResult(42);
        }, cts.Token);

        Assert.True(tokenPassed);
        Assert.True(result.IsOk);
    }

    [Fact]
    public async Task GivenAsyncFactoryReturningStringWhenTryReturnOkWithString()
    {
        var result = await Result.Try(ct => Task.FromResult("async-result"), CancellationToken.None);

        Assert.True(result.IsOk);
        Assert.Equal("async-result", result.Value);
    }

    [Fact]
    public void GivenChainOfTryAndBindWhenAllSucceedReturnFinalResult()
    {
        var result = Result.Try(() => int.Parse("10"))
            .Bind(x => Result<int>.Create.Ok(x * 2))
            .Map(x => $"Result: {x}");

        Assert.True(result.IsOk);
        Assert.Equal("Result: 20", result.Value);
    }

    [Fact]
    public void GivenTryFailsWhenChainedWithOrElseReturnFallback()
    {
        var result = Result.Try(() => int.Parse("bad"))
            .OrElse(Result<int>.Create.Ok(0));

        Assert.True(result.IsOk);
        Assert.Equal(0, result.Value);
    }
}
