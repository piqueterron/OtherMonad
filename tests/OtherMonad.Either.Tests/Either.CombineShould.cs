namespace Monads.Either.Tests;

using OtherMonad;

[Trait("Either", "Combine")]
public class EitherCombineShould
{
    [Fact]
    public void Given_two_eithers_with_state_success_when_apply_combine_return_all_results()
    {
        var expected = "test1test2";

        Either<string, Exception> either1 = "test1";
        Either<string, Exception> either2 = "test2";

        var result = either1.Combine(either2, (x, y) => x + y, (x, y) => new AggregateException(x, y));

        Assert.True(result.IsLeft);
        Assert.Equal(expected, result.Left);
    }

    [Fact]
    public void Given_two_eithers_with_state_fail_when_apply_combine_return_all_exceptions()
    {
        var msg1 = "Exception either 1";
        var msg2 = "Exception either 2";

        Either<string, Exception> either1 = new Exception(msg1);
        Either<string, Exception> either2 = new Exception(msg2);

        var result = either1.Combine(either2, (x, y) => x + y, (x, y) => new AggregateException(x, y));

        Assert.False(result.IsLeft);
        Assert.IsType<AggregateException>(result.Right);
        Assert.Equal(msg1, result.Right.InnerExceptions[0].Message);
        Assert.Equal(msg2, result.Right.InnerExceptions[1].Message);
    }

    [Fact]
    public void Given_two_eithers_with_state_fail_when_apply_combine_return_distinct_exceptions()
    {
        var msg1 = "Exception either 1";
        var msg2 = "ArgumentException either 2";

        Either<string, Exception> either1 = new Exception(msg1);
        Either<string, ArgumentException> either2 = new ArgumentException(msg2);

        var result = either1.Combine(either2, (x, y) => x + y, (x, y) => new AggregateException(x, y));

        Assert.False(result.IsLeft);
        Assert.IsType<AggregateException>(result.Right);
        Assert.Equal(msg1, result.Right.InnerExceptions[0].Message);
        Assert.Equal(msg2, result.Right.InnerExceptions[1].Message);
    }

    [Fact]
    public void Given_two_eithers_with_one_state_success_and_other_fail_when_apply_combine_return_all_exceptions()
    {
        var msg1 = "Exception either 1";

        Either<string, Exception> either1 = "test";
        Either<string, Exception> either2 = new Exception(msg1);

        var result = either1.Combine(either2,
            (x, y) => x + y,
            (x, y) =>
                x is not null && y is not null
                    ? new AggregateException(x, y)
                    : x is not null
                        ? new AggregateException(x)
                        : new AggregateException(y));

        Assert.False(result.IsLeft);
        Assert.IsType<AggregateException>(result.Right);
        Assert.Equal(msg1, result.Right.InnerExceptions[0].Message);
    }
}