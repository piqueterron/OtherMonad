namespace OtherMonad.Either.Tests;

[Trait("Either", "Create")]
public class EitherCreateShould
{
    [Fact]
    public void GivenEitherWithSameGenericsValuesWhenApplyCreateLeftReturnEitherWithLeftSetter()
    {
        var result = Either<string, string>.Create.Left("l");

        Assert.True(result.IsLeft);
        Assert.NotNull(result.Left);
        Assert.Null(result.Right);
    }

    [Fact]
    public void GivenEitherWithSameGenericsValuesWhenApplyCreateRightReturnEitherWithRightSetter()
    {
        var result = Either<string, string>.Create.Right("r");

        Assert.False(result.IsLeft);
        Assert.Null(result.Left);
        Assert.NotNull(result.Right);
    }

    [Fact]
    public void GivenEitherWithSameGenericsValuesWhenApplyCreateLeftThrowArgumentnullexception()
    {
        Assert.Throws<ArgumentNullException>(() => Either<string, string>.Create.Left(null));
    }

    [Fact]
    public void GivenEitherWithSameGenericsValuesWhenApplyCreateRightThrowArgumentnullexception()
    {
        Assert.Throws<ArgumentNullException>(() => Either<string, string>.Create.Right(null));
    }
}