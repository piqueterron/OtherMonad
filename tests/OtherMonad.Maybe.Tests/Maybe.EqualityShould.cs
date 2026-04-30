namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Equality")]
public class MaybeEqualityShould
{
    [Fact]
    public void GivenObjectToApplyEqualityReturnEquals()
    {
        var result = (object)Maybe<int>.None;

        Assert.True(Maybe<int>.None.Equals(result));
    }

    [Fact]
    public void GivenObjectToApplyEqualityReturnNoEquals()
    {
        var result = (object)Maybe<string>.None;

        Assert.False(Maybe<int>.None.Equals(result));
    }

    [Fact]
    public void GivenTwoMaybesToApplyEqualityReturnEquals()
    {
        var result = Maybe<int>.None;

        Assert.Equal(Maybe<int>.None, result);
    }

    [Fact]
    public void GivenTwoMaybesToApplyEqualityReturnNoEquals()
    {
        Maybe<int> result = 10;

        Assert.NotEqual(Maybe<int>.None, result);
    }

    [Fact]
    public void GivenMaybeOfZeroIntAndMaybeNoneIntWhenApplyEqualityOperatorReturnNotEquals()
    {
        Maybe<int> result = 0;

        Assert.False(Maybe<int>.None == result);
    }

    [Fact]
    public void GivenTwoMaybesToApplyEqualityOperatorReturnEquals()
    {
        Maybe<int> result = Maybe<int>.None;

        Assert.True(Maybe<int>.None == result);
    }

    [Fact]
    public void GivenTwoMaybesToApplyEqualityOperatorReturnNoEquals()
    {
        Maybe<int> result = 10;

        Assert.True(Maybe<int>.None != result);
    }

    [Fact]
    public void GivenTwoMaybesWithSameValueWhenCompareReturnEquals()
    {
        Maybe<int> maybe1 = 42;
        Maybe<int> maybe2 = 42;

        Assert.Equal(maybe1, maybe2);
        Assert.True(maybe1 == maybe2);
    }

    [Fact]
    public void GivenTwoMaybesWithDifferentValuesWhenCompareReturnNotEquals()
    {
        Maybe<int> maybe1 = 42;
        Maybe<int> maybe2 = 43;

        Assert.NotEqual(maybe1, maybe2);
        Assert.True(maybe1 != maybe2);
    }

    [Fact]
    public void GivenTwoMaybesNoneWhenCompareWithOperatorReturnEquals()
    {
        var maybe1 = Maybe<int>.None;
        var maybe2 = Maybe<int>.None;

        Assert.True(maybe1 == maybe2);
        Assert.False(maybe1 != maybe2);
    }

    [Fact]
    public void GivenTwoMaybesWithSameStringValueWhenCompareReturnEquals()
    {
        Maybe<string> maybe1 = "test";
        Maybe<string> maybe2 = "test";

        Assert.Equal(maybe1, maybe2);
        Assert.True(maybe1.Equals(maybe2));
    }

    [Fact]
    public void GivenTwoMaybesWithDifferentStringValuesWhenCompareReturnNotEquals()
    {
        Maybe<string> maybe1 = "test";
        Maybe<string> maybe2 = "other";

        Assert.NotEqual(maybe1, maybe2);
        Assert.False(maybe1.Equals(maybe2));
    }

    [Fact]
    public void GivenMaybeWithValueAndNullObjectWhenCompareReturnNotEquals()
    {
        Maybe<int> maybe = 42;
        object? nullObject = null;

        Assert.False(maybe.Equals(nullObject));
    }

    [Fact]
    public void GivenMaybeNoneAndObjectNoneWhenCompareReturnEquals()
    {
        var maybe = Maybe<string>.None;
        object objMaybe = Maybe<string>.None;

        Assert.True(maybe.Equals(objMaybe));
    }

    [Fact]
    public void GivenTwoMaybesWithSameValueWhenGetHashCodeReturnSameHash()
    {
        Maybe<int> maybe1 = 42;
        Maybe<int> maybe2 = 42;

        Assert.Equal(maybe1.GetHashCode(), maybe2.GetHashCode());
    }

    [Fact]
    public void GivenTwoMaybesNoneWhenGetHashCodeReturnSameHash()
    {
        var maybe1 = Maybe<int>.None;
        var maybe2 = Maybe<int>.None;

        Assert.Equal(maybe1.GetHashCode(), maybe2.GetHashCode());
    }

    [Fact]
    public void GivenTwoMaybesWithDifferentValuesWhenGetHashCodeMayReturnDifferentHash()
    {
        Maybe<int> maybe1 = 42;
        Maybe<int> maybe2 = 43;

        // Los hash codes pueden ser diferentes (pero no es garantizado)
        // Solo verificamos que se pueden calcular sin error
        var hash1 = maybe1.GetHashCode();
        var hash2 = maybe2.GetHashCode();

        Assert.NotEqual(0, hash1);
        Assert.NotEqual(0, hash2);
    }

    [Fact]
    public void GivenMaybeWithNullStringWhenCompareWithNoneReturnEquals()
    {
        Maybe<string> maybe1 = null;
        var maybe2 = Maybe<string>.None;

        Assert.Equal(maybe1, maybe2);
        Assert.True(maybe1 == maybe2);
    }

    [Fact]
    public void GivenMaybeOfStructWithSameValuesWhenCompareReturnEquals()
    {
        Maybe<DummyStruct> maybe1 = new DummyStruct { Id = 1, Name = "test" };
        Maybe<DummyStruct> maybe2 = new DummyStruct { Id = 1, Name = "test" };

        Assert.Equal(maybe1, maybe2);
        Assert.True(maybe1 == maybe2);
    }

    [Fact]
    public void GivenMaybeOfStructWithDifferentValuesWhenCompareReturnNotEquals()
    {
        Maybe<DummyStruct> maybe1 = new DummyStruct { Id = 1, Name = "test" };
        Maybe<DummyStruct> maybe2 = new DummyStruct { Id = 2, Name = "test" };

        Assert.NotEqual(maybe1, maybe2);
        Assert.True(maybe1 != maybe2);
    }

    [Fact]
    public void GivenMaybeOfBoolTrueAndFalseWhenCompareReturnNotEquals()
    {
        Maybe<bool> maybe1 = true;
        Maybe<bool> maybe2 = false;

        Assert.NotEqual(maybe1, maybe2);
        Assert.False(maybe1 == maybe2);
    }

    [Fact]
    public void GivenMaybeOfEmptyStringAndNullWhenCompareReturnNotEquals()
    {
        Maybe<string> maybe1 = "";
        Maybe<string> maybe2 = null;

        Assert.NotEqual(maybe1, maybe2);
        Assert.True(maybe1 != maybe2);
    }

    [Fact]
    public void GivenMaybeToStringWhenHasValueReturnExpectedFormat()
    {
        Maybe<int> maybe = 42;

        var result = maybe.ToString();

        Assert.Contains("42", result);
        Assert.Contains("HasValue = true", result);
    }

    [Fact]
    public void GivenMaybeToStringWhenNoneReturnExpectedFormat()
    {
        var maybe = Maybe<int>.None;

        var result = maybe.ToString();

        Assert.Contains("HasValue = false", result);
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