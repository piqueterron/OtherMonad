# [<img src="OtherMonad.256x256.png" width="25"/>](OtherMonad.256x256.png "OtherMonad") OtherMonad

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=piqueterron_OtherMonad&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=piqueterron_OtherMonad)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=piqueterron_OtherMonad&metric=coverage)](https://sonarcloud.io/summary/new_code?id=piqueterron_OtherMonad)

Librería ligera de mónadas para .NET con tres tipos principales:

- `Maybe<T>`: valor opcional (valor o ausencia)
- `Either<TLeft, TRight>`: éxito/fracaso tipado (`Right = éxito`, `Left = error`)
- `Result<T>`: especialización semántica de `Either<Exception, T>`

## Paquetes

| Package | NuGet |
|---|---|
| `OtherMonad.Maybe` | [![NuGet](https://img.shields.io/nuget/v/OtherMonad.Maybe)](https://www.nuget.org/packages/OtherMonad.Maybe) |
| `OtherMonad.Either` | [![NuGet](https://img.shields.io/nuget/v/OtherMonad.Either)](https://www.nuget.org/packages/OtherMonad.Either) |
| `OtherMonad.Result` | [![NuGet](https://img.shields.io/nuget/v/OtherMonad.Result)](https://www.nuget.org/packages/OtherMonad.Result) |

## Instalación

```bash
dotnet add package OtherMonad.Maybe
dotnet add package OtherMonad.Either
dotnet add package OtherMonad.Result
```

## Guía rápida de API

> Guía completa por paquete:
> - [`src/OtherMonad.Maybe/README.md`](src/OtherMonad.Maybe/README.md)
> - [`src/OtherMonad.Either/README.md`](src/OtherMonad.Either/README.md)
> - [`src/OtherMonad.Result/README.md`](src/OtherMonad.Result/README.md)

### Maybe

- `Wrap`, `Unwrap`
- `Bind` (sync/async + defer)
- `Map` (sobre secuencias, sync/async + defer)
- `Match` (sync/async + defer)
- `OrElse` (sync/async + defer)
- `Combine`, `TryCombine` (+ variantes `Defer`)

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

## Result vs Either: ¿por qué existen ambos?

Usa **`Either<TLeft, TRight>`** cuando:
- quieres modelar errores de dominio tipados (`ValidationError`, `ProblemDetails`, etc.)
- `Left` no es necesariamente una excepción

Usa **`Result<T>`** cuando:
- tu flujo de errores es por `Exception`
- quieres API más expresiva para aplicaciones C# (`Ok` / `Err`)
- quieres interoperar con APIs que ya lanzan excepciones (`Result.Try`)

`Result<T>` implementa `IEither<Exception, T>` y convierte implícitamente a/desde `Either<Exception, T>`.

## Escenarios avanzados

### 1) Async/await (patrón BindAsync / MapAsync)

No hay métodos llamados `BindAsync` o `MapAsync`; se usan las sobrecargas async de `Bind` y `Map`:

```csharp
using OtherMonad;

var either = Either<string, int>.Create.Right(10);

var mapped = await either.Map(async (v, ct) =>
{
    await Task.Delay(10, ct);
    return v * 2;
});

var chained = await either.Bind(async (v, ct) =>
{
    await Task.Delay(10, ct);
    return v > 0
        ? Either<string, string>.Create.Right($"ok:{v}")
        : Either<string, string>.Create.Left("invalid");
});
```

### 2) Composición de mónadas

Ejemplo: `Maybe<T>` dentro de `Either<TLeft, TRight>`:

```csharp
using OtherMonad;

Either<string, Maybe<int>> userAge = Either<string, Maybe<int>>.Create.Right(42.Wrap());

string message = userAge.Match(
    left: err => $"Error: {err}",
    right: maybeAge => maybeAge.Match(
        some: age => $"Edad: {age}",
        none: () => "Sin edad"));
```

### 3) Conversiones entre tipos

#### Maybe -> Either

```csharp
using OtherMonad;

Maybe<int> maybe = 5.Wrap();
Either<string, int> either = maybe.Match(
    some: v => Either<string, int>.Create.Right(v),
    none: () => Either<string, int>.Create.Left("No value"));
```

#### Either<Exception, T> <-> Result<T>

```csharp
using OtherMonad;

Either<Exception, int> either = Either<Exception, int>.Create.Right(7);
Result<int> result = either;                 // implícita
Either<Exception, int> again = result;       // implícita
```

#### Result -> Maybe

```csharp
using OtherMonad;

Result<int> result = Result<int>.Create.Ok(7);
Maybe<int> maybe = result.Match(
    onErr: _ => Maybe<int>.None,
    onOk: v => v.Wrap());
```

## Nota sobre Cast

La API actual no expone `Cast` / `TryCast` en código fuente; para conversiones usa `Match`, fábricas (`Create.Left/Right`, `Create.Ok/Err`) y conversiones implícitas de `Result`/`Either`.

## License

This project is licensed under the [MIT License](LICENSE).
