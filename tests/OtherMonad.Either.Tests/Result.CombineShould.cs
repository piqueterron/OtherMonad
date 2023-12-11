namespace Monads.Either.Tests;

using OtherMonad;

[Trait("Result", "Combine")]
public class ResultCombineShould
{
    [Fact]
    public void Given_two_result_with_state_success_when_apply_combine_return_all_results()
    {
        var either1 = InternalBuilders.Create("test1");
        var either2 = InternalBuilders.Create("test2");

        var result = Result.Combine(either1, either2);

        Assert.True(result.IsLeft);
        Assert.Equal("test1", result.Left.Result1);
        Assert.Equal("test2", result.Left.Result2);
    }

    [Fact]
    public void Given_two_result_with_state_fail_when_apply_combine_return_all_exceptions()
    {
        var either1 = InternalBuilders.Create<string>(new Exception());
        var either2 = InternalBuilders.Create<string>(new ArgumentNullException());

        var result = Result.Combine(either1, either2);

        Assert.False(result.IsLeft);
        Assert.IsType<AggregateException>(result.Right);
    }

    [Fact]
    public void Given_three_result_with_state_success_when_apply_combine_return_all_results()
    {
        var either1 = InternalBuilders.Create("test1");
        var either2 = InternalBuilders.Create("test2");
        var either3 = InternalBuilders.Create("test3");

        var result = Result.Combine(either1, either2, either3);

        Assert.True(result.IsLeft);
        Assert.Equal("test1", result.Left.Result1);
        Assert.Equal("test2", result.Left.Result2);
        Assert.Equal("test3", result.Left.Result3);
    }

    [Fact]
    public void Given_three_result_with_state_fail_when_apply_combine_return_all_exceptions()
    {
        var either1 = InternalBuilders.Create<string>(new Exception());
        var either2 = InternalBuilders.Create<string>(new ArgumentNullException());
        var either3 = InternalBuilders.Create<string>(new ApplicationException());

        var result = Result.Combine(either1, either2, either3);

        Assert.False(result.IsLeft);
        Assert.IsType<AggregateException>(result.Right);
    }

    [Fact]
    public void Given_four_result_with_state_success_when_apply_combine_return_all_results()
    {
        var either1 = InternalBuilders.Create("test1");
        var either2 = InternalBuilders.Create("test2");
        var either3 = InternalBuilders.Create("test3");
        var either4 = InternalBuilders.Create("test4");

        var result = Result.Combine(either1, either2, either3, either4);

        Assert.True(result.IsLeft);
        Assert.Equal("test1", result.Left.Result1);
        Assert.Equal("test2", result.Left.Result2);
        Assert.Equal("test3", result.Left.Result3);
        Assert.Equal("test4", result.Left.Result4);
    }

    [Fact]
    public void Given_four_result_with_state_fail_when_apply_combine_return_all_exceptions()
    {
        var either1 = InternalBuilders.Create<string>(new Exception());
        var either2 = InternalBuilders.Create<string>(new ArgumentNullException());
        var either3 = InternalBuilders.Create<string>(new ApplicationException());
        var either4 = InternalBuilders.Create<string>(new ArithmeticException());

        var result = Result.Combine(either1, either2, either3, either4);

        Assert.False(result.IsLeft);
        Assert.IsType<AggregateException>(result.Right);
    }

    [Fact]
    public void Given_five_result_with_state_success_when_apply_combine_return_all_results()
    {
        var either1 = InternalBuilders.Create("test1");
        var either2 = InternalBuilders.Create("test2");
        var either3 = InternalBuilders.Create("test3");
        var either4 = InternalBuilders.Create("test4");
        var either5 = InternalBuilders.Create("test5");

        var result = Result.Combine(either1, either2, either3, either4, either5);

        Assert.True(result.IsLeft);
        Assert.Equal("test1", result.Left.Result1);
        Assert.Equal("test2", result.Left.Result2);
        Assert.Equal("test3", result.Left.Result3);
        Assert.Equal("test4", result.Left.Result4);
        Assert.Equal("test5", result.Left.Result5);
    }

    [Fact]
    public void Given_five_result_with_state_fail_when_apply_combine_return_all_exceptions()
    {
        var either1 = InternalBuilders.Create<string>(new Exception());
        var either2 = InternalBuilders.Create<string>(new ArgumentNullException());
        var either3 = InternalBuilders.Create<string>(new ApplicationException());
        var either4 = InternalBuilders.Create<string>(new ArithmeticException());
        var either5 = InternalBuilders.Create<string>(new ArithmeticException());

        var result = Result.Combine(either1, either2, either3, either4, either5);

        Assert.False(result.IsLeft);
        Assert.IsType<AggregateException>(result.Right);
    }

    [Fact]
    public void Given_six_result_with_state_success_when_apply_combine_return_all_results()
    {
        var either1 = InternalBuilders.Create("test1");
        var either2 = InternalBuilders.Create("test2");
        var either3 = InternalBuilders.Create("test3");
        var either4 = InternalBuilders.Create("test4");
        var either5 = InternalBuilders.Create("test5");
        var either6 = InternalBuilders.Create("test6");

        var result = Result.Combine(either1, either2, either3, either4, either5, either6);

        Assert.True(result.IsLeft);
        Assert.Equal("test1", result.Left.Result1);
        Assert.Equal("test2", result.Left.Result2);
        Assert.Equal("test3", result.Left.Result3);
        Assert.Equal("test4", result.Left.Result4);
        Assert.Equal("test5", result.Left.Result5);
        Assert.Equal("test6", result.Left.Result6);
    }

    [Fact]
    public void Given_six_result_with_state_fail_when_apply_combine_return_all_exceptions()
    {
        var either1 = InternalBuilders.Create<string>(new Exception());
        var either2 = InternalBuilders.Create<string>(new ArgumentNullException());
        var either3 = InternalBuilders.Create<string>(new ApplicationException());
        var either4 = InternalBuilders.Create<string>(new ArithmeticException());
        var either5 = InternalBuilders.Create<string>(new ArithmeticException());
        var either6 = InternalBuilders.Create<string>(new ArithmeticException());

        var result = Result.Combine(either1, either2, either3, either4, either5, either6);

        Assert.False(result.IsLeft);
        Assert.IsType<AggregateException>(result.Right);
    }
}
