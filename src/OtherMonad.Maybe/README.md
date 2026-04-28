# OtherMonad.Maybe

`OtherMonad.Maybe` provides the `Maybe<T>` type — a safe container for an optional value. An instance either _has_ a value or it is `Maybe<T>.None`, eliminating `null` references and guarding against `NullReferenceException`.

## Table of Contents

- [Getting Started](#getting-started)
- [Core Type](#core-type)
- [Creating Values](#creating-values)
- [Extension Methods](#extension-methods)
  - [Bind](#bind)
  - [Map](#map)
  - [Match](#match)
  - [OrElse](#orelse)
  - [Combine / TryCombine](#combine--trycombine)
  - [Cast / TryCast](#cast--trycast)
  - [Wrap / Unwrap](#wrap--unwrap)
- [Deferred Execution](#deferred-execution)
- [Async Support](#async-support)

## Getting Started

```bash
dotnet add package OtherMonad.Maybe
```

```csharp
using OtherMonad;
```

## Core Type

```csharp
public readonly struct Maybe<TSource> : IEquatable<Maybe<TSource>>
```

| Member | Description |
|--------|-------------|
| `Value` | The encapsulated value. Undefined when `HasValue` is `false`. |
| `HasValue` | `true` when a non-null value is present. |
| `None` | Static singleton representing the empty/absent state. |

## Creating Values

```csharp
// Implicit conversion
Maybe<string> some = "hello";            // HasValue = true
Maybe<string> none = Maybe<string>.None; // HasValue = false
Maybe<string> fromNull = (string)null;   // HasValue = false (same as None)

// Using Wrap
Maybe<int> wrapped = 42.Wrap();
```

## Extension Methods

### Bind

Transforms the inner value when present; returns `None` otherwise.

```csharp
Maybe<int>    length = "hello".Wrap().Bind(s => s.Length); // 5
Maybe<string> upper  = Maybe<string>.None.Bind(s => s.ToUpper()); // None
```

### Map

Projects each element of an `IEnumerable<Maybe<T>>` sequence.

```csharp
IEnumerable<Maybe<int>> lengths = names.Map(s => s.Length);
```

### Match

Branches on presence/absence and returns a result.

```csharp
string result = maybe.Match(
    some: value => $"Found: {value}",
    none: ()    => "Nothing");
```

### OrElse

Returns the Maybe as-is if it has a value, or wraps the fallback value.

```csharp
Maybe<string> result = empty.OrElse("default");
```

### Combine / TryCombine

Merges two `Maybe` instances; returns `None` when either is empty.

```csharp
Maybe<int> sum = a.Combine(b, (x, y) => x + y);

// TryCombine — returns defaultValueFactory() on any exception
Maybe<int> safe = a.TryCombine(b, (x, y) => x + y, () => -1);
```

### Cast / TryCast

Casts an `object` to `Maybe<T>`.

```csharp
Maybe<int>    casted = obj.Cast<int>();      // throws InvalidCastException on failure
Maybe<string> safe   = obj.TryCast<string>(); // returns None on failure
```

### Wrap / Unwrap

Convert between `T` and `Maybe<T>`.

```csharp
Maybe<int> wrapped   = 99.Wrap();
int        unwrapped = wrapped.Unwrap();
int        safe      = wrapped.Unwrap(@default: 0); // 0 when HasValue = false
```

## Deferred Execution

Deferred variants (`BindDefer`, `MapDefer`, `OrElseDefer`, `CombineDefer`, `CastDefer`, …) return a `Deferred<Maybe<T>>` delegate that is only evaluated when invoked. This supports lazy pipelines without allocating intermediate values.

```csharp
Deferred<Maybe<int>> lazy = "hello"
    .Wrap()
    .BindDefer(s => s.Length);

Maybe<int> result = lazy(); // evaluated on demand
```

## Async Support

All core methods have `Task<T>` overloads accepting a `CancellationToken`.

```csharp
Maybe<int> result = await maybe.Bind(
    async (s, ct) => await ComputeAsync(s, ct),
    cancellationToken);
```

`DeferredTask<T>` variants combine deferred and asynchronous execution:

```csharp
DeferredTask<Maybe<int>> lazyAsync = maybe.BindDefer(
    async (s, ct) => await ComputeAsync(s, ct),
    cancellationToken);

Maybe<int> result = await lazyAsync();
```
