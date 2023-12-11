namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Cast")]
public class MaybeCastShould
{
    [Fact]
    public void Given_object_with_string_apply_cast_to_string_return_maybe_of_string()
    {
        object @object = "test";

        var result = @object.Cast<string>();

        Assert.True(result.HasValue);
        Assert.Equal(@object, result.Value);
    }

    [Fact]
    public void Given_object_with_string_apply_cast_to_string_throw_invalidcastexception()
    {
        object @object = "test";

        Assert.Throws<InvalidCastException>(() => @object.Cast<int>());
    }

    [Fact]
    public void Given_object_with_string_apply_trycast_to_string_return_maybe_of_string()
    {
        object @object = "test";

        var result = @object.TryCast<string>();

        Assert.True(result.HasValue);
        Assert.Equal(@object, result.Value);
    }

    [Fact]
    public void Given_object_with_null_apply_trycast_to_string_return_maybe_none_of_string()
    {
        object @object = null;

        var result = @object.TryCast<string>();

        Assert.False(result.HasValue);
        Assert.Equal(@object, Maybe<string>.None.Value);
    }

    [Fact]
    public void Given_object_of_int_apply_trycast_to_maybe_of_string_return_maybe_none_of_string()
    {
        object @object = 1;

        var result = @object.TryCast<string>();

        Assert.False(result.HasValue);
        Assert.Equal(result, Maybe<string>.None);
    }
}