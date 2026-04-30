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
    public void GivenMaybeWithValueAndNoneWhenCombineThenReturnsNone()
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

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1), TestContext.Current.CancellationToken);
        var defer2 = @object2.BindDefer((x, ct) => Task.FromResult(x + 1), TestContext.Current.CancellationToken);

        var comb = defer1.CombineDefer(defer2, (obj1, obj2, ct) => Task.FromResult(obj1 + obj2), TestContext.Current.CancellationToken);

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

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1), TestContext.Current.CancellationToken);

        var comb = defer1.CombineDefer(@object2, (obj1, obj2, ct) => Task.FromResult(obj1 + obj2), TestContext.Current.CancellationToken);

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

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1), TestContext.Current.CancellationToken);

        var comb = defer1.TryCombineDefer(@object2, (obj1, obj2, ctx) => Task.FromResult(obj1 + obj2), () => Task.FromResult(0), TestContext.Current.CancellationToken);

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

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1), TestContext.Current.CancellationToken);

        var comb = defer1.TryCombineDefer(@object2, (obj1, obj2, ctx) => throw new ApplicationException(), () => Task.FromResult(0), TestContext.Current.CancellationToken);

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

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1), TestContext.Current.CancellationToken);
        var defer2 = @object2.BindDefer((x, ct) => Task.FromResult(x + 1), TestContext.Current.CancellationToken);

        var comb = defer1.TryCombineDefer(defer2, (obj1, obj2, ctx) => Task.FromResult(obj1 + obj2), () => Task.FromResult(0), TestContext.Current.CancellationToken);

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

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1), TestContext.Current.CancellationToken);
        var defer2 = @object2.BindDefer((x, ct) => Task.FromResult(x + 1), TestContext.Current.CancellationToken);

        var comb = defer1.TryCombineDefer(defer2, (obj1, obj2, ctx) => throw new ApplicationException(), () => Task.FromResult(0), TestContext.Current.CancellationToken);

        var result = comb();

        var data = await result;

        Assert.True(data.HasValue);
        Assert.Equal(expected, data.Value);
    }

    [Fact]
    public void GivenTwoNoneMaybesWhenCombineReturnNone()
    {
        var @object1 = Maybe<int>.None;
        var @object2 = Maybe<int>.None;

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 + obj2);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<int>.None, result);
    }

    [Fact]
    public void GivenNoneAndValueWhenCombineThenReturnsNone()
    {
        var @object1 = Maybe<int>.None;
        Maybe<int> @object2 = 3;

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 + obj2);

        Assert.False(result.HasValue);
    }

    [Fact]
    public void GivenMaybesOfDifferentTypesWhenCombineReturnExpectedResult()
    {
        var expected = "Value: 5";
        Maybe<int> @object1 = 5;
        Maybe<string> @object2 = "Value: ";

        var result = @object1.Combine(@object2, (obj1, obj2) => $"{obj2}{obj1}");

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybesOfValueTypesWhenCombineReturnProduct()
    {
        var expected = 12;
        Maybe<int> @object1 = 3;
        Maybe<int> @object2 = 4;

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 * obj2);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybesOfBoolWhenCombineReturnLogicalAnd()
    {
        Maybe<bool> @object1 = true;
        Maybe<bool> @object2 = false;

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 && obj2);

        Assert.True(result.HasValue);
        Assert.False(result.Value);
    }

    [Fact]
    public void GivenMaybesWithDefaultValuesWhenCombineReturnExpectedResult()
    {
        Maybe<int> @object1 = 0;
        Maybe<int> @object2 = 0;

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 + obj2);

        Assert.True(result.HasValue);
        Assert.Equal(0, result.Value);
    }

    [Fact]
    public void GivenMaybesOfStructsWhenCombineReturnCombinedStruct()
    {
        var expected = new DummyStruct { Id = 3, Name = "AB" };
        Maybe<DummyStruct> @object1 = new DummyStruct { Id = 1, Name = "A" };
        Maybe<DummyStruct> @object2 = new DummyStruct { Id = 2, Name = "B" };

        var result = @object1.Combine(@object2, (obj1, obj2) => 
            new DummyStruct { Id = obj1.Id + obj2.Id, Name = obj1.Name + obj2.Name });

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybesWhenTryCombineThrowsAndHasDefaultFactoryReturnDefault()
    {
        var expected = -999;
        Maybe<int> @object1 = 5;
        Maybe<int> @object2 = 10;

        var result = @object1.TryCombine(@object2, 
            (obj1, obj2) => throw new InvalidOperationException(), 
            () => expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenEmptyStringsWhenCombineReturnConcatenatedEmpty()
    {
        var expected = "";
        Maybe<string> @object1 = "";
        Maybe<string> @object2 = "";

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 + obj2);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenComplexChainOfCombinesReturnExpectedResult()
    {
        var expected = 24; // (2 + 3) * (1 + 1) = 5 * 2 + 14 = 24
        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;
        Maybe<int> @object3 = 1;
        Maybe<int> @object4 = 1;

        var result = @object1.Combine(@object2, (a, b) => a + b)
            .Combine(@object3.Combine(@object4, (c, d) => c + d), (x, y) => x * y + 14);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenSelectorReturnsNullWhenCombineReturnNone()
    {
        Maybe<string> @object1 = "test";
        Maybe<string> @object2 = "data";

        var result = @object1.Combine(@object2, (obj1, obj2) => (string?)null);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string?>.None, result);
    }

    [Fact]
    public async Task GivenCancellationTokenWhenApplyCombineDeferAsyncUseCancellationToken()
    {
        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;
        var tokenPassed = false;

        var defer = @object1.BindDefer((x, ct) => Task.FromResult(x + 1), TestContext.Current.CancellationToken);
        var comb = defer.CombineDefer(@object2, (obj1, obj2, ct) =>
        {
            tokenPassed = ct == TestContext.Current.CancellationToken;
            return Task.FromResult(obj1 + obj2);
        }, TestContext.Current.CancellationToken);

        await comb();

        Assert.True(tokenPassed);
    }

    [Fact]
    public void GivenMultipleCombineDeferCallsWhenExecutedReturnExpectedResult()
    {
        var expected = 17; // (2 * 2) + 3 + 10 = 4 + 3 + 10 = 17
        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;
        Maybe<int> @object3 = 10;

        var defer1 = @object1.BindDefer(x => x * 2);
        var comb1 = defer1.CombineDefer(@object2, (a, b) => a + b);
        var defer2 = comb1.BindDefer(x => x + @object3.Value);

        var result = defer2();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenNoneInDeferredCombineWhenExecutedReturnNone()
    {
        var @object1 = Maybe<int>.None;
        Maybe<int> @object2 = 3;

        var defer = @object1.BindDefer(x => x + 1);
        var comb = defer.CombineDefer(@object2, (obj1, obj2) => obj1 + obj2);

        var result = comb();

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<int>.None, result);
    }

    [Fact]
    public async Task GivenTryCombineDeferWithExceptionWhenExecutedReturnDefault()
    {
        var expected = 100;
        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer((x, ct) => Task.FromResult(x + 1), TestContext.Current.CancellationToken);
        var comb = defer1.TryCombineDefer(@object2, 
            (obj1, obj2, ct) => throw new InvalidOperationException(), 
            () => Task.FromResult(expected), 
            TestContext.Current.CancellationToken);

        var result = await comb();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    public struct DummyStruct : IEquatable<DummyStruct>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public bool Equals(DummyStruct other)
        {
            return Id == other.Id && Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            return obj is DummyStruct other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}