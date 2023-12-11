namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Map")]
public class MaybeMapShould
{
    [Fact]
    public void Given_list_of_maybes_when_apply_map_return_expected_list_of_maybes()
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
    public async Task Given_list_of_maybes_when_apply_map_into_iteraterable_async_of_type_task_return_expected_list_of_maybes()
    {
        var result = new List<Maybe<int>>();

        await foreach (var item in FetchDummyItemsAsync().Map((x, ct) => Task.FromResult(x * 2), CancellationToken.None))
            result.Add(item);

        Assert.Collection(result,
            e => Assert.Equal(2, e),
            e => Assert.Equal(4, e),
            e => Assert.Equal(6, e),
            e => Assert.Equal(8, e),
            e => Assert.Equal(10, e));
    }

    [Fact]
    public async Task Given_list_of_maybes_when_apply_map_into_iteraterable_of_task_return_expected_list_of_maybes()
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
            e => Assert.Equal(10, e));
    }

    [Fact]
    public async Task Given_list_of_maybes_when_apply_map_into_iteraterable_async_of_type_valuetask__return_expected_list_of_maybes()
    {
        var result = new List<Maybe<int>>();

        await foreach (var item in FetchDummyItemsAsync().Map((x, ct) => ValueTask.FromResult(x * 2), CancellationToken.None))
            result.Add(item);

        Assert.Collection(result,
            e => Assert.Equal(2, e),
            e => Assert.Equal(4, e),
            e => Assert.Equal(6, e),
            e => Assert.Equal(8, e),
            e => Assert.Equal(10, e));
    }

    [Fact]
    public async Task Given_list_of_maybes_when_apply_map_into_iteraterable_of_type_valuetask__return_expected_list_of_maybes()
    {
        var items = FetchDummyItems();
        var result = new List<Maybe<int>>();

        await foreach (var item in items.Map((x, ct) => ValueTask.FromResult(x * 2), CancellationToken.None))
            result.Add(item);

        Assert.Collection(result,
            e => Assert.Equal(2, e),
            e => Assert.Equal(4, e),
            e => Assert.Equal(6, e),
            e => Assert.Equal(8, e),
            e => Assert.Equal(10, e));
    }

    private static async IAsyncEnumerable<Maybe<int>> FetchDummyItemsAsync()
    {
        for (var i = 1; i <= 5; i++)
        {
            yield return i;
        }

        yield return Maybe<int>.None;
    }

    private static IEnumerable<Maybe<int>> FetchDummyItems()
    {
        var items = new List<Maybe<int>>();

        for (var i = 1; i <= 5; i++)
            items.Add(i);

        items.Add(Maybe<int>.None);

        return items;
    }
}