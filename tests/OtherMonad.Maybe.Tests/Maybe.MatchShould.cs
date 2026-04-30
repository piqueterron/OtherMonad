namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Match")]
public class MaybeMatchShould
{
    [Fact]
    public void GivenMaybeOfStringWhenApplyMatchExecuteSomeCondition()
    {
        Maybe<string> @object = "test";

        var result = @object.Match(c => true, () => false);

        Assert.True(result);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyMatchExecuteNoneCondition()
    {
        Maybe<string> @object = null;

        var result = @object.Match(c => true, () => false);

        Assert.False(result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyMatchTypeOfTaskExecuteSomeCondition()
    {
        Maybe<string> @object = "test";

        var result = await @object.Match((c, ct) => Task.FromResult(true), (ct) => Task.FromResult(false), TestContext.Current.CancellationToken);

        Assert.True(result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyMatchTypeOfTaskExecuteNoneCondition()
    {
        Maybe<string> @object = null;

        var result = await @object.Match((c, ct) => Task.FromResult(true), (ct) => Task.FromResult(false), TestContext.Current.CancellationToken);

        Assert.False(result);
    }

    [Fact]
    public void GivenNullSomeFunctionWhenApplyMatchThrowArgumentNullException()
    {
        Maybe<string> @object = "test";

        Assert.Throws<ArgumentNullException>(() => @object.Match<string, bool>(null!, () => false));
    }

    [Fact]
    public void GivenNullNoneFunctionWhenApplyMatchThrowArgumentNullException()
    {
        Maybe<string> @object = "test";

        Assert.Throws<ArgumentNullException>(() => @object.Match(c => true, null!));
    }

    [Fact]
    public async Task GivenNullAsyncSomeFunctionWhenApplyMatchThrowArgumentNullException()
    {
        Maybe<string> @object = "test";

        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await @object.Match<string, bool>(null!, (ct) => Task.FromResult(false), TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task GivenNullAsyncNoneFunctionWhenApplyMatchThrowArgumentNullException()
    {
        Maybe<string> @object = "test";

        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await @object.Match((c, ct) => Task.FromResult(true), null!, TestContext.Current.CancellationToken));
    }

    [Fact]
    public void GivenMaybeOfIntWhenApplyMatchReturnTransformedValue()
    {
        Maybe<int> @object = 42;

        var result = @object.Match(
            c => $"Value is {c}",
            () => "No value");

        Assert.Equal("Value is 42", result);
    }

    [Fact]
    public void GivenMaybeOfNoneWhenApplyMatchReturnNoneValue()
    {
        var @object = Maybe<int>.None;

        var result = @object.Match(
            c => $"Value is {c}",
            () => "No value");

        Assert.Equal("No value", result);
    }

    [Fact]
    public void GivenMaybeOfBoolWhenApplyMatchExecuteCorrectBranch()
    {
        Maybe<bool> @object = true;

        var result = @object.Match(
            c => c ? "true branch" : "false branch",
            () => "none branch");

        Assert.Equal("true branch", result);
    }

    [Fact]
    public void GivenMaybeOfStructWhenApplyMatchReturnExpectedResult()
    {
        Maybe<DummyStruct> @object = new DummyStruct { Id = 1, Name = "Test" };

        var result = @object.Match(
            c => $"Id: {c.Id}, Name: {c.Name}",
            () => "No struct");

        Assert.Equal("Id: 1, Name: Test", result);
    }

    [Fact]
    public async Task GivenMaybeOfIntWhenApplyMatchAsyncReturnTransformedValue()
    {
        Maybe<int> @object = 100;

        var result = await @object.Match(
            (c, ct) => Task.FromResult($"Async value: {c}"),
            (ct) => Task.FromResult("Async no value"),
            TestContext.Current.CancellationToken);

        Assert.Equal("Async value: 100", result);
    }

    [Fact]
    public async Task GivenCancellationTokenWhenApplyMatchAsyncUseCancellationToken()
    {
        Maybe<int> @object = 42;
        var someCalled = false;
        var tokenPassed = false;

        var result = await @object.Match(
            (c, ct) =>
            {
                someCalled = true;
                tokenPassed = ct == TestContext.Current.CancellationToken;
                return Task.FromResult(c * 2);
            },
            (ct) =>
            {
                tokenPassed = ct == TestContext.Current.CancellationToken;
                return Task.FromResult(0);
            },
            TestContext.Current.CancellationToken);

        Assert.True(someCalled);
        Assert.True(tokenPassed);
        Assert.Equal(84, result);
    }

    [Fact]
    public async Task GivenMaybeNoneWhenApplyMatchAsyncWithCancellationTokenExecuteNoneBranch()
    {
        var @object = Maybe<int>.None;
        var noneCalled = false;
        var tokenPassed = false;

        var result = await @object.Match(
            (c, ct) => Task.FromResult(c * 2),
            (ct) =>
            {
                noneCalled = true;
                tokenPassed = ct == TestContext.Current.CancellationToken;
                return Task.FromResult(-1);
            },
            TestContext.Current.CancellationToken);

        Assert.True(noneCalled);
        Assert.True(tokenPassed);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void GivenDeferredMaybeWithValueWhenApplyMatchExecuteSomeBranch()
    {
        Maybe<string> @object = "deferred";
        var deferred = @object.BindDefer(x => x.ToUpperInvariant());

        var result = deferred.Match(
            c => $"Got: {c}",
            () => "Got nothing");

        Assert.Equal("Got: DEFERRED", result);
    }

    [Fact]
    public void GivenDeferredMaybeNoneWhenApplyMatchExecuteNoneBranch()
    {
        var @object = Maybe<string>.None;
        var deferred = @object.BindDefer(x => x.ToUpperInvariant());

        var result = deferred.Match(
            c => $"Got: {c}",
            () => "Got nothing");

        Assert.Equal("Got nothing", result);
    }

    [Fact]
    public async Task GivenDeferredTaskMaybeWithValueWhenApplyMatchExecuteSomeBranch()
    {
        Maybe<int> @object = 5;
        var deferred = @object.BindDefer((x, ct) => Task.FromResult(x * 10), TestContext.Current.CancellationToken);

        var result = await deferred.Match(
            c => c + 5,
            () => 0);

        Assert.Equal(55, result);
    }

    [Fact]
    public async Task GivenDeferredTaskMaybeNoneWhenApplyMatchExecuteNoneBranch()
    {
        var @object = Maybe<int>.None;
        var deferred = @object.BindDefer((x, ct) => Task.FromResult(x * 10), TestContext.Current.CancellationToken);

        var result = await deferred.Match(
            c => c + 5,
            () => -999);

        Assert.Equal(-999, result);
    }

    [Fact]
    public void GivenMaybeWhenMatchReturnsComplexTypeReturnExpectedObject()
    {
        Maybe<int> @object = 3;

        var result = @object.Match(
            c => new { Value = c, IsPresent = true },
            () => new { Value = 0, IsPresent = false });

        Assert.True(result.IsPresent);
        Assert.Equal(3, result.Value);
    }

    [Fact]
    public void GivenEmptyStringMaybeWhenApplyMatchExecuteSomeBranch()
    {
        Maybe<string> @object = "";

        var result = @object.Match(
            c => "some",
            () => "none");

        Assert.Equal("some", result);
    }

    [Fact]
    public void GivenMaybeOfZeroWhenApplyMatchExecuteSomeBranch()
    {
        Maybe<int> @object = 0;

        var result = @object.Match(
            c => "has value",
            () => "no value");

        Assert.Equal("has value", result);
    }

    public struct DummyStruct
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}