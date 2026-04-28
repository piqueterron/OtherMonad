namespace OtherMonad.Either.Tests;

[Trait("Either", "Bind/Map/OrElse")]
public class EitherBindShould
{
    // ── Bind ────────────────────────────────────────────────────────────────────

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

    // ── OrElse ──────────────────────────────────────────────────────────────────

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

        var result = await either.OrElse((ct) => Task.FromResult(Either<Exception, string>.Create.Right("fallback")));

        Assert.True(result.IsRight);
        Assert.Equal("ok", result.Right);
    }

    [Fact]
    public async Task GivenEitherInLeftStateWhenOrElseAsyncReturnsFallback()
    {
        var either = Either<Exception, string>.Create.Left(new Exception("error"));

        var result = await either.OrElse((ct) => Task.FromResult(Either<Exception, string>.Create.Right("fallback")));

        Assert.True(result.IsRight);
        Assert.Equal("fallback", result.Right);
    }

    [Fact]
    public async Task GivenNullFallbackFactoryWhenOrElseAsyncThrowsArgumentNullException()
    {
        var either = Either<Exception, string>.Create.Left(new Exception("error"));

        await Assert.ThrowsAsync<ArgumentNullException>(() => either.OrElse(null!));
    }
}
