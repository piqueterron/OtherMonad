namespace OtherMonad.Tests;

[Trait("Result", "OrElse/GetValueOrDefault")]
public class ResultOrElseShould
{
    [Fact]
    public void GivenResultOkWhenOrElseReturnSelf()
    {
        var result = Result<string>.Create.Ok("ok");
        var fallback = Result<string>.Create.Ok("fallback");

        var output = result.OrElse(fallback);

        Assert.True(output.IsOk);
        Assert.Equal("ok", output.Value);
    }

    [Fact]
    public void GivenResultErrWhenOrElseReturnFallback()
    {
        var result = Result<string>.Create.Err(new Exception("error"));
        var fallback = Result<string>.Create.Ok("fallback");

        var output = result.OrElse(fallback);

        Assert.True(output.IsOk);
        Assert.Equal("fallback", output.Value);
    }

    [Fact]
    public void GivenResultErrWhenOrElseWithErrFallbackReturnErrFallback()
    {
        var result = Result<string>.Create.Err(new Exception("first"));
        var fallback = Result<string>.Create.Err(new Exception("second"));

        var output = result.OrElse(fallback);

        Assert.True(output.IsErr);
        Assert.Equal("second", output.Error.Message);
    }

    [Fact]
    public void GivenChainOfOrElseWhenFirstOkReturnFirst()
    {
        var a = Result<int>.Create.Ok(1);
        var b = Result<int>.Create.Ok(2);
        var c = Result<int>.Create.Ok(3);

        var output = a.OrElse(b).OrElse(c);

        Assert.True(output.IsOk);
        Assert.Equal(1, output.Value);
    }

    [Fact]
    public void GivenChainOfOrElseWhenAllErrReturnLastErr()
    {
        var a = Result<int>.Create.Err(new Exception("e1"));
        var b = Result<int>.Create.Err(new Exception("e2"));
        var c = Result<int>.Create.Err(new Exception("e3"));

        var output = a.OrElse(b).OrElse(c);

        Assert.True(output.IsErr);
        Assert.Equal("e3", output.Error.Message);
    }

    [Fact]
    public async Task GivenResultOkWhenOrElseAsyncReturnSelf()
    {
        var result = Result<string>.Create.Ok("ok");

        var output = await result.OrElse(
            ct => Task.FromResult(Result<string>.Create.Ok("fallback")),
            TestContext.Current.CancellationToken);

        Assert.True(output.IsOk);
        Assert.Equal("ok", output.Value);
    }

    [Fact]
    public async Task GivenResultErrWhenOrElseAsyncReturnFallback()
    {
        var result = Result<string>.Create.Err(new Exception("error"));

        var output = await result.OrElse(
            ct => Task.FromResult(Result<string>.Create.Ok("fallback")),
            TestContext.Current.CancellationToken);

        Assert.True(output.IsOk);
        Assert.Equal("fallback", output.Value);
    }

    [Fact]
    public async Task GivenNullFallbackFactoryWhenOrElseAsyncThrowsArgumentNullException()
    {
        var result = Result<string>.Create.Err(new Exception("error"));

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            result.OrElse(null!, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task GivenResultOkWhenOrElseAsyncFactoryNotCalled()
    {
        var result = Result<string>.Create.Ok("ok");
        var factoryCalled = false;

        var output = await result.OrElse(
            ct =>
            {
                factoryCalled = true;
                return Task.FromResult(Result<string>.Create.Ok("fallback"));
            },
            TestContext.Current.CancellationToken);

        Assert.False(factoryCalled);
        Assert.True(output.IsOk);
        Assert.Equal("ok", output.Value);
    }

    [Fact]
    public async Task GivenResultErrWhenOrElseAsyncWithCancellationTokenPassesToken()
    {
        var cts = new CancellationTokenSource();
        var result = Result<int>.Create.Err(new Exception("error"));
        var tokenPassed = false;

        await result.OrElse(
            ct =>
            {
                tokenPassed = ct == cts.Token;
                return Task.FromResult(Result<int>.Create.Ok(0));
            },
            cts.Token);

        Assert.True(tokenPassed);
    }

    // ── GetValueOrDefault ────────────────────────────────────────────────────

    [Fact]
    public void GivenResultOkWhenGetValueOrDefaultReturnValue()
    {
        var result = Result<int>.Create.Ok(42);

        var output = result.GetValueOrDefault(0);

        Assert.Equal(42, output);
    }

    [Fact]
    public void GivenResultErrWhenGetValueOrDefaultReturnDefault()
    {
        var result = Result<int>.Create.Err(new Exception("error"));

        var output = result.GetValueOrDefault(99);

        Assert.Equal(99, output);
    }

    [Fact]
    public void GivenResultErrWhenGetValueOrDefaultWithNoArgReturnTypeDefault()
    {
        var result = Result<int>.Create.Err(new Exception("error"));

        var output = result.GetValueOrDefault();

        Assert.Equal(0, output);
    }

    [Fact]
    public void GivenResultOkWithStringWhenGetValueOrDefaultReturnValue()
    {
        var result = Result<string>.Create.Ok("hello");

        var output = result.GetValueOrDefault("default");

        Assert.Equal("hello", output);
    }

    [Fact]
    public void GivenResultErrWithStringWhenGetValueOrDefaultReturnDefault()
    {
        var result = Result<string>.Create.Err(new Exception("error"));

        var output = result.GetValueOrDefault("fallback");

        Assert.Equal("fallback", output);
    }

    [Fact]
    public void GivenComplexChainWhenBindMapOrElseReturnCorrectResult()
    {
        var result = Result<int>.Create.Ok(5);

        var output = result
            .Bind(x => x > 3 ? Result<int>.Create.Ok(x * 2) : Result<int>.Create.Err(new Exception("too small")))
            .Map(x => x + 10)
            .OrElse(Result<int>.Create.Ok(0));

        Assert.True(output.IsOk);
        Assert.Equal(20, output.Value); // (5 * 2) + 10 = 20
    }

    [Fact]
    public void GivenComplexChainWhenBindFailsOrElseProvidesFallback()
    {
        var result = Result<int>.Create.Ok(2);

        var output = result
            .Bind(x => x > 3 ? Result<int>.Create.Ok(x * 2) : Result<int>.Create.Err(new Exception("too small")))
            .Map(x => x + 10)
            .OrElse(Result<int>.Create.Ok(999));

        Assert.True(output.IsOk);
        Assert.Equal(999, output.Value);
    }
}
