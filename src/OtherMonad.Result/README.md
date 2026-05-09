# OtherMonad.Result

`Result<T>` modela operaciones que pueden:

- terminar bien (`Ok`) con un valor `T`
- fallar (`Err`) con una `Exception`

Internamente es una especialización de `Either<Exception, T>`.

## ¿Por qué `Result` además de `Either`?

`Either<TLeft, TRight>` es genérico para cualquier tipo de error en `Left`.

`Result<T>` existe para el caso más común en C#:
- modelar error con `Exception`
- tener vocabulario claro (`Ok`/`Err`)
- simplificar integración con código imperativo que lanza excepciones (`Result.Try`)

### Cuándo usar cada uno

Usa `Either<TLeft, TRight>` cuando el error es de dominio (no excepción).

Usa `Result<T>` cuando el error es una `Exception` o quieres ergonomía directa para ese caso.

## Instalación

```bash
dotnet add package OtherMonad.Result
```

```csharp
using OtherMonad;
```

## Tipo base

```csharp
public readonly struct Result<T> : IResult<T>, IEquatable<Result<T>>
```

- `IsOk`, `IsErr`
- `Value`, `Error`
- `Create.Ok(value)`, `Create.Err(exception)`
- Conversión implícita con `Either<Exception, T>`

## API completa

### Match / TryMatch (sync/async)

- `Match<T, TResult>(this IResult<T>, Func<Exception, TResult> onErr, Func<T, TResult> onOk)`
- `Match<T, TResult>(this IResult<T>, Func<Exception, CancellationToken, Task<TResult>> onErr, Func<T, CancellationToken, Task<TResult>> onOk, CancellationToken)`
- `TryMatch<T, TResult>(..., TResult default = default!)` (sync y async)

```csharp
string text = Result<int>.Create.Ok(5).Match(
    onErr: ex => $"Err: {ex.Message}",
    onOk: v => $"Ok: {v}");
```

### Bind (sync/async)

- `Bind<T, TResult>(this Result<T>, Func<T, Result<TResult>> selector)`
- `Bind<T, TResult>(this Result<T>, Func<T, CancellationToken, Task<Result<TResult>>> selector, CancellationToken)`

```csharp
Result<string> chained = Result<int>.Create.Ok(5)
    .Bind(v => Result<string>.Create.Ok(v.ToString()));
```

### Map (sync/async)

- `Map<T, TResult>(this Result<T>, Func<T, TResult> selector)`
- `Map<T, TResult>(this Result<T>, Func<T, CancellationToken, Task<TResult>> selector, CancellationToken)`

```csharp
Result<string> mapped = Result<int>.Create.Ok(5).Map(v => $"#{v}");
```

### OrElse (sync/async)

- `OrElse<T>(this Result<T>, Result<T> fallback)`
- `OrElse<T>(this Result<T>, Func<CancellationToken, Task<Result<T>>> fallbackFactory, CancellationToken)`

```csharp
Result<int> safe = Result<int>.Create.Err(new Exception("x"))
    .OrElse(Result<int>.Create.Ok(0));
```

### GetValueOrDefault

- `GetValueOrDefault<T>(this Result<T>, T default = default!)`

```csharp
int value = Result<int>.Create.Err(new Exception("x")).GetValueOrDefault(0);
```

### Combine

- `Combine<T, TOther, TResult>(this Result<T>, Result<TOther>, Func<T, TOther, TResult> selectorOk)`

```csharp
var combined = Result<int>.Create.Ok(2)
    .Combine(Result<int>.Create.Ok(3), (a, b) => a + b);
```

### Try (sync/async)

- `Try<T>(Func<T> factory)`
- `Try<T>(Func<CancellationToken, Task<T>> factory, CancellationToken)`

```csharp
Result<int> parsed = Result.Try(() => int.Parse("42"));
```

## Escenarios avanzados

### Async/await (patrón BindAsync/MapAsync)

```csharp
Result<int> ok = Result<int>.Create.Ok(21);
Result<int> doubled = await ok.Map(async (v, ct) =>
{
    await Task.Delay(10, ct);
    return v * 2;
});
```

### Composición con Maybe

```csharp
Result<Maybe<int>> nested = Result<Maybe<int>>.Create.Ok(10.Wrap());
```

### Conversiones entre tipos

#### Either<Exception, T> <-> Result<T>

```csharp
Either<Exception, int> either = Either<Exception, int>.Create.Right(7);
Result<int> result = either;
Either<Exception, int> again = result;
```

#### Maybe -> Result

```csharp
Maybe<int> maybe = Maybe<int>.None;
Result<int> result = maybe.Match(
    some: v => Result<int>.Create.Ok(v),
    none: () => Result<int>.Create.Err(new InvalidOperationException("No value")));
```
