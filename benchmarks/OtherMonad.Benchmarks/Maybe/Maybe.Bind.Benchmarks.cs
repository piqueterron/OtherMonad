namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using Optional;

/// <summary>
/// Benchmarks comparing OtherMonad.Maybe Bind operation against LanguageExt.Option and Optional library.
/// Measures monadic bind (FlatMap) performance.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class MaybeBindBenchmarks
{
    private const int VALUE = 42;

    private static readonly Maybe<int> _otherMonadSome = VALUE;
    private static readonly LanguageExt.Option<int> _langExtSome = Prelude.Some(VALUE);
    private static readonly global::Optional.Option<int> _optionalSome = VALUE.Some();

    [BenchmarkCategory("Bind"), Benchmark(Baseline = true)]
    public Maybe<int> OtherMonad_Maybe_Bind()
    {
        return _otherMonadSome.Bind(x => (Maybe<int>)(x * 2));
    }

    [BenchmarkCategory("Bind"), Benchmark]
    public LanguageExt.Option<int> LanguageExt_Option_Bind()
    {
        return _langExtSome.Bind(x => Prelude.Some(x * 2));
    }

    [BenchmarkCategory("Bind"), Benchmark]
    public global::Optional.Option<int> Optional_Option_FlatMap()
    {
        return _optionalSome.FlatMap(x => (x * 2).Some());
    }
}
