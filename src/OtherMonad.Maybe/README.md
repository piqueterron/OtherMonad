# OtherMonad.Maybe

`Maybe<T>` is an optional container.

- `HasValue = true` means it contains a value.
- `Maybe<T>.None` means the value is absent.

Use it to avoid `null` checks and model optional data explicitly.

## Installation

```bash
dotnet add package OtherMonad.Maybe
```

```csharp
using OtherMonad;
```

## Core type

```csharp
public readonly struct Maybe<T> : IEquatable<Maybe<T>>
```

Key members:
- `Value`: contained value (only valid when `HasValue == true`)
- `HasValue`: indicates whether a value is present
- `None`: empty state
- implicit conversion `T -> Maybe<T>`

## Complete API

### Wrap / Unwrap

- `Wrap<T>(this T source)`
- `Unwrap<T>(this Maybe<T> source)`
- `Unwrap<T>(this Maybe<T> source, T default)`

```csharp
using OtherMonad;

Maybe<string> userName = "alice".Wrap();
Maybe<string> emptyName = Maybe<string>.None;

string required = userName.Unwrap();
string withFallback = emptyName.Unwrap("guest");
```

### Bind (sync/async)

- `Bind<TSource, TResult>(this Maybe<TSource>, Func<TSource, TResult>)`
- `Bind<TSource, TResult>(this Maybe<TSource>, Func<TSource, CancellationToken, Task<TResult>>, CancellationToken)`

```csharp
using OtherMonad;

Maybe<string> rawEmail = "  user@example.com  ".Wrap();

Maybe<string> normalized = rawEmail
    .Bind(email => email.Trim())
    .Bind(email => email.ToLowerInvariant());

Maybe<bool> isCompanyEmail = await normalized.Bind(async (email, ct) =>
{
    await Task.Delay(5, ct);
    return email.EndsWith("@example.com", StringComparison.OrdinalIgnoreCase);
});
```

### Map (over sequences)

- `Map<TSource, TResult>(this IEnumerable<Maybe<TSource>>, Func<TSource, TResult>)`
- `Map<TSource, TResult>(this IEnumerable<Maybe<TSource>>, Func<TSource, CancellationToken, Task<TResult>>, CancellationToken)`
- `Map<TSource, TResult>(this IAsyncEnumerable<Maybe<TSource>>, Func<TSource, CancellationToken, Task<TResult>>, CancellationToken)`

```csharp
using OtherMonad;

var maybeAges = new[]
{
    10.Wrap(),
    Maybe<int>.None,
    30.Wrap()
};

IEnumerable<Maybe<string>> labels = maybeAges.Map(age => $"Age={age}");

foreach (var item in labels)
{
    Console.WriteLine(item.Match(
        some: value => value,
        none: () => "Age is missing"));
}
```

### Match (sync/async + deferred)

- `Match<TSource, TResult>(this Maybe<TSource>, Func<TSource, TResult> some, Func<TResult> none)`
- `Match<TSource, TResult>(this Maybe<TSource>, Func<TSource, CancellationToken, Task<TResult>> some, Func<CancellationToken, Task<TResult>> none, CancellationToken)`
- `Match<TSource, TResult>(this Deferred<Maybe<TSource>>, Func<TSource, TResult> some, Func<TResult> none)`
- `Match<TSource, TResult>(this DeferredTask<Maybe<TSource>>, Func<TSource, TResult> some, Func<TResult> none)`

```csharp
using OtherMonad;

Maybe<int> maybeScore = 87.Wrap();

string gradeText = maybeScore.Match(
    some: score => score >= 90 ? "A" : score >= 80 ? "B" : "C",
    none: () => "No score available");

string asyncText = await maybeScore.Match(
    some: async (score, ct) =>
    {
        await Task.Delay(5, ct);
        return $"Score={score}";
    },
    none: async ct =>
    {
        await Task.Delay(5, ct);
        return "No score";
    });
```

### OrElse (sync/async + deferred)

- `OrElse<T>(this Maybe<T> source, T default)`
- `OrElse<T>(this Task<Maybe<T>> source, T default)`
- `OrElseDefer<T>(this Maybe<T> source, T default)`
- `OrElseDefer<T>(this Deferred<Maybe<T>> source, T default)`
- `OrElseDefer<T>(this DeferredTask<Maybe<T>> source, T default)`

```csharp
using OtherMonad;

Maybe<string> preferredLocale = Maybe<string>.None;
Maybe<string> locale = preferredLocale.OrElse("en-US");

Task<Maybe<string>> localeTask = Task.FromResult(Maybe<string>.None);
Maybe<string> localeFromTask = await localeTask.OrElse("en-GB");
```

### Combine / TryCombine (sync + deferred)

- `Combine<TSource, TCombine, TResult>(this Maybe<TSource>, Maybe<TCombine>, Func<TSource, TCombine, TResult>)`
- `TryCombine<TSource, TCombine, TResult>(this Maybe<TSource>, Maybe<TCombine>, Func<TSource, TCombine, TResult>, Func<TResult> defaultValueFactory)`
- `CombineDefer(...)`, `TryCombineDefer(...)` on `Deferred` / `DeferredTask`

```csharp
using OtherMonad;

Maybe<string> firstName = "Ada".Wrap();
Maybe<string> lastName = "Lovelace".Wrap();
Maybe<string> missingName = Maybe<string>.None;

Maybe<string> fullName = firstName.Combine(lastName, (f, l) => $"{f} {l}");
Maybe<string> fallbackName = firstName.TryCombine(missingName, (f, l) => $"{f} {l}", () => "Unknown User");
```

## Advanced scenarios

### Async/await pipeline pattern (`Bind` + `Match`)

```csharp
using OtherMonad;

Maybe<string> maybeUserId = "42".Wrap();

Maybe<int> maybeParsedId = await maybeUserId.Bind(async (text, ct) =>
{
    await Task.Delay(5, ct);
    return int.TryParse(text, out var id) ? id : default;
});

string message = maybeParsedId.Match(
    some: id => $"Valid user id: {id}",
    none: () => "Invalid or missing user id");
```

### Compose with `Either`

```csharp
using OtherMonad;

Either<string, Maybe<int>> maybeAgeFromApi = Either<string, Maybe<int>>.Create.Right(29.Wrap());

string text = maybeAgeFromApi.Match(
    left: err => $"Request failed: {err}",
    right: maybeAge => maybeAge.Match(
        some: age => $"Age={age}",
        none: () => "Age not provided"));
```

### Convert `Maybe<T>` to `Either<TLeft, TRight>`

```csharp
using OtherMonad;

Maybe<int> maybeTimeout = Maybe<int>.None;
Either<string, int> timeout = maybeTimeout.Match(
    some: value => value > 0
        ? Either<string, int>.Create.Right(value)
        : Either<string, int>.Create.Left("Timeout must be > 0"),
    none: () => Either<string, int>.Create.Left("Timeout is missing"));
```
