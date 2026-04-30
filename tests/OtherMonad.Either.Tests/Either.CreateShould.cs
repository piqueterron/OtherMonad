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

    [Fact]
    public void GivenEitherWithIntWhenApplyCreateRightReturnEitherWithRightValue()
    {
        var value = 42;
        var result = Either<Exception, int>.Create.Right(value);

        Assert.True(result.IsRight);
        Assert.False(result.IsLeft);
        Assert.Equal(value, result.Right);
        Assert.Throws<InvalidOperationException>(() => _ = result.Left);
    }

    [Fact]
    public void GivenEitherWithIntWhenApplyCreateLeftReturnEitherWithLeftValue()
    {
        var error = new Exception("test error");
        var result = Either<Exception, int>.Create.Left(error);

        Assert.True(result.IsLeft);
        Assert.False(result.IsRight);
        Assert.Same(error, result.Left);
        Assert.Throws<InvalidOperationException>(() => _ = result.Right);
    }

    [Fact]
    public void GivenEitherWithBoolWhenApplyCreateRightReturnTrue()
    {
        var result = Either<string, bool>.Create.Right(true);

        Assert.True(result.IsRight);
        Assert.True(result.Right);
    }

    [Fact]
    public void GivenEitherWithBoolWhenApplyCreateRightReturnFalse()
    {
        var result = Either<string, bool>.Create.Right(false);

        Assert.True(result.IsRight);
        Assert.False(result.Right);
    }

    [Fact]
    public void GivenEitherWithStructWhenApplyCreateRightReturnEitherWithStruct()
    {
        var value = new DummyStruct { Id = 1, Name = "Test" };
        var result = Either<Exception, DummyStruct>.Create.Right(value);

        Assert.True(result.IsRight);
        Assert.Equal(value, result.Right);
    }

    [Fact]
    public void GivenEitherWithZeroWhenApplyCreateRightReturnEitherWithZero()
    {
        var result = Either<string, int>.Create.Right(0);

        Assert.True(result.IsRight);
        Assert.Equal(0, result.Right);
    }

    [Fact]
    public void GivenEitherWithEmptyStringWhenApplyCreateRightReturnEitherWithEmptyString()
    {
        var result = Either<Exception, string>.Create.Right("");

        Assert.True(result.IsRight);
        Assert.Equal("", result.Right);
    }

    [Fact]
    public void GivenEitherWithEmptyStringWhenApplyCreateLeftReturnEitherWithEmptyString()
    {
        var result = Either<string, int>.Create.Left("");

        Assert.True(result.IsLeft);
        Assert.Equal("", result.Left);
    }

    [Fact]
    public void GivenEitherWithDifferentErrorTypesWhenApplyCreateLeftReturnCorrectErrorType()
    {
        var error = new ArgumentException("argument error");
        var result = Either<ArgumentException, string>.Create.Left(error);

        Assert.True(result.IsLeft);
        Assert.IsType<ArgumentException>(result.Left);
        Assert.Equal("argument error", result.Left.Message);
    }

    [Fact]
    public void GivenEitherWithComplexRightTypeWhenApplyCreateRightReturnComplexObject()
    {
        var value = new { Id = 1, Name = "Complex" };
        var result = Either<string, object>.Create.Right(value);

        Assert.True(result.IsRight);
        Assert.NotNull(result.Right);
    }

    [Fact]
    public void GivenMultipleCreationsWhenApplyCreateReturnIndependentInstances()
    {
        var either1 = Either<string, int>.Create.Right(1);
        var either2 = Either<string, int>.Create.Right(2);

        Assert.NotEqual(either1.Right, either2.Right);
    }

    [Fact]
    public void GivenEitherWithSameTypeLeftAndRightWhenCreateLeftThenIsLeftIsTrue()
    {
        var result = Either<int, int>.Create.Left(100);

        Assert.True(result.IsLeft);
        Assert.False(result.IsRight);
        Assert.Equal(100, result.Left);
    }

    [Fact]
    public void GivenEitherWithSameTypeLeftAndRightWhenCreateRightThenIsRightIsTrue()
    {
        var result = Either<int, int>.Create.Right(200);

        Assert.False(result.IsLeft);
        Assert.True(result.IsRight);
        Assert.Equal(200, result.Right);
    }

    [Fact]
    public void GivenEitherWithCustomErrorTypeWhenApplyCreateLeftReturnCustomError()
    {
        var error = new CustomError { Code = 404, Message = "Not Found" };
        var result = Either<CustomError, string>.Create.Left(error);

        Assert.True(result.IsLeft);
        Assert.Equal(404, result.Left.Code);
        Assert.Equal("Not Found", result.Left.Message);
    }

    public struct DummyStruct : IEquatable<DummyStruct>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public bool Equals(DummyStruct other)
        {
            return Id == other.Id && Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            return obj is DummyStruct other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }

    public class CustomError
    {
        public int Code { get; set; }
        public string? Message { get; set; }
    }
}