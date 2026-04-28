namespace OtherMonad.Maybe.Tests;

[Trait("Maybe", "Combine")]
public class MaybeCombineShould
{
    [Fact]
    public void GivenTwoMaybesOfIntWhenCombineReturnSumBoth()
    {
        var expected = 5;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 + obj2);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenTwoMaybesOfIntWhenCombineReturnSumEqualsTwo()
    {
        Maybe<int> @object1 = 2;
        var @object2 = Maybe<int>.None;

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 + obj2);

        Assert.False(result.HasValue);
    }

    [Fact]
    public void GivenTwoMaybesOfIntWhenCombineReturnSumEqualsSix()
    {
        var expected = 6;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;
        Maybe<int> @object3 = 1;

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 + obj2)
            .Combine(@object3, (obj1, obj2) => obj1 + obj2);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenTwoMaybesOfIntWhenTrycombineThrowExceptionReturnDeafult()
    {
        var expected = -1;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var result = @object1.TryCombine(@object2, (obj1, obj2) => throw new Exception(), () => expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenTwoMaybesOfIntWhenCombineDeferReturnSumBoth()
    {
        var expected = 6;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer(x => x + 1);

        var comb = defer1.CombineDefer(@object2, (obj1, obj2) => obj1 + obj2);

        var result = comb();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenTwoDeferredMaybesOfIntWhenCombineDeferReturnSumBoth()
    {
        var expected = 7;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer(x => x + 1);
        var defer2 = @object2.BindDefer(x => x + 1);

        var comb = defer1.CombineDefer(defer2, (obj1, obj2) => obj1 + obj2);

        var result = comb();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task GivenTwoDeferredTaskMaybesOfIntWhenCombineDeferReturnSumBoth()
    {
        var expected = 7;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1));
        var defer2 = @object2.BindDefer((x, ct) => Task.FromResult(x + 1));

        var comb = defer1.CombineDefer(defer2, (obj1, obj2, ct) => Task.FromResult(obj1 + obj2));

        var result = comb();
        var data = await result;

        Assert.True(data.HasValue);
        Assert.Equal(expected, data.Value);
    }

    [Fact]
    public async Task GivenTwoDeferredTaskAndMaybesOfIntWhenCombineDeferReturnSumBoth()
    {
        var expected = 6;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1));

        var comb = defer1.CombineDefer(@object2, (obj1, obj2, ct) => Task.FromResult(obj1 + obj2));

        var result = comb();
        var data = await result;

        Assert.True(data.HasValue);
        Assert.Equal(expected, data.Value);
    }

    [Fact]
    public void GivenTwoMaybesOfIntWhenTryCombineDeferReturnSumBoth()
    {
        var expected = 6;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer(x => x + 1);

        var comb = defer1.TryCombineDefer(@object2, (obj1, obj2) => obj1 + obj2, () => 0);

        var result = comb();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenTwoDeferredMaybesOfIntWhenTryCombineDeferReturnSumBoth()
    {
        var expected = 7;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer(x => x + 1);
        var defer2 = @object2.BindDefer(x => x + 1);

        var comb = defer1.TryCombineDefer(defer2, (obj1, obj2) => obj1 + obj2, () => 0);

        var result = comb();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenTwoMaybesOfIntWhenTryCombineDeferReturnDefault()
    {
        var expected = "default";

        Maybe<int> @object1 = 2;
        Maybe<string> @object2 = "test";

        var defer1 = @object1.BindDefer(x => x + 1);

        var comb = defer1.TryCombineDefer(@object2, (obj1, obj2) => throw new ApplicationException(), () => expected);

        var result = comb();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenTwoDeferredMaybesOfIntWhenTryCombineDeferReturnDefault()
    {
        var expected = "default";

        Maybe<int> @object1 = 2;
        Maybe<string> @object2 = "test";

        var defer1 = @object1.BindDefer(x => x + 1);
        var defer2 = @object2.BindDefer(x => x + "1");

        var comb = defer1.TryCombineDefer(defer2, (obj1, obj2) => throw new ApplicationException(), () => expected);

        var result = comb();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task GivenTwoDeferredTaskMaybesOfIntWhenTryCombineDeferReturnExpected()
    {
        var expected = 6;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1));

        var comb = defer1.TryCombineDefer(@object2, (obj1, obj2, ctx) => Task.FromResult(obj1 + obj2), () => Task.FromResult(0), CancellationToken.None);

        var result = comb();

        var data = await result;

        Assert.True(data.HasValue);
        Assert.Equal(expected, data.Value);
    }

    [Fact]
    public async Task GivenTwoDeferredTaskMaybesOfIntWhenTryCombineDeferReturnDefault()
    {
        var expected = 0;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1));

        var comb = defer1.TryCombineDefer(@object2, (obj1, obj2, ctx) => throw new ApplicationException(), () => Task.FromResult(0), CancellationToken.None);

        var result = comb();

        var data = await result;

        Assert.True(data.HasValue);
        Assert.Equal(expected, data.Value);
    }

    [Fact]
    public async Task GivenTwoDeferredTaskIntWhenTryCombineDeferReturnExpected()
    {
        var expected = 7;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1));
        var defer2 = @object2.BindDefer((x, ct) => Task.FromResult(x + 1));

        var comb = defer1.TryCombineDefer(defer2, (obj1, obj2, ctx) => Task.FromResult(obj1 + obj2), () => Task.FromResult(0), CancellationToken.None);

        var result = comb();

        var data = await result;

        Assert.True(data.HasValue);
        Assert.Equal(expected, data.Value);
    }

    [Fact]
    public async Task GivenTwoDeferredTaskIntWhenTryCombineDeferReturnDefault()
    {
        var expected = 0;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1));
        var defer2 = @object2.BindDefer((x, ct) => Task.FromResult(x + 1));

        var comb = defer1.TryCombineDefer(defer2, (obj1, obj2, ctx) => throw new ApplicationException(), () => Task.FromResult(0), CancellationToken.None);

        var result = comb();

        var data = await result;

        Assert.True(data.HasValue);
        Assert.Equal(expected, data.Value);
    }
}