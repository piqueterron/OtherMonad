namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using Optional;

/// <summary>
/// Benchmarks comparing OtherMonad.Maybe Map operation against LanguageExt.Option and Optional library.
/// Measures Map with Some (value present) and None (no value).
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class MaybeMapBenchmarks
{
    private const int VALUE = 42;

    private static readonly Maybe<int> _otherMonadSome = VALUE;
    private static readonly LanguageExt.Option<int> _langExtSome = Prelude.Some(VALUE);
    private static readonly global::Optional.Option<int> _optionalSome = VALUE.Some();

    [BenchmarkCategory("MapSome"), Benchmark(Baseline = true)]
    public Maybe<string> OtherMonad_Maybe_Map()
    {
        return _otherMonadSome.Map(x => x.ToString());
    }

    [BenchmarkCategory("MapSome"), Benchmark]
    public LanguageExt.Option<string> LanguageExt_Option_Map()
    {
        return _langExtSome.Map(x => x.ToString());
    }

    [BenchmarkCategory("MapSome"), Benchmark]
    public global::Optional.Option<string> Optional_Option_Map()
    {
        return _optionalSome.Map(x => x.ToString());
    }

    private static readonly Maybe<int> _otherMonadNone = Maybe<int>.None;
    private static readonly LanguageExt.Option<int> _langExtNone = LanguageExt.Option<int>.None;
    private static readonly global::Optional.Option<int> _optionalNone = global::Optional.Option.None<int>();

    [BenchmarkCategory("MapNone"), Benchmark]
    public Maybe<string> OtherMonad_Maybe_Map_None()
    {
        return _otherMonadNone.Map(x => x.ToString());
    }

    [BenchmarkCategory("MapNone"), Benchmark]
    public LanguageExt.Option<string> LanguageExt_Option_Map_None()
    {
        return _langExtNone.Map(x => x.ToString());
    }

    [BenchmarkCategory("MapNone"), Benchmark]
    public global::Optional.Option<string> Optional_Option_Map_None()
    {
        return _optionalNone.Map(x => x.ToString());
    }
}
