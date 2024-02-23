namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Cast")]
public class MaybeCastShould
{
    [Fact]
    public void GivenObjectWithStringApplyCastToStringReturnMaybeOfString()
    {
        object @object = "test";

        var result = @object.Cast<string>();

        Assert.True(result.HasValue);
        Assert.Equal(@object, result.Value);
    }

    [Fact]
    public void GivenObjectWithStringApplyCastToStringThrowInvalidcastexception()
    {
        object @object = "test";

        Assert.Throws<InvalidCastException>(() => @object.Cast<int>());
    }

    [Fact]
    public void GivenObjectWithStringApplyTrycastToStringReturnMaybeOfString()
    {
        object @object = "test";

        var result = @object.TryCast<string>();

        Assert.True(result.HasValue);
        Assert.Equal(@object, result.Value);
    }

    [Fact]
    public void GivenObjectWithNullApplyTrycastToStringReturnMaybeNoneOfString()
    {
        object @object = null;

        var result = @object.TryCast<string>();

        Assert.False(result.HasValue);
        Assert.Equal(@object, Maybe<string>.None.Value);
    }

    [Fact]
    public void GivenObjectOfIntApplyTrycastToMaybeOfStringReturnMaybeNoneOfString()
    {
        object @object = 1;

        var result = @object.TryCast<string>();

        Assert.False(result.HasValue);
        Assert.Equal(result, Maybe<string>.None);
    }
}