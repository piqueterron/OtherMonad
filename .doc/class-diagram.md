# OtherMonad — Class Diagram

```mermaid
classDiagram
    %% ─── Delegates ───────────────────────────────────────────────────────────
    class Deferred~TResult~ {
        <<delegate>>
        +Invoke() TResult
    }

    class DeferredTask~TResult~ {
        <<delegate>>
        +Invoke() Task~TResult~
    }

    %% ─── Maybe ───────────────────────────────────────────────────────────────
    class Maybe~TSource~ {
        <<struct>>
        +TSource Value
        +bool HasValue
        +None$ Maybe~TSource~
        +operator ==(Maybe, Maybe) bool
        +operator !=(Maybe, Maybe) bool
        +Equals(Maybe~TSource~) bool
        +Equals(object) bool
        +GetHashCode() int
        +implicit operator Maybe~TSource~(TSource)
    }

    class Maybe {
        <<static partial class>>
        +Bind~TSource,TResult~(Maybe~TSource~, Func~TSource,TResult~) Maybe~TResult~
        +Bind~TSource,TResult~(Maybe~TSource~, Func~TSource,CancellationToken,Task~TResult~~, CancellationToken) Task~Maybe~TResult~~
        +BindDefer~TSource,TResult~(Maybe~TSource~, Func) Deferred~Maybe~TResult~~
        +BindDefer~TSource,TResult~(DeferredTask~Maybe~TSource~~, Func) DeferredTask~Maybe~TResult~~
        +Map~TSource,TResult~(IEnumerable~Maybe~TSource~~, Func) IEnumerable~Maybe~TResult~~
        +Map~TSource,TResult~(IEnumerable~Maybe~TSource~~, Func, CancellationToken) IAsyncEnumerable~Maybe~TResult~~
        +Map~TSource,TResult~(IAsyncEnumerable~Maybe~TSource~~, Func, CancellationToken) IAsyncEnumerable~Maybe~TResult~~
        +MapDefer~TSource,TResult~(Deferred, Func) Deferred
        +MapDefer~TSource,TResult~(DeferredTask, Func, CancellationToken) DeferredTask
        +Match~TSource,TResult~(Maybe~TSource~, Func, Func) TResult
        +Match~TSource,TResult~(Maybe~TSource~, Func, Func, CancellationToken) Task~TResult~
        +Match~TSource,TResult~(Deferred~Maybe~TSource~~, Func, Func) TResult
        +Match~TSource,TResult~(DeferredTask~Maybe~TSource~~, Func, Func) Task~TResult~
        +OrElse~TSource~(Maybe~TSource~, TSource) Maybe~TSource~
        +OrElse~TSource~(Task~Maybe~TSource~~, TSource) Task~Maybe~TSource~~
        +OrElseDefer~TSource~(Maybe~TSource~, TSource) Deferred~Maybe~TSource~~
        +OrElseDefer~TSource~(Deferred~Maybe~TSource~~, TSource) Deferred~Maybe~TSource~~
        +OrElseDefer~TSource~(DeferredTask~Maybe~TSource~~, TSource) DeferredTask~Maybe~TSource~~
        +Combine~TSource,TCombine,TResult~(Maybe~TSource~, Maybe~TCombine~, Func) Maybe~TResult~
        +TryCombine~TSource,TCombine,TResult~(Maybe~TSource~, Maybe~TCombine~, Func, Func) Maybe~TResult~
        +CombineDefer~TSource,TCombine,TResult~(Deferred, Maybe~TCombine~, Func) Deferred
        +CombineDefer~TSource,TCombine,TResult~(DeferredTask, Maybe~TCombine~, Func, CancellationToken) DeferredTask
        +TryCombineDefer~TSource,TCombine,TResult~(Deferred, Maybe~TCombine~, Func, Func) Deferred
        +TryCombineDefer~TSource,TCombine,TResult~(DeferredTask, Maybe~TCombine~, Func, Func, CancellationToken) DeferredTask
        +Cast~TResult~(object) Maybe~TResult~
        +TryCast~TSource~(object) Maybe~TSource~
        +CastDefer~TResult~(object) Deferred~Maybe~TResult~~
        +TryCastDefer~TSource~(object) Deferred~Maybe~TSource~~
        +Wrap~TSource~(TSource) Maybe~TSource~
        +Unwrap~TSource~(Maybe~TSource~) TSource
        +Unwrap~TSource~(Maybe~TSource~, TSource) TSource
    }

    Maybe~TSource~ ..> Maybe : "extended by"
    Maybe ..> Deferred~TResult~ : "uses"
    Maybe ..> DeferredTask~TResult~ : "uses"

    %% ─── Either ──────────────────────────────────────────────────────────────
    class IEither~TLeft,TRight~ {
        <<interface>>
        +bool IsLeft
        +TLeft Left
        +TRight Right
    }

    class Either~TLeft,TRight~ {
        <<struct>>
        +TLeft Left
        +TRight Right
        +bool IsLeft
        +implicit operator Either(TLeft)
        +implicit operator Either(TRight)
    }

    class EitherCreate~TLeft,TRight~ {
        <<struct>>
        +Left(TLeft) Either~TLeft,TRight~$
        +Right(TRight) Either~TLeft,TRight~$
    }

    class Either {
        <<static partial class>>
        +Match~TLeft,TRight,TResult~(IEither, Func, Func) TResult
        +Match~TLeft,TRight,TResult~(IEither, Func, Func, CancellationToken) Task~TResult~
        +TryMatch~TLeft,TRight,TResult~(IEither, Func, Func, TResult) TResult
        +TryMatch~TLeft,TRight,TResult~(IEither, Func, Func, TResult, CancellationToken) Task~TResult~
        +Combine~TSourceLeft,TSourceRight,TOtherLeft,TOtherRight,TLeft,TRight~(IEither, IEither, Func, Func) Either~TLeft,TRight~
    }

    Either~TLeft,TRight~ ..|> IEither~TLeft,TRight~ : implements
    Either~TLeft,TRight~ +-- EitherCreate~TLeft,TRight~ : nested
    Either~TLeft,TRight~ ..> Either : "extended by"
```
