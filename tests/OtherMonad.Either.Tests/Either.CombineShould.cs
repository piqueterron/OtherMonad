namespace Monads.Either.Tests;

using OtherMonad;

[Trait("Either", "Combine")]
public class EitherCombineShould
{
    [Fact]
    public void GivenTwoEithersWithStateSuccessWhenApplyCombineReturnAllResults()
    {
        var expected = "test1test2";

        var either1 = Either<Exception, string>.Create.Right("test1");
        var either2 = Either<Exception, string>.Create.Right("test2");

        var result = either1.Combine(either2, (x, y) => new AggregateException(x!, y!), (x, y) => x + y);

        Assert.True(result.IsRight);
        Assert.Equal(expected, result.Right);
    }

    [Fact]
    public void GivenTwoEithersWithStateFailWhenApplyCombineReturnAllExceptions()
    {
        var msg1 = "Exception either 1";
        var msg2 = "Exception either 2";

        var either1 = Either<Exception, string>.Create.Left(new Exception(msg1));
        var either2 = Either<Exception, string>.Create.Left(new Exception(msg2));

        var result = either1.Combine(either2, (x, y) => new AggregateException(x, y), (x, y) => x + y);

        Assert.False(result.IsRight);
        Assert.IsType<AggregateException>(result.Left);
        Assert.Equal(msg1, result.Left.InnerExceptions[0].Message);
        Assert.Equal(msg2, result.Left.InnerExceptions[1].Message);
    }

    [Fact]
    public void GivenTwoEithersWithStateFailWhenApplyCombineReturnDistinctExceptions()
    {
        var msg1 = "Exception either 1";
        var msg2 = "ArgumentException either 2";

        var either1 = Either<Exception, string>.Create.Left(new Exception(msg1));
        var either2 = Either<ArgumentException, string>.Create.Left(new ArgumentException(msg2));

        var result = either1.Combine(either2, (x, y) => new AggregateException(x, y), (x, y) => x + y);

        Assert.False(result.IsRight);
        Assert.IsType<AggregateException>(result.Left);
        Assert.Equal(msg1, result.Left.InnerExceptions[0].Message);
        Assert.Equal(msg2, result.Left.InnerExceptions[1].Message);
    }

    [Fact]
    public void GivenTwoEithersWithOneStateSuccessAndOtherFailWhenApplyCombineReturnFailure()
    {
        var msg1 = "Exception either 1";

        var either1 = Either<Exception, string>.Create.Left(new Exception(msg1));
        var either2 = Either<Exception, string>.Create.Right("test");

        var result = either1.Combine(either2,
            (x, y) =>
                x is not null && y is not null
                    ? new AggregateException(x, y)
                    : x is not null
                        ? new AggregateException(x)
                        : new AggregateException(y!),
            (x, y) => x + y);

        Assert.False(result.IsRight);
        Assert.IsType<AggregateException>(result.Left);
        Assert.Equal(msg1, result.Left.InnerExceptions[0].Message);
    }
}