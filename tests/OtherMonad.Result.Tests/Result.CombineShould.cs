namespace OtherMonad.Tests;

[Trait("Result", "Combine")]
public class ResultCombineShould
{
    [Fact]
    public void GivenTwoResultsOkWhenCombineReturnCombinedValue()
    {
        var a = Result<string>.Create.Ok("hello");
        var b = Result<string>.Create.Ok("world");

        var output = a.Combine(b, (x, y) => x + y);

        Assert.True(output.IsOk);
        Assert.Equal("helloworld", output.Value);
    }

    [Fact]
    public void GivenTwoResultsErrWhenCombineReturnAggregateException()
    {
        var a = Result<string>.Create.Err(new Exception("err1"));
        var b = Result<string>.Create.Err(new Exception("err2"));

        var output = a.Combine(b, (x, y) => x + y);

        Assert.True(output.IsErr);
        Assert.IsType<AggregateException>(output.Error);

        var agg = (AggregateException)output.Error;
        Assert.Equal("err1", agg.InnerExceptions[0].Message);
        Assert.Equal("err2", agg.InnerExceptions[1].Message);
    }

    [Fact]
    public void GivenFirstErrAndSecondOkWhenCombineReturnFirstErr()
    {
        var error = new Exception("first err");
        var a = Result<string>.Create.Err(error);
        var b = Result<string>.Create.Ok("ok");

        var output = a.Combine(b, (x, y) => x + y);

        Assert.True(output.IsErr);
        Assert.Same(error, output.Error);
    }

    [Fact]
    public void GivenFirstOkAndSecondErrWhenCombineReturnSecondErr()
    {
        var error = new Exception("second err");
        var a = Result<string>.Create.Ok("ok");
        var b = Result<string>.Create.Err(error);

        var output = a.Combine(b, (x, y) => x + y);

        Assert.True(output.IsErr);
        Assert.Same(error, output.Error);
    }

    [Fact]
    public void GivenNullSelectorWhenCombineThrowsArgumentNullException()
    {
        var a = Result<int>.Create.Ok(1);
        var b = Result<int>.Create.Ok(2);

        Assert.Throws<ArgumentNullException>(() => a.Combine(b, (Func<int, int, int>)null!));
    }

    [Fact]
    public void GivenTwoResultsOkWithIntsWhenCombineReturnSum()
    {
        var a = Result<int>.Create.Ok(10);
        var b = Result<int>.Create.Ok(5);

        var output = a.Combine(b, (x, y) => x + y);

        Assert.True(output.IsOk);
        Assert.Equal(15, output.Value);
    }

    [Fact]
    public void GivenTwoResultsOkWithDifferentTypesWhenCombineReturnCombinedObject()
    {
        var a = Result<string>.Create.Ok("Value: ");
        var b = Result<int>.Create.Ok(42);

        var output = a.Combine(b, (x, y) => x + y);

        Assert.True(output.IsOk);
        Assert.Equal("Value: 42", output.Value);
    }

    [Fact]
    public void GivenTwoResultsOkWithBoolsWhenCombineReturnLogicalAnd()
    {
        var a = Result<bool>.Create.Ok(true);
        var b = Result<bool>.Create.Ok(false);

        var output = a.Combine(b, (x, y) => x && y);

        Assert.True(output.IsOk);
        Assert.False(output.Value);
    }

    [Fact]
    public void GivenTwoResultsOkWithZerosWhenCombineReturnZero()
    {
        var a = Result<int>.Create.Ok(0);
        var b = Result<int>.Create.Ok(0);

        var output = a.Combine(b, (x, y) => x + y);

        Assert.True(output.IsOk);
        Assert.Equal(0, output.Value);
    }

    [Fact]
    public void GivenThreeResultsCombinedWhenAllOkReturnCombinedResult()
    {
        var a = Result<int>.Create.Ok(10);
        var b = Result<int>.Create.Ok(20);
        var c = Result<int>.Create.Ok(30);

        var output = a.Combine(b, (x, y) => x + y)
                      .Combine(c, (x, y) => x + y);

        Assert.True(output.IsOk);
        Assert.Equal(60, output.Value);
    }

    [Fact]
    public void GivenTwoErrResultsWithDifferentExceptionTypesWhenCombineReturnAggregated()
    {
        var a = Result<string>.Create.Err(new InvalidOperationException("inv-op"));
        var b = Result<string>.Create.Err(new ArgumentException("arg"));

        var output = a.Combine(b, (x, y) => x + y);

        Assert.True(output.IsErr);
        Assert.IsType<AggregateException>(output.Error);

        var agg = (AggregateException)output.Error;
        Assert.IsType<InvalidOperationException>(agg.InnerExceptions[0]);
        Assert.IsType<ArgumentException>(agg.InnerExceptions[1]);
    }
}
