namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Bind")]
public class MaybeBindShould
{
    [Fact]
    public void GivenMaybeOfStringWhenApplyBindReturnExpectedMaybe()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = @object.Bind(e => $"{e}-1");

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeOfIntWhenApplyChainOfBindReturnExpectedMaybe()
    {
        var expected = 10;
        Maybe<int> @object = 2;

        var result = @object.Bind(e => e + 2)
            .Bind(e => e + 2)
            .Bind(e => e + 2)
            .Bind(e => e + 2);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyBindReturnNewMaybeOfObject()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = @object.Bind(e => new Dummy { Value = $"{e}-1" });

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value.Value);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyBindToMaybeOfNoneReturnMaybeNoneOfObject()
    {
        Maybe<string> @object = null;

        var result = @object.Bind(e => new Dummy { Value = $"{e}-1" });

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<Dummy>.None, result);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyBindToMaybeOfNoneReturnMaybeNoneOfString()
    {
        Maybe<string> @object = null;

        var result = @object.Bind(e => $"{e}-1");

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBindFromTaskReturnExpectedMaybe()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBindFromTaskReturnMaybeNoneOfString()
    {
        Maybe<string> @object = null;

        var result = await @object.Bind((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBindFromTaskReturnExpectedMaybeOfString()
    {
        var expected = "test-1";
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult(new Dummy { Value = $"{e}-1" }), CancellationToken.None);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value.Value);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBindFromTaskReturnMaybeNoneOfObject()
    {
        Maybe<string> @object = null!;

        var result = await @object.Bind((e, ct) => Task.FromResult(new Dummy { Value = $"{e}-1" }), CancellationToken.None);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<Dummy>.None, result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBindFromTaskReturnExpectedMaybeOfNoneObject()
    {
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult(default(Dummy)), CancellationToken.None);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<Dummy?>.None, result);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyBinddeferredReturnExpectedMaybe()
    {
        var expected = "test-1-1";
        Maybe<string> @object = "test";

        var deferred = @object.BindDefer(e => $"{e}-1")
            .BindDefer(e => $"{e}-1");

        var result = deferred();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyBinddeferredFromTaskReturnExpectedMaybeOfString()
    {
        var expected = "test-1-1";
        Maybe<string> @object = "test";

        var deferred = @object.BindDefer((e, ct) => Task.FromResult(new Dummy { Value = $"{e}-1" }), CancellationToken.None)
            .BindDefer((e, ct) => Task.FromResult(new Dummy { Value = $"{e.Value}-1" }), CancellationToken.None);

        var result = await deferred();

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value.Value);
    }

    [Fact]
    public void GivenNullSelectorWhenApplyBindThrowArgumentNullException()
    {
        Maybe<string> @object = "test";

        Assert.Throws<ArgumentNullException>(() => @object.Bind<string, string>(null!));
    }

    [Fact]
    public async Task GivenNullAsyncSelectorWhenApplyBindThrowArgumentNullException()
    {
        Maybe<string> @object = "test";

        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await @object.Bind<string, string>(null!, CancellationToken.None));
    }

    [Fact]
    public void GivenNullSelectorWhenApplyBindDeferThrowArgumentNullException()
    {
        Maybe<string> @object = "test";

        Assert.Throws<ArgumentNullException>(() => @object.BindDefer<string, string>(null!));
    }

    [Fact]
    public void GivenNullAsyncSelectorWhenApplyBindDeferThrowArgumentNullException()
    {
        Maybe<string> @object = "test";

        Assert.Throws<ArgumentNullException>(
            () => @object.BindDefer<string, string>(null!, CancellationToken.None));
    }

    [Fact]
    public void GivenMaybeOfValueTypeWhenApplyBindReturnExpectedMaybe()
    {
        var expected = 10;
        Maybe<int> @object = 5;

        var result = @object.Bind(e => e * 2);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeOfValueTypeNoneWhenApplyBindReturnMaybeNone()
    {
        Maybe<int> @object = Maybe<int>.None;

        var result = @object.Bind(e => e * 2);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<int>.None, result);
    }

    [Fact]
    public void GivenMaybeOfStructWhenApplyBindReturnExpectedMaybe()
    {
        var expected = new DummyStruct { Id = 1, Name = "test" };
        Maybe<int> @object = 1;

        var result = @object.Bind(e => new DummyStruct { Id = e, Name = "test" });

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenChainOfBindsWhenAllSucceedReturnExpectedResult()
    {
        Maybe<int> @object = 2;

        var result = @object.Bind(e => e + 2)
            .Bind(e => e * 2)
            .Bind(e => e + 2);

        Assert.True(result.HasValue);
        Assert.Equal(10, result.Value);
    }

    [Fact]
    public void GivenChainOfBindsWithSelectorReturningNullReturnMaybeNone()
    {
        Maybe<string> @object = "test";

        var result = @object.Bind(e => e + "-1")
            .Bind(e => e.Length < 10 ? null : e + "-2")
            .Bind(e => e + "-3");

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public void GivenSelectorReturnsNullWhenApplyBindReturnMaybeNone()
    {
        Maybe<string> @object = "test";

        var result = @object.Bind(e => (string?)null);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public async Task GivenAsyncSelectorReturnsNullWhenApplyBindReturnMaybeNone()
    {
        Maybe<string> @object = "test";

        var result = await @object.Bind((e, ct) => Task.FromResult((string?)null), CancellationToken.None);

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public void GivenMaybeOfNoneWhenApplyBindDeferReturnDeferredMaybeNone()
    {
        Maybe<string> @object = null;

        var deferred = @object.BindDefer(e => $"{e}-1");
        var result = deferred();

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public async Task GivenMaybeOfNoneWhenApplyBindDeferAsyncReturnDeferredMaybeNone()
    {
        Maybe<string> @object = null;

        var deferred = @object.BindDefer((e, ct) => Task.FromResult($"{e}-1"), CancellationToken.None);
        var result = await deferred();

        Assert.False(result.HasValue);
        Assert.Equal(Maybe<string>.None, result);
    }

    [Fact]
    public async Task GivenCancellationTokenWhenApplyBindAsyncUseCancellationToken()
    {
        var cts = new CancellationTokenSource();
        Maybe<string> @object = "test";
        var tokenPassed = false;

        var result = await @object.Bind((e, ct) =>
        {
            tokenPassed = ct == cts.Token;
            return Task.FromResult($"{e}-1");
        }, cts.Token);

        Assert.True(tokenPassed);
        Assert.True(result.HasValue);
    }

    [Fact]
    public async Task GivenCancellationTokenWhenApplyBindDeferAsyncUseCancellationToken()
    {
        var cts = new CancellationTokenSource();
        Maybe<string> @object = "test";
        var tokenPassed = false;

        var deferred = @object.BindDefer((e, ct) =>
        {
            tokenPassed = ct == cts.Token;
            return Task.FromResult($"{e}-1");
        }, cts.Token);

        var result = await deferred();

        Assert.True(tokenPassed);
        Assert.True(result.HasValue);
    }

    [Fact]
    public void GivenEmptyStringWhenApplyBindReturnExpectedMaybe()
    {
        var expected = "-suffix";
        Maybe<string> @object = "";

        var result = @object.Bind(e => $"{e}-suffix");

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenDefaultValueTypeWhenApplyBindReturnExpectedMaybe()
    {
        var expected = 0;
        Maybe<int> @object = 0;

        var result = @object.Bind(e => e);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenMaybeOfBoolWhenApplyBindReturnExpectedMaybe()
    {
        Maybe<bool> @object = true;

        var result = @object.Bind(e => !e);

        Assert.True(result.HasValue);
        Assert.False(result.Value);
    }

    [Fact]
    public async Task GivenLongRunningAsyncOperationWhenApplyBindReturnExpectedMaybe()
    {
        var expected = "test-delayed";
        Maybe<string> @object = "test";

        var result = await @object.Bind(async (e, ct) =>
        {
            await Task.Delay(10, ct);
            return $"{e}-delayed";
        }, CancellationToken.None);

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenComplexChainWhenApplyMixedBindsReturnExpectedResult()
    {
        var expected = "RESULT: 10";
        Maybe<int> @object = 2;

        var result = @object
            .Bind(x => x * 2)
            .Bind(x => x + 1)
            .Bind(x => x * 2)
            .Bind(x => $"RESULT: {x}");

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GivenBindToSameTypeWhenApplyBindReturnExpectedMaybe()
    {
        var expected = "TEST";
        Maybe<string> @object = "test";

        var result = @object.Bind(e => e.ToUpperInvariant());

        Assert.True(result.HasValue);
        Assert.Equal(expected, result.Value);
    }

    public class Dummy
    {
        public string? Value { get; set; }
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