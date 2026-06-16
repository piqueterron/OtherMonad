namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using CSharpFunctionalExtensions;

/// <summary>
/// Benchmarks comparing OtherMonad.Either Bind operation against LanguageExt.Either and CSharpFunctionalExtensions.Result.
/// Measures Bind performance with Right (success path) and Left (error path).
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class EitherBindBenchmarks
{
    private const int RIGHT_VALUE = 42;
    private const string LEFT_VALUE = "error";

    private static readonly Either<string, int> _otherMonadRight = Either<string, int>.Right(RIGHT_VALUE);
    private static readonly LanguageExt.Either<string, int> _langExtRight = Prelude.Right<string, int>(RIGHT_VALUE);
    private static readonly CSharpFunctionalExtensions.Result<int, string> _csFESuccess = CSharpFunctionalExtensions.Result.Success<int, string>(RIGHT_VALUE);

    [BenchmarkCategory("BindRight"), Benchmark(Baseline = true)]
    public Either<string, int> OtherMonad_Either_Bind_Right()
    {
        return _otherMonadRight.Bind(x => Either<string, int>.Right(x * 2));
    }

    [BenchmarkCategory("BindRight"), Benchmark]
    public LanguageExt.Either<string, int> LanguageExt_Either_Bind_Right()
    {
        return _langExtRight.Bind(x => Prelude.Right<string, int>(x * 2));
    }

    [BenchmarkCategory("BindRight"), Benchmark]
    public CSharpFunctionalExtensions.Result<int, string> CSharpFE_Result_Bind_Success()
    {
        return _csFESuccess.Bind(x => CSharpFunctionalExtensions.Result.Success<int, string>(x * 2));
    }

    private static readonly Either<string, int> _otherMonadLeft = Either<string, int>.Left(LEFT_VALUE);
    private static readonly LanguageExt.Either<string, int> _langExtLeft = Prelude.Left<string, int>(LEFT_VALUE);
    private static readonly CSharpFunctionalExtensions.Result<int, string> _csFEFailure = CSharpFunctionalExtensions.Result.Failure<int, string>(LEFT_VALUE);

    [BenchmarkCategory("BindLeft"), Benchmark]
    public Either<string, int> OtherMonad_Either_Bind_Left()
    {
        return _otherMonadLeft.Bind(x => Either<string, int>.Right(x * 2));
    }

    [BenchmarkCategory("BindLeft"), Benchmark]
    public LanguageExt.Either<string, int> LanguageExt_Either_Bind_Left()
    {
        return _langExtLeft.Bind(x => Prelude.Right<string, int>(x * 2));
    }

    [BenchmarkCategory("BindLeft"), Benchmark]
    public CSharpFunctionalExtensions.Result<int, string> CSharpFE_Result_Bind_Failure()
    {
        return _csFEFailure.Bind(x => CSharpFunctionalExtensions.Result.Success<int, string>(x * 2));
    }
}
