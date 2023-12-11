namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Linq.Where")]
public class MaybeLinqWhereShould
{
    [Fact]
    public void Given_list_of_ints_when_apply_where_return_value_match_search_criteria()
    {
        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            2.Wrap(),
        };

        var result = list.Where(x => x == 2);

        Assert.NotEmpty(result);
        Assert.Single(result, a => a.Unwrap() == 2);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_where_with_index_return_value_match_search_criteria()
    {
        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            2.Wrap(),
        };

        var result = list.Where((x, i) => x == 2);

        Assert.NotEmpty(result);
        Assert.Single(result, a => a.Unwrap() == 2);
    }
}