namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "OrElse")]
public class MaybeIfShould
{
    [Fact]
    public void GivenMaybeOfNoneWhenApplyOrelseReturnDefaultValue()
    {
        var expected = "default";
        Maybe<string> @object = "test";

        var result = @object.Bind<string, string>(e => Maybe<string>.None)
            .OrElse(expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyOrelseReturnExpectedMaybeOfString()
    {
        Maybe<string> @object = "test";

        var result = @object.Map(e => "test").OrElse("default");

        Assert.True(result.HasValue);
        Assert.Equal(@object, result);
    }

    [Fact]
    public async Task GivenMaybeOfNoneWhenApplyOrelseFromTaskReturnDefaultValue()
    {
        Maybe<string> @object = null!;
        var expected = "default";

        var result = await @object.Map((e, ct) => Task.FromResult($"{e}-1"), TestContext.Current.CancellationToken)
            .OrElse(expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task GivenMaybeOfNoneWhenApplyOrelseFromTaskReturnMaybeOfString()
    {
        Maybe<string> @object = "test";

        var result = await @object.Map((e, ct) => Task.FromResult($"{e}-1"), TestContext.Current.CancellationToken)
            .OrElse("default");

        Assert.True(result.HasValue);
        Assert.Equal("test-1", result.Value);
    }

    [Fact]
    public void GivenMaybeOfNoneWhenApplyOrelsedeferredReturnDefaultValue()
    {
        var expected = "default";
        Maybe<string> @object = "test";

        var deferred = @object.BindDefer<string, string>(e => Maybe<string>.None)
            .OrElseDefer(expected);

        var result = deferred();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyOrelsedeferredReturnExpectedMaybeOfString()
    {
        Maybe<string> @object = "test";

        var deferred = @object.MapDefer(e => "test").OrElseDefer("default");
        var result = deferred();

        Assert.True(result.HasValue);
        Assert.Equal(@object, result);
    }

    [Fact]
    public async Task GivenMaybeOfNoneWhenApplyOrelsedeferredFromTaskReturnDefaultValue()
    {
        Maybe<string> @object = null;
        var expected = "default";

        var deferred = @object.MapDefer((e, ct) => Task.FromResult($"{e}-1"),  TestContext.Current.CancellationToken)
            .OrElseDefer(expected);

        var result = await deferred();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task GivenMaybeOfNoneWhenApplyOrelsedeferredFromTaskReturnMaybeOfString()
    {
        Maybe<string> @object = "test";

        var deferred = @object.MapDefer((e, ct) => Task.FromResult($"{e}-1"), TestContext.Current.CancellationToken)
            .OrElseDefer("default");

        var result = await deferred();

        Assert.True(result.HasValue);
        Assert.Equal("test-1", result.Value);
    }

    [Fact]
    public void GivenMaybeWhenApplyOrelsedeferredReturnDefaultValue()
    {
        var expected = "default";
        Maybe<string> @object = null;

        var deferred = @object.OrElseDefer(expected);

        var result = deferred();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeWithValueWhenApplyOrElseReturnOriginalValue()
    {
        Maybe<int> @object = 42;

        var result = @object.OrElse(100);

        Assert.True(result.HasValue);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void GivenMaybeNoneWhenApplyOrElseReturnDefaultValue()
    {
        var @object = Maybe<int>.None;

        var result = @object.OrElse(100);

        Assert.True(result.HasValue);
        Assert.Equal(100, result.Value);
    }

    [Fact]
    public void GivenMaybeOfZeroWhenApplyOrElseReturnZero()
    {
        Maybe<int> @object = 0;

        var result = @object.OrElse(100);

        Assert.True(result.HasValue);
        Assert.Equal(0, result.Value);
    }

    [Fact]
    public void GivenMaybeOfEmptyStringWhenApplyOrElseReturnEmptyString()
    {
        Maybe<string> @object = "";

        var result = @object.OrElse("default");

        Assert.True(result.HasValue);
        Assert.Equal("", result.Value);
    }

    [Fact]
    public void GivenMaybeOfBoolFalseWhenApplyOrElseReturnFalse()
    {
        Maybe<bool> @object = false;

        var result = @object.OrElse(true);

        Assert.True(result.HasValue);
        Assert.False(result.Value);
    }

    [Fact]
    public void GivenMaybeOfStructWhenApplyOrElseReturnOriginalStruct()
    {
        var expected = new DummyStruct { Id = 1, Name = "Original" };
        Maybe<DummyStruct> @object = expected;

        var result = @object.OrElse(new DummyStruct { Id = 2, Name = "Default" });

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeNoneOfStructWhenApplyOrElseReturnDefaultStruct()
    {
        var expected = new DummyStruct { Id = 2, Name = "Default" };
        var @object = Maybe<DummyStruct>.None;

        var result = @object.OrElse(expected);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task GivenTaskMaybeWithValueWhenApplyOrElseReturnOriginalValue()
    {
        var task = Task.FromResult<Maybe<int>>(42);

        var result = await task.OrElse(100);

        Assert.True(result.HasValue);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public async Task GivenTaskMaybeNoneWhenApplyOrElseReturnDefaultValue()
    {
        var task = Task.FromResult(Maybe<int>.None);

        var result = await task.OrElse(100);

        Assert.True(result.HasValue);
        Assert.Equal(100, result.Value);
    }

    [Fact]
    public void GivenChainOfOrElseWhenFirstHasValueReturnFirst()
    {
        Maybe<string> @object = "first";

        var result = @object.OrElse("second").OrElse("third");

        Assert.True(result.HasValue);
        Assert.Equal("first", result.Value);
    }

    [Fact]
    public void GivenChainOfOrElseWhenFirstIsNoneReturnSecond()
    {
        var @object = Maybe<string>.None;

        var result = @object.OrElse("second").OrElse("third");

        Assert.True(result.HasValue);
        Assert.Equal("second", result.Value);
    }

    [Fact]
    public void GivenDeferredMaybeWithValueWhenApplyOrElseDeferReturnOriginalValue()
    {
        Maybe<int> @object = 42;
        var deferred = @object.MapDefer(x => x * 2);

        var result = deferred.OrElseDefer(100)();

        Assert.True(result.HasValue);
        Assert.Equal(84, result.Value);
    }

    [Fact]
    public void GivenDeferredMaybeNoneWhenApplyOrElseDeferReturnDefault()
    {
        var @object = Maybe<int>.None;
        var deferred = @object.MapDefer(x => x * 2);

        var result = deferred.OrElseDefer(100)();

        Assert.True(result.HasValue);
        Assert.Equal(100, result.Value);
    }

    [Fact]
    public async Task GivenDeferredTaskMaybeWithValueWhenApplyOrElseDeferReturnOriginalValue()
    {
        Maybe<int> @object = 5;
        var deferred = @object.MapDefer((x, ct) => Task.FromResult(x * 10), TestContext.Current.CancellationToken);

        var result = await deferred.OrElseDefer(999)();

        Assert.True(result.HasValue);
        Assert.Equal(50, result.Value);
    }

    [Fact]
    public async Task GivenDeferredTaskMaybeNoneWhenApplyOrElseDeferReturnDefault()
    {
        var @object = Maybe<int>.None;
        var deferred = @object.MapDefer((x, ct) => Task.FromResult(x * 10), TestContext.Current.CancellationToken);

        var result = await deferred.OrElseDefer(999)();

        Assert.True(result.HasValue);
        Assert.Equal(999, result.Value);
    }

    [Fact]
    public void GivenMaybeWithNullDefaultWhenApplyOrElseReturnNone()
    {
        var @object = Maybe<string>.None;

        var result = @object.OrElse(null);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public void GivenComplexChainWithBindAndOrElseReturnExpectedResult()
    {
        Maybe<int> @object = 5;

        var result = @object
            .Bind(x => x > 10 ? (Maybe<int>)(x * 2) : Maybe<int>.None)
            .OrElse(100)
            .Map(x => x + 50);

        Assert.True(result.HasValue);
        Assert.Equal(150, result.Value);
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