# OtherMonad.Either

`OtherMonad.Either` provides the `Either<TLeft, TRight>` type — a discriminated union that holds **one of two possible values**. By convention, **Left** represents the _success_ case and **Right** represents the _failure_ case (railway-oriented programming).

## Table of Contents

- [Getting Started](#getting-started)
- [Core Type](#core-type)
- [Creating Values](#creating-values)
- [Extension Methods](#extension-methods)
  - [Match](#match)
  - [TryMatch](#trymatch)
  - [Combine](#combine)
- [Async Support](#async-support)

## Getting Started

```bash
dotnet add package OtherMonad.Either
```

```csharp
using OtherMonad;
```

## Core Type

```csharp
public readonly struct Either<TLeft, TRight> : IEither<TLeft, TRight>
```

| Member | Description |
|--------|-------------|
| `Left` | The success value of type `TLeft`. |
| `Right` | The failure value of type `TRight`. |
| `IsLeft` | `true` when the instance holds a Left (success) value. |

## Creating Values

### Implicit conversion

```csharp
Either<int, string> ok  = 42;           // Left / success
Either<int, string> err = "not found";  // Right / failure
```

### Explicit factory

```csharp
var ok  = Either<int, string>.Create.Left(42);
var err = Either<int, string>.Create.Right("not found");
```

> Both factory methods throw `ArgumentNullException` if `null` is supplied.

## Extension Methods

### Match

Evaluates the Either and returns a result by applying the corresponding function.

```csharp
// Synchronous
string msg = either.Match(
    left:  v   => $"Value: {v}",
    right: err => $"Error: {err}");

// Asynchronous
string msg = await either.Match(
    left:  async (v, ct)   => await GetSuccessMessage(v, ct),
    right: async (e, ct)   => await GetErrorMessage(e, ct),
    cancellation: token);
```

### TryMatch

Same as `Match` but silently returns `@default` if either function is `null` or throws.

```csharp
string safe = either.TryMatch(
    left:     v   => $"Value: {v}",
    right:    err => $"Error: {err}",
    @default: "fallback");
```

### Combine

Merges two `Either` instances using a left selector (both success) or a right selector (any failure).

```csharp
var combined = first.Combine(
    second,
    selectorLeft:  (a, b) => a + b,
    selectorRight: (e1, e2) => $"{e1} | {e2}");
```

> When states are mixed (one Left, one Right), the result is always a failure.

## Async Support

All `Match` variants have `Task<TResult>` overloads that accept `CancellationToken`.

```csharp
var result = await either.Match(
    left:  (v, ct)   => Task.FromResult($"ok: {v}"),
    right: (e, ct)   => Task.FromResult($"err: {e}"));
```
