namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Bind")]
public class MaybeBindShould
{
    [Fact]
    public void Given_maybe_of_string_when_apply_bind_return_expected_maybe()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = @object.Bind(e => $"{e}-1");

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Given_maybe_of_int_when_apply_chain_of_bind_return_expected_maybe()
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
    public void Given_maybe_of_string_when_apply_bind_return_new_maybe_of_object()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = @object.Bind(e => new Dummy { Value = $"{e}-1" });

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value.Value);
    }

    [Fact]
    public void Given_maybe_of_string_when_apply_bind_to_maybe_of_none_return_maybe_none_of_object()
    {
        Maybe<string> @object = null;

        var result = @object.Bind(e => new Dummy { Value = $"{e}-1" });

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<Dummy>.None, result);
    }

    [Fact]
    public void Given_maybe_of_string_when_apply_bind_to_maybe_of_none_return_maybe_none_of_string()
    {
        Maybe<string> @object = null;

        var result = @object.Bind(e => $"{e}-1");

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public async Task Given_maybe_of_string_when_apply_bind_from_task_return_expected_maybe()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task Given_maybe_of_string_when_apply_bind_from_task_return_maybe_none_of_string()
    {
        Maybe<string> @object = null;

        var result = await @object.Bind((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public async Task Given_maybe_of_string_when_apply_bind_from_task_return_expected_maybe_of_string()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult(new Dummy { Value = $"{e}-1" }), CancellationToken.None);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value.Value);
    }

    [Fact]
    public async Task Given_maybe_of_string_when_apply_bind_from_task_return_maybe_none_of_object()
    {
        Maybe<string> @object = null;

        var result = await @object.Bind((e, ct) => Task.FromResult(new Dummy { Value = $"{e}-1" }), CancellationToken.None);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<Dummy>.None, result);
    }

    [Fact]
    public async Task Given_maybe_of_string_when_apply_bind_from_task_return_expected_maybe_of_none_object()
    {
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult(default(Dummy)), CancellationToken.None);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<Dummy>.None, result);
    }

    public class Dummy
    {
        public string Value { get; set; }
    }
}