namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Map")]
public class MaybeMapShould
{
    [Fact]
    public void GivenListOfMaybesWhenApplyMapReturnExpectedListOfMaybes()
    {
        var maybes = new List<Maybe<int>>
        {
            1, 2, 3, 4, 5
        };

        var result = maybes.Map(v => v * 2);

        Assert.Collection(result,
            e => Assert.Equal(2, e),
            e => Assert.Equal(4, e),
            e => Assert.Equal(6, e),
            e => Assert.Equal(8, e),
            e => Assert.Equal(10, e));
    }

    [Fact]
    public async Task GivenListOfMaybesWhenApplyMapIntoIteraterableAsyncOfTypeTaskReturnExpectedListOfMaybes()
    {
        var result = new List<Maybe<int>>();

        await foreach (var item in FetchDummyItemsAsync().Map((x, ct) => Task.FromResult(x * 2), CancellationToken.None))
            result.Add(item);

        Assert.Collection(result,
            e => Assert.Equal(2, e),
            e => Assert.Equal(4, e),
            e => Assert.Equal(6, e),
            e => Assert.Equal(8, e),
            e => Assert.Equal(10, e),
            e => Assert.Equal(0, e));
    }

    [Fact]
    public async Task GivenListOfMaybesWhenApplyMapIntoIteraterableOfTaskReturnExpectedListOfMaybes()
    {
        var items = FetchDummyItems();
        var result = new List<Maybe<int>>();

        await foreach (var item in items.Map((x, ct) => Task.FromResult(x * 2), CancellationToken.None))
            result.Add(item);

        Assert.Collection(result,
            e => Assert.Equal(2, e),
            e => Assert.Equal(4, e),
            e => Assert.Equal(6, e),
            e => Assert.Equal(8, e),
            e => Assert.Equal(10, e),
            e => Assert.Equal(0, e));
    }

#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
    private static async IAsyncEnumerable<Maybe<int>> FetchDummyItemsAsync()
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica
    {
        for (var i = 1; i <= 5; i++)
        {
            yield return i;
        }

        yield return Maybe<int>.None;
    }

    private static List<Maybe<int>> FetchDummyItems()
    {
        var items = new List<Maybe<int>>();

        for (var i = 1; i <= 5; i++)
            items.Add(i);

        items.Add(Maybe<int>.None);

        return items;
    }
}