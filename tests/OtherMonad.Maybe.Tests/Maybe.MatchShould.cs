namespace Monads.Maybe.Tests;

using OtherMonad;

[Trait("Maybe", "Match")]
public class MaybeMatchShould
{
    [Fact]
    public void Given_maybe_of_string_when_apply_match_execute_left_condition()
    {
        Maybe<string> @object = "test";

        var result = @object.Match(c => true, () => false);

        Assert.True(result);
    }

    [Fact]
    public void Given_maybe_of_string_when_apply_match_execute_right_condition()
    {
        Maybe<string> @object = null;

        var result = @object.Match(c => true, () => false);

        Assert.False(result);
    }

    [Fact]
    public async Task Given_maybe_of_string_when_apply_match_type_of_task_execute_left_condition()
    {
        Maybe<string> @object = "test";

        var result = await @object.Match((c, ct) => Task.FromResult(true), (ct) => Task.FromResult(false));

        Assert.True(result);
    }

    [Fact]
    public async Task Given_maybe_of_string_when_apply_match_type_of_task_execute_right_condition()
    {
        Maybe<string> @object = null;

        var result = await @object.Match((c, ct) => Task.FromResult(true), (ct) => Task.FromResult(false));

        Assert.False(result);
    }
}