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
    private const int VALUE = 42;

    [BenchmarkCategory("ChainedMap"), Benchmark(Baseline = true)]
    public Maybe<int> OtherMonad_Maybe_ChainedMap()
    {
        Maybe<int> maybe = VALUE;
        return maybe
            .Map(x => x + 1)
            .Map(x => x * 2)
            .Map(x => x - 3)
            .Map(x => x / 2);
    }

    [BenchmarkCategory("ChainedMap"), Benchmark]
    public LanguageExt.Option<int> LanguageExt_Option_ChainedMap()
    {
        var option = Prelude.Some(VALUE);
        return option
            .Map(x => x + 1)
            .Map(x => x * 2)
            .Map(x => x - 3)
            .Map(x => x / 2);
    }

    [BenchmarkCategory("ChainedMap"), Benchmark]
    public global::Optional.Option<int> Optional_Option_ChainedMap()
    {
        var option = VALUE.Some();
        return option
            .Map(x => x + 1)
            .Map(x => x * 2)
            .Map(x => x - 3)
            .Map(x => x / 2);
    }
}
