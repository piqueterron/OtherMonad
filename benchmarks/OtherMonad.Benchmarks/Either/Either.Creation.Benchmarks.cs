namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using CSharpFunctionalExtensions;

/// <summary>
/// Benchmarks comparing OtherMonad.Either creation against LanguageExt.Either and CSharpFunctionalExtensions.Result.
/// Measures creation of Right (success) and Left (error) values.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class EitherCreationBenchmarks
{
    private const int RIGHT_VALUE = 42;
    private const string LEFT_VALUE = "error";

    [BenchmarkCategory("CreationRight"), Benchmark(Baseline = true)]
    public Either<string, int> OtherMonad_Either_Create_Right()
    {
        return Either<string, int>.Right(RIGHT_VALUE);
    }

    [BenchmarkCategory("CreationRight"), Benchmark]
    public LanguageExt.Either<string, int> LanguageExt_Either_Create_Right()
    {
        return Prelude.Right<string, int>(RIGHT_VALUE);
    }

    [BenchmarkCategory("CreationRight"), Benchmark]
    public CSharpFunctionalExtensions.Result<int, string> CSharpFE_Result_Create_Success()
    {
        return CSharpFunctionalExtensions.Result.Success<int, string>(RIGHT_VALUE);
    }

    [BenchmarkCategory("CreationLeft"), Benchmark]
    public Either<string, int> OtherMonad_Either_Create_Left()
    {
        return Either<string, int>.Left(LEFT_VALUE);
    }

    [BenchmarkCategory("CreationLeft"), Benchmark]
    public LanguageExt.Either<string, int> LanguageExt_Either_Create_Left()
    {
        return Prelude.Left<string, int>(LEFT_VALUE);
    }

    [BenchmarkCategory("CreationLeft"), Benchmark]
    public CSharpFunctionalExtensions.Result<int, string> CSharpFE_Result_Create_Failure()
    {
        return CSharpFunctionalExtensions.Result.Failure<int, string>(LEFT_VALUE);
    }
}
