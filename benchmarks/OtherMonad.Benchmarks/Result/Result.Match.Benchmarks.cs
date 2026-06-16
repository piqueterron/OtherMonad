namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CSharpFunctionalExtensions;
using LanguageExt;
using OtherMonad;

/// <summary>
/// Benchmarks comparing OtherMonad.Result Match operation against LanguageExt.Fin and CSharpFunctionalExtensions.
/// Measures pattern matching performance for extracting values from Result types.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ResultMatchBenchmarks
{
    private const int VALUE = 42;

    private static readonly OtherMonad.Result<int> _otherMonadOk = OtherMonad.Result<int>.Create.Ok(VALUE);
    private static readonly LanguageExt.Fin<int> _langExtSucc = LanguageExt.Fin<int>.Succ(VALUE);
    private static readonly CSharpFunctionalExtensions.Result<int> _csFESuccess = CSharpFunctionalExtensions.Result.Success(VALUE);

    [BenchmarkCategory("Match"), Benchmark(Baseline = true)]
    public string OtherMonad_Result_Match()
    {
        return _otherMonadOk.Match(
            e => e.Message,
            x => x.ToString());
    }

    [BenchmarkCategory("Match"), Benchmark]
    public string LanguageExt_Fin_Match()
    {
        return _langExtSucc.Match(
            Succ: x => x.ToString(),
            Fail: e => e.Message);
    }

    [BenchmarkCategory("Match"), Benchmark]
    public string CSharpFE_Result_Match()
    {
        return _csFESuccess.Match(
            onSuccess: x => x.ToString(),
            onFailure: e => e);
    }
}
