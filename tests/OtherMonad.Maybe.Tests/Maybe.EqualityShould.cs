namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Equality")]
public class MaybeEqualityShould
{
    [Fact]
    public void GivenObjectToApplyEqualityReturnEquals()
    {
        var result = (object)Maybe<int>.None;

        Assert.True(Maybe<int>.None.Equals(result));
    }

    [Fact]
    public void GivenObjectToApplyEqualityReturnNoEquals()
    {
        var result = (object)Maybe<string>.None;

        Assert.False(Maybe<int>.None.Equals(result));
    }

    [Fact]
    public void GivenTwoMaybesToApplyEqualityReturnEquals()
    {
        var result = Maybe<int>.None;

        Assert.Equal(Maybe<int>.None, result);
    }

    [Fact]
    public void GivenTwoMaybesToApplyEqualityReturnNoEquals()
    {
        Maybe<int> result = 10;

        Assert.NotEqual(Maybe<int>.None, result);
    }

    [Fact]
    public void GivenTwoMaybesToApplyEqualityOperatorReturnEquals()
    {
        Maybe<int> result = 0;

        Assert.True(Maybe<int>.None == result);
    }

    [Fact]
    public void GivenTwoMaybesToApplyEqualityOperatorReturnNoEquals()
    {
        Maybe<int> result = 10;

        Assert.True(Maybe<int>.None != result);
    }
}