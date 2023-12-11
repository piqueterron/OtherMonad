namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "OrElse")]
public class MaybeIfShould
{
    [Fact]
    public void Given_maybe_of_none_when_apply_orelse_return_default_value()
    {
        var expected = "default";
        Maybe<string> @object = "test";

        var result = @object.Bind<string, string>(e => null)
            .OrElse(expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Given_maybe_of_string_when_apply_orelse_return_expected_maybe_of_string()
    {
        Maybe<string> @object = "test";

        var result = @object.Bind(e => "test").OrElse("default");

        Assert.True(result.HasValue);
        Assert.Equal(@object, result);
    }

    [Fact]
    public async Task Given_maybe_of_none_when_apply_orelse_from_task_return_default_value()
    {
        Maybe<string> @object = null;
        var expected = "default";

        var result = await @object.Bind((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None)
            .OrElse(expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task Given_maybe_of_none_when_apply_orelse_from_task_return_maybe_of_string()
    {
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None)
            .OrElse("default");

        Assert.True(result.HasValue);
        Assert.Equal("test-1", result.Value);
    }

    [Fact]
    public async Task Given_maybe_of_none_when_apply_orelse_from_valuetask_return_default_value()
    {
        Maybe<string> @object = null;
        var expected = "default";

        var result = await @object.Bind((e, ct) => ValueTask.FromResult($"{e}-1"), CancellationToken.None)
            .OrElse(expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task Given_maybe_of_none_when_apply_orelse_from_valuetask_return_maybe_of_string()
    {
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => ValueTask.FromResult($"{e}-1"), CancellationToken.None)
            .OrElse("default");

        Assert.True(result.HasValue);
        Assert.Equal("test-1", result.Value);
    }
}