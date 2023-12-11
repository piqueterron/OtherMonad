namespace OtherMonad;

using System;

/// <summary>
/// <para>The Maybe monad encapsulates an optional value. An instance of Maybe either has a value of the encapsulated type or it doesn't in which case it is a <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>.None]]></see>. This type is meant to be used in cases where your method might or might not return a value.</para>
/// </summary>
/// <typeparam name="TSource"></typeparam>
public readonly struct Maybe<TSource> : IEquatable<Maybe<TSource>>, IEquatable<object>
{
    /// <summary>
    /// Current value of type <typeparamref name="TSource"/>, otherwise <see cref="Nullable{TSource}"/>
    /// </summary>
    public TSource Value { get; }

    /// <summary>
    /// Flag indicates whether it contains value or not
    /// </summary>
    public bool HasValue { get; }

    private Maybe(TSource value)
    {
        HasValue = !Equals(value, null);
        Value = value;
    }

    /// <inheritdoc/>
    public bool Equals(Maybe<TSource> other) =>
        GetHashCode() == other.GetHashCode();

    /// <inheritdoc/>
    public override bool Equals(object obj) =>
        obj is Maybe<TSource> maybe && Equals(maybe);

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 13;
            hash = hash * 7 ^ Value?.GetHashCode() ?? 0;

            return hash;
        }
    }

    /// <summary>
    /// Nullable <see cref="Maybe{TSource}"><![CDATA[Maybe<]]><typeparamref name="TSource"/><![CDATA[>]]></see>
    /// </summary>
    public static readonly Maybe<TSource> None = new();

    /// <summary>
    /// Implicit operators are used when the conversion is guaranteed to succeed without data loss.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator Maybe<TSource>(TSource value)
    {
        if (Equals(value, null))
            return new();

        return new(value);
    }

    /// <summary>
    /// Implicit operators compare two instances of <typeparamref name="TSource"/> if equals.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(Maybe<TSource> left, Maybe<TSource> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Implicit operators compare two instances of <typeparamref name="TSource"/> if distinct.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(Maybe<TSource> left, Maybe<TSource> right)
    {
        return !(left == right);
    }
}