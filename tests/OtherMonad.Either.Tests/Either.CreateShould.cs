namespace OtherMonad.Either.Tests;

[Trait("Either", "Create")]
public class EitherCreateShould
{
    [Fact]
    public void Given_either_with_same_generics_values_when_apply_create_left_return_either_with_left_setter()
    {
        var result = Either<string, string>.Create.Left("l");

        Assert.True(result.IsLeft);
        Assert.NotNull(result.Left);
        Assert.Null(result.Right);
    }

    [Fact]
    public void Given_either_with_same_generics_values_when_apply_create_right_return_either_with_right_setter()
    {
        var result = Either<string, string>.Create.Right("r");

        Assert.False(result.IsLeft);
        Assert.Null(result.Left);
        Assert.NotNull(result.Right);
    }

    [Fact]
    public void Given_either_with_same_generics_values_when_apply_create_left_throw_argumentnullexception()
    {
        Assert.Throws<ArgumentNullException>(() => Either<string, string>.Create.Left(null));
    }

    [Fact]
    public void Given_either_with_same_generics_values_when_apply_create_right_throw_argumentnullexception()
    {
        Assert.Throws<ArgumentNullException>(() => Either<string, string>.Create.Right(null));
    }
}