# OtherMonad.Maybe

`Maybe<T>` representa un valor opcional: o hay valor (`HasValue = true`) o no (`Maybe<T>.None`).

## Instalación

```bash
dotnet add package OtherMonad.Maybe
```

```csharp
using OtherMonad;
```

## Tipo base

```csharp
public readonly struct Maybe<T> : IEquatable<Maybe<T>>
```

- `Value`: valor encapsulado (válido solo si `HasValue == true`)
- `HasValue`: indica presencia de valor
- `None`: estado vacío
- Conversión implícita `T -> Maybe<T>`

## API completa

### Wrap / Unwrap

- `Wrap<T>(this T source)`
- `Unwrap<T>(this Maybe<T> source)`
- `Unwrap<T>(this Maybe<T> source, T default)`

```csharp
Maybe<int> some = 42.Wrap();
int value = some.Unwrap();
int safe = Maybe<int>.None.Unwrap(0);
```

### Bind (sync/async)

- `Bind<TSource, TResult>(this Maybe<TSource>, Func<TSource, TResult>)`
- `Bind<TSource, TResult>(this Maybe<TSource>, Func<TSource, CancellationToken, Task<TResult>>, CancellationToken)`

```csharp
Maybe<int> length = "hello".Wrap().Bind(s => s.Length);
Maybe<int> asyncLength = await "hello".Wrap().Bind(async (s, ct) =>
{
    await Task.Delay(10, ct);
    return s.Length;
});
```

### Map (secuencias)

- `Map<TSource, TResult>(this IEnumerable<Maybe<TSource>>, Func<TSource, TResult>)`
- `Map<TSource, TResult>(this IEnumerable<Maybe<TSource>>, Func<TSource, CancellationToken, Task<TResult>>, CancellationToken)`
- `Map<TSource, TResult>(this IAsyncEnumerable<Maybe<TSource>>, Func<TSource, CancellationToken, Task<TResult>>, CancellationToken)`

```csharp
var source = new[] { 1.Wrap(), Maybe<int>.None, 3.Wrap() };
IEnumerable<Maybe<string>> mapped = source.Map(v => $"n:{v}");
```

### Match (sync/async + deferred)

- `Match<TSource, TResult>(this Maybe<TSource>, Func<TSource, TResult> some, Func<TResult> none)`
- `Match<TSource, TResult>(this Maybe<TSource>, Func<TSource, CancellationToken, Task<TResult>> some, Func<CancellationToken, Task<TResult>> none, CancellationToken)`
- `Match<TSource, TResult>(this Deferred<Maybe<TSource>>, Func<TSource, TResult> some, Func<TResult> none)`
- `Match<TSource, TResult>(this DeferredTask<Maybe<TSource>>, Func<TSource, TResult> some, Func<TResult> none)`

```csharp
string text = Maybe<int>.None.Match(
    some: v => $"Value: {v}",
    none: () => "No value");
```

### OrElse (sync/async + deferred)

- `OrElse<T>(this Maybe<T> source, T default)`
- `OrElse<T>(this Task<Maybe<T>> source, T default)`
- `OrElseDefer<T>(this Maybe<T> source, T default)`
- `OrElseDefer<T>(this Deferred<Maybe<T>> source, T default)`
- `OrElseDefer<T>(this DeferredTask<Maybe<T>> source, T default)`

```csharp
Maybe<string> fallback = Maybe<string>.None.OrElse("guest");
```

### Combine / TryCombine (sync + deferred)

- `Combine<TSource, TCombine, TResult>(this Maybe<TSource>, Maybe<TCombine>, Func<TSource, TCombine, TResult>)`
- `TryCombine<TSource, TCombine, TResult>(this Maybe<TSource>, Maybe<TCombine>, Func<TSource, TCombine, TResult>, Func<TResult> defaultValueFactory)`
- `CombineDefer(...)`, `TryCombineDefer(...)` (todas las variantes en `Deferred` / `DeferredTask`)

```csharp
Maybe<int> a = 2.Wrap();
Maybe<int> b = 3.Wrap();
Maybe<int> sum = a.Combine(b, (x, y) => x + y);
```

## Escenarios avanzados

### Async/await (patrón BindAsync/MapAsync)

```csharp
Maybe<int> value = 10.Wrap();
Maybe<int> result = await value.Bind(async (v, ct) =>
{
    await Task.Delay(5, ct);
    return v * 2;
});
```

### Composición con Either

```csharp
Either<string, Maybe<int>> composed = Either<string, Maybe<int>>.Create.Right(10.Wrap());
```

### Maybe -> Either

```csharp
Maybe<int> maybe = Maybe<int>.None;
Either<string, int> either = maybe.Match(
    some: v => Either<string, int>.Create.Right(v),
    none: () => Either<string, int>.Create.Left("No value"));
```

## Nota sobre Cast

Aunque versiones anteriores de documentación mencionaban `Cast/TryCast`, la API actual de `OtherMonad.Maybe` no expone esos métodos.
