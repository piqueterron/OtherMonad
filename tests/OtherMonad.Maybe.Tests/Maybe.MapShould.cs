namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Map")]
public class MaybeMapShould
{
    [Fact]
    public void GivenMaybeOfStringWhenApplyMapReturnExpectedMaybe()
    {
        Maybe<string> @object = "test";

        var result = @object.Map(e => $"{e}-1");

        Assert.True(result.HasValue);
        Assert.Equal("test-1", result.Value);
    }

    [Fact]
    public void GivenMaybeOfNoneWhenApplyMapReturnMaybeNone()
    {
        Maybe<string> @object = null;

        var result = @object.Map(e => $"{e}-1");

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyMapAsyncReturnExpectedMaybe()
    {
        Maybe<string> @object = "test";

        var result = await @object.Map((e, ct) => Task.FromResult($"{e}-1"), TestContext.Current.CancellationToken);

        Assert.True(result.HasValue);
        Assert.Equal("test-1", result.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyMapDeferReturnExpectedMaybe()
    {
        Maybe<string> @object = "test";

        var deferred = @object.MapDefer(e => $"{e}-1");
        var result = deferred();

        Assert.True(result.HasValue);
        Assert.Equal("test-1", result.Value);
    }

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

        await foreach (var item in FetchDummyItemsAsync().Map((x, ct) => Task.FromResult(x * 2), TestContext.Current.CancellationToken))
            result.Add(item);

        Assert.Collection(result,
            e => Assert.Equal(2, e),
            e => Assert.Equal(4, e),
            e => Assert.Equal(6, e),
            e => Assert.Equal(8, e),
            e => Assert.Equal(10, e),
            e => Assert.Equal(Maybe<int>.None, e));
    }

    [Fact]
    public async Task GivenListOfMaybesWhenApplyMapIntoIteraterableOfTaskReturnExpectedListOfMaybes()
    {
        var items = FetchDummyItems();
        var result = new List<Maybe<int>>();

        await foreach (var item in items.Map((x, ct) => Task.FromResult(x * 2), TestContext.Current.CancellationToken))
            result.Add(item);

        Assert.Collection(result,
            e => Assert.Equal(2, e),
            e => Assert.Equal(4, e),
            e => Assert.Equal(6, e),
            e => Assert.Equal(8, e),
            e => Assert.Equal(10, e),
            e => Assert.Equal(Maybe<int>.None, e));
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

    [Fact]
    public void GivenNullSelectorWhenApplyMapThrowArgumentNullException()
    {
        var maybes = new List<Maybe<int>> { 1, 2, 3 };

        Assert.Throws<ArgumentNullException>(() => maybes.Map<int, int>(null!).ToList());
    }

    [Fact]
    public async Task GivenNullAsyncSelectorWhenApplyMapThrowArgumentNullException()
    {
        var maybes = new List<Maybe<int>> { 1, 2, 3 };

        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await foreach (var _ in maybes.Map<int, int>(null!, TestContext.Current.CancellationToken))
            {
            }
        });
    }

    [Fact]
    public void GivenEmptyListOfMaybesWhenApplyMapReturnEmptyList()
    {
        var maybes = new List<Maybe<int>>();

        var result = maybes.Map(v => v * 2);

        Assert.Empty(result);
    }

    [Fact]
    public void GivenListWithNoneMaybesWhenApplyMapReturnListWithNone()
    {
        var maybes = new List<Maybe<int>>
        {
            1, Maybe<int>.None, 3
        };

        var result = maybes.Map(v => v * 2);

        Assert.Collection(result,
            e => Assert.Equal(2, e.Value),
            e => Assert.False(e.HasValue),
            e => Assert.Equal(6, e.Value));
    }

    [Fact]
    public void GivenListOfMaybesWhenMapTransformTypeReturnExpectedList()
    {
        var maybes = new List<Maybe<int>>
        {
            1, 2, 3
        };

        var result = maybes.Map(v => $"Value: {v}");

        Assert.Collection(result,
            e => Assert.Equal("Value: 1", e.Value),
            e => Assert.Equal("Value: 2", e.Value),
            e => Assert.Equal("Value: 3", e.Value));
    }

    [Fact]
    public void GivenListOfStringMaybesWhenApplyMapReturnTransformedList()
    {
        var maybes = new List<Maybe<string>>
        {
            "hello", "world"
        };

        var result = maybes.Map(v => v.ToUpperInvariant());

        Assert.Collection(result,
            e => Assert.Equal("HELLO", e.Value),
            e => Assert.Equal("WORLD", e.Value));
    }

    [Fact]
    public void GivenListOfMaybesWhenMapReturnsNullReturnNoneInList()
    {
        var maybes = new List<Maybe<int>>
        {
            1, 2, 3
        };

        var result = maybes.Map(v => v > 1 ? null : "valid");

        Assert.Collection(result,
            e => Assert.Equal("valid", e.Value),
            e => Assert.False(e.HasValue),
            e => Assert.False(e.HasValue));
    }

    [Fact]
    public void GivenListOfMaybesWhenMapToStructReturnExpectedStructList()
    {
        var maybes = new List<Maybe<int>>
        {
            1, 2
        };

        var result = maybes.Map(v => new DummyStruct { Id = v, Name = $"Item{v}" });

        Assert.Collection(result,
            e =>
            {
                Assert.True(e.HasValue);
                Assert.Equal(1, e.Value.Id);
                Assert.Equal("Item1", e.Value.Name);
            },
            e =>
            {
                Assert.True(e.HasValue);
                Assert.Equal(2, e.Value.Id);
                Assert.Equal("Item2", e.Value.Name);
            });
    }

    [Fact]
    public async Task GivenListOfMaybesWhenApplyMapAsyncWithDelayReturnExpectedList()
    {
        var maybes = new List<Maybe<int>>
        {
            1, 2, 3
        };
        var result = new List<Maybe<int>>();

        await foreach (var item in maybes.Map(async (x, ct) =>
        {
            await Task.Delay(1, ct);
            return x * 3;
        }, TestContext.Current.CancellationToken))
        {
            result.Add(item);
        }

        Assert.Collection(result,
            e => Assert.Equal(3, e.Value),
            e => Assert.Equal(6, e.Value),
            e => Assert.Equal(9, e.Value));
    }

    [Fact]
    public async Task GivenListOfMaybesWhenApplyMapAsyncWithNoneReturnListWithNone()
    {
        var maybes = new List<Maybe<int>>
        {
            1, Maybe<int>.None, 3
        };
        var result = new List<Maybe<int>>();

        await foreach (var item in maybes.Map((x, ct) => Task.FromResult(x * 2), TestContext.Current.CancellationToken))
        {
            result.Add(item);
        }

        Assert.Collection(result,
            e => Assert.Equal(2, e.Value),
            e => Assert.False(e.HasValue),
            e => Assert.Equal(6, e.Value));
    }

    [Fact]
    public async Task GivenCancellationTokenWhenApplyMapAsyncUseCancellationToken()
    {
        var maybes = new List<Maybe<int>> { 1, 2 };
        var tokenPassed = false;

        await foreach (var item in maybes.Map((x, ct) =>
        {
            tokenPassed = ct == TestContext.Current.CancellationToken;
            return Task.FromResult(x * 2);
        }, TestContext.Current.CancellationToken))
        {
            // Process items
        }

        Assert.True(tokenPassed);
    }

    [Fact]
    public async Task GivenAsyncEnumerableWhenApplyMapReturnTransformedAsyncEnumerable()
    {
        var result = new List<Maybe<int>>();

        await foreach (var item in FetchDummyItemsAsync().Map((x, ct) => Task.FromResult(x + 10), CancellationToken.None))
        {
            result.Add(item);
        }

        Assert.Collection(result,
            e => Assert.Equal(11, e.Value),
            e => Assert.Equal(12, e.Value),
            e => Assert.Equal(13, e.Value),
            e => Assert.Equal(14, e.Value),
            e => Assert.Equal(15, e.Value),
            e => Assert.False(e.HasValue));
    }

    [Fact]
    public void GivenSingleMaybeListWhenApplyMapReturnSingleTransformedItem()
    {
        var maybes = new List<Maybe<int>> { 5 };

        var result = maybes.Map(v => v * 10);

        Assert.Collection(result,
            e => Assert.Equal(50, e.Value));
    }

    [Fact]
    public void GivenListOfBoolMaybesWhenApplyMapReturnNegatedList()
    {
        var maybes = new List<Maybe<bool>>
        {
            true, false, true
        };

        var result = maybes.Map(v => !v);

        Assert.Collection(result,
            e => Assert.False(e.Value),
            e => Assert.True(e.Value),
            e => Assert.False(e.Value));
    }

    [Fact]
    public void GivenListOfMaybesWhenChainMultipleMapCallsReturnExpectedResult()
    {
        var maybes = new List<Maybe<int>>
        {
            1, 2, 3
        };

        var result = maybes
            .Map(v => v * 2)
            .Map(v => v + 1)
            .Map(v => $"Result: {v}");

        Assert.Collection(result,
            e => Assert.Equal("Result: 3", e.Value),
            e => Assert.Equal("Result: 5", e.Value),
            e => Assert.Equal("Result: 7", e.Value));
    }

    public struct DummyStruct
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}