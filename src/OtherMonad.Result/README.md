# OtherMonad.Result

`OtherMonad.Result` provides the `Result<T>` type — a **semantic specialisation of `Either<Exception, T>`** that models operations that either succeed with a value or fail with an exception.

> **Ok = success &nbsp;·&nbsp; Err = failure/error**

`Result<T>` wraps `Either<Exception, T>` internally and re-exposes its full behaviour under idiomatic C# vocabulary. Because it implements `IEither<Exception, T>`, any generic code that operates on `IEither` works transparently with `Result<T>`.

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
  - [GetValueOrDefault](#getvalueordefault)
  - [Combine](#combine)
  - [Try](#try)
- [Implicit Conversions](#implicit-conversions)
- [Equality](#equality)
- [Async Support](#async-support)

## Getting Started

```bash
dotnet add package OtherMonad.Result
```

```csharp
using OtherMonad;
```

## Core Type

```csharp
public readonly struct Result<T> : IResult<T>, IEquatable<Result<T>>
```

| Member | Description |
|--------|-------------|
| `Value` | The **success** value of type `T`. Throws `InvalidOperationException` if accessed in the Err state. |
| `Error` | The **failure** `Exception`. Throws `InvalidOperationException` if accessed in the Ok state. |
| `IsOk` | `true` when the instance holds a success value. |
| `IsErr` | `true` when the instance holds an exception. |

## Creating Values

```csharp
// Ok = success
var ok = Result<int>.Create.Ok(42);

// Err = failure
var err = Result<int>.Create.Err(new Exception("not found"));
```

> Both factory methods throw `ArgumentNullException` if `null` is supplied.

## Extension Methods

### Match

Evaluates the Result and returns a value by applying the corresponding function.

```csharp
// Synchronous — onErr handles failure, onOk handles success
string msg = result.Match(
    onErr: ex  => $"Error: {ex.Message}",
    onOk:  v   => $"Value: {v}");

// Asynchronous
string msg = await result.Match(
    onErr: async (ex, ct) => await GetErrorMessage(ex, ct),
    onOk:  async (v,  ct) => await GetSuccessMessage(v, ct),
    cancellation: token);
```

### TryMatch

Same as `Match` but silently returns `@default` if either function is `null` or throws.

```csharp
string safe = result.TryMatch(
    onErr:    ex => $"Error: {ex.Message}",
    onOk:     v  => $"Value: {v}",
    @default: "fallback");
```

### Bind

If in the Ok state, applies a function that returns a new `Result`. Propagates Err unchanged.

```csharp
Result<string> result = result.Bind(n => Result<string>.Create.Ok(n.ToString()));
```

### Map

If in the Ok state, transforms the value. Propagates Err unchanged.

```csharp
Result<string> result = result.Map(n => n.ToString());
```

### OrElse

If in the Err state, returns the provided fallback Result. Returns self when Ok.

```csharp
Result<int> final = result.OrElse(Result<int>.Create.Ok(0));

// Async overload with factory
Result<int> final = await result.OrElse(
    ct => Task.FromResult(Result<int>.Create.Ok(0)), token);
```

### GetValueOrDefault

Returns the success value if Ok; otherwise returns the specified default.

```csharp
int value = result.GetValueOrDefault(0);
```

### Combine

Merges two `Result` instances:
- **Both Ok**: applies `selectorOk`.
- **Both Err**: wraps both exceptions in an `AggregateException`.
- **Mixed**: propagates the available exception.

```csharp
Result<int> sum = first.Combine(second, (a, b) => a + b);
```

### Try

Wraps a potentially-throwing delegate, capturing any exception as an Err.

```csharp
// Synchronous
Result<int> result = Result.Try(() => int.Parse(input));

// Asynchronous
Result<string> result = await Result.Try(
    ct => httpClient.GetStringAsync(url, ct), token);
```

## Implicit Conversions

`Result<T>` and `Either<Exception, T>` convert to each other implicitly:

```csharp
Either<Exception, int> either = result;   // Result<T> → Either<Exception, T>
Result<int> result2 = either;             // Either<Exception, T> → Result<T>
```

## Equality

`Result<T>` implements `IEquatable<Result<T>>` with `==` / `!=` operators and a correct `GetHashCode`.

```csharp
var a = Result<int>.Create.Ok(42);
var b = Result<int>.Create.Ok(42);

bool equal = a == b; // true
```

## Async Support

`Bind`, `Map`, `OrElse`, and all `Match` variants have `Task<TResult>` overloads that accept `CancellationToken`.

```csharp
var result = await result.Map(
    (v, ct) => Task.FromResult(v.ToString()),
    cancellationToken);
```
