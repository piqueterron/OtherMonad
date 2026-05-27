# OtherMonad.Result

`Result<T>` models an operation that either:

- succeeds with a value (`Ok`)
- fails with an `Exception` (`Err`)

Internally, it is a specialization of `Either<Exception, T>`.

## Why `Result` in addition to `Either`?

`Either<TLeft, TRight>` supports any failure type on `Left`.

`Result<T>` exists for the common .NET case where failures are exceptions:
- clearer C# vocabulary (`Ok`/`Err`)
- direct interoperability with exception-based APIs via `Result.Try`
- still compatible with `Either<Exception, T>` through implicit conversions

### When to use each type

Use `Either<TLeft, TRight>` when your failure is a domain type.

Use `Result<T>` when your failure model is `Exception`.

## Installation

```bash
dotnet add package OtherMonad.Result
```

```csharp
using OtherMonad;
```

## Core type

```csharp
public readonly struct Result<T> : IResult<T>, IEquatable<Result<T>>
```

Key members:
- `IsOk`, `IsErr`
- `Value`, `Error`
- `Create.Ok(value)`, `Create.Err(exception)`
- implicit conversion with `Either<Exception, T>`

## Complete API

### Match / TryMatch (sync/async)

- `Match<T, TResult>(this IResult<T>, Func<Exception, TResult> onErr, Func<T, TResult> onOk)`
- `Match<T, TResult>(this IResult<T>, Func<Exception, CancellationToken, Task<TResult>> onErr, Func<T, CancellationToken, Task<TResult>> onOk, CancellationToken)`
- `TryMatch<T, TResult>(..., TResult default = default!)` (sync and async)

```csharp
using OtherMonad;

Result<int> score = Result<int>.Create.Ok(92);

string report = score.Match(
    onErr: ex => $"Could not compute report: {ex.Message}",
    onOk: value => value >= 90 ? "Excellent" : "Keep going");

string safeReport = score.TryMatch(
    onErr: _ => throw new InvalidOperationException(),
    onOk: value => $"Score={value}",
    @default: "Report unavailable");
```

### Bind (sync/async)

- `Bind<T, TResult>(this Result<T>, Func<T, Result<TResult>> selector)`
- `Bind<T, TResult>(this Result<T>, Func<T, CancellationToken, Task<Result<TResult>>> selector, CancellationToken)`

```csharp
using OtherMonad;

Result<string> input = Result<string>.Create.Ok("42");

Result<int> parsed = input.Bind(text =>
    int.TryParse(text, out var value)
        ? Result<int>.Create.Ok(value)
        : Result<int>.Create.Err(new FormatException("Input is not numeric")));

Result<int> parsedAsync = await input.Bind(async (text, ct) =>
{
    await Task.Delay(5, ct);
    return int.TryParse(text, out var value)
        ? Result<int>.Create.Ok(value)
        : Result<int>.Create.Err(new FormatException("Input is not numeric"));
});
```

### Map (sync/async)

- `Map<T, TResult>(this Result<T>, Func<T, TResult> selector)`
- `Map<T, TResult>(this Result<T>, Func<T, CancellationToken, Task<TResult>> selector, CancellationToken)`

```csharp
using OtherMonad;

Result<int> baseValue = Result<int>.Create.Ok(5);
Result<string> mapped = baseValue.Map(v => $"Value:{v}");

Result<string> mappedAsync = await baseValue.Map(async (v, ct) =>
{
    await Task.Delay(5, ct);
    return $"AsyncValue:{v}";
});
```

### OrElse (sync/async)

- `OrElse<T>(this Result<T>, Result<T> fallback)`
- `OrElse<T>(this Result<T>, Func<CancellationToken, Task<Result<T>>> fallbackFactory, CancellationToken)`

```csharp
using OtherMonad;

Result<int> primary = Result<int>.Create.Err(new Exception("Primary failed"));
Result<int> fallback = Result<int>.Create.Ok(10);

Result<int> recovered = primary.OrElse(fallback);

Result<int> recoveredAsync = await primary.OrElse(async ct =>
{
    await Task.Delay(5, ct);
    return Result<int>.Create.Ok(20);
});
```

### GetValueOrDefault

- `GetValueOrDefault<T>(this Result<T>, T default = default!)`

```csharp
using OtherMonad;

int value1 = Result<int>.Create.Ok(7).GetValueOrDefault(0);
int value2 = Result<int>.Create.Err(new Exception("x")).GetValueOrDefault(0);
```

### Combine

- `Combine<T, TOther, TResult>(this Result<T>, Result<TOther>, Func<T, TOther, TResult> selectorOk)`

```csharp
using OtherMonad;

Result<int> left = Result<int>.Create.Ok(3);
Result<int> right = Result<int>.Create.Ok(4);

Result<int> multiplied = left.Combine(right, (a, b) => a * b);
```

### Try (sync/async)

- `Try<T>(Func<T> factory)`
- `Try<T>(Func<CancellationToken, Task<T>> factory, CancellationToken)`

```csharp
using OtherMonad;

Result<int> parsed = Result.Try(() => int.Parse("42"));
Result<int> failed = Result.Try(() => int.Parse("not-a-number"));

Result<string> downloaded = await Result.Try(async ct =>
{
    await Task.Delay(5, ct);
    return "payload";
});
```

## Advanced Scenarios

### Async/await Processing Pipeline

```csharp
using OtherMonad;

Result<string> input = Result<string>.Create.Ok("25");

var final = await input
    .Bind(async (text, ct) =>
    {
        await Task.Delay(5, ct);
        return int.TryParse(text, out var number)
            ? Result<int>.Create.Ok(number)
            : Result<int>.Create.Err(new FormatException("Invalid integer"));
    })
    .Map(async (number, ct) =>
    {
        await Task.Delay(5, ct);
        return number * 2;
    });

string output = final.Match(
    onErr: ex => $"Pipeline failed: {ex.Message}",
    onOk: value => $"Pipeline value: {value}");
```

### Compose with `Maybe`

```csharp
using OtherMonad;

Result<Maybe<int>> maybeQuota = Result<Maybe<int>>.Create.Ok(Maybe<int>.None);

string text = maybeQuota.Match(
    onErr: ex => $"Error: {ex.Message}",
    onOk: maybe => maybe.Match(
        some: quota => $"Quota={quota}",
        none: () => "No quota configured"));
```

### Type Conversions

#### `Either<Exception, T>` <-> `Result<T>`

```csharp
using OtherMonad;

Either<Exception, int> either = Either<Exception, int>.Create.Right(7);
Result<int> result = either;
Either<Exception, int> again = result;
```

#### `Maybe<T>` -> `Result<T>`

```csharp
using OtherMonad;

Maybe<int> maybe = Maybe<int>.None;

Result<int> asResult = maybe.Match(
    some: value => Result<int>.Create.Ok(value),
    none: () => Result<int>.Create.Err(new InvalidOperationException("Value is missing")));
```
