# [<img src="OtherMonad.256x256.png" width="25"/>](OtherMonad.256x256.png "OtherMonad") OtherMonad

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=piqueterron_OtherMonad&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=piqueterron_OtherMonad)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=piqueterron_OtherMonad&metric=coverage)](https://sonarcloud.io/summary/new_code?id=piqueterron_OtherMonad)

A lightweight .NET functional library with three monadic types:

- `Maybe<T>`: optional values (`Some` or `None`)
- `Either<TLeft, TRight>`: typed failure/success (`Left = failure`, `Right = success`)
- `Result<T>`: semantic specialization of `Either<Exception, T>`

## Packages

| Package | NuGet |
|---|---|
| `OtherMonad.Maybe` | [![NuGet](https://img.shields.io/nuget/v/OtherMonad.Maybe)](https://www.nuget.org/packages/OtherMonad.Maybe) |
| `OtherMonad.Either` | [![NuGet](https://img.shields.io/nuget/v/OtherMonad.Either)](https://www.nuget.org/packages/OtherMonad.Either) |
| `OtherMonad.Result` | [![NuGet](https://img.shields.io/nuget/v/OtherMonad.Result)](https://www.nuget.org/packages/OtherMonad.Result) |

## Installation

```bash
dotnet add package OtherMonad.Maybe
dotnet add package OtherMonad.Either
dotnet add package OtherMonad.Result
```

## API Guide

Full package guides:
- [`src/OtherMonad.Maybe/README.md`](src/OtherMonad.Maybe/README.md)
- [`src/OtherMonad.Either/README.md`](src/OtherMonad.Either/README.md)
- [`src/OtherMonad.Result/README.md`](src/OtherMonad.Result/README.md)

Quick overview:

### Maybe

- `Wrap`, `Unwrap`
- `Map` (single `Maybe` + sequence overloads, sync/async + deferred variants)
- `Bind` (sync/async + deferred variants that return `Maybe`)
- `Match` (sync/async + deferred variants)
- `OrElse` (sync/async + deferred variants)
- `Combine`, `TryCombine` (+ `Defer` variants)

### Either

- `Match`, `TryMatch` (sync/async)
- `Bind` (sync/async)
- `Map` (sync/async)
- `OrElse` (sync/async)
- `Combine`

### Result

- `Match`, `TryMatch` (sync/async)
- `Bind` (sync/async)
- `Map` (sync/async)
- `OrElse` (sync/async)
- `GetValueOrDefault`
- `Combine`
- `Try` (sync/async)

## Why both `Result` and `Either`?

Use **`Either<TLeft, TRight>`** when:
- your error side is a domain type (for example `ValidationError`, `ApiProblem`, `ErrorCode`)
- the failure side is not necessarily an `Exception`

Use **`Result<T>`** when:
- your error flow is exception-based
- you prefer explicit `Ok` / `Err` terminology
- you need to wrap exception-throwing code with `Result.Try(...)`

`Result<T>` implements `IEither<Exception, T>` and can convert implicitly to/from `Either<Exception, T>`.

## Advanced Scenarios

### 1) Async/await Pipelines (`Bind`/`Map` async overloads)

`OtherMonad` does not expose methods named `BindAsync` or `MapAsync`; instead, use the async overloads of `Bind` and `Map`.

```csharp
using OtherMonad;

var parseAge = await Either<string, string>.Create.Right("42")
    .Bind(async (text, ct) =>
    {
        await Task.Delay(5, ct);
        return int.TryParse(text, out var age)
            ? Either<string, int>.Create.Right(age)
            : Either<string, int>.Create.Left("Age is not numeric");
    });

var category = await parseAge.Map(async (age, ct) =>
{
    await Task.Delay(5, ct);
    return age >= 18 ? "adult" : "minor";
});

string message = category.Match(
    left: error => $"Cannot classify user: {error}",
    right: value => $"User category: {value}");
```

### 2) Monad Composition (nested monads)

Example: `Either<string, Maybe<int>>` representing transport success/failure with an optional payload.

```csharp
using OtherMonad;

Either<string, Maybe<int>> maybeDiscountFromService =
    Either<string, Maybe<int>>.Create.Right(20.Wrap());

string result = maybeDiscountFromService.Match(
    left: serviceError => $"Service error: {serviceError}",
    right: maybeDiscount => maybeDiscount.Match(
        some: discount => $"Discount applied: {discount}%",
        none: () => "Request succeeded but no discount is available"));
```

### 3) Type Conversions

#### `Maybe<T>` -> `Either<TLeft, TRight>`

```csharp
using OtherMonad;

Maybe<int> maybePort = 8080.Wrap();

Either<string, int> validatedPort = maybePort.Match(
    some: port => port > 0
        ? Either<string, int>.Create.Right(port)
        : Either<string, int>.Create.Left("Port must be greater than zero"),
    none: () => Either<string, int>.Create.Left("Port value is missing"));
```

#### `Either<Exception, T>` <-> `Result<T>`

```csharp
using OtherMonad;

Either<Exception, int> either = Either<Exception, int>.Create.Right(7);
Result<int> result = either;                     // implicit conversion
Either<Exception, int> roundTrip = result;       // implicit conversion
```

#### `Result<T>` -> `Maybe<T>`

```csharp
using OtherMonad;

Result<int> parsed = Result.Try(() => int.Parse("42"));

Maybe<int> maybeValue = parsed.Match(
    onErr: _ => Maybe<int>.None,
    onOk: value => value.Wrap());
```

## License

This project is licensed under the [MIT License](LICENSE).
