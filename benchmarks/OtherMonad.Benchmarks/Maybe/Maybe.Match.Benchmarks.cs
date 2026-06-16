namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using Optional;

/// <summary>
/// Benchmarks comparing OtherMonad.Maybe Match operation against LanguageExt.Option and Optional library.
/// Measures pattern matching performance for extracting values.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class MaybeMatchBenchmarks
{
    private const int VALUE = 42;

    private static readonly Maybe<int> _otherMonadSome = VALUE;
    private static readonly LanguageExt.Option<int> _langExtSome = Prelude.Some(VALUE);
    private static readonly global::Optional.Option<int> _optionalSome = VALUE.Some();

    [BenchmarkCategory("Match"), Benchmark(Baseline = true)]
    public string OtherMonad_Maybe_Match()
    {
        return _otherMonadSome.Match(
            some: x => x.ToString(),
            none: () => "none");
    }

    [BenchmarkCategory("Match"), Benchmark]
    public string LanguageExt_Option_Match()
    {
        return _langExtSome.Match(
            Some: x => x.ToString(),
            None: () => "none");
    }

    [BenchmarkCategory("Match"), Benchmark]
    public string Optional_Option_Match()
    {
        return _optionalSome.Match(
            some: x => x.ToString(),
            none: () => "none");
    }
}
