namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Wrap")]
public class MaybeWrapShould
{
    [Fact]
    public void Given_string_when_apply_wrap_return_maybe_of_string()
    {
        var @object = "test";

        var result = @object.Wrap();

        Assert.True(result.HasValue);
        Assert.Equal(@object, result.Value);
    }

    [Fact]
    public void Given_object_null_when_apply_wrap_return_maybe_none_of_object()
    {
        object @object = null;

        var result = @object.Wrap();

        Assert.False(result.HasValue);
        Assert.Equal(@object, result.Value);
    }

    [Fact]
    public void Given_maybe_of_string_when_apply_unwrap_return_value()
    {
        Maybe<string> maybe = "test";

        var result = maybe.Unwrap();

        Assert.Equal(result, maybe.Value);
    }

    [Fact]
    public void Given_maybe_of_string_when_apply_unwrap_return_null()
    {
        var maybe = Maybe<string>.None;

        var result = maybe.Unwrap();

        Assert.Equal(result, maybe.Value);
    }

    [Fact]
    public void Given_maybe_of_string_when_apply_unwrap_default_return_value()
    {
        Maybe<string> maybe = "test";

        var result = maybe.Unwrap("default");

        Assert.Equal("test", result);
    }

    [Fact]
    public void Given_maybe_of_string_when_apply_unwrap_default_dont_has_value_return_default_value()
    {
        var expected = "default";

        var maybe = Maybe<string>.None;

        var result = maybe.Unwrap(expected);

        Assert.Equal(expected, result);
    }
}