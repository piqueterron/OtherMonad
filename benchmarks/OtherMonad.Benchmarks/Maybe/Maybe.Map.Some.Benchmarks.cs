namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using Optional;

/// <summary>
/// Benchmarks comparing OtherMonad.Maybe Map operation against LanguageExt.Option and Optional library.
/// Measures Map with Some (value present).
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
[DisassemblyDiagnoser(
    maxDepth: 4,
    exportCombinedDisassemblyReport: true,
    printSource: true,
    printInstructionAddresses: true)]
public class MaybeMapSomeBenchmarks
{
    private Maybe<int> _otherMonadSome;
    private LanguageExt.Option<int> _langExtSome;
    private global::Optional.Option<int> _optionalSome;

    [GlobalSetup]
    public void Setup()
    {
        _otherMonadSome = 42;
        _langExtSome = Prelude.Some(42);
        _optionalSome = 42.Some();
    }

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
}
