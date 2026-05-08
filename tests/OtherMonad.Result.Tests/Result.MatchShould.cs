namespace OtherMonad.Tests;

[Trait("Result", "Match")]
public class ResultMatchShould
{
    [Fact]
    public void GivenResultOkWhenMatchExecutesOnOk()
    {
        var result = Result<string>.Create.Ok("test");

        var output = result.Match(
            onErr: ex => "error",
            onOk:  v  => "success");

        Assert.Equal("success", output);
    }

    [Fact]
    public void GivenResultErrWhenMatchExecutesOnErr()
    {
        var result = Result<string>.Create.Err(new Exception());

        var output = result.Match(
            onErr: ex => "error",
            onOk:  v  => "success");

        Assert.Equal("error", output);
    }

    [Fact]
    public void GivenNullOnErrWhenMatchThrowsArgumentNullException()
    {
        var result = Result<string>.Create.Ok("test");

        Assert.Throws<ArgumentNullException>(() =>
            result.Match(null!, v => ""));
    }

    [Fact]
    public void GivenNullOnOkWhenMatchThrowsArgumentNullException()
    {
        var result = Result<string>.Create.Ok("test");

        Assert.Throws<ArgumentNullException>(() =>
            result.Match(ex => "", null!));
    }

    [Fact]
    public async Task GivenResultOkWhenMatchAsyncExecutesOnOk()
    {
        var result = Result<string>.Create.Ok("test");

        var output = await result.Match(
            onErr: (ex, ct) => Task.FromResult("error"),
            onOk:  (v,  ct) => Task.FromResult("success"),
            TestContext.Current.CancellationToken);

        Assert.Equal("success", output);
    }

    [Fact]
    public async Task GivenResultErrWhenMatchAsyncExecutesOnErr()
    {
        var result = Result<string>.Create.Err(new Exception());

        var output = await result.Match(
            onErr: (ex, ct) => Task.FromResult("error"),
            onOk:  (v,  ct) => Task.FromResult("success"),
            TestContext.Current.CancellationToken);

        Assert.Equal("error", output);
    }

    [Fact]
    public async Task GivenNullOnErrWhenMatchAsyncThrowsArgumentNullException()
    {
        var result = Result<string>.Create.Ok("test");

        await Assert.ThrowsAnyAsync<ArgumentNullException>(() =>
            result.Match(null!, (v, ct) => Task.FromResult(""), TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task GivenNullOnOkWhenMatchAsyncThrowsArgumentNullException()
    {
        var result = Result<string>.Create.Ok("test");

        await Assert.ThrowsAnyAsync<ArgumentNullException>(() =>
            result.Match((ex, ct) => Task.FromResult(""), null!, TestContext.Current.CancellationToken));
    }

    [Fact]
    public void GivenNullOnErrWhenTryMatchReturnsDefault()
    {
        var result = Result<string>.Create.Ok("test");

        var output = result.TryMatch(null!, v => "ok", "default");

        Assert.Equal("default", output);
    }

    [Fact]
    public void GivenNullOnOkWhenTryMatchReturnsDefault()
    {
        var result = Result<string>.Create.Ok("test");

        var output = result.TryMatch(ex => "err", null!, "default");

        Assert.Equal("default", output);
    }

    [Fact]
    public void GivenResultOkWhenTryMatchExecutesOnOk()
    {
        var result = Result<string>.Create.Ok("hello");

        var output = result.TryMatch(
            onErr: ex => "error",
            onOk:  v  => v.ToUpperInvariant(),
            @default: "default");

        Assert.Equal("HELLO", output);
    }

    [Fact]
    public void GivenResultErrWhenTryMatchExecutesOnErr()
    {
        var result = Result<string>.Create.Err(new Exception("failed"));

        var output = result.TryMatch(
            onErr: ex => ex.Message.ToUpperInvariant(),
            onOk:  v  => v,
            @default: "default");

        Assert.Equal("FAILED", output);
    }

    [Fact]
    public void GivenOnOkThrowsWhenTryMatchReturnsDefault()
    {
        var result = Result<string>.Create.Ok("test");

        var output = result.TryMatch(
            onErr: ex => "error",
            onOk:  v  => throw new Exception("boom"),
            @default: "default");

        Assert.Equal("default", output);
    }

    [Fact]
    public void GivenOnErrThrowsWhenTryMatchReturnsDefault()
    {
        var result = Result<string>.Create.Err(new Exception("err"));

        var output = result.TryMatch(
            onErr: ex => throw new Exception("boom"),
            onOk:  v  => "ok",
            @default: "default");

        Assert.Equal("default", output);
    }

    [Fact]
    public async Task GivenNullFunctionsWhenTryMatchAsyncReturnsDefault()
    {
        var result = Result<string>.Create.Ok("test");

        var output = await result.TryMatch(
            null!,
            null!,
            "fallback",
            TestContext.Current.CancellationToken);

        Assert.Equal("fallback", output);
    }

    [Fact]
    public async Task GivenResultOkWhenTryMatchAsyncExecutesOnOk()
    {
        var result = Result<string>.Create.Ok("test");

        var output = await result.TryMatch(
            onErr: (ex, ct) => Task.FromResult("error"),
            onOk:  (v,  ct) => Task.FromResult("success"),
            @default: "default",
            TestContext.Current.CancellationToken);

        Assert.Equal("success", output);
    }

    [Fact]
    public async Task GivenResultErrWhenTryMatchAsyncExecutesOnErr()
    {
        var result = Result<string>.Create.Err(new Exception());

        var output = await result.TryMatch(
            onErr: (ex, ct) => Task.FromResult("error"),
            onOk:  (v,  ct) => Task.FromResult("success"),
            @default: "default",
            TestContext.Current.CancellationToken);

        Assert.Equal("error", output);
    }

    [Fact]
    public async Task GivenOnOkThrowsWhenTryMatchAsyncReturnsDefault()
    {
        var result = Result<string>.Create.Ok("test");

        var output = await result.TryMatch(
            onErr: (ex, ct) => Task.FromResult("error"),
            onOk:  (v,  ct) => throw new InvalidOperationException("boom"),
            @default: "default",
            TestContext.Current.CancellationToken);

        Assert.Equal("default", output);
    }

    [Fact]
    public async Task GivenResultOkWhenMatchAsyncWithCancellationTokenPassesToken()
    {
        var cts = new CancellationTokenSource();
        var result = Result<int>.Create.Ok(42);
        var tokenPassed = false;

        await result.Match(
            onErr: (ex, ct) => Task.FromResult("error"),
            onOk:  (v,  ct) =>
            {
                tokenPassed = ct == cts.Token;
                return Task.FromResult("ok");
            },
            cts.Token);

        Assert.True(tokenPassed);
    }

    [Fact]
    public void GivenResultOkWhenMatchTransformsToComplexObjectReturnCorrectObject()
    {
        var result = Result<int>.Create.Ok(100);

        var output = result.Match(
            onErr: ex => new { Status = "Error",   Value = 0 },
            onOk:  v  => new { Status = "Success", Value = v });

        Assert.Equal("Success", output.Status);
        Assert.Equal(100, output.Value);
    }

    [Fact]
    public void GivenResultErrWhenMatchTransformsToComplexObjectReturnCorrectObject()
    {
        var result = Result<int>.Create.Err(new Exception("fail"));

        var output = result.Match(
            onErr: ex => new { Status = "Error",   Message = ex.Message },
            onOk:  v  => new { Status = "Success", Message = "OK" });

        Assert.Equal("Error", output.Status);
        Assert.Equal("fail", output.Message);
    }
}
