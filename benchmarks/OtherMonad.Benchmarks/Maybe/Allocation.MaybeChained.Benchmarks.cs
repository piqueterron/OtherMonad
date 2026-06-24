namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using Optional;

/// <summary>
/// Allocation-focused benchmarks to validate that OtherMonad's struct-based monads
/// achieve zero-allocation for common operations compared to class-based alternatives.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
[DisassemblyDiagnoser(
    maxDepth: 4,
    exportCombinedDisassemblyReport: true,
    printSource: true,
    printInstructionAddresses: true)]
public class AllocationMaybeChainedBenchmarks
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

    [BenchmarkCategory("ChainedMap"), Benchmark(Baseline = true)]
    public Maybe<int> OtherMonad_Maybe_ChainedMap()
    {
        return _otherMonadSome
            .Map(x => x + 1)
            .Map(x => x * 2)
            .Map(x => x - 3)
            .Map(x => x / 2);
    }

    [BenchmarkCategory("ChainedMap"), Benchmark]
    public LanguageExt.Option<int> LanguageExt_Option_ChainedMap()
    {
        return _langExtSome
            .Map(x => x + 1)
            .Map(x => x * 2)
            .Map(x => x - 3)
            .Map(x => x / 2);
    }

    [BenchmarkCategory("ChainedMap"), Benchmark]
    public global::Optional.Option<int> Optional_Option_ChainedMap()
    {
        return _optionalSome
            .Map(x => x + 1)
            .Map(x => x * 2)
            .Map(x => x - 3)
            .Map(x => x / 2);
    }
}
