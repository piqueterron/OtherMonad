# [<img src="OtherMonad.256x256.png" width="25"/>](OtherMonad.256x256.png "OtherMonad") OtherMonad

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=piqueterron_OtherMonad&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=piqueterron_OtherMonad) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=piqueterron_OtherMonad&metric=coverage)](https://sonarcloud.io/summary/new_code?id=piqueterron_OtherMonad)

A lightweight .NET library providing functional monadic types — **Maybe** and **Either** — to write expressive, null-safe, and railway-oriented code.

## Table of Contents

- [Overview](#overview)
- [Packages](#packages)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Contributing](#contributing)
- [License](#license)

## Overview

OtherMonad brings two core functional programming abstractions to C#:

| Type | Purpose |
|------|---------|
| `Maybe<T>` | Represents an optional value — either _something_ or _nothing_ (`None`). Eliminates `null` checks. |
| `Either<TLeft, TRight>` | Represents a value that is one of two alternatives — by convention **Left** is success and **Right** is failure. |

Both types expose a rich set of extension methods (Bind, Map, Match, Combine, OrElse, Cast, Wrap …) and support synchronous, asynchronous, and deferred execution patterns.

## Packages

| Package | NuGet |
|---------|-------|
| `OtherMonad.Maybe` | [![NuGet](https://img.shields.io/nuget/v/OtherMonad.Maybe)](https://www.nuget.org/packages/OtherMonad.Maybe) |
| `OtherMonad.Either` | [![NuGet](https://img.shields.io/nuget/v/OtherMonad.Either)](https://www.nuget.org/packages/OtherMonad.Either) |

## Installation

```bash
dotnet add package OtherMonad.Maybe
dotnet add package OtherMonad.Either
```

## Quick Start

### Maybe

```csharp
using OtherMonad;

Maybe<string> name = "Alice";
Maybe<string> empty = Maybe<string>.None;

// Bind — transform the value if present
Maybe<int> length = name.Bind(s => s.Length);  // Maybe<int> { Value = 5, HasValue = true }

// Match — branch on presence/absence
string result = name.Match(
    left:  s  => $"Hello, {s}!",
    right: () => "No name provided");

// OrElse — provide a fallback
Maybe<string> fallback = empty.OrElse("default");
```

### Either

```csharp
using OtherMonad;

Either<int, string> success = Either<int, string>.Create.Left(42);
Either<int, string> failure = Either<int, string>.Create.Right("Something went wrong");

// Match — handle both branches
string message = success.Match(
    left:  value => $"Got {value}",
    right: error => $"Error: {error}");
```

## Contributing

Pull requests are welcome. Please open an issue first to discuss significant changes.

## License

This project is licensed under the [MIT License](LICENSE).
