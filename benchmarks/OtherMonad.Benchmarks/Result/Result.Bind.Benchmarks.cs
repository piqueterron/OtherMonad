namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CSharpFunctionalExtensions;
using LanguageExt;
using OtherMonad;

/// <summary>
/// Benchmarks comparing OtherMonad.Result Bind operation against LanguageExt.Fin and CSharpFunctionalExtensions.
/// Measures Bind performance with Ok/Success (happy path) and Err/Failure (error path).
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ResultBindBenchmarks
{
    private const int VALUE = 42;
    private static readonly Exception _error = new InvalidOperationException("test error");

    private static readonly OtherMonad.Result<int> _otherMonadOk = OtherMonad.Result<int>.Create.Ok(VALUE);
    private static readonly LanguageExt.Fin<int> _langExtSucc = LanguageExt.Fin<int>.Succ(VALUE);
    private static readonly CSharpFunctionalExtensions.Result<int> _csFESuccess = CSharpFunctionalExtensions.Result.Success(VALUE);

    [BenchmarkCategory("BindOk"), Benchmark(Baseline = true)]
    public OtherMonad.Result<int> OtherMonad_Result_Bind_Ok()
    {
        return _otherMonadOk.Bind(x => OtherMonad.Result<int>.Create.Ok(x * 2));
    }

    [BenchmarkCategory("BindOk"), Benchmark]
    public LanguageExt.Fin<int> LanguageExt_Fin_Bind_Succ()
    {
        return _langExtSucc.Bind(x => LanguageExt.Fin<int>.Succ(x * 2));
    }

    [BenchmarkCategory("BindOk"), Benchmark]
    public CSharpFunctionalExtensions.Result<int> CSharpFE_Result_Bind_Success()
    {
        return _csFESuccess.Bind(x => CSharpFunctionalExtensions.Result.Success(x * 2));
    }

    private static readonly OtherMonad.Result<int> _otherMonadErr = OtherMonad.Result<int>.Create.Err(_error);
    private static readonly LanguageExt.Fin<int> _langExtFail = LanguageExt.Fin<int>.Fail(LanguageExt.Common.Error.New(_error));
    private static readonly CSharpFunctionalExtensions.Result<int> _csFEFailure = CSharpFunctionalExtensions.Result.Failure<int>(_error.Message);

    [BenchmarkCategory("BindErr"), Benchmark]
    public OtherMonad.Result<int> OtherMonad_Result_Bind_Err()
    {
        return _otherMonadErr.Bind(x => OtherMonad.Result<int>.Create.Ok(x * 2));
    }

    [BenchmarkCategory("BindErr"), Benchmark]
    public LanguageExt.Fin<int> LanguageExt_Fin_Bind_Fail()
    {
        return _langExtFail.Bind(x => LanguageExt.Fin<int>.Succ(x * 2));
    }

    [BenchmarkCategory("BindErr"), Benchmark]
    public CSharpFunctionalExtensions.Result<int> CSharpFE_Result_Bind_Failure()
    {
        return _csFEFailure.Bind(x => CSharpFunctionalExtensions.Result.Success(x * 2));
    }
}
