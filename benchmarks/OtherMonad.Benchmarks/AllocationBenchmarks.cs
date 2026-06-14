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
public class AllocationBenchmarks
{
    private const int Iterations = 1000;
    private const int Value = 42;

    // ─── Maybe Allocations in Loop ──────────────────────────────────────────────

    [BenchmarkCategory("MaybeLoop"), Benchmark(Baseline = true)]
    public int OtherMonad_Maybe_Loop()
    {
        var sum = 0;
        for (var i = 0; i < Iterations; i++)
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
        for (var i = 0; i < Iterations; i++)
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
        for (var i = 0; i < Iterations; i++)
        {
            var option = i.Some();
            sum += option.Map(x => x * 2).Match(some: x => x, none: () => 0);
        }
        return sum;
    }

    // ─── Either Allocations in Loop ─────────────────────────────────────────────

    [BenchmarkCategory("EitherLoop"), Benchmark(Baseline = true)]
    public int OtherMonad_Either_Loop()
    {
        var sum = 0;
        for (var i = 0; i < Iterations; i++)
        {
            var either = Either<string, int>.Create.Right(i);
            sum += either.Bind(x => Either<string, int>.Create.Right(x * 2))
                         .Match(right: x => x, left: _ => 0);
        }
        return sum;
    }

    [BenchmarkCategory("EitherLoop"), Benchmark]
    public int LanguageExt_Either_Loop()
    {
        var sum = 0;
        for (var i = 0; i < Iterations; i++)
        {
            var either = Prelude.Right<string, int>(i);
            sum += either.Bind(x => Prelude.Right<string, int>(x * 2))
                         .Match(Right: x => x, Left: _ => 0);
        }
        return sum;
    }

    // ─── Chained Map Allocations ────────────────────────────────────────────────

    [BenchmarkCategory("ChainedMap"), Benchmark(Baseline = true)]
    public Maybe<int> OtherMonad_Maybe_ChainedMap()
    {
        Maybe<int> maybe = Value;
        return maybe
            .Map(x => x + 1)
            .Map(x => x * 2)
            .Map(x => x - 3)
            .Map(x => x / 2);
    }

    [BenchmarkCategory("ChainedMap"), Benchmark]
    public Option<int> LanguageExt_Option_ChainedMap()
    {
        var option = Prelude.Some(Value);
        return option
            .Map(x => x + 1)
            .Map(x => x * 2)
            .Map(x => x - 3)
            .Map(x => x / 2);
    }

    [BenchmarkCategory("ChainedMap"), Benchmark]
    public Option<int> Optional_Option_ChainedMap()
    {
        var option = Value.Some();
        return option
            .Map(x => x + 1)
            .Map(x => x * 2)
            .Map(x => x - 3)
            .Map(x => x / 2);
    }
}
