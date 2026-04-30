namespace OtherMonad.Either.Tests;

[Trait("Either", "Bind/Map/OrElse")]
public class EitherBindShould
{
    [Fact]
    public void GivenEitherInRightStateWhenBindReturnsSelectorResult()
    {
        var either = Either<Exception, int>.Create.Right(5);

        var result = either.Bind(x => Either<Exception, string>.Create.Right(x.ToString()));

        Assert.True(result.IsRight);
        Assert.Equal("5", result.Right);
    }

    [Fact]
    public void GivenEitherInLeftStateWhenBindPropagatesLeft()
    {
        var error = new Exception("error");
        var either = Either<Exception, int>.Create.Left(error);

        var result = either.Bind(x => Either<Exception, string>.Create.Right(x.ToString()));

        Assert.True(result.IsLeft);
        Assert.Same(error, result.Left);
    }

    [Fact]
    public void GivenNullSelectorWhenBindThrowsArgumentNullException()
    {
        var either = Either<Exception, int>.Create.Right(1);

        Assert.Throws<ArgumentNullException>(() => either.Bind((Func<int, Either<Exception, string>>)null!));
    }

    [Fact]
    public async Task GivenEitherInRightStateWhenBindAsyncReturnsSelectorResult()
    {
        var either = Either<Exception, int>.Create.Right(42);

        var result = await either.Bind((x, ct) => Task.FromResult(Either<Exception, string>.Create.Right(x.ToString())));

        Assert.True(result.IsRight);
        Assert.Equal("42", result.Right);
    }

    [Fact]
    public async Task GivenEitherInLeftStateWhenBindAsyncPropagatesLeft()
    {
        var error = new Exception("fail");
        var either = Either<Exception, int>.Create.Left(error);

        var result = await either.Bind((x, ct) => Task.FromResult(Either<Exception, string>.Create.Right(x.ToString())));

        Assert.True(result.IsLeft);
        Assert.Same(error, result.Left);
    }

    // ── Map ─────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenEitherInRightStateWhenMapReturnsTransformedRight()
    {
        var either = Either<Exception, int>.Create.Right(10);

        var result = either.Map(x => x * 2);

        Assert.True(result.IsRight);
        Assert.Equal(20, result.Right);
    }

    [Fact]
    public void GivenEitherInLeftStateWhenMapPropagatesLeft()
    {
        var error = new Exception("error");
        var either = Either<Exception, int>.Create.Left(error);

        var result = either.Map(x => x * 2);

        Assert.True(result.IsLeft);
        Assert.Same(error, result.Left);
    }

    [Fact]
    public void GivenNullSelectorWhenMapThrowsArgumentNullException()
    {
        var either = Either<Exception, int>.Create.Right(1);

        Assert.Throws<ArgumentNullException>(() => either.Map((Func<int, string>)null!));
    }

    [Fact]
    public async Task GivenEitherInRightStateWhenMapAsyncReturnsTransformedRight()
    {
        var either = Either<Exception, int>.Create.Right(7);

        var result = await either.Map((x, ct) => Task.FromResult(x.ToString()));

        Assert.True(result.IsRight);
        Assert.Equal("7", result.Right);
    }

    [Fact]
    public async Task GivenEitherInLeftStateWhenMapAsyncPropagatesLeft()
    {
        var error = new Exception("fail");
        var either = Either<Exception, int>.Create.Left(error);

        var result = await either.Map((x, ct) => Task.FromResult(x.ToString()));

        Assert.True(result.IsLeft);
        Assert.Same(error, result.Left);
    }

    [Fact]
    public void GivenEitherInRightStateWhenOrElseReturnsSelf()
    {
        var either = Either<Exception, string>.Create.Right("ok");
        var fallback = Either<Exception, string>.Create.Right("fallback");

        var result = either.OrElse(fallback);

        Assert.True(result.IsRight);
        Assert.Equal("ok", result.Right);
    }

    [Fact]
    public void GivenEitherInLeftStateWhenOrElseReturnsFallback()
    {
        var either = Either<Exception, string>.Create.Left(new Exception("error"));
        var fallback = Either<Exception, string>.Create.Right("fallback");

        var result = either.OrElse(fallback);

        Assert.True(result.IsRight);
        Assert.Equal("fallback", result.Right);
    }

    [Fact]
    public async Task GivenEitherInRightStateWhenOrElseAsyncReturnsSelf()
    {
        var either = Either<Exception, string>.Create.Right("ok");

        var result = await either.OrElse((ct) => Task.FromResult(Either<Exception, string>.Create.Right("fallback")), TestContext.Current.CancellationToken);

        Assert.True(result.IsRight);
        Assert.Equal("ok", result.Right);
    }

    [Fact]
    public async Task GivenEitherInLeftStateWhenOrElseAsyncReturnsFallback()
    {
        var either = Either<Exception, string>.Create.Left(new Exception("error"));

        var result = await either.OrElse((ct) => Task.FromResult(Either<Exception, string>.Create.Right("fallback")), TestContext.Current.CancellationToken);

        Assert.True(result.IsRight);
        Assert.Equal("fallback", result.Right);
    }

    [Fact]
    public async Task GivenNullFallbackFactoryWhenOrElseAsyncThrowsArgumentNullException()
    {
        var either = Either<Exception, string>.Create.Left(new Exception("error"));

        await Assert.ThrowsAsync<ArgumentNullException>(() => either.OrElse(null!, TestContext.Current.CancellationToken));
    }

    [Fact]
    public void GivenEitherRightWhenBindToValueTypeReturnCorrectResult()
    {
        var either = Either<string, int>.Create.Right(10);

        var result = either.Bind(x => Either<string, int>.Create.Right(x * 2));

        Assert.True(result.IsRight);
        Assert.Equal(20, result.Right);
    }

    [Fact]
    public void GivenEitherRightWhenBindChangesTypeReturnCorrectType()
    {
        var either = Either<Exception, int>.Create.Right(42);

        var result = either.Bind(x => Either<Exception, bool>.Create.Right(x > 40));

        Assert.True(result.IsRight);
        Assert.True(result.Right);
    }

    [Fact]
    public void GivenEitherRightWhenBindReturnsLeftPropagatesToLeft()
    {
        var either = Either<string, int>.Create.Right(5);
        var errorMsg = "Value too small";

        var result = either.Bind(x => x > 10
            ? Either<string, int>.Create.Right(x)
            : Either<string, int>.Create.Left(errorMsg));

        Assert.True(result.IsLeft);
        Assert.Equal(errorMsg, result.Left);
    }

    [Fact]
    public void GivenChainOfBindsWhenAllRightReturnFinalResult()
    {
        var either = Either<string, int>.Create.Right(2);

        var result = either
            .Bind(x => Either<string, int>.Create.Right(x + 2))
            .Bind(x => Either<string, int>.Create.Right(x * 3))
            .Bind(x => Either<string, int>.Create.Right(x - 2));

        Assert.True(result.IsRight);
        Assert.Equal(10, result.Right); // (2 + 2) * 3 - 2 = 10
    }

    [Fact]
    public void GivenChainOfBindsWhenIntermediateLeftStopsPropagation()
    {
        var either = Either<string, int>.Create.Right(2);
        var errorMsg = "Intermediate error";

        var result = either
            .Bind(x => Either<string, int>.Create.Right(x + 2))
            .Bind(x => Either<string, int>.Create.Left(errorMsg))
            .Bind(x => Either<string, int>.Create.Right(x * 10));

        Assert.True(result.IsLeft);
        Assert.Equal(errorMsg, result.Left);
    }

    [Fact]
    public async Task GivenEitherRightWhenBindAsyncWithCancellationTokenUsesToken()
    {
        var cts = new CancellationTokenSource();
        var either = Either<Exception, int>.Create.Right(100);
        var tokenPassed = false;

        var result = await either.Bind((x, ct) =>
        {
            tokenPassed = ct == cts.Token;
            return Task.FromResult(Either<Exception, string>.Create.Right(x.ToString()));
        }, cts.Token);

        Assert.True(tokenPassed);
        Assert.True(result.IsRight);
    }

    [Fact]
    public async Task GivenEitherLeftWhenBindAsyncCancellationTokenNotUsed()
    {
        var cts = new CancellationTokenSource();
        var error = new Exception("error");
        var either = Either<Exception, int>.Create.Left(error);
        var selectorCalled = false;

        var result = await either.Bind((x, ct) =>
        {
            selectorCalled = true;
            return Task.FromResult(Either<Exception, string>.Create.Right(x.ToString()));
        }, cts.Token);

        Assert.False(selectorCalled);
        Assert.True(result.IsLeft);
        Assert.Same(error, result.Left);
    }

    [Fact]
    public void GivenEitherWithStructWhenBindReturnCorrectStruct()
    {
        var either = Either<string, DummyStruct>.Create.Right(new DummyStruct { Id = 1, Name = "Test" });

        var result = either.Bind(x => Either<string, DummyStruct>.Create.Right(
            new DummyStruct { Id = x.Id * 2, Name = x.Name + "_Modified" }));

        Assert.True(result.IsRight);
        Assert.Equal(2, result.Right.Id);
        Assert.Equal("Test_Modified", result.Right.Name);
    }

    [Fact]
    public void GivenEitherRightWhenMapWithComplexTransformReturnCorrectResult()
    {
        var either = Either<string, int>.Create.Right(5);

        var result = either.Map(x => $"Result: {x * x}");

        Assert.True(result.IsRight);
        Assert.Equal("Result: 25", result.Right);
    }

    [Fact]
    public void GivenEitherRightWhenMapToStructReturnCorrectStruct()
    {
        var either = Either<Exception, int>.Create.Right(42);

        var result = either.Map(x => new DummyStruct { Id = x, Name = $"Item{x}" });

        Assert.True(result.IsRight);
        Assert.Equal(42, result.Right.Id);
        Assert.Equal("Item42", result.Right.Name);
    }

    [Fact]
    public void GivenEitherRightWithZeroWhenMapReturnCorrectResult()
    {
        var either = Either<string, int>.Create.Right(0);

        var result = either.Map(x => x + 100);

        Assert.True(result.IsRight);
        Assert.Equal(100, result.Right);
    }

    [Fact]
    public void GivenEitherRightWithBoolWhenMapReturnNegated()
    {
        var either = Either<string, bool>.Create.Right(true);

        var result = either.Map(x => !x);

        Assert.True(result.IsRight);
        Assert.False(result.Right);
    }

    [Fact]
    public void GivenChainOfMapsWhenAllSucceedReturnFinalResult()
    {
        var either = Either<string, int>.Create.Right(10);

        var result = either
            .Map(x => x * 2)
            .Map(x => x + 5)
            .Map(x => $"Final: {x}");

        Assert.True(result.IsRight);
        Assert.Equal("Final: 25", result.Right);
    }

    [Fact]
    public async Task GivenEitherRightWhenMapAsyncWithDelayReturnCorrectResult()
    {
        var either = Either<string, int>.Create.Right(7);

        var result = await either.Map(async (x, ct) =>
        {
            await Task.Delay(1, ct);
            return x * 10;
        }, CancellationToken.None);

        Assert.True(result.IsRight);
        Assert.Equal(70, result.Right);
    }

    [Fact]
    public async Task GivenEitherRightWhenMapAsyncWithCancellationTokenUsesToken()
    {
        var cts = new CancellationTokenSource();
        var either = Either<Exception, int>.Create.Right(50);
        var tokenPassed = false;

        var result = await either.Map((x, ct) =>
        {
            tokenPassed = ct == cts.Token;
            return Task.FromResult(x.ToString());
        }, cts.Token);

        Assert.True(tokenPassed);
        Assert.True(result.IsRight);
    }

    [Fact]
    public void GivenEitherLeftWhenOrElseWithRightFallbackReturnsRightFallback()
    {
        var either = Either<string, int>.Create.Left("error");
        var fallback = Either<string, int>.Create.Right(100);

        var result = either.OrElse(fallback);

        Assert.True(result.IsRight);
        Assert.Equal(100, result.Right);
    }

    [Fact]
    public void GivenEitherLeftWhenOrElseWithLeftFallbackReturnsLeftFallback()
    {
        var either = Either<string, int>.Create.Left("first error");
        var fallback = Either<string, int>.Create.Left("second error");

        var result = either.OrElse(fallback);

        Assert.True(result.IsLeft);
        Assert.Equal("second error", result.Left);
    }

    [Fact]
    public void GivenChainOfOrElseWhenFirstRightReturnFirst()
    {
        var either = Either<string, int>.Create.Right(1);
        var fallback1 = Either<string, int>.Create.Right(2);
        var fallback2 = Either<string, int>.Create.Right(3);

        var result = either.OrElse(fallback1).OrElse(fallback2);

        Assert.True(result.IsRight);
        Assert.Equal(1, result.Right);
    }

    [Fact]
    public void GivenChainOfOrElseWhenAllLeftReturnLastLeft()
    {
        var either = Either<string, int>.Create.Left("err1");
        var fallback1 = Either<string, int>.Create.Left("err2");
        var fallback2 = Either<string, int>.Create.Left("err3");

        var result = either.OrElse(fallback1).OrElse(fallback2);

        Assert.True(result.IsLeft);
        Assert.Equal("err3", result.Left);
    }

    [Fact]
    public async Task GivenEitherLeftWhenOrElseAsyncFactoryReturnsFallback()
    {
        var either = Either<string, int>.Create.Left("error");

        var result = await either.OrElse((ct) => Task.FromResult(Either<string, int>.Create.Right(999)), TestContext.Current.CancellationToken);

        Assert.True(result.IsRight);
        Assert.Equal(999, result.Right);
    }

    [Fact]
    public async Task GivenEitherRightWhenOrElseAsyncFactoryNotCalled()
    {
        var either = Either<string, int>.Create.Right(42);
        var factoryCalled = false;

        var result = await either.OrElse((ct) =>
        {
            factoryCalled = true;
            return Task.FromResult(Either<string, int>.Create.Right(999));
        }, TestContext.Current.CancellationToken);

        Assert.False(factoryCalled);
        Assert.True(result.IsRight);
        Assert.Equal(42, result.Right);
    }

    [Fact]
    public async Task GivenEitherLeftWhenOrElseAsyncWithCancellationTokenUsesToken()
    {
        var cts = new CancellationTokenSource();
        var either = Either<string, int>.Create.Left("error");
        var tokenPassed = false;

        var result = await either.OrElse((ct) =>
        {
            tokenPassed = ct == cts.Token;
            return Task.FromResult(Either<string, int>.Create.Right(100));
        }, cts.Token);

        Assert.True(tokenPassed);
        Assert.True(result.IsRight);
    }

    [Fact]
    public void GivenComplexChainWhenBindMapOrElseReturnCorrectResult()
    {
        var either = Either<string, int>.Create.Right(5);

        var result = either
            .Bind(x => x > 3 ? Either<string, int>.Create.Right(x * 2) : Either<string, int>.Create.Left("too small"))
            .Map(x => x + 10)
            .OrElse(Either<string, int>.Create.Right(0));

        Assert.True(result.IsRight);
        Assert.Equal(20, result.Right); // (5 * 2) + 10 = 20
    }

    [Fact]
    public void GivenComplexChainWhenBindFailsOrElseProvidesFallback()
    {
        var either = Either<string, int>.Create.Right(2);

        var result = either
            .Bind(x => x > 3 ? Either<string, int>.Create.Right(x * 2) : Either<string, int>.Create.Left("too small"))
            .Map(x => x + 10)
            .OrElse(Either<string, int>.Create.Right(999));

        Assert.True(result.IsRight);
        Assert.Equal(999, result.Right);
    }

    [Fact]
    public void GivenEitherRightWhenMapThenBindReturnCorrectResult()
    {
        var either = Either<string, int>.Create.Right(3);

        var result = either
            .Map(x => x * 3)
            .Bind(x => Either<string, string>.Create.Right($"Value: {x}"));

        Assert.True(result.IsRight);
        Assert.Equal("Value: 9", result.Right);
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
