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
        var expected = 2;

        Maybe<int> @object1 = 2;
        var @object2 = Maybe<int>.None;

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 + obj2);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
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

        var result = defer1.Combine(@object2, (obj1, obj2) => obj1 + obj2);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenTwoMaybesOfIntWhenTryCombineDeferReturnSumBoth()
    {
        var expected = 6;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var defer1 = @object1.BindDefer(x => x + 1);

        var result = defer1.TryCombine(@object2, (obj1, obj2) => obj1 + obj2, () => 0);

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

        var result = defer1.TryCombine(@object2, (obj1, obj2) => throw new ApplicationException(), () => "default");

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }
}