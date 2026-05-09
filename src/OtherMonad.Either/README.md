# OtherMonad.Either

`Either<TLeft, TRight>` is a two-state discriminated union:

- `Right`: success value
- `Left`: failure value

By convention in this library: **Right is success**.

## Installation

```bash
dotnet add package OtherMonad.Either
```

```csharp
using OtherMonad;
```

## Core type

```csharp
public readonly struct Either<TLeft, TRight> : IEither<TLeft, TRight>, IEquatable<Either<TLeft, TRight>>
```

Key members:
- `IsRight`, `IsLeft`
- `Right`, `Left`
- `Create.Right(value)`, `Create.Left(error)`

## Complete API

### Match / TryMatch (sync/async)

- `Match<TLeft, TRight, TResult>(this IEither<TLeft, TRight>, Func<TLeft, TResult> left, Func<TRight, TResult> right)`
- `Match<TLeft, TRight, TResult>(this IEither<TLeft, TRight>, Func<TLeft, CancellationToken, Task<TResult>> left, Func<TRight, CancellationToken, Task<TResult>> right, CancellationToken)`
- `TryMatch<TLeft, TRight, TResult>(..., TResult default = default!)` (sync and async)

```csharp
using OtherMonad;

Either<string, int> parseResult = Either<string, int>.Create.Right(42);

string display = parseResult.Match(
    left: error => $"Could not parse input: {error}",
    right: value => $"Parsed value: {value}");

string safeDisplay = parseResult.TryMatch(
    left: error => throw new InvalidOperationException(error),
    right: value => $"Value={value}",
    @default: "Fallback display");
```

### Bind (sync/async)

- `Bind<TLeft, TRight, TResult>(this Either<TLeft, TRight>, Func<TRight, Either<TLeft, TResult>> selector)`
- `Bind<TLeft, TRight, TResult>(this Either<TLeft, TRight>, Func<TRight, CancellationToken, Task<Either<TLeft, TResult>>> selector, CancellationToken)`

```csharp
using OtherMonad;

Either<string, int> portInput = Either<string, int>.Create.Right(8080);

Either<string, string> endpoint = portInput.Bind(port =>
    port is > 0 and < 65536
        ? Either<string, string>.Create.Right($"https://localhost:{port}")
        : Either<string, string>.Create.Left("Port out of range"));

Either<string, string> endpointAsync = await portInput.Bind(async (port, ct) =>
{
    await Task.Delay(5, ct);
    return port % 2 == 0
        ? Either<string, string>.Create.Right($"even-port:{port}")
        : Either<string, string>.Create.Left("Only even ports are allowed");
});
```

### Map (sync/async)

- `Map<TLeft, TRight, TResult>(this Either<TLeft, TRight>, Func<TRight, TResult> selector)`
- `Map<TLeft, TRight, TResult>(this Either<TLeft, TRight>, Func<TRight, CancellationToken, Task<TResult>> selector, CancellationToken)`

```csharp
using OtherMonad;

Either<string, int> length = Either<string, string>.Create.Right("othermonad")
    .Map(text => text.Length);

Either<string, string> asyncMapped = await length.Map(async (value, ct) =>
{
    await Task.Delay(5, ct);
    return $"Length={value}";
});
```

### OrElse (sync/async)

- `OrElse<TLeft, TRight>(this Either<TLeft, TRight>, Either<TLeft, TRight> fallback)`
- `OrElse<TLeft, TRight>(this Either<TLeft, TRight>, Func<CancellationToken, Task<Either<TLeft, TRight>>> fallbackFactory, CancellationToken)`

```csharp
using OtherMonad;

Either<string, int> failing = Either<string, int>.Create.Left("Primary source failed");
Either<string, int> recovered = failing.OrElse(Either<string, int>.Create.Right(100));

Either<string, int> recoveredAsync = await failing.OrElse(async ct =>
{
    await Task.Delay(5, ct);
    return Either<string, int>.Create.Right(200);
});
```

### Combine

- `Combine<TSourceLeft, TSourceRight, TOtherLeft, TOtherRight, TLeft, TRight>(this IEither<TSourceLeft, TSourceRight>, IEither<TOtherLeft, TOtherRight>, Func<TSourceLeft?, TOtherLeft?, TLeft> selectorLeft, Func<TSourceRight, TOtherRight, TRight> selectorRight)`

```csharp
using OtherMonad;

var serviceA = Either<string, int>.Create.Right(20);
var serviceB = Either<string, int>.Create.Right(22);

Either<string, int> total = serviceA.Combine(
    serviceB,
    selectorLeft: (leftA, leftB) => $"Errors: {leftA ?? "none"} | {leftB ?? "none"}",
    selectorRight: (a, b) => a + b);
```

## Advanced scenarios

### Async/await flow (`Bind` + `Map`)

```csharp
using OtherMonad;

Either<string, string> input = Either<string, string>.Create.Right("500");

var validated = await input.Bind(async (raw, ct) =>
{
    await Task.Delay(5, ct);
    return int.TryParse(raw, out var number)
        ? Either<string, int>.Create.Right(number)
        : Either<string, int>.Create.Left("Input is not a number");
});

var httpCode = await validated.Map(async (code, ct) =>
{
    await Task.Delay(5, ct);
    return $"HTTP {code}";
});
```

### Compose with `Maybe`

```csharp
using OtherMonad;

Either<string, Maybe<int>> maybeValue = Either<string, Maybe<int>>.Create.Right(Maybe<int>.None);

string result = maybeValue.Match(
    left: error => $"Failure: {error}",
    right: maybe => maybe.Match(
        some: value => $"Success with value {value}",
        none: () => "Success but no value"));
```

### Convert with `Result`

```csharp
using OtherMonad;

Either<Exception, int> either = Either<Exception, int>.Create.Right(123);
Result<int> asResult = either;
Either<Exception, int> asEitherAgain = asResult;
```
