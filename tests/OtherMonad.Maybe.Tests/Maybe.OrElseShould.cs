namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "OrElse")]
public class MaybeIfShould
{
    [Fact]
    public void GivenMaybeOfNoneWhenApplyOrelseReturnDefaultValue()
    {
        var expected = "default";
        Maybe<string> @object = "test";

        var result = @object.Bind<string, string>(e => null)
            .OrElse(expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyOrelseReturnExpectedMaybeOfString()
    {
        Maybe<string> @object = "test";

        var result = @object.Bind(e => "test").OrElse("default");

        Assert.True(result.HasValue);
        Assert.Equal(@object, result);
    }

    [Fact]
    public async Task GivenMaybeOfNoneWhenApplyOrelseFromTaskReturnDefaultValue()
    {
        Maybe<string> @object = null;
        var expected = "default";

        var result = await @object.Bind((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None)
            .OrElse(expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task GivenMaybeOfNoneWhenApplyOrelseFromTaskReturnMaybeOfString()
    {
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None)
            .OrElse("default");

        Assert.True(result.HasValue);
        Assert.Equal("test-1", result.Value);
    }

    [Fact]
    public void GivenMaybeOfNoneWhenApplyOrelsedeferredReturnDefaultValue()
    {
        var expected = "default";
        Maybe<string> @object = "test";

        var deferred = @object.BindDefer<string, string>(e => null)
            .OrElseDefer(expected);

        var result = deferred();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyOrelsedeferredReturnExpectedMaybeOfString()
    {
        Maybe<string> @object = "test";

        var deferred = @object.BindDefer(e => "test").OrElseDefer("default");
        var result = deferred();

        Assert.True(result.HasValue);
        Assert.Equal(@object, result);
    }

    [Fact]
    public async Task GivenMaybeOfNoneWhenApplyOrelsedeferredFromTaskReturnDefaultValue()
    {
        Maybe<string> @object = null;
        var expected = "default";

        var deferred = @object.BindDefer((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None)
            .OrElseDefer(expected);

        var result = await deferred();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task GivenMaybeOfNoneWhenApplyOrelsedeferredFromTaskReturnMaybeOfString()
    {
        Maybe<string> @object = "test";

        var deferred = @object.BindDefer((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None)
            .OrElseDefer("default");

        var result = await deferred();

        Assert.True(result.HasValue);
        Assert.Equal("test-1", result.Value);
    }
}