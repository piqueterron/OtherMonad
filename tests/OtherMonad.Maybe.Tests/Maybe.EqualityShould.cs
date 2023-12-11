namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Equality")]
public class MaybeEqualityShould
{
    [Fact]
    public void Given_object_to_apply_equality_return_equals()
    {
        var result = (object)Maybe<int>.None;

        Assert.True(Maybe<int>.None.Equals(result));
    }

    [Fact]
    public void Given_object_to_apply_equality_return_no_equals()
    {
        var result = (object)Maybe<string>.None;

        Assert.False(Maybe<int>.None.Equals(result));
    }

    [Fact]
    public void Given_two_maybes_to_apply_equality_return_equals()
    {
        var result = Maybe<int>.None;

        Assert.Equal(Maybe<int>.None, result);
    }

    [Fact]
    public void Given_two_maybes_to_apply_equality_return_no_equals()
    {
        Maybe<int> result = 10;

        Assert.NotEqual(Maybe<int>.None, result);
    }

    [Fact]
    public void Given_two_maybes_to_apply_equality_operator_return_equals()
    {
        Maybe<int> result = 0;

        Assert.True(Maybe<int>.None == result);
    }

    [Fact]
    public void Given_two_maybes_to_apply_equality_operator_return_no_equals()
    {
        Maybe<int> result = 10;

        Assert.True(Maybe<int>.None != result);
    }
}