namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CSharpFunctionalExtensions;
using LanguageExt;
using OtherMonad;

/// <summary>
/// Benchmarks comparing OtherMonad.Result creation against LanguageExt.Fin and CSharpFunctionalExtensions.
/// Measures creation of Ok/Success (with value) and Err/Failure (with error).
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ResultCreationBenchmarks
{
    private const int VALUE = 42;
    private static readonly Exception _error = new InvalidOperationException("test error");

    [BenchmarkCategory("CreationOk"), Benchmark(Baseline = true)]
    public OtherMonad.Result<int> OtherMonad_Result_Create_Ok()
    {
        return OtherMonad.Result<int>.Create.Ok(VALUE);
    }

    [BenchmarkCategory("CreationOk"), Benchmark]
    public LanguageExt.Fin<int> LanguageExt_Fin_Create_Succ()
    {
        return LanguageExt.Fin<int>.Succ(VALUE);
    }

    [BenchmarkCategory("CreationOk"), Benchmark]
    public CSharpFunctionalExtensions.Result<int> CSharpFE_Result_Create_Success()
    {
        return CSharpFunctionalExtensions.Result.Success(VALUE);
    }

    [BenchmarkCategory("CreationErr"), Benchmark]
    public OtherMonad.Result<int> OtherMonad_Result_Create_Err()
    {
        return OtherMonad.Result<int>.Create.Err(_error);
    }

    [BenchmarkCategory("CreationErr"), Benchmark]
    public LanguageExt.Fin<int> LanguageExt_Fin_Create_Fail()
    {
        return LanguageExt.Fin<int>.Fail(LanguageExt.Common.Error.New(_error));
    }

    [BenchmarkCategory("CreationErr"), Benchmark]
    public CSharpFunctionalExtensions.Result<int> CSharpFE_Result_Create_Failure()
    {
        return CSharpFunctionalExtensions.Result.Failure<int>(_error.Message);
    }
}
