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
public class MaybeBindSomeBenchmarks
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

    [BenchmarkCategory("Bind-Some"), Benchmark(Baseline = true)]
    public Maybe<int> OtherMonad_Bind_Some()
    {
        return _otherMonadSome.Bind(x => (Maybe<int>)(x * 2));
    }

    [BenchmarkCategory("Bind-Some"), Benchmark]
    public LanguageExt.Option<int> LanguageExt_Bind_Some()
    {
        return _langExtSome.Bind(x => Prelude.Some(x * 2));
    }

    [BenchmarkCategory("Bind-Some"), Benchmark]
    public global::Optional.Option<int> Optional_FlatMap_Some()
    {
        return _optionalSome.FlatMap(x => (x * 2).Some());
    }
}