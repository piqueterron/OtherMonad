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
public class MaybeBindChainedBenchmarks
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

    [BenchmarkCategory("Bind-Chain"), Benchmark(Baseline = true)]
    public Maybe<int> OtherMonad_Chain_Bind()
    {
        return _otherMonadSome
            .Bind(x => (Maybe<int>)(x + 5))
            .Bind(x => (Maybe<int>)(x * 2))
            .Bind(x => (Maybe<int>)(x - 3));
    }

    [BenchmarkCategory("Bind-Chain"), Benchmark]
    public LanguageExt.Option<int> LanguageExt_Chain_Bind()
    {
        return _langExtSome
            .Bind(x => Prelude.Some(x + 5))
            .Bind(x => Prelude.Some(x * 2))
            .Bind(x => Prelude.Some(x - 3));
    }

    [BenchmarkCategory("Bind-Chain"), Benchmark]
    public global::Optional.Option<int> Optional_Chain_FlatMap()
    {
        return _optionalSome
            .FlatMap(x => (x + 5).Some())
            .FlatMap(x => (x * 2).Some())
            .FlatMap(x => (x - 3).Some());
    }
}