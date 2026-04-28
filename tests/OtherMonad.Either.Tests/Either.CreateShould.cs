namespace OtherMonad.Either.Tests;

[Trait("Either", "Create")]
public class EitherCreateShould
{
    [Fact]
    public void GivenEitherWithSameGenericsValuesWhenApplyCreateLeftReturnEitherWithLeftSetter()
    {
        var result = Either<string, string>.Create.Left("l");

        Assert.True(result.IsLeft);
        Assert.False(result.IsRight);
        Assert.NotNull(result.Left);
        Assert.Throws<InvalidOperationException>(() => _ = result.Right);
    }

    [Fact]
    public void GivenEitherWithSameGenericsValuesWhenApplyCreateRightReturnEitherWithRightSetter()
    {
        var result = Either<string, string>.Create.Right("r");

        Assert.False(result.IsLeft);
        Assert.True(result.IsRight);
        Assert.NotNull(result.Right);
        Assert.Throws<InvalidOperationException>(() => _ = result.Left);
    }

    [Fact]
    public void GivenEitherWithSameGenericsValuesWhenApplyCreateLeftThrowArgumentnullexception()
    {
        Assert.Throws<ArgumentNullException>(() => Either<string, string>.Create.Left(null!));
    }

    [Fact]
    public void GivenEitherWithSameGenericsValuesWhenApplyCreateRightThrowArgumentnullexception()
    {
        Assert.Throws<ArgumentNullException>(() => Either<string, string>.Create.Right(null!));
    }
}