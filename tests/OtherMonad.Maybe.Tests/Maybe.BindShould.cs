namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Bind")]
public class MaybeBindShould
{
    [Fact]
    public void GivenMaybeOfStringWhenApplyBindReturnExpectedMaybe()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = @object.Bind(e => $"{e}-1");

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeOfIntWhenApplyChainOfBindReturnExpectedMaybe()
    {
        var expected = 10;
        Maybe<int> @object = 2;

        var result = @object.Bind(e => e + 2)
            .Bind(e => e + 2)
            .Bind(e => e + 2)
            .Bind(e => e + 2);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyBindReturnNewMaybeOfObject()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = @object.Bind(e => new Dummy { Value = $"{e}-1" });

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyBindToMaybeOfNoneReturnMaybeNoneOfObject()
    {
        Maybe<string> @object = null;

        var result = @object.Bind(e => new Dummy { Value = $"{e}-1" });

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<Dummy>.None, result);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyBindToMaybeOfNoneReturnMaybeNoneOfString()
    {
        Maybe<string> @object = null;

        var result = @object.Bind(e => $"{e}-1");

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBindFromTaskReturnExpectedMaybe()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBindFromTaskReturnMaybeNoneOfString()
    {
        Maybe<string> @object = null;

        var result = await @object.Bind((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBindFromTaskReturnExpectedMaybeOfString()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult(new Dummy { Value = $"{e}-1" }), CancellationToken.None);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value.Value);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBindFromTaskReturnMaybeNoneOfObject()
    {
        Maybe<string> @object = null;

        var result = await @object.Bind((e, ct) => Task.FromResult(new Dummy { Value = $"{e}-1" }), CancellationToken.None);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<Dummy>.None, result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBindFromTaskReturnExpectedMaybeOfNoneObject()
    {
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult(default(Dummy)), CancellationToken.None);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<Dummy>.None, result);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyBinddeferredReturnExpectedMaybe()
    {
        var expected = "test-1-1";
        Maybe<string> @object = "test";

        var deferred = @object.BindDefer(e => $"{e}-1")
            .BindDefer(e => $"{e}-1");

        var result = deferred();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBinddeferredFromTaskReturnExpectedMaybeOfString()
    {
        var expected = "test-1-1";
        Maybe<string> @object = "test";

        var deferred = @object.BindDefer((e, ct) => Task.FromResult(new Dummy { Value = $"{e}-1" }), CancellationToken.None)
            .BindDefer((e, ct) => Task.FromResult(new Dummy { Value = $"{e.Value}-1" }), CancellationToken.None);

        var result = await deferred();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value.Value);
    }

    public class Dummy
    {
        public string Value { get; set; }
    }
}