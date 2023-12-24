namespace OtherMonad;

using System;

/// <summary>
/// <para>The Maybe monad encapsulates an optional value.</para>
/// </summary>
/// <typeparam name="TSource"></typeparam>
public interface IMaybe<TSource> : IEquatable<Maybe<TSource>>
{
    bool HasValue { get; }

    TSource Value { get; }
}
