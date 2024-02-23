namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Match")]
public class MaybeMatchShould
{
    [Fact]
    public void GivenMaybeOfStringWhenApplyMatchExecuteLeftCondition()
    {
        Maybe<string> @object = "test";

        var result = @object.Match(c => true, () => false);

        Assert.True(result);
    }

    [Fact]
    public void GivenMaybeOfStringWhenApplyMatchExecuteRightCondition()
    {
        Maybe<string> @object = null;

        var result = @object.Match(c => true, () => false);

        Assert.False(result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyMatchTypeOfTaskExecuteLeftCondition()
    {
        Maybe<string> @object = "test";

        var result = await @object.Match((c, ct) => Task.FromResult(true), (ct) => Task.FromResult(false));

        Assert.True(result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyMatchTypeOfTaskExecuteRightCondition()
    {
        Maybe<string> @object = null;

        var result = await @object.Match((c, ct) => Task.FromResult(true), (ct) => Task.FromResult(false));

        Assert.False(result);
    }
}