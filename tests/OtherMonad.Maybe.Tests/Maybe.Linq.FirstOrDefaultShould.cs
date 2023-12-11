namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Linq.FirstOrDefault")]
public class MaybeLinqFirstOrDefaultShould
{
    [Fact]
    public void Given_list_of_ints_when_apply_firstordefault_return_first_element()
    {
        var expected = 1;

        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            2.Wrap(),
            3.Wrap(),
        };

        var result = list.FirstOrDefault();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_firstordefault_return_none()
    {
        var list = new List<Maybe<int>>
        {
            Maybe<int>.None
        };

        var result = list.FirstOrDefault();

        Assert.False(result.HasValue);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_firstordefault_with_predicate_return_first_element()
    {
        var expected = 3;

        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            2.Wrap(),
            3.Wrap(),
        };

        var result = list.FirstOrDefault(x => x == 3);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_firstordefault_with_predicate_return_none()
    {
        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            2.Wrap(),
            3.Wrap(),
        };

        var result = list.FirstOrDefault(x => x == 5);

        Assert.False(result.HasValue);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_firstordefault_with_predicate_and_default_return_default()
    {
        var expected = 0;
        var @default = expected;

        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            2.Wrap(),
            3.Wrap(),
        };

        var result = list.FirstOrDefault(x => x == 5, @default);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_firstordefault_with_predicate_and_default_return_value()
    {
        var expected = 3;
        var @default = 0;

        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            2.Wrap(),
            3.Wrap(),
        };

        var result = list.FirstOrDefault(x => x == 3, @default);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }
}