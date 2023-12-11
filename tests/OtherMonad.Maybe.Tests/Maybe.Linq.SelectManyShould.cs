namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Linq.SelectMany")]
public class MaybeLinqSelectManyShould
{
    [Fact]
    public void Given_list_of_lists_when_apply_selectmany_return_flatten_list()
    {
        var expected = 6;

        var list = new List<Maybe<List<int>>>
        {
            new List<int>{ 1, 2, 3 }.Wrap(),
            new List<int>{ 4, 5, 6 }.Wrap()
        };

        var result = list.SelectMany(list => list).ToList();

        Assert.IsAssignableFrom<IEnumerable<Maybe<int>>>(result);
        Assert.Equal(expected, result.Count);
    }

    [Fact]
    public void Given_list_of_lists_when_apply_selectmany_with_projection_return_flatten_list()
    {
        var list = new List<Maybe<List<int>>>
        {
            new List<int>{ 1, 2, 3 }.Wrap(),
            new List<int>{ 4, 5, 6 }.Wrap()
        };

        var result = list.SelectMany(x => x.Where(y => y > 5), (a, b) => b).ToList();

        Assert.IsAssignableFrom<IEnumerable<Maybe<int>>>(result);
        Assert.Single(result);
    }

    [Fact]
    public void Given_list_of_lists_when_apply_selectmany_with_index_and_projection_return_flatten_list()
    {
        var list = new List<Maybe<List<int>>>
        {
            new List<int>{ 1, 2, 3 }.Wrap(),
            new List<int>{ 4, 5, 6 }.Wrap()
        };

        var result = list.SelectMany((x, i) => x.Where(y => y > 5), (a, b) => b).ToList();

        Assert.IsAssignableFrom<IEnumerable<Maybe<int>>>(result);
        Assert.Single(result);
    }

    [Fact]
    public void Given_list_of_lists_when_apply_selectmany_with_index_return_flatten_list()
    {
        var expected = 5;

        var list = new List<Maybe<List<int>>>
        {
            new List<int>{ 1, 2, 3 }.Wrap(),
            new List<int>{ 4, 5, 6 }.Wrap()
        };

        var result = list.SelectMany((x, i) => x.Where(y => y > 1)).ToList();

        Assert.IsAssignableFrom<IEnumerable<Maybe<int>>>(result);
        Assert.Equal(expected, result.Count);
    }
}
