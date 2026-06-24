namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;

/// <summary>
/// Benchmarks comparing OtherMonad.Maybe Map operation against LanguageExt.Option and Optional library.
/// Measures Map with None (no value).
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
[DisassemblyDiagnoser(
    maxDepth: 4,
    exportCombinedDisassemblyReport: true,
    printSource: true,
    printInstructionAddresses: true)]
public class MaybeMapNoneBenchmarks
{
    private Maybe<int> _otherMonadNone;
    private LanguageExt.Option<int> _langExtNone;
    private global::Optional.Option<int> _optionalNone;

    [GlobalSetup]
    public void Setup()
    {
        _otherMonadNone = Maybe<int>.None;
        _langExtNone = LanguageExt.Option<int>.None;
        _optionalNone = global::Optional.Option.None<int>();
    }

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
