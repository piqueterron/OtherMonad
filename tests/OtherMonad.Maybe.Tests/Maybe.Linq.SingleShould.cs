namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Linq.Single")]
public class MaybeLinqSingleShould
{
    [Fact]
    public void Given_list_of_ints_when_apply_single_with_filter_return_value()
    {
        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            2.Wrap(),
        };

        var result = list.Single(x => x == 2);

        Assert.True(result.HasValue);
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_single_with_filter_throw_invalidoperationexception()
    {
        var list = new List<Maybe<int>>
        {
            2.Wrap(),
            2.Wrap(),
        };

        Assert.Throws<InvalidOperationException>(() => list.Single(x => x == 2));
    }

    [Fact]
    public void Given_list_of_none_when_apply_single_with_filter_throw_invalidoperationexception()
    {
        var list = new List<Maybe<int>>
        {
            Maybe<int>.None
        };

        Assert.Throws<InvalidOperationException>(() => list.Single(x => x == 2));
    }

    [Fact]
    public void Given_list_of_ints_when_apply_single_return_value()
    {
        var list = new List<Maybe<int>>
        {
            1.Wrap(),
        };

        var result = list.Single();

        Assert.True(result.HasValue);
        Assert.Equal(1, result.Value);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_single_throw_invalidoperationexception()
    {
        var list = new List<Maybe<int>>
        {
            2.Wrap(),
            2.Wrap(),
        };

        Assert.Throws<InvalidOperationException>(() => list.Single());
    }

    [Fact]
    public void Given_list_of_none_when_apply_single_throw_invalidoperationexception()
    {
        var list = new List<Maybe<int>>
        {
            Maybe<int>.None
        };

        Assert.Throws<InvalidOperationException>(() => list.Single());
    }
}