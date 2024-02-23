namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Wrap")]
public class MaybeWrapShould
{
    [Fact]
    public void GivenStringWhenApplyWrapReturnMaybeOfString()
    {
        var @object = "test";

        var result = @object.Wrap();

        Assert.True(result.HasValue);
        Assert.Equal(@object, result.Value);
    }

    [Fact]
    public void GivenObjectNullWhenApplyWrapReturnMaybeNoneOfObject()
    {
        object @object = null;

        var result = @object.Wrap();

        Assert.False(result.HasValue);
        Assert.Equal(@object, result.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyUnwrapReturnValue()
    {
        Maybe<string> maybe = "test";

        var result = maybe.Unwrap();

        Assert.Equal(result, maybe.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyUnwrapReturnNull()
    {
        var maybe = Maybe<string>.None;

        var result = maybe.Unwrap();

        Assert.Equal(result, maybe.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyUnwrapDefaultReturnValue()
    {
        Maybe<string> maybe = "test";

        var result = maybe.Unwrap("default");

        Assert.Equal("test", result);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyUnwrapDefaultDontHasValueReturnDefaultValue()
    {
        var expected = "default";

        var maybe = Maybe<string>.None;

        var result = maybe.Unwrap(expected);

        Assert.Equal(expected, result);
    }
}