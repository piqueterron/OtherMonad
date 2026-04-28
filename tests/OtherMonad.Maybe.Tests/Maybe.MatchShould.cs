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

        var result = await @object.Match((c, ct) => Task.FromResult(true), (ct) => Task.FromResult(false));

        Assert.True(result);
    }

    [Fact]
    public async Task GivenMaybeOfStringWhenApplyMatchTypeOfTaskExecuteNoneCondition()
    {
        Maybe<string> @object = null;

        var result = await @object.Match((c, ct) => Task.FromResult(true), (ct) => Task.FromResult(false));

        Assert.False(result);
    }
}