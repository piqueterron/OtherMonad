namespace OtherMonad.Tests;

[Trait("Result", "Create")]
public class ResultCreateShould
{
    [Fact]
    public void GivenResultWhenCreateOkReturnOkState()
    {
        var result = Result<int>.Create.Ok(42);

        Assert.True(result.IsOk);
        Assert.False(result.IsErr);
        Assert.Equal(42, result.Value);
        Assert.Throws<InvalidOperationException>(() => _ = result.Error);
    }

    [Fact]
    public void GivenResultWhenCreateErrReturnErrState()
    {
        var error = new Exception("test error");
        var result = Result<int>.Create.Err(error);

        Assert.True(result.IsErr);
        Assert.False(result.IsOk);
        Assert.Same(error, result.Error);
        Assert.Throws<InvalidOperationException>(() => _ = result.Value);
    }

    [Fact]
    public void GivenResultWhenCreateOkWithNullThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Result<string>.Create.Ok(null!));
    }

    [Fact]
    public void GivenResultWhenCreateErrWithNullThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Result<int>.Create.Err(null!));
    }

    [Fact]
    public void GivenResultOkWithStringWhenCreateReturnCorrectValue()
    {
        var result = Result<string>.Create.Ok("hello");

        Assert.True(result.IsOk);
        Assert.Equal("hello", result.Value);
    }

    [Fact]
    public void GivenResultOkWithZeroWhenCreateReturnZero()
    {
        var result = Result<int>.Create.Ok(0);

        Assert.True(result.IsOk);
        Assert.Equal(0, result.Value);
    }

    [Fact]
    public void GivenResultOkWithEmptyStringWhenCreateReturnEmptyString()
    {
        var result = Result<string>.Create.Ok("");

        Assert.True(result.IsOk);
        Assert.Equal("", result.Value);
    }

    [Fact]
    public void GivenResultOkWithBoolTrueWhenCreateReturnTrue()
    {
        var result = Result<bool>.Create.Ok(true);

        Assert.True(result.IsOk);
        Assert.True(result.Value);
    }

    [Fact]
    public void GivenResultOkWithBoolFalseWhenCreateReturnFalse()
    {
        var result = Result<bool>.Create.Ok(false);

        Assert.True(result.IsOk);
        Assert.False(result.Value);
    }

    [Fact]
    public void GivenResultOkWithStructWhenCreateReturnStruct()
    {
        var value = new DummyStruct { Id = 1, Name = "Test" };
        var result = Result<DummyStruct>.Create.Ok(value);

        Assert.True(result.IsOk);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void GivenResultErrWithDerivedExceptionWhenCreateReturnCorrectType()
    {
        var error = new ArgumentException("argument error");
        var result = Result<string>.Create.Err(error);

        Assert.True(result.IsErr);
        Assert.IsType<ArgumentException>(result.Error);
        Assert.Equal("argument error", result.Error.Message);
    }

    [Fact]
    public void GivenResultWhenImplicitConversionToEitherReturnEquivalentEither()
    {
        var result = Result<int>.Create.Ok(42);

        Either<Exception, int> either = result;

        Assert.True(either.IsRight);
        Assert.Equal(42, either.Right);
    }

    [Fact]
    public void GivenEitherRightWhenImplicitConversionToResultReturnOkState()
    {
        Either<Exception, int> either = Either<Exception, int>.Create.Right(99);

        Result<int> result = either;

        Assert.True(result.IsOk);
        Assert.Equal(99, result.Value);
    }

    [Fact]
    public void GivenEitherLeftWhenImplicitConversionToResultReturnErrState()
    {
        var error = new Exception("fail");
        Either<Exception, int> either = Either<Exception, int>.Create.Left(error);

        Result<int> result = either;

        Assert.True(result.IsErr);
        Assert.Same(error, result.Error);
    }

    [Fact]
    public void GivenResultOkWhenToStringContainsOkAndValue()
    {
        var result = Result<int>.Create.Ok(42);

        var str = result.ToString();

        Assert.Contains("Ok", str, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("42", str);
    }

    [Fact]
    public void GivenResultErrWhenToStringContainsErr()
    {
        var result = Result<int>.Create.Err(new Exception("error"));

        var str = result.ToString();

        Assert.Contains("Err", str, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void GivenResultImplementsIResultInterface()
    {
        IResult<int> result = Result<int>.Create.Ok(5);

        Assert.True(result.IsOk);
        Assert.True(result.IsRight);
        Assert.Equal(5, result.Value);
        Assert.Equal(5, result.Right);
    }

    [Fact]
    public void GivenResultErrImplementsIResultInterface()
    {
        var error = new Exception("err");
        IResult<int> result = Result<int>.Create.Err(error);

        Assert.True(result.IsErr);
        Assert.True(result.IsLeft);
        Assert.Same(error, result.Error);
        Assert.Same(error, result.Left);
    }

    [Fact]
    public void GivenTwoOkResultsWithSameValueWhenEqualityReturnTrue()
    {
        var a = Result<int>.Create.Ok(1);
        var b = Result<int>.Create.Ok(1);

        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact]
    public void GivenTwoOkResultsWithDifferentValuesWhenEqualityReturnFalse()
    {
        var a = Result<int>.Create.Ok(1);
        var b = Result<int>.Create.Ok(2);

        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void GivenTwoErrResultsWithSameExceptionWhenEqualityReturnTrue()
    {
        var error = new Exception("err");
        var a = Result<int>.Create.Err(error);
        var b = Result<int>.Create.Err(error);

        Assert.True(a.Equals(b));
        Assert.True(a == b);
    }

    [Fact]
    public void GivenOkAndErrResultsWhenEqualityReturnFalse()
    {
        var a = Result<int>.Create.Ok(1);
        var b = Result<int>.Create.Err(new Exception("err"));

        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void GivenTwoEqualOkResultsWhenGetHashCodeReturnSameHash()
    {
        var a = Result<string>.Create.Ok("hello");
        var b = Result<string>.Create.Ok("hello");

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    public struct DummyStruct : IEquatable<DummyStruct>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public bool Equals(DummyStruct other) => Id == other.Id && Name == other.Name;
        public override bool Equals(object? obj) => obj is DummyStruct other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Id, Name);
    }
}
