namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CSharpFunctionalExtensions;
using OtherMonad;

/// <summary>
/// Benchmarks comparing OtherMonad.Result Try operation against CSharpFunctionalExtensions.
/// Measures exception handling performance for successful and failing operations.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ResultTryBenchmarks
{
    private const int VALUE = 42;
    private static readonly Exception _error = new InvalidOperationException("test error");

    [BenchmarkCategory("TrySuccess"), Benchmark(Baseline = true)]
    public OtherMonad.Result<int> OtherMonad_Result_Try_Success()
    {
        return OtherMonad.Result.Try(() => VALUE * 2);
    }

    [BenchmarkCategory("TrySuccess"), Benchmark]
    public CSharpFunctionalExtensions.Result<int> CSharpFE_Result_Try_Success()
    {
        try
        {
            return CSharpFunctionalExtensions.Result.Success(VALUE * 2);
        }
        catch (Exception ex)
        {
            return CSharpFunctionalExtensions.Result.Failure<int>(ex.Message);
        }
    }

    [BenchmarkCategory("TryFailure"), Benchmark]
    public OtherMonad.Result<int> OtherMonad_Result_Try_Failure()
    {
        return OtherMonad.Result.Try<int>(() => throw _error);
    }

    [BenchmarkCategory("TryFailure"), Benchmark]
    public CSharpFunctionalExtensions.Result<int> CSharpFE_Result_Try_Failure()
    {
        try
        {
            throw _error;
        }
        catch (Exception ex)
        {
            return CSharpFunctionalExtensions.Result.Failure<int>(ex.Message);
        }
    }
}
