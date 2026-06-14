namespace OtherMonad.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using LanguageExt;
using Optional;
using Optional.Unsafe;

/// <summary>
/// Benchmarks comparing OtherMonad.Maybe against LanguageExt.Option and Optional library.
/// Measures core monadic operations: creation, Map, Bind, and Match.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class MaybeBenchmarks
{
    private const int Value = 42;

    // ─── Creation ───────────────────────────────────────────────────────────────

    [BenchmarkCategory("Creation"), Benchmark(Baseline = true)]
    public Maybe<int> OtherMonad_Maybe_Create_Some()
    {
        return Value;
    }

    [BenchmarkCategory("Creation"), Benchmark]
    public Option<int> LanguageExt_Option_Create_Some()
    {
        return Prelude.Some(Value);
    }

    [BenchmarkCategory("Creation"), Benchmark]
    public Option<int> Optional_Option_Create_Some()
    {
        return Value.Some();
    }

    [BenchmarkCategory("Creation"), Benchmark]
    public Maybe<int> OtherMonad_Maybe_Create_None()
    {
        return Maybe<int>.None;
    }

    [BenchmarkCategory("Creation"), Benchmark]
    public Option<int> LanguageExt_Option_Create_None()
    {
        return Option<int>.None;
    }

    [BenchmarkCategory("Creation"), Benchmark]
    public Option<int> Optional_Option_Create_None()
    {
        return Option.None<int>();
    }

    // ─── Map ────────────────────────────────────────────────────────────────────

    private static readonly Maybe<int> _otherMonadSome = Value;
    private static readonly Option<int> _langExtSome = Prelude.Some(Value);
    private static readonly Option<int> _optionalSome = Value.Some();

    [BenchmarkCategory("Map"), Benchmark(Baseline = true)]
    public Maybe<string> OtherMonad_Maybe_Map()
    {
        return _otherMonadSome.Map(x => x.ToString());
    }

    [BenchmarkCategory("Map"), Benchmark]
    public Option<string> LanguageExt_Option_Map()
    {
        return _langExtSome.Map(x => x.ToString());
    }

    [BenchmarkCategory("Map"), Benchmark]
    public Option<string> Optional_Option_Map()
    {
        return _optionalSome.Map(x => x.ToString());
    }

    // ─── Map None ───────────────────────────────────────────────────────────────

    private static readonly Maybe<int> _otherMonadNone = Maybe<int>.None;
    private static readonly Option<int> _langExtNone = Option<int>.None;
    private static readonly Option<int> _optionalNone = Option.None<int>();

    [BenchmarkCategory("MapNone"), Benchmark(Baseline = true)]
    public Maybe<string> OtherMonad_Maybe_Map_None()
    {
        return _otherMonadNone.Map(x => x.ToString());
    }

    [BenchmarkCategory("MapNone"), Benchmark]
    public Option<string> LanguageExt_Option_Map_None()
    {
        return _langExtNone.Map(x => x.ToString());
    }

    [BenchmarkCategory("MapNone"), Benchmark]
    public Option<string> Optional_Option_Map_None()
    {
        return _optionalNone.Map(x => x.ToString());
    }

    // ─── Bind ───────────────────────────────────────────────────────────────────

    [BenchmarkCategory("Bind"), Benchmark(Baseline = true)]
    public Maybe<int> OtherMonad_Maybe_Bind()
    {
        return _otherMonadSome.Bind(x => (Maybe<int>)(x * 2));
    }

    [BenchmarkCategory("Bind"), Benchmark]
    public Option<int> LanguageExt_Option_Bind()
    {
        return _langExtSome.Bind(x => Prelude.Some(x * 2));
    }

    [BenchmarkCategory("Bind"), Benchmark]
    public Option<int> Optional_Option_FlatMap()
    {
        return _optionalSome.FlatMap(x => (x * 2).Some());
    }

    // ─── Match ──────────────────────────────────────────────────────────────────

    [BenchmarkCategory("Match"), Benchmark(Baseline = true)]
    public string OtherMonad_Maybe_Match()
    {
        return _otherMonadSome.Match(
            some: x => x.ToString(),
            none: () => "none");
    }

    [BenchmarkCategory("Match"), Benchmark]
    public string LanguageExt_Option_Match()
    {
        return _langExtSome.Match(
            Some: x => x.ToString(),
            None: () => "none");
    }

    [BenchmarkCategory("Match"), Benchmark]
    public string Optional_Option_Match()
    {
        return _optionalSome.Match(
            some: x => x.ToString(),
            none: () => "none");
    }

    // ─── Chained Operations ─────────────────────────────────────────────────────

    [BenchmarkCategory("Chain"), Benchmark(Baseline = true)]
    public Maybe<string> OtherMonad_Maybe_Chain()
    {
        return _otherMonadSome
            .Map(x => x * 2)
            .Bind(x => (Maybe<string>)(x > 0 ? x.ToString() : null!));
    }

    [BenchmarkCategory("Chain"), Benchmark]
    public Option<string> LanguageExt_Option_Chain()
    {
        return _langExtSome
            .Map(x => x * 2)
            .Bind(x => x > 0 ? Prelude.Some(x.ToString()) : Option<string>.None);
    }

    [BenchmarkCategory("Chain"), Benchmark]
    public Option<string> Optional_Option_Chain()
    {
        return _optionalSome
            .Map(x => x * 2)
            .FlatMap(x => x > 0 ? x.ToString().Some() : Option.None<string>());
    }
}
