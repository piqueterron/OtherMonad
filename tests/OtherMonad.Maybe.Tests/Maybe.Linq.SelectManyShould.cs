namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Linq.SelectMany")]
public class MaybeLinqSelectManyShould
{
    [Fact]
    public void Given_list_of_lists_when_apply_selectmany_return_flatten_list()
    {
        var list = new List<IEnumerable<Maybe<int>>>
        {
            new List<Maybe<int>>
            {
                1
            }
        };

        var result = list.SelectMany(x => x).ToList();

        Assert.IsAssignableFrom<IEnumerable<Maybe<int>>>(result);
        Assert.Single(result);
    }

    [Fact]
    public void Given_list_of_lists_when_apply_selectmany_with_projection_return_flatten_list()
    {
        var list = new List<IEnumerable<Maybe<int>>>
        {
            new List<Maybe<int>>
            {
                1,
                2,
                3
            },
            new List<Maybe<int>>
            {
                4,
                5,
                6
            }
        };

        var result = list.SelectMany(x => x.Where(y => y > 5), (a, b) => b).ToList();

        Assert.IsAssignableFrom<IEnumerable<Maybe<int>>>(result);
        Assert.Single(result);
    }

    [Fact]
    public void Given_list_of_lists_when_apply_selectmany_with_index_and_projection_return_flatten_list()
    {
        var list = new List<IEnumerable<Maybe<int>>>
        {
            new List<Maybe<int>>
            {
                1,
                2,
                3
            },
            new List<Maybe<int>>
            {
                4,
                5,
                6
            }
        };

        var result = list.SelectMany((x, i) => x.Where(y => y > 5), (a, b) => b).ToList();

        Assert.IsAssignableFrom<IEnumerable<Maybe<int>>>(result);
        Assert.Single(result);
    }

    [Fact]
    public void Given_list_of_lists_when_apply_selectmany_with_index_return_flatten_list()
    {
        var expected = 5;

        var list = new List<IEnumerable<Maybe<int>>>
        {
            new List<Maybe<int>>
            {
                1,
                2,
                3
            },
            new List<Maybe<int>>
            {
                4,
                5,
                6
            }
        };

        var result = list.SelectMany((x, i) => x.Where(y => y > 1)).ToList();

        Assert.IsAssignableFrom<IEnumerable<Maybe<int>>>(result);
        Assert.Equal(expected, result.Count);
    }
}
