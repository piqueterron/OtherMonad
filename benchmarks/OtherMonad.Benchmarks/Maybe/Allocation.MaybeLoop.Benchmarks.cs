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
public class AllocationMaybeLoopBenchmarks
{
    private const int ITERATIONS = 1000;

    [BenchmarkCategory("MaybeLoop"), Benchmark(Baseline = true)]
    public int OtherMonad_Maybe_Loop()
    {
        var sum = 0;
        for (var i = 0; i < ITERATIONS; i++)
        {
            Maybe<int> maybe = i;
            sum += maybe.Map(x => x * 2).Match(some: x => x, none: () => 0);
        }
        return sum;
    }

    [BenchmarkCategory("MaybeLoop"), Benchmark]
    public int LanguageExt_Option_Loop()
    {
        var sum = 0;
        for (var i = 0; i < ITERATIONS; i++)
        {
            var option = Prelude.Some(i);
            sum += option.Map(x => x * 2).Match(Some: x => x, None: () => 0);
        }
        return sum;
    }

    [BenchmarkCategory("MaybeLoop"), Benchmark]
    public int Optional_Option_Loop()
    {
        var sum = 0;
        for (var i = 0; i < ITERATIONS; i++)
        {
            var option = i.Some();
            sum += option.Map(x => x * 2).Match(some: x => x, none: () => 0);
        }
        return sum;
    }
}
