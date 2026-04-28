namespace OtherMonad.Either.Tests;

[Trait("Either", "Equality")]
public class EitherEqualityShould
{
    [Fact]
    public void GivenTwoRightEithersWithSameValueWhenApplyEqualityReturnTrue()
    {
        var a = Either<Exception, string>.Create.Right("hello");
        var b = Either<Exception, string>.Create.Right("hello");

        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact]
    public void GivenTwoRightEithersWithDifferentValueWhenApplyEqualityReturnFalse()
    {
        var a = Either<Exception, string>.Create.Right("hello");
        var b = Either<Exception, string>.Create.Right("world");

        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void GivenTwoLeftEithersWithSameValueWhenApplyEqualityReturnTrue()
    {
        var error = new Exception("err");
        var a = Either<Exception, string>.Create.Left(error);
        var b = Either<Exception, string>.Create.Left(error);

        Assert.True(a.Equals(b));
        Assert.True(a == b);
    }

    [Fact]
    public void GivenLeftAndRightEitherWhenApplyEqualityReturnFalse()
    {
        var a = Either<string, string>.Create.Left("x");
        var b = Either<string, string>.Create.Right("x");

        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void GivenEitherWhenEqualsObjectReturnTrueForSameValue()
    {
        var a = Either<Exception, string>.Create.Right("ok");
        object b = Either<Exception, string>.Create.Right("ok");

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void GivenEitherWhenEqualsObjectReturnFalseForDifferentType()
    {
        var a = Either<Exception, string>.Create.Right("ok");

        Assert.False(a.Equals("ok"));
    }

    [Fact]
    public void GivenTwoEqualEithersWhenGetHashCodeReturnSameHash()
    {
        var a = Either<Exception, string>.Create.Right("hash");
        var b = Either<Exception, string>.Create.Right("hash");

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void GivenLeftAndRightEithersWithSameStringWhenGetHashCodeReturnDifferentHash()
    {
        var a = Either<string, string>.Create.Left("x");
        var b = Either<string, string>.Create.Right("x");

        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
    }
}
