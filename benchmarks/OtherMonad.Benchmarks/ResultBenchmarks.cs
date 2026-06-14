namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CSharpFunctionalExtensions;
using LanguageExt;

/// <summary>
/// Benchmarks comparing OtherMonad.Result against LanguageExt and CSharpFunctionalExtensions
/// for Result/Try patterns. Measures creation, Bind, Match, and Try operations.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ResultBenchmarks
{
    private const int Value = 42;
    private static readonly Exception Error = new InvalidOperationException("test error");

    // ─── Creation Ok ────────────────────────────────────────────────────────────

    [BenchmarkCategory("CreationOk"), Benchmark(Baseline = true)]
    public Result<int> OtherMonad_Result_Create_Ok()
    {
        return Result<int>.Create.Ok(Value);
    }

    [BenchmarkCategory("CreationOk"), Benchmark]
    public LanguageExt.Fin<int> LanguageExt_Fin_Create_Succ()
    {
        return LanguageExt.Fin<int>.Succ(Value);
    }

    [BenchmarkCategory("CreationOk"), Benchmark]
    public CSharpFunctionalExtensions.Result<int> CSharpFE_Result_Create_Success()
    {
        return CSharpFunctionalExtensions.Result.Success(Value);
    }

    // ─── Creation Err ───────────────────────────────────────────────────────────

    [BenchmarkCategory("CreationErr"), Benchmark(Baseline = true)]
    public Result<int> OtherMonad_Result_Create_Err()
    {
        return Result<int>.Create.Err(Error);
    }

    [BenchmarkCategory("CreationErr"), Benchmark]
    public LanguageExt.Fin<int> LanguageExt_Fin_Create_Fail()
    {
        return LanguageExt.Fin<int>.Fail(LanguageExt.Common.Error.New(Error));
    }

    [BenchmarkCategory("CreationErr"), Benchmark]
    public CSharpFunctionalExtensions.Result<int> CSharpFE_Result_Create_Failure()
    {
        return CSharpFunctionalExtensions.Result.Failure<int>(Error.Message);
    }

    // ─── Bind Ok ────────────────────────────────────────────────────────────────

    private static readonly Result<int> _otherMonadOk = Result<int>.Create.Ok(Value);
    private static readonly LanguageExt.Fin<int> _langExtSucc = LanguageExt.Fin<int>.Succ(Value);
    private static readonly CSharpFunctionalExtensions.Result<int> _csFESuccess = CSharpFunctionalExtensions.Result.Success(Value);

    [BenchmarkCategory("BindOk"), Benchmark(Baseline = true)]
    public Result<int> OtherMonad_Result_Bind_Ok()
    {
        return _otherMonadOk.Bind(x => Result<int>.Create.Ok(x * 2));
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

    // ─── Bind Err ───────────────────────────────────────────────────────────────

    private static readonly Result<int> _otherMonadErr = Result<int>.Create.Err(Error);
    private static readonly LanguageExt.Fin<int> _langExtFail = LanguageExt.Fin<int>.Fail(LanguageExt.Common.Error.New(Error));
    private static readonly CSharpFunctionalExtensions.Result<int> _csFEFailure = CSharpFunctionalExtensions.Result.Failure<int>(Error.Message);

    [BenchmarkCategory("BindErr"), Benchmark(Baseline = true)]
    public Result<int> OtherMonad_Result_Bind_Err()
    {
        return _otherMonadErr.Bind(x => Result<int>.Create.Ok(x * 2));
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

    // ─── Match ──────────────────────────────────────────────────────────────────

    [BenchmarkCategory("Match"), Benchmark(Baseline = true)]
    public string OtherMonad_Result_Match()
    {
        return _otherMonadOk.Match(
            ok: x => x.ToString(),
            err: e => e.Message);
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

    // ─── Try ────────────────────────────────────────────────────────────────────

    [BenchmarkCategory("TrySuccess"), Benchmark(Baseline = true)]
    public Result<int> OtherMonad_Result_Try_Success()
    {
        return Result.Try(() => Value * 2);
    }

    [BenchmarkCategory("TrySuccess"), Benchmark]
    public CSharpFunctionalExtensions.Result<int> CSharpFE_Result_Try_Success()
    {
        try
        {
            return CSharpFunctionalExtensions.Result.Success(Value * 2);
        }
        catch (Exception ex)
        {
            return CSharpFunctionalExtensions.Result.Failure<int>(ex.Message);
        }
    }

    [BenchmarkCategory("TryFailure"), Benchmark(Baseline = true)]
    public Result<int> OtherMonad_Result_Try_Failure()
    {
        return Result.Try<int>(() => throw Error);
    }

    [BenchmarkCategory("TryFailure"), Benchmark]
    public CSharpFunctionalExtensions.Result<int> CSharpFE_Result_Try_Failure()
    {
        try
        {
            throw Error;
        }
        catch (Exception ex)
        {
            return CSharpFunctionalExtensions.Result.Failure<int>(ex.Message);
        }
    }
}
