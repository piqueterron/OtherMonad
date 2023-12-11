namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Linq.SingleOrDefault")]
public class MaybeLinqSingleOrDefaultShould
{
    [Fact]
    public void Given_list_of_ints_when_apply_singleordefault_return_value()
    {
        var list = new List<Maybe<int>>
        {
            1.Wrap(),
        };

        var result = list.SingleOrDefault();

        Assert.True(result.HasValue);
        Assert.Equal(1, result.Value);
    }

    [Fact]
    public void Given_list_void_when_apply_singleordefault_return_none()
    {
        var list = new List<Maybe<int>>
        {
        };

        var result = list.SingleOrDefault();

        Assert.False(result.HasValue);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_singleordefault_throw_invalidoperationexception()
    {
        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            1.Wrap()
        };

        Assert.Throws<InvalidOperationException>(() => list.SingleOrDefault());
    }

    [Fact]
    public void Given_list_of_ints_when_apply_singleordefault_with_default_return_default()
    {
        var @default = 5;
        var list = new List<Maybe<int>>
        {
        };

        var result = list.SingleOrDefault(@default);

        Assert.True(result.HasValue);
        Assert.Equal(@default, result.Value);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_singleordefault_with_default_return_value()
    {
        var expected = 1;
        var @default = 5;

        var list = new List<Maybe<int>>
        {
            1.Wrap(),
        };

        var result = list.SingleOrDefault(@default);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_singleordefault_with_predicate_return_value()
    {
        var expected = 2;

        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            2.Wrap(),
        };

        var result = list.SingleOrDefault(x => x == expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_singleordefault_with_predicate_throw_invalidoperationexception()
    {
        var expected = 2;

        var list = new List<Maybe<int>>
        {
            2.Wrap(),
            2.Wrap(),
        };

        Assert.Throws<InvalidOperationException>(() => list.SingleOrDefault(x => x == expected));
    }

    [Fact]
    public void Given_list_of_ints_when_apply_singleordefault_with_predicate_return_none()
    {
        var list = new List<Maybe<int>>
        {
        };

        var result = list.SingleOrDefault(x => x == 2);

        Assert.False(result.HasValue);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_singleordefault_with_predicate_and_default_return_default()
    {
        var @default = 5;
        var list = new List<Maybe<int>>
        {
        };

        var result = list.SingleOrDefault(x => x == 2, @default);

        Assert.True(result.HasValue);
        Assert.Equal(@default, result.Value);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_singleordefault_with_predicate_and_default_return_value()
    {
        var expected = 1;
        var @default = 5;

        var list = new List<Maybe<int>>
        {
            1.Wrap(),
        };

        var result = list.SingleOrDefault(x => x == 1, @default);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }
}