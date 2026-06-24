namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using Optional;

/// <summary>
/// Benchmarks comparing OtherMonad.Maybe chained operations against LanguageExt.Option and Optional library.
/// Measures performance of multiple operations chained together (Map + Bind).
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
[DisassemblyDiagnoser(
    maxDepth: 4,
    exportCombinedDisassemblyReport: true,
    printSource: true,
    printInstructionAddresses: true)]
public class MaybeChainBenchmarks
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

    [BenchmarkCategory("Chain"), Benchmark(Baseline = true)]
    public Maybe<string> OtherMonad_Maybe_Chain()
    {
        return _otherMonadSome
            .Map(x => x * 2)
            .Bind(x => (Maybe<string>)(x > 0 ? x.ToString() : null!));
    }

    [BenchmarkCategory("Chain"), Benchmark]
    public LanguageExt.Option<string> LanguageExt_Option_Chain()
    {
        return _langExtSome
            .Map(x => x * 2)
            .Bind(x => x > 0 ? Prelude.Some(x.ToString()) : LanguageExt.Option<string>.None);
    }

    [BenchmarkCategory("Chain"), Benchmark]
    public global::Optional.Option<string> Optional_Option_Chain()
    {
        return _optionalSome
            .Map(x => x * 2)
            .FlatMap(x => x > 0 ? x.ToString().Some() : global::Optional.Option.None<string>());
    }
}
