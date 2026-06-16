namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using CSharpFunctionalExtensions;

/// <summary>
/// Benchmarks comparing OtherMonad.Either Match operation against LanguageExt.Either and CSharpFunctionalExtensions.Result.
/// Measures pattern matching performance for extracting values from Either types.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class EitherMatchBenchmarks
{
    private const int RIGHT_VALUE = 42;

    private static readonly Either<string, int> _otherMonadRight = Either<string, int>.Right(RIGHT_VALUE);
    private static readonly LanguageExt.Either<string, int> _langExtRight = Prelude.Right<string, int>(RIGHT_VALUE);
    private static readonly CSharpFunctionalExtensions.Result<int, string> _csFESuccess = CSharpFunctionalExtensions.Result.Success<int, string>(RIGHT_VALUE);

    [BenchmarkCategory("Match"), Benchmark(Baseline = true)]
    public string OtherMonad_Either_Match()
    {
        return _otherMonadRight.Match(
            x => x.ToString(),
            e => e);
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
