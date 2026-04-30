namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Wrap")]
public class MaybeWrapShould
{
    [Fact]
    public void GivenStringWhenApplyWrapReturnMaybeOfString()
    {
        var @object = "test";

        var result = @object.Wrap();

        Assert.True(result.HasValue);
        Assert.Equal(@object, result.Value);
    }

    [Fact]
    public void GivenObjectNullWhenApplyWrapReturnMaybeNoneOfObject()
    {
        object @object = null;

        var result = @object.Wrap();

        Assert.False(result.HasValue);
        Assert.Equal(@object, result.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyUnwrapReturnValue()
    {
        Maybe<string> maybe = "test";

        var result = maybe.Unwrap();

        Assert.Equal(result, maybe.Value);
    }

    [Fact]
    public void GivenMaybeNoneWhenApplyUnwrapThrowsInvalidOperationException()
    {
        var maybe = Maybe<string>.None;

        Assert.Throws<InvalidOperationException>(() => maybe.Unwrap());
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyUnwrapDefaultReturnValue()
    {
        Maybe<string> maybe = "test";

        var result = maybe.Unwrap("default");

        Assert.Equal("test", result);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyUnwrapDefaultDontHasValueReturnDefaultValue()
    {
        var expected = "default";

        var maybe = Maybe<string>.None;

        var result = maybe.Unwrap(expected);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GivenIntWhenApplyWrapReturnMaybeOfInt()
    {
        var value = 42;

        var result = value.Wrap();

        Assert.True(result.HasValue);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void GivenNullStringWhenApplyWrapReturnMaybeNone()
    {
        string? value = null;

        var result = value.Wrap();

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public void GivenStructWhenApplyWrapReturnMaybeOfStruct()
    {
        var value = new DummyStruct { Id = 1, Name = "Test" };

        var result = value.Wrap();

        Assert.True(result.HasValue);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void GivenBoolWhenApplyWrapReturnMaybeOfBool()
    {
        var value = true;

        var result = value.Wrap();

        Assert.True(result.HasValue);
        Assert.True(result.Value);
    }

    [Fact]
    public void GivenZeroWhenApplyWrapReturnMaybeOfZero()
    {
        var value = 0;

        var result = value.Wrap();

        Assert.True(result.HasValue);
        Assert.Equal(0, result.Value);
    }

    [Fact]
    public void GivenEmptyStringWhenApplyWrapReturnMaybeOfEmptyString()
    {
        var value = "";

        var result = value.Wrap();

        Assert.True(result.HasValue);
        Assert.Equal("", result.Value);
    }

    [Fact]
    public void GivenMaybeOfIntWhenApplyUnwrapReturnInt()
    {
        Maybe<int> maybe = 100;

        var result = maybe.Unwrap();

        Assert.Equal(100, result);
    }

    [Fact]
    public void GivenMaybeNoneOfIntWhenApplyUnwrapWithDefaultReturnDefault()
    {
        var maybe = Maybe<int>.None;

        var result = maybe.Unwrap(999);

        Assert.Equal(999, result);
    }

    [Fact]
    public void GivenMaybeOfStructWhenApplyUnwrapReturnStruct()
    {
        var expected = new DummyStruct { Id = 5, Name = "Unwrap" };
        Maybe<DummyStruct> maybe = expected;

        var result = maybe.Unwrap();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GivenMaybeNoneOfStructWhenApplyUnwrapThrowsException()
    {
        var maybe = Maybe<DummyStruct>.None;

        Assert.Throws<InvalidOperationException>(() => maybe.Unwrap());
    }

    [Fact]
    public void GivenMaybeNoneOfStructWhenApplyUnwrapWithDefaultReturnDefault()
    {
        var expected = new DummyStruct { Id = 99, Name = "Default" };
        var maybe = Maybe<DummyStruct>.None;

        var result = maybe.Unwrap(expected);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GivenWrapAndUnwrapChainReturnOriginalValue()
    {
        var original = "chain test";

        var result = original.Wrap().Unwrap();

        Assert.Equal(original, result);
    }

    [Fact]
    public void GivenMultipleWrapCallsReturnNestedMaybe()
    {
        var value = 42;

        var result = value.Wrap().Wrap();

        Assert.True(result.HasValue);
        Assert.True(result.Value.HasValue);
        Assert.Equal(42, result.Value.Value);
    }

    [Fact]
    public void GivenMaybeWithValueWhenUnwrapWithDefaultReturnValue()
    {
        Maybe<string> maybe = "has value";

        var result = maybe.Unwrap("default");

        Assert.Equal("has value", result);
    }

    [Fact]
    public void GivenNullObjectWhenApplyWrapAndUnwrapWithDefaultReturnDefault()
    {
        object? value = null;

        var result = value.Wrap().Unwrap(new object());

        Assert.NotNull(result);
    }

    [Fact]
    public void GivenMaybeOfFalseWhenApplyUnwrapReturnFalse()
    {
        Maybe<bool> maybe = false;

        var result = maybe.Unwrap();

        Assert.False(result);
    }

    [Fact]
    public void GivenMaybeOfNullableIntWhenApplyWrapAndUnwrapReturnValue()
    {
        int? value = 123;

        var result = value.Wrap().Unwrap();

        Assert.Equal(123, result);
    }

    [Fact]
    public void GivenMaybeNoneWhenUnwrapThrowsInvalidOperationExceptionWithMessage()
    {
        var maybe = Maybe<int>.None;

        var exception = Assert.Throws<InvalidOperationException>(() => maybe.Unwrap());

        Assert.Contains("no value", exception.Message, StringComparison.OrdinalIgnoreCase);
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