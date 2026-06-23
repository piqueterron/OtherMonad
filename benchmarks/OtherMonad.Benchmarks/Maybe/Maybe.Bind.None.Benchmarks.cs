namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using Optional;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
[DisassemblyDiagnoser(
    maxDepth: 4,
    exportCombinedDisassemblyReport: true,
    printSource: true,
    printInstructionAddresses: true)]
public class MaybeBindNoneBenchmarks
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

    [BenchmarkCategory("Bind-None"), Benchmark(Baseline = true)]
    public Maybe<int> OtherMonad_Bind_None()
    {
        return _otherMonadNone.Bind(x => (Maybe<int>)(x * 2));
    }

    [BenchmarkCategory("Bind-None"), Benchmark]
    public LanguageExt.Option<int> LanguageExt_Bind_None()
    {
        return _langExtNone.Bind(x => Prelude.Some(x * 2));
    }

    [BenchmarkCategory("Bind-None"), Benchmark]
    public global::Optional.Option<int> Optional_FlatMap_None()
    {
        return _optionalNone.FlatMap(x => (x * 2).Some());
    }
}