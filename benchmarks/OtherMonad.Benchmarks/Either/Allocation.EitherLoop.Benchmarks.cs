namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;

/// <summary>
/// Allocation-focused benchmarks to validate that OtherMonad's struct-based monads
/// achieve zero-allocation for common operations compared to class-based alternatives.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class AllocationEitherLoopBenchmarks
{
    private const int ITERATIONS = 1000;

    [BenchmarkCategory("EitherLoop"), Benchmark(Baseline = true)]
    public int OtherMonad_Either_Loop()
    {
        var sum = 0;
        for (var i = 0; i < ITERATIONS; i++)
        {
            var either = Either<string, int>.Right(i);
            sum += either.Bind(x => Either<string, int>.Right(x * 2))
                .Match(x => x, _ => 0);
        }
        return sum;
    }

    [BenchmarkCategory("EitherLoop"), Benchmark]
    public int LanguageExt_Either_Loop()
    {
        var sum = 0;
        for (var i = 0; i < ITERATIONS; i++)
        {
            var either = Prelude.Right<string, int>(i);
            sum += either.Bind(x => Prelude.Right<string, int>(x * 2))
                .Match(Right: x => x, Left: _ => 0);
        }
        return sum;
    }
}
