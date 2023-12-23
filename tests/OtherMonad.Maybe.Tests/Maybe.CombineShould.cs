namespace OtherMonad.Maybe.Tests;

[Trait("Maybe", "Combine")]
public class MaybeCombineShould
{
    [Fact]
    public void Given_two_maybes_of_int_when_combine_return_sum_both()
    {
        var expected = 5;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 + obj2);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Given_two_maybes_of_int_when_combine_return_sum_equals_two()
    {
        var expected = 2;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = Maybe<int>.None;

        var result = @object1.Combine(@object2, (obj1, obj2) => obj1 + obj2);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Given_two_maybes_of_int_when_combine_return_sum_equals_six()
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
    public void Given_two_maybes_of_int_when_trycombine_throw_exception_return_deafult()
    {
        var expected = -1;

        Maybe<int> @object1 = 2;
        Maybe<int> @object2 = 3;

        var result = @object1.TryCombine(@object2, (obj1, obj2) => throw new Exception(), () => expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }
}