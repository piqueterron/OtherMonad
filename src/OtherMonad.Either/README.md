# OtherMonad.Either

`Either<TLeft, TRight>` es una unión discriminada con dos estados:

- `Right`: éxito
- `Left`: error/fracaso

## Instalación

```bash
dotnet add package OtherMonad.Either
```

```csharp
using OtherMonad;
```

## Tipo base

```csharp
public readonly struct Either<TLeft, TRight> : IEither<TLeft, TRight>, IEquatable<Either<TLeft, TRight>>
```

- `IsRight`, `IsLeft`
- `Right`, `Left`
- `Create.Right(value)`, `Create.Left(error)`

## API completa

### Match / TryMatch (sync/async)

- `Match<TLeft, TRight, TResult>(this IEither<TLeft, TRight>, Func<TLeft, TResult> left, Func<TRight, TResult> right)`
- `Match<TLeft, TRight, TResult>(this IEither<TLeft, TRight>, Func<TLeft, CancellationToken, Task<TResult>> left, Func<TRight, CancellationToken, Task<TResult>> right, CancellationToken)`
- `TryMatch<TLeft, TRight, TResult>(..., TResult default = default!)` (sync y async)

```csharp
Either<string, int> either = Either<string, int>.Create.Right(42);
string msg = either.Match(
    left: err => $"Error: {err}",
    right: v => $"Value: {v}");
```

### Bind (sync/async)

- `Bind<TLeft, TRight, TResult>(this Either<TLeft, TRight>, Func<TRight, Either<TLeft, TResult>> selector)`
- `Bind<TLeft, TRight, TResult>(this Either<TLeft, TRight>, Func<TRight, CancellationToken, Task<Either<TLeft, TResult>>> selector, CancellationToken)`

```csharp
Either<string, string> next = either.Bind(v =>
    v > 0
        ? Either<string, string>.Create.Right($"ok:{v}")
        : Either<string, string>.Create.Left("invalid"));
```

### Map (sync/async)

- `Map<TLeft, TRight, TResult>(this Either<TLeft, TRight>, Func<TRight, TResult> selector)`
- `Map<TLeft, TRight, TResult>(this Either<TLeft, TRight>, Func<TRight, CancellationToken, Task<TResult>> selector, CancellationToken)`

```csharp
Either<string, string> mapped = either.Map(v => v.ToString());
```

### OrElse (sync/async)

- `OrElse<TLeft, TRight>(this Either<TLeft, TRight>, Either<TLeft, TRight> fallback)`
- `OrElse<TLeft, TRight>(this Either<TLeft, TRight>, Func<CancellationToken, Task<Either<TLeft, TRight>>> fallbackFactory, CancellationToken)`

```csharp
Either<string, int> safe = Either<string, int>.Create.Left("bad")
    .OrElse(Either<string, int>.Create.Right(0));
```

### Combine

- `Combine<TSourceLeft, TSourceRight, TOtherLeft, TOtherRight, TLeft, TRight>(this IEither<TSourceLeft, TSourceRight>, IEither<TOtherLeft, TOtherRight>, Func<TSourceLeft?, TOtherLeft?, TLeft> selectorLeft, Func<TSourceRight, TOtherRight, TRight> selectorRight)`

```csharp
var combined = Either<string, int>.Create.Right(2).Combine(
    Either<string, int>.Create.Right(3),
    selectorLeft: (l1, l2) => l1 ?? l2 ?? "error",
    selectorRight: (r1, r2) => r1 + r2);
```

## Escenarios avanzados

### Async/await (patrón BindAsync/MapAsync)

```csharp
var asyncMapped = await either.Map(async (v, ct) =>
{
    await Task.Delay(10, ct);
    return v * 10;
});
```

### Composición con Maybe

```csharp
Either<string, Maybe<int>> nested = Either<string, Maybe<int>>.Create.Right(7.Wrap());
```

### Conversiones con Result

```csharp
Either<Exception, int> e = Either<Exception, int>.Create.Right(7);
Result<int> r = e;                 // implícita
Either<Exception, int> e2 = r;     // implícita
```
