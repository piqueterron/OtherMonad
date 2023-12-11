namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Linq.Select")]
public class MaybeLinqSelectShould
{
    [Fact]
    public void Given_list_of_ints_when_apply_select_return_list_of_maybe_of_int()
    {
        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            2.Wrap(),
        };

        var result = list.Select(x => x * 2);

        Assert.Single(result, a => a.Unwrap() == 2);
        Assert.Single(result, a => a.Unwrap() == 4);
    }

    [Fact]
    public void Given_list_of_ints_when_apply_select_with_index_return_list_of_maybe_of_int()
    {
        var list = new List<Maybe<int>>
        {
            1.Wrap(),
            2.Wrap(),
        };

        var result = list.Select((x, i) => x * i);

        Assert.Single(result, a => a.Unwrap() == 0);
        Assert.Single(result, a => a.Unwrap() == 2);
    }
}
