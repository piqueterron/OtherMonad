# OtherMonad.Either

`OtherMonad.Either` provides the `Either<TLeft, TRight>` type — a discriminated union that holds **one of two possible values**. Following the convention used by Haskell, fp-ts, and LanguageExt:

> **Right = success &nbsp;·&nbsp; Left = failure/error** *(right is right)*

## Table of Contents

- [Getting Started](#getting-started)
- [Core Type](#core-type)
- [Creating Values](#creating-values)
- [Extension Methods](#extension-methods)
  - [Match](#match)
  - [TryMatch](#trymatch)
  - [Bind](#bind)
  - [Map](#map)
  - [OrElse](#orelse)
  - [Combine](#combine)
- [Equality](#equality)
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
public readonly struct Either<TLeft, TRight> : IEither<TLeft, TRight>, IEquatable<Either<TLeft, TRight>>
```

| Member | Description |
|--------|-------------|
| `Right` | The **success** value of type `TRight`. Throws `InvalidOperationException` if accessed in the Left state. |
| `Left` | The **failure/error** value of type `TLeft`. Throws `InvalidOperationException` if accessed in the Right state. |
| `IsRight` | `true` when the instance holds a Right (success) value. |
| `IsLeft` | `true` when the instance holds a Left (failure/error) value. |

## Creating Values

```csharp
// Right = success
var ok  = Either<Exception, int>.Create.Right(42);

// Left = failure/error
var err = Either<Exception, int>.Create.Left(new Exception("not found"));
```

> Both factory methods throw `ArgumentNullException` if `null` is supplied.

## Extension Methods

### Match

Evaluates the Either and returns a result by applying the corresponding function.

```csharp
// Synchronous — left handles failure, right handles success
string msg = either.Match(
    left:  err => $"Error: {err.Message}",
    right: v   => $"Value: {v}");

// Asynchronous
string msg = await either.Match(
    left:  async (err, ct) => await GetErrorMessage(err, ct),
    right: async (v,   ct) => await GetSuccessMessage(v, ct),
    cancellation: token);
```

### TryMatch

Same as `Match` but silently returns `@default` if either function is `null` or throws.

```csharp
string safe = either.TryMatch(
    left:     err => $"Error: {err.Message}",
    right:    v   => $"Value: {v}",
    @default: "fallback");
```

### Bind

If in the Right (success) state, applies a function that returns a new Either. Propagates Left unchanged.

```csharp
Either<Exception, string> result = either.Bind(n => Either<Exception, string>.Create.Right(n.ToString()));
```

### Map

If in the Right (success) state, transforms the Right value. Propagates Left unchanged.

```csharp
Either<Exception, string> result = either.Map(n => n.ToString());
```

### OrElse

If in the Left (failure) state, returns the provided fallback Either. Returns self when Right.

```csharp
Either<Exception, int> result = either.OrElse(Either<Exception, int>.Create.Right(0));

// Async overload with factory
Either<Exception, int> result = await either.OrElse(
    (ct) => Task.FromResult(Either<Exception, int>.Create.Right(0)), token);
```

### Combine

Merges two `Either` instances:
- **Both Right (success)**: applies `selectorRight`.
- **Both Left (failure)**: applies `selectorLeft`.
- **Mixed**: always returns Left (failure). `selectorLeft` is called with the available Left value and `null` for the missing side.

```csharp
var combined = first.Combine(
    second,
    selectorLeft:  (e1, e2) => new AggregateException(e1, e2),
    selectorRight: (a,  b)  => a + b);
```

## Equality

`Either<TLeft, TRight>` implements `IEquatable<Either<TLeft, TRight>>` with `==` / `!=` operators and a correct `GetHashCode`.

```csharp
var a = Either<Exception, int>.Create.Right(42);
var b = Either<Exception, int>.Create.Right(42);

bool equal = a == b; // true
```

## Async Support

`Bind`, `Map`, `OrElse`, and all `Match` variants have `Task<TResult>` overloads that accept `CancellationToken`.

```csharp
var result = await either.Map(
    (v, ct) => Task.FromResult(v.ToString()),
    cancellationToken);
```
