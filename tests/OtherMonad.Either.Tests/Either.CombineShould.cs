namespace Monads.Either.Tests;

using OtherMonad;

[Trait("Either", "Combine")]
public class EitherCombineShould
{
    [Fact]
    public void Given_two_eithers_with_state_success_when_apply_combine_return_all_results()
    {
        Either<string, Exception> either1 = "test1";
        Either<string, Exception> either2 = "test2";

        var result = Either.Combine(either1, either2);

        Assert.True(result.IsLeft);
        Assert.Equal("test1", result.Left.Left1);
        Assert.Equal("test2", result.Left.Left2);
    }

    [Fact]
    public void Given_two_eithers_with_state_fail_when_apply_combine_return_all_exceptions()
    {
        Either<string, Exception> either1 = new Exception();
        Either<string, Exception> either2 = new Exception();

        var result = Either.Combine(either1, either2);

        Assert.False(result.IsLeft);
        Assert.IsType<Exception>(result.Right.Right1);
        Assert.IsType<Exception>(result.Right.Right2);
    }

    [Fact]
    public void Given_three_eithers_with_state_success_when_apply_combine_return_all_results()
    {
        Either<string, Exception> either1 = "test1";
        Either<string, Exception> either2 = "test2";
        Either<string, Exception> either3 = "test3";

        var result = Either.Combine(either1, either2, either3);

        Assert.True(result.IsLeft);
        Assert.Equal("test1", result.Left.Left1);
        Assert.Equal("test2", result.Left.Left2);
        Assert.Equal("test3", result.Left.Left3);
    }

    [Fact]
    public void Given_three_eithers_with_state_fail_when_apply_combine_return_all_exceptions()
    {
        Either<string, Exception> either1 = new Exception();
        Either<string, Exception> either2 = new Exception();
        Either<string, Exception> either3 = new Exception();

        var result = Either.Combine(either1, either2, either3);

        Assert.False(result.IsLeft);
        Assert.IsType<Exception>(result.Right.Right1);
        Assert.IsType<Exception>(result.Right.Right2);
        Assert.IsType<Exception>(result.Right.Right3);
    }

    [Fact]
    public void Given_four_eithers_with_state_success_when_apply_combine_return_all_results()
    {
        Either<string, Exception> either1 = "test1";
        Either<string, Exception> either2 = "test2";
        Either<string, Exception> either3 = "test3";
        Either<string, Exception> either4 = "test4";

        var result = Either.Combine(either1, either2, either3, either4);

        Assert.True(result.IsLeft);
        Assert.Equal("test1", result.Left.Left1);
        Assert.Equal("test2", result.Left.Left2);
        Assert.Equal("test3", result.Left.Left3);
        Assert.Equal("test4", result.Left.Left4);
    }

    [Fact]
    public void Given_four_eithers_with_state_fail_when_apply_combine_return_all_exceptions()
    {
        Either<string, Exception> either1 = new Exception();
        Either<string, Exception> either2 = new Exception();
        Either<string, Exception> either3 = new Exception();
        Either<string, Exception> either4 = new Exception();

        var result = Either.Combine(either1, either2, either3, either4);

        Assert.False(result.IsLeft);
        Assert.IsType<Exception>(result.Right.Right1);
        Assert.IsType<Exception>(result.Right.Right2);
        Assert.IsType<Exception>(result.Right.Right3);
        Assert.IsType<Exception>(result.Right.Right4);
    }

    [Fact]
    public void Given_five_eithers_with_state_success_when_apply_combine_return_all_results()
    {
        Either<string, Exception> either1 = "test1";
        Either<string, Exception> either2 = "test2";
        Either<string, Exception> either3 = "test3";
        Either<string, Exception> either4 = "test4";
        Either<string, Exception> either5 = "test5";

        var result = Either.Combine(either1, either2, either3, either4, either5);

        Assert.True(result.IsLeft);
        Assert.Equal("test1", result.Left.Left1);
        Assert.Equal("test2", result.Left.Left2);
        Assert.Equal("test3", result.Left.Left3);
        Assert.Equal("test4", result.Left.Left4);
        Assert.Equal("test5", result.Left.Left5);
    }

    [Fact]
    public void Given_five_eithers_with_state_fail_when_apply_combine_return_all_exceptions()
    {
        Either<string, Exception> either1 = new Exception();
        Either<string, Exception> either2 = new Exception();
        Either<string, Exception> either3 = new Exception();
        Either<string, Exception> either4 = new Exception();
        Either<string, Exception> either5 = new Exception();

        var result = Either.Combine(either1, either2, either3, either4, either5);

        Assert.False(result.IsLeft);
        Assert.IsType<Exception>(result.Right.Right1);
        Assert.IsType<Exception>(result.Right.Right2);
        Assert.IsType<Exception>(result.Right.Right3);
        Assert.IsType<Exception>(result.Right.Right4);
        Assert.IsType<Exception>(result.Right.Right5);
    }

    [Fact]
    public void Given_six_eithers_with_state_success_when_apply_combine_return_all_results()
    {
        Either<string, Exception> either1 = "test1";
        Either<string, Exception> either2 = "test2";
        Either<string, Exception> either3 = "test3";
        Either<string, Exception> either4 = "test4";
        Either<string, Exception> either5 = "test5";
        Either<string, Exception> either6 = "test6";

        var result = Either.Combine(either1, either2, either3, either4, either5, either6);

        Assert.True(result.IsLeft);
        Assert.Equal("test1", result.Left.Left1);
        Assert.Equal("test2", result.Left.Left2);
        Assert.Equal("test3", result.Left.Left3);
        Assert.Equal("test4", result.Left.Left4);
        Assert.Equal("test5", result.Left.Left5);
        Assert.Equal("test6", result.Left.Left6);
    }

    [Fact]
    public void Given_six_eithers_with_state_fail_when_apply_combine_return_all_exceptions()
    {
        Either<string, Exception> either1 = new Exception();
        Either<string, Exception> either2 = new Exception();
        Either<string, Exception> either3 = new Exception();
        Either<string, Exception> either4 = new Exception();
        Either<string, Exception> either5 = new Exception();
        Either<string, Exception> either6 = new Exception();

        var result = Either.Combine(either1, either2, either3, either4, either5, either6);

        Assert.False(result.IsLeft);
        Assert.IsType<Exception>(result.Right.Right1);
        Assert.IsType<Exception>(result.Right.Right2);
        Assert.IsType<Exception>(result.Right.Right3);
        Assert.IsType<Exception>(result.Right.Right4);
        Assert.IsType<Exception>(result.Right.Right5);
        Assert.IsType<Exception>(result.Right.Right6);
    }
}