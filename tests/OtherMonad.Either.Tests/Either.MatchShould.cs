namespace Monads.Tests.Either;

using OtherMonad;

[Trait("Either", "Match")]
public class EitherMatchShould
{
    [Fact]
    public void Given_either_task_when_apply_match_with_left_null_condition_throw_argumentnullexception()
    {
        Either<string, Exception> either = "test";

        Assert.Throws<ArgumentNullException>(() =>
        {
            either.Match(null, c => "");
        });
    }

    [Fact]
    public void Given_either_task_when_apply_match_with_right_null_condition_throw_argumentnullexception()
    {
        Either<string, Exception> either = "test";

        Assert.Throws<ArgumentNullException>(() =>
        {
            either.Match(c => "", null);
        });
    }

    [Fact]
    public void Given_either_task_when_apply_match_with_successs_state_execute_left_condition()
    {
        Either<string, Exception> either = "test";

        var result = either.Match(c => "success", c => "fail");

        Assert.Equal("success", result);
    }

    [Fact]
    public void Given_either_task_when_apply_match_with_error_state_execute_right_condition()
    {
        Either<string, Exception> either = new Exception();

        var result = either.Match(c => "success", c => "fail");

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task Given_either_taskasync_when_apply_match_with_left_null_condition_throw_argumentnullexception()
    {
        Either<string, Exception> either = "test";

        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () =>
        {
            await either.Match(null, (c, ct) => Task.FromResult(""), CancellationToken.None);
        });
    }

    [Fact]
    public async Task Given_either_taskasync_when_apply_match_with_right_null_condition_throw_argumentnullexception()
    {
        Either<string, Exception> either = "test";

        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () =>
        {
            await either.Match((c, ct) => Task.FromResult(""), null, CancellationToken.None);
        });
    }

    [Fact]
    public async Task Given_either_taskasync_when_apply_match_with_successs_state_execute_left_condition()
    {
        Either<string, Exception> either = "test";

        var result = await either.Match((c, ct) => Task.FromResult("success"), (c, ct) => Task.FromResult("fail"), CancellationToken.None);

        Assert.Equal("success", result);
    }

    [Fact]
    public async Task Given_either_taskasync_when_apply_match_with_error_state_execute_right_condition()
    {
        Either<string, Exception> either = new Exception();

        var result = await either.Match((c, ct) => Task.FromResult("success"), (c, ct) => Task.FromResult("fail"), CancellationToken.None);

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task Given_either_taskasync_when_apply_trymatch_with_left_condition_null_return_default()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch(null, (c, ct) => Task.FromResult(""), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task Given_either_taskasync_when_apply_trymatch_with_right_condition_null_return_default()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch((c, ct) => Task.FromResult(""), null, "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task Given_either_taskasync_when_apply_trymatch_left_condition_return_left_value()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch((c, ct) => Task.FromResult("success"), (c, ct) => Task.FromResult("fail"), "default", CancellationToken.None);

        Assert.Equal("success", result);
    }

    [Fact]
    public async Task Given_either_taskasync_when_apply_trymatch_right_condition_return_right_value()
    {
        Either<string, Exception> either = new Exception();

        var result = await either.TryMatch((c, ct) => Task.FromResult("success"), (c, ct) => Task.FromResult("fail"), "default", CancellationToken.None);

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task Given_either_taskasync_when_apply_trymatch_left_condition_throw_exception_return_default()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch((c, ct) => throw new Exception(), (c, ct) => Task.FromResult("fail"), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task Given_either_taskasync_when_apply_trymatch_right_condition_throw_exception_return_default()
    {
        Either<string, Exception> either = new Exception();

        var result = await either.TryMatch((c, ct) => Task.FromResult("success"), (c, ct) => throw new Exception(), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task Given_either_valuetaskasync_when_apply_trymatch_with_left_condition_null_return_default()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch(null, (c, ct) => ValueTask.FromResult(""), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task Given_either_valuetaskasync_when_apply_trymatch_with_right_condition_null_return_default()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch((c, ct) => ValueTask.FromResult(""), null, "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task Given_either_valuetaskasync_when_apply_trymatch_left_condition_return_left_value()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch((c, ct) => ValueTask.FromResult("success"), (c, ct) => ValueTask.FromResult("fail"), "default", CancellationToken.None);

        Assert.Equal("success", result);
    }

    [Fact]
    public async Task Given_either_valuetaskasync_when_apply_trymatch_right_condition_return_right_value()
    {
        Either<string, Exception> either = new Exception();

        var result = await either.TryMatch((c, ct) => ValueTask.FromResult("success"), (c, ct) => ValueTask.FromResult("fail"), "default", CancellationToken.None);

        Assert.Equal("fail", result);
    }

    [Fact]
    public async Task Given_either_valuetaskasync_when_apply_trymatch_left_condition_throw_exception_return_default()
    {
        Either<string, Exception> either = "test";

        var result = await either.TryMatch((c, ct) => throw new Exception(), (c, ct) => ValueTask.FromResult("fail"), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public async Task Given_either_valuetaskasync_when_apply_trymatch_right_condition_throw_exception_return_default()
    {
        Either<string, Exception> either = new Exception();

        var result = await either.TryMatch((c, ct) => ValueTask.FromResult("success"), (c, ct) => throw new Exception(), "default", CancellationToken.None);

        Assert.Equal("default", result);
    }

    [Fact]
    public void Given_either_when_apply_trymatch_with_right_and_left_condition_null_return_default()
    {
        Either<string, Exception> either = "test";

        var result = either.TryMatch(null, null, true);

        Assert.True(result);
    }

    [Fact]
    public void Given_either_when_apply_trymatch_with_left_condition_null_return_default()
    {
        Either<string, Exception> either = "test";

        var result = either.TryMatch(null, c => false, true);

        Assert.True(result);
    }

    [Fact]
    public void Given_either_when_apply_trymatch_with_right_condition_null_return_default()
    {
        Either<string, Exception> either = "test";

        var result = either.TryMatch(c => false, null, true);

        Assert.True(result);
    }

    [Fact]
    public void Given_either_when_apply_trymatch_left_condition_throw_exception_return_default()
    {
        Either<string, Exception> either = "test";

        var result = either.TryMatch(c => throw new Exception(), c => false, true);

        Assert.True(result);
    }

    [Fact]
    public void Given_either_when_apply_trymatch_right_condition_throw_exception_return_default()
    {
        Either<string, Exception> either = new Exception();

        var result = either.TryMatch(c => false, c => throw new Exception(), true);

        Assert.True(result);
    }

    [Fact]
    public async Task Given_either_valuetask_when_apply_trymatch_with_left_condition_null_throw_argumentnullexception()
    {
        Either<string, Exception> either = "test";

        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () =>
        {
            await either.Match(null, (c, ct) => ValueTask.FromResult(false), CancellationToken.None);
        });
    }

    [Fact]
    public async Task Given_either_valuetask_when_apply_match_with_right_condition_null_throw_argumentnullexception()
    {
        Either<string, Exception> either = "test";

        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () =>
        {
            await either.Match((c, ct) => ValueTask.FromResult(""), null, CancellationToken.None);
        });
    }

    [Fact]
    public async Task Given_either_valuetask_when_apply_match_with_success_state_execute_left_condition()
    {
        Either<string, Exception> either = "test";

        var result = await either.Match((c, ct) => ValueTask.FromResult("success"), (c, ct) => ValueTask.FromResult("fail"), CancellationToken.None);

        Assert.Equal("success", result);
    }

    [Fact]
    public async Task Given_either_valuetask_when_apply_match_with_error_state_execute_right_condition()
    {
        Either<string, Exception> either = new Exception();

        var result = await either.Match((c, ct) => ValueTask.FromResult("success"), (c, ct) => ValueTask.FromResult("fail"), CancellationToken.None);

        Assert.Equal("fail", result);
    }
}