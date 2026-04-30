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

    [Fact]
    public void GivenTwoLeftEithersWithDifferentValuesWhenApplyEqualityReturnFalse()
    {
        var error1 = new Exception("err1");
        var error2 = new Exception("err2");
        var a = Either<Exception, string>.Create.Left(error1);
        var b = Either<Exception, string>.Create.Left(error2);

        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void GivenTwoRightEithersWithValueTypesWhenApplyEqualityReturnTrue()
    {
        var a = Either<string, int>.Create.Right(42);
        var b = Either<string, int>.Create.Right(42);

        Assert.True(a.Equals(b));
        Assert.True(a == b);
    }

    [Fact]
    public void GivenTwoRightEithersWithStructsWhenApplyEqualityReturnTrue()
    {
        var value = new DummyStruct { Id = 1, Name = "Test" };
        var a = Either<string, DummyStruct>.Create.Right(value);
        var b = Either<string, DummyStruct>.Create.Right(value);

        Assert.True(a.Equals(b));
        Assert.True(a == b);
    }

    [Fact]
    public void GivenTwoRightEithersWithDifferentStructsWhenApplyEqualityReturnFalse()
    {
        var a = Either<string, DummyStruct>.Create.Right(new DummyStruct { Id = 1, Name = "A" });
        var b = Either<string, DummyStruct>.Create.Right(new DummyStruct { Id = 2, Name = "B" });

        Assert.False(a.Equals(b));
        Assert.False(a == b);
    }

    [Fact]
    public void GivenTwoRightEithersWithBoolWhenApplyEqualityReturnCorrectResult()
    {
        var a = Either<string, bool>.Create.Right(true);
        var b = Either<string, bool>.Create.Right(true);
        var c = Either<string, bool>.Create.Right(false);

        Assert.True(a.Equals(b));
        Assert.False(a.Equals(c));
    }

    [Fact]
    public void GivenEitherWhenEqualsNullReturnFalse()
    {
        var a = Either<Exception, string>.Create.Right("ok");

        Assert.False(a.Equals(null));
    }

    [Fact]
    public void GivenEitherWhenEqualsSelfReturnTrue()
    {
        var a = Either<Exception, string>.Create.Right("self");

        Assert.True(a.Equals(a));
        Assert.True(a == a);
    }

    [Fact]
    public void GivenTwoLeftEithersWithSameStructWhenApplyEqualityReturnTrue()
    {
        var error = new DummyStruct { Id = 1, Name = "Error" };
        var a = Either<DummyStruct, string>.Create.Left(error);
        var b = Either<DummyStruct, string>.Create.Left(error);

        Assert.True(a.Equals(b));
        Assert.True(a == b);
    }

    [Fact]
    public void GivenTwoRightEithersWithZeroWhenGetHashCodeReturnSameHash()
    {
        var a = Either<string, int>.Create.Right(0);
        var b = Either<string, int>.Create.Right(0);

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void GivenTwoLeftEithersWithEmptyStringWhenApplyEqualityReturnTrue()
    {
        var a = Either<string, int>.Create.Left("");
        var b = Either<string, int>.Create.Left("");

        Assert.True(a.Equals(b));
        Assert.True(a == b);
    }

    [Fact]
    public void GivenTwoRightEithersWithEmptyStringWhenApplyEqualityReturnTrue()
    {
        var a = Either<Exception, string>.Create.Right("");
        var b = Either<Exception, string>.Create.Right("");

        Assert.True(a.Equals(b));
        Assert.True(a == b);
    }

    [Fact]
    public void GivenTwoRightEithersWithDifferentValuesWhenGetHashCodeMayReturnDifferentHash()
    {
        var a = Either<string, int>.Create.Right(1);
        var b = Either<string, int>.Create.Right(2);

        var hash1 = a.GetHashCode();
        var hash2 = b.GetHashCode();

        Assert.NotEqual(0, hash1);
        Assert.NotEqual(0, hash2);
    }

    [Fact]
    public void GivenEitherRightWhenToStringContainsRightValue()
    {
        var either = Either<string, int>.Create.Right(42);

        var result = either.ToString();

        Assert.Contains("42", result);
        Assert.Contains("Right", result, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void GivenEitherLeftWhenToStringContainsLeftValue()
    {
        var either = Either<Exception, string>.Create.Left(new Exception("error"));

        var result = either.ToString();

        Assert.Contains("Left", result, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void GivenTwoRightEithersWithSameValueButDifferentLeftTypesWhenNotComparable()
    {
        var a = Either<Exception, string>.Create.Right("same");
        var b = Either<ArgumentException, string>.Create.Right("same");

        // Different left types mean different Either types, can't compare directly
        // This test just verifies both are Right with same value
        Assert.True(a.IsRight);
        Assert.True(b.IsRight);
        Assert.Equal("same", a.Right);
        Assert.Equal("same", b.Right);
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
}
