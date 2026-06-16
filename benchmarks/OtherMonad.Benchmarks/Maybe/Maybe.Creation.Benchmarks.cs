namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using Optional;

/// <summary>
/// Benchmarks comparing OtherMonad.Maybe creation against LanguageExt.Option and Optional library.
/// Measures creation of Some (with value) and None (without value).
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class MaybeCreationBenchmarks
{
    private const int VALUE = 42;

    [BenchmarkCategory("CreationSome"), Benchmark(Baseline = true)]
    public Maybe<int> OtherMonad_Maybe_Create_Some()
    {
        return VALUE;
    }

    [BenchmarkCategory("CreationSome"), Benchmark]
    public LanguageExt.Option<int> LanguageExt_Option_Create_Some()
    {
        return Prelude.Some(VALUE);
    }

    [BenchmarkCategory("CreationSome"), Benchmark]
    public global::Optional.Option<int> Optional_Option_Create_Some()
    {
        return VALUE.Some();
    }

    [BenchmarkCategory("CreationNone"), Benchmark]
    public Maybe<int> OtherMonad_Maybe_Create_None()
    {
        return Maybe<int>.None;
    }

    [BenchmarkCategory("CreationNone"), Benchmark]
    public LanguageExt.Option<int> LanguageExt_Option_Create_None()
    {
        return LanguageExt.Option<int>.None;
    }

    [BenchmarkCategory("CreationNone"), Benchmark]
    public global::Optional.Option<int> Optional_Option_Create_None()
    {
        return global::Optional.Option.None<int>();
    }
}
