namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using CSharpFunctionalExtensions;

/// <summary>
/// Benchmarks comparing OtherMonad.Either against LanguageExt.Either and CSharpFunctionalExtensions.Result.
/// Measures core monadic operations: creation, Map/Bind, and Match.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class EitherBenchmarks
{
    private const int RightValue = 42;
    private const string LeftValue = "error";

    // ─── Creation Right ─────────────────────────────────────────────────────────

    [BenchmarkCategory("CreationRight"), Benchmark(Baseline = true)]
    public Either<string, int> OtherMonad_Either_Create_Right()
    {
        return Either<string, int>.Create.Right(RightValue);
    }

    [BenchmarkCategory("CreationRight"), Benchmark]
    public LanguageExt.Either<string, int> LanguageExt_Either_Create_Right()
    {
        return Prelude.Right<string, int>(RightValue);
    }

    [BenchmarkCategory("CreationRight"), Benchmark]
    public CSharpFunctionalExtensions.Result<int, string> CSharpFE_Result_Create_Success()
    {
        return CSharpFunctionalExtensions.Result.Success<int, string>(RightValue);
    }

    // ─── Creation Left ──────────────────────────────────────────────────────────

    [BenchmarkCategory("CreationLeft"), Benchmark(Baseline = true)]
    public Either<string, int> OtherMonad_Either_Create_Left()
    {
        return Either<string, int>.Create.Left(LeftValue);
    }

    [BenchmarkCategory("CreationLeft"), Benchmark]
    public LanguageExt.Either<string, int> LanguageExt_Either_Create_Left()
    {
        return Prelude.Left<string, int>(LeftValue);
    }

    [BenchmarkCategory("CreationLeft"), Benchmark]
    public CSharpFunctionalExtensions.Result<int, string> CSharpFE_Result_Create_Failure()
    {
        return CSharpFunctionalExtensions.Result.Failure<int, string>(LeftValue);
    }

    // ─── Bind Right ─────────────────────────────────────────────────────────────

    private static readonly Either<string, int> _otherMonadRight = Either<string, int>.Create.Right(RightValue);
    private static readonly LanguageExt.Either<string, int> _langExtRight = Prelude.Right<string, int>(RightValue);
    private static readonly CSharpFunctionalExtensions.Result<int, string> _csFESuccess = CSharpFunctionalExtensions.Result.Success<int, string>(RightValue);

    [BenchmarkCategory("BindRight"), Benchmark(Baseline = true)]
    public Either<string, int> OtherMonad_Either_Bind_Right()
    {
        return _otherMonadRight.Bind(x => Either<string, int>.Create.Right(x * 2));
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

    // ─── Bind Left ──────────────────────────────────────────────────────────────

    private static readonly Either<string, int> _otherMonadLeft = Either<string, int>.Create.Left(LeftValue);
    private static readonly LanguageExt.Either<string, int> _langExtLeft = Prelude.Left<string, int>(LeftValue);
    private static readonly CSharpFunctionalExtensions.Result<int, string> _csFEFailure = CSharpFunctionalExtensions.Result.Failure<int, string>(LeftValue);

    [BenchmarkCategory("BindLeft"), Benchmark(Baseline = true)]
    public Either<string, int> OtherMonad_Either_Bind_Left()
    {
        return _otherMonadLeft.Bind(x => Either<string, int>.Create.Right(x * 2));
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

    // ─── Match ──────────────────────────────────────────────────────────────────

    [BenchmarkCategory("Match"), Benchmark(Baseline = true)]
    public string OtherMonad_Either_Match()
    {
        return _otherMonadRight.Match(
            right: x => x.ToString(),
            left: e => e);
    }

    [BenchmarkCategory("Match"), Benchmark]
    public string LanguageExt_Either_Match()
    {
        return _langExtRight.Match(
            Right: x => x.ToString(),
            Left: e => e);
    }

    [BenchmarkCategory("Match"), Benchmark]
    public string CSharpFE_Result_Match()
    {
        return _csFESuccess.Match(
            onSuccess: x => x.ToString(),
            onFailure: e => e);
    }
}
