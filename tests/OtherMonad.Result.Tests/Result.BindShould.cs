namespace OtherMonad.Tests;

[Trait("Result", "Bind/Map")]
public class ResultBindShould
{
    [Fact]
    public void GivenResultOkWhenBindReturnsSelectorResult()
    {
        var result = Result<int>.Create.Ok(5);

        var output = result.Bind(x => Result<string>.Create.Ok(x.ToString()));

        Assert.True(output.IsOk);
        Assert.Equal("5", output.Value);
    }

    [Fact]
    public void GivenResultErrWhenBindPropagatesError()
    {
        var error = new Exception("error");
        var result = Result<int>.Create.Err(error);

        var output = result.Bind(x => Result<string>.Create.Ok(x.ToString()));

        Assert.True(output.IsErr);
        Assert.Same(error, output.Error);
    }

    [Fact]
    public void GivenNullSelectorWhenBindThrowsArgumentNullException()
    {
        var result = Result<int>.Create.Ok(1);

        Assert.Throws<ArgumentNullException>(() => result.Bind((Func<int, Result<string>>)null!));
    }

    [Fact]
    public async Task GivenResultOkWhenBindAsyncReturnsSelectorResult()
    {
        var result = Result<int>.Create.Ok(42);

        var output = await result.Bind((x, ct) => Task.FromResult(Result<string>.Create.Ok(x.ToString())));

        Assert.True(output.IsOk);
        Assert.Equal("42", output.Value);
    }

    [Fact]
    public async Task GivenResultErrWhenBindAsyncPropagatesError()
    {
        var error = new Exception("fail");
        var result = Result<int>.Create.Err(error);

        var output = await result.Bind((x, ct) => Task.FromResult(Result<string>.Create.Ok(x.ToString())));

        Assert.True(output.IsErr);
        Assert.Same(error, output.Error);
    }

    [Fact]
    public async Task GivenNullSelectorWhenBindAsyncThrowsArgumentNullException()
    {
        var result = Result<int>.Create.Ok(1);

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            result.Bind((Func<int, CancellationToken, Task<Result<string>>>)null!));
    }

    [Fact]
    public void GivenResultOkWhenBindReturnsErrPropagatesErr()
    {
        var result = Result<int>.Create.Ok(2);
        var error = new Exception("too small");

        var output = result.Bind(x => x > 10
            ? Result<int>.Create.Ok(x)
            : Result<int>.Create.Err(error));

        Assert.True(output.IsErr);
        Assert.Same(error, output.Error);
    }

    [Fact]
    public void GivenChainOfBindsWhenAllOkReturnFinalResult()
    {
        var result = Result<int>.Create.Ok(2);

        var output = result
            .Bind(x => Result<int>.Create.Ok(x + 2))
            .Bind(x => Result<int>.Create.Ok(x * 3))
            .Bind(x => Result<int>.Create.Ok(x - 2));

        Assert.True(output.IsOk);
        Assert.Equal(10, output.Value); // (2 + 2) * 3 - 2 = 10
    }

    [Fact]
    public void GivenChainOfBindsWhenIntermediateErrStopsPropagation()
    {
        var result = Result<int>.Create.Ok(2);
        var error = new Exception("intermediate error");

        var output = result
            .Bind(x => Result<int>.Create.Ok(x + 2))
            .Bind(_ => Result<int>.Create.Err(error))
            .Bind(x => Result<int>.Create.Ok(x * 10));

        Assert.True(output.IsErr);
        Assert.Same(error, output.Error);
    }

    [Fact]
    public async Task GivenResultOkWhenBindAsyncWithCancellationTokenPassesToken()
    {
        var cts = new CancellationTokenSource();
        var result = Result<int>.Create.Ok(100);
        var tokenPassed = false;

        var output = await result.Bind((x, ct) =>
        {
            tokenPassed = ct == cts.Token;
            return Task.FromResult(Result<string>.Create.Ok(x.ToString()));
        }, cts.Token);

        Assert.True(tokenPassed);
        Assert.True(output.IsOk);
    }

    [Fact]
    public void GivenResultOkWhenMapReturnsTransformedValue()
    {
        var result = Result<int>.Create.Ok(10);

        var output = result.Map(x => x * 2);

        Assert.True(output.IsOk);
        Assert.Equal(20, output.Value);
    }

    [Fact]
    public void GivenResultErrWhenMapPropagatesError()
    {
        var error = new Exception("error");
        var result = Result<int>.Create.Err(error);

        var output = result.Map(x => x * 2);

        Assert.True(output.IsErr);
        Assert.Same(error, output.Error);
    }

    [Fact]
    public void GivenNullSelectorWhenMapThrowsArgumentNullException()
    {
        var result = Result<int>.Create.Ok(1);

        Assert.Throws<ArgumentNullException>(() => result.Map((Func<int, string>)null!));
    }

    [Fact]
    public async Task GivenResultOkWhenMapAsyncReturnsTransformedValue()
    {
        var result = Result<int>.Create.Ok(7);

        var output = await result.Map((x, ct) => Task.FromResult(x.ToString()));

        Assert.True(output.IsOk);
        Assert.Equal("7", output.Value);
    }

    [Fact]
    public async Task GivenResultErrWhenMapAsyncPropagatesError()
    {
        var error = new Exception("fail");
        var result = Result<int>.Create.Err(error);

        var output = await result.Map((x, ct) => Task.FromResult(x.ToString()));

        Assert.True(output.IsErr);
        Assert.Same(error, output.Error);
    }

    [Fact]
    public void GivenChainOfMapsWhenAllOkReturnFinalResult()
    {
        var result = Result<int>.Create.Ok(10);

        var output = result
            .Map(x => x * 2)
            .Map(x => x + 5)
            .Map(x => $"Final: {x}");

        Assert.True(output.IsOk);
        Assert.Equal("Final: 25", output.Value);
    }

    [Fact]
    public void GivenResultOkWhenMapThenBindReturnCorrectResult()
    {
        var result = Result<int>.Create.Ok(3);

        var output = result
            .Map(x => x * 3)
            .Bind(x => Result<string>.Create.Ok($"Value: {x}"));

        Assert.True(output.IsOk);
        Assert.Equal("Value: 9", output.Value);
    }

    [Fact]
    public async Task GivenResultOkWhenMapAsyncWithCancellationTokenPassesToken()
    {
        var cts = new CancellationTokenSource();
        var result = Result<int>.Create.Ok(50);
        var tokenPassed = false;

        var output = await result.Map((x, ct) =>
        {
            tokenPassed = ct == cts.Token;
            return Task.FromResult(x.ToString());
        }, cts.Token);

        Assert.True(tokenPassed);
        Assert.True(output.IsOk);
    }
}
