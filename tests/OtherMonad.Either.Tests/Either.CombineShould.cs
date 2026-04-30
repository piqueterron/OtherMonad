namespace Monads.Either.Tests;

using OtherMonad;

[Trait("Either", "Combine")]
public class EitherCombineShould
{
    [Fact]
    public void GivenTwoEithersWithStateSuccessWhenApplyCombineReturnAllResults()
    {
        var expected = "test1test2";

        var either1 = Either<Exception, string>.Create.Right("test1");
        var either2 = Either<Exception, string>.Create.Right("test2");

        var result = either1.Combine(either2, (x, y) => new AggregateException(x!, y!), (x, y) => x + y);

        Assert.True(result.IsRight);
        Assert.Equal(expected, result.Right);
    }

    [Fact]
    public void GivenTwoEithersWithStateFailWhenApplyCombineReturnAllExceptions()
    {
        var msg1 = "Exception either 1";
        var msg2 = "Exception either 2";

        var either1 = Either<Exception, string>.Create.Left(new Exception(msg1));
        var either2 = Either<Exception, string>.Create.Left(new Exception(msg2));

        var result = either1.Combine(either2, (x, y) => new AggregateException(x, y), (x, y) => x + y);

        Assert.False(result.IsRight);
        Assert.IsType<AggregateException>(result.Left);
        Assert.Equal(msg1, result.Left.InnerExceptions[0].Message);
        Assert.Equal(msg2, result.Left.InnerExceptions[1].Message);
    }

    [Fact]
    public void GivenTwoEithersWithStateFailWhenApplyCombineReturnDistinctExceptions()
    {
        var msg1 = "Exception either 1";
        var msg2 = "ArgumentException either 2";

        var either1 = Either<Exception, string>.Create.Left(new Exception(msg1));
        var either2 = Either<ArgumentException, string>.Create.Left(new ArgumentException(msg2));

        var result = either1.Combine(either2, (x, y) => new AggregateException(x, y), (x, y) => x + y);

        Assert.False(result.IsRight);
        Assert.IsType<AggregateException>(result.Left);
        Assert.Equal(msg1, result.Left.InnerExceptions[0].Message);
        Assert.Equal(msg2, result.Left.InnerExceptions[1].Message);
    }

    [Fact]
    public void GivenTwoEithersWithOneStateSuccessAndOtherFailWhenApplyCombineReturnFailure()
    {
        var msg1 = "Exception either 1";

        var either1 = Either<Exception, string>.Create.Left(new Exception(msg1));
        var either2 = Either<Exception, string>.Create.Right("test");

        var result = either1.Combine(either2,
            (x, y) =>
                x is not null && y is not null
                    ? new AggregateException(x, y)
                    : x is not null
                        ? new AggregateException(x)
                        : new AggregateException(y!),
            (x, y) => x + y);

        Assert.False(result.IsRight);
        Assert.IsType<AggregateException>(result.Left);
        Assert.Equal(msg1, result.Left.InnerExceptions[0].Message);
    }

    [Fact]
    public void GivenTwoEithersRightWithIntWhenCombineReturnSum()
    {
        var expected = 15;
        var either1 = Either<Exception, int>.Create.Right(10);
        var either2 = Either<Exception, int>.Create.Right(5);

        var result = either1.Combine(either2,
            (x, y) => new AggregateException(x!, y!),
            (x, y) => x + y);

        Assert.True(result.IsRight);
        Assert.Equal(expected, result.Right);
    }

    [Fact]
    public void GivenTwoEithersRightWithDifferentTypesWhenCombineReturnCombined()
    {
        var expected = "Value: 42";
        var either1 = Either<Exception, string>.Create.Right("Value: ");
        var either2 = Either<Exception, int>.Create.Right(42);

        var result = either1.Combine(either2,
            (x, y) => new AggregateException(x!, y!),
            (x, y) => x + y);

        Assert.True(result.IsRight);
        Assert.Equal(expected, result.Right);
    }

    [Fact]
    public void GivenRightAndLeftEithersWhenCombineReturnLeftError()
    {
        var errorMsg = "Error in second";
        var either1 = Either<Exception, int>.Create.Right(10);
        var either2 = Either<Exception, int>.Create.Left(new Exception(errorMsg));

        var result = either1.Combine(either2,
            (x, y) => x is not null && y is not null
                ? new AggregateException(x, y)
                : (x ?? y)!,
            (x, y) => x + y);

        Assert.False(result.IsRight);
        Assert.Equal(errorMsg, result.Left.Message);
    }

    [Fact]
    public void GivenTwoRightEithersWithBoolWhenCombineReturnLogicalAnd()
    {
        var either1 = Either<string, bool>.Create.Right(true);
        var either2 = Either<string, bool>.Create.Right(false);

        var result = either1.Combine(either2,
            (x, y) => $"{x}; {y}",
            (x, y) => x && y);

        Assert.True(result.IsRight);
        Assert.False(result.Right);
    }

    [Fact]
    public void GivenTwoRightEithersWithStructWhenCombineReturnCombinedStruct()
    {
        var expected = new DummyStruct { Id = 3, Name = "AB" };
        var either1 = Either<string, DummyStruct>.Create.Right(new DummyStruct { Id = 1, Name = "A" });
        var either2 = Either<string, DummyStruct>.Create.Right(new DummyStruct { Id = 2, Name = "B" });

        var result = either1.Combine(either2,
            (x, y) => $"{x}; {y}",
            (x, y) => new DummyStruct { Id = x.Id + y.Id, Name = x.Name + y.Name });

        Assert.True(result.IsRight);
        Assert.Equal(expected, result.Right);
    }

    [Fact]
    public void GivenThreeEithersCombinedWhenAllRightReturnCombinedResult()
    {
        var expected = 60; // 10 + 20 + 30
        var either1 = Either<Exception, int>.Create.Right(10);
        var either2 = Either<Exception, int>.Create.Right(20);
        var either3 = Either<Exception, int>.Create.Right(30);

        var result = either1.Combine(either2,
                (x, y) => new AggregateException(x!, y!),
                (x, y) => x + y)
            .Combine(either3,
                (x, y) => new AggregateException(x!, y!),
                (x, y) => x + y);

        Assert.True(result.IsRight);
        Assert.Equal(expected, result.Right);
    }

    [Fact]
    public void GivenThreeEithersWhenOneLeftReturnAggregatedErrors()
    {
        var msg1 = "Error 1";
        var msg2 = "Error 2";
        var either1 = Either<Exception, int>.Create.Left(new Exception(msg1));
        var either2 = Either<Exception, int>.Create.Right(20);
        var either3 = Either<Exception, int>.Create.Left(new Exception(msg2));

        var result = either1.Combine(either2,
                (x, y) => x is not null && y is not null ? new AggregateException(x, y) : (x ?? y)!,
                (x, y) => x + y)
            .Combine(either3,
                (x, y) => x is not null && y is not null ? new AggregateException(x, y) : (x ?? y)!,
                (x, y) => x + y);

        Assert.False(result.IsRight);
        Assert.IsType<AggregateException>(result.Left);
    }

    [Fact]
    public void GivenTwoRightEithersWithZeroWhenCombineReturnZero()
    {
        var either1 = Either<string, int>.Create.Right(0);
        var either2 = Either<string, int>.Create.Right(0);

        var result = either1.Combine(either2,
            (x, y) => $"{x}; {y}",
            (x, y) => x + y);

        Assert.True(result.IsRight);
        Assert.Equal(0, result.Right);
    }

    [Fact]
    public void GivenTwoRightEithersWhenCombineWithMultiplicationReturnProduct()
    {
        var expected = 24;
        var either1 = Either<string, int>.Create.Right(6);
        var either2 = Either<string, int>.Create.Right(4);

        var result = either1.Combine(either2,
            (x, y) => $"{x}; {y}",
            (x, y) => x * y);

        Assert.True(result.IsRight);
        Assert.Equal(expected, result.Right);
    }

    [Fact]
    public void GivenTwoLeftEithersWithDifferentErrorTypesWhenCombineReturnAggregatedErrors()
    {
        var msg1 = "Invalid operation";
        var msg2 = "Null reference";
        var either1 = Either<Exception, string>.Create.Left(new InvalidOperationException(msg1));
        var either2 = Either<Exception, string>.Create.Left(new NullReferenceException(msg2));

        var result = either1.Combine(either2,
            (x, y) => new AggregateException(x, y),
            (x, y) => x + y);

        Assert.False(result.IsRight);
        Assert.IsType<AggregateException>(result.Left);
        Assert.IsType<InvalidOperationException>(result.Left.InnerExceptions[0]);
        Assert.IsType<NullReferenceException>(result.Left.InnerExceptions[1]);
    }

    [Fact]
    public void GivenTwoRightEithersWithEmptyStringsWhenCombineReturnEmptyString()
    {
        var either1 = Either<Exception, string>.Create.Right("");
        var either2 = Either<Exception, string>.Create.Right("");

        var result = either1.Combine(either2,
            (x, y) => new AggregateException(x!, y!),
            (x, y) => x + y);

        Assert.True(result.IsRight);
        Assert.Equal("", result.Right);
    }

    [Fact]
    public void GivenCombineWithComplexSelectorWhenRightReturnComplexObject()
    {
        var either1 = Either<string, int>.Create.Right(10);
        var either2 = Either<string, string>.Create.Right("test");

        var result = either1.Combine(either2,
            (x, y) => $"{x}; {y}",
            (x, y) => new { Number = x, Text = y });

        Assert.True(result.IsRight);
        Assert.Equal(10, result.Right.Number);
        Assert.Equal("test", result.Right.Text);
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