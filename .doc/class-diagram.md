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
        +None() Maybe~TSource~
        +operator ==(Maybe~TSource~, Maybe~TSource~) bool
        +operator !=(Maybe~TSource~, Maybe~TSource~) bool
        +Equals(Maybe~TSource~) bool
        +Equals(object) bool
        +GetHashCode() int
        +ToString() string
        +implicit operator Maybe~TSource~(TSource)
    }

    class Maybe {
        <<static partial class>>
        +Bind~TSource,TResult~(...) Maybe~TResult~
        +BindAsync~TSource,TResult~(...) Task~Maybe~TResult~~
        +BindDefer~TSource,TResult~(...) Deferred~Maybe~TResult~~
        +BindDeferAsync~TSource,TResult~(...) DeferredTask~Maybe~TResult~~

        +Map~TSource,TResult~(...) IEnumerable~Maybe~TResult~~
        +MapAsync~TSource,TResult~(...) IAsyncEnumerable~Maybe~TResult~~

        +Match~TSource,TResult~(...) TResult
        +MatchAsync~TSource,TResult~(...) Task~TResult~

        +OrElse~TSource~(...) Maybe~TSource~
        +OrElseAsync~TSource~(...) Task~Maybe~TSource~~

        +Combine~TSource,TCombine,TResult~(...) Maybe~TResult~
        +TryCombine~TSource,TCombine,TResult~(...) Maybe~TResult~

        +Cast~TResult~(object) Maybe~TResult~
        +TryCast~TSource~(object) Maybe~TSource~

        +Wrap~TSource~(TSource) Maybe~TSource~
        +Unwrap~TSource~(Maybe~TSource~) TSource
        +UnwrapOr~TSource~(Maybe~TSource~, TSource) TSource
    }

    Maybe~TSource~ ..> Maybe : extended by
    Maybe ..> Deferred~TResult~ : uses
    Maybe ..> DeferredTask~TResult~ : uses

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
        +bool IsRight
        +Equals(Either~TLeft,TRight~) bool
        +Equals(object) bool
        +GetHashCode() int
        +ToString() string
        +operator ==(Either~TLeft,TRight~, Either~TLeft,TRight~) bool
        +operator !=(Either~TLeft,TRight~, Either~TLeft,TRight~) bool
    }

    class EitherCreate~TLeft,TRight~ {
        <<struct>>
        +Left(TLeft) Either~TLeft,TRight~
        +Right(TRight) Either~TLeft,TRight~
    }

    class Either {
        <<static partial class>>
        +Match~TLeft,TRight,TResult~(...) TResult
        +MatchAsync~TLeft,TRight,TResult~(...) Task~TResult~
        +TryMatch~TLeft,TRight,TResult~(...) TResult
        +TryMatchAsync~TLeft,TRight,TResult~(...) Task~TResult~
        +Combine~TLeft,TRight,TResultLeft,TResultRight~(...) Either~TResultLeft,TResultRight~
    }

    Either~TLeft,TRight~ ..|> IEither~TLeft,TRight~ : implements
    Either~TLeft,TRight~ *-- EitherCreate~TLeft,TRight~ : nested
    Either~TLeft,TRight~ ..> Either : extended by