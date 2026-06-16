namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using Optional;

/// <summary>
/// Benchmarks comparing OtherMonad.Maybe chained operations against LanguageExt.Option and Optional library.
/// Measures performance of multiple operations chained together (Map + Bind).
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class MaybeChainBenchmarks
{
    private const int VALUE = 42;

    private static readonly Maybe<int> _otherMonadSome = VALUE;
    private static readonly LanguageExt.Option<int> _langExtSome = Prelude.Some(VALUE);
    private static readonly global::Optional.Option<int> _optionalSome = VALUE.Some();

    [BenchmarkCategory("Chain"), Benchmark(Baseline = true)]
    public Maybe<string> OtherMonad_Maybe_Chain()
    {
        return _otherMonadSome
            .Map(x => x * 2)
            .Bind(x => (Maybe<string>)(x > 0 ? x.ToString() : null!));
    }

    [BenchmarkCategory("Chain"), Benchmark]
    public LanguageExt.Option<string> LanguageExt_Option_Chain()
    {
        return _langExtSome
            .Map(x => x * 2)
            .Bind(x => x > 0 ? Prelude.Some(x.ToString()) : LanguageExt.Option<string>.None);
    }

    [BenchmarkCategory("Chain"), Benchmark]
    public global::Optional.Option<string> Optional_Option_Chain()
    {
        return _optionalSome
            .Map(x => x * 2)
            .FlatMap(x => x > 0 ? x.ToString().Some() : global::Optional.Option.None<string>());
    }
}
