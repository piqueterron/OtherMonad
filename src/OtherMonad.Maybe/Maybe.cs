namespace OtherMonad;

using System;

public delegate TResult Deferred<out TResult>();
public delegate Task<TResult> DeferredTask<TResult>();

/// <summary>
/// Represents an optional value. An instance either contains a value of type <typeparamref name="TSource"/>
/// or is <see cref="None"/>. Use this type to model optional data and avoid null checks.
/// </summary>
/// <typeparam name="TSource">The type of the value when present.</typeparam>
public readonly struct Maybe<TSource> : IEquatable<Maybe<TSource>>
{
    /// <summary>
    /// Gets the contained value.
    /// </summary>
    /// <remarks>
    /// Only valid when <see cref="HasValue"/> is <see langword="true"/>.
    /// Accessing this property when <see cref="HasValue"/> is <see langword="false"/> yields the default value of <typeparamref name="TSource"/>.
    /// </remarks>
    public TSource Value { get; }

    /// <summary>
    /// Gets a value indicating whether this instance contains a value.
    /// </summary>
    public bool HasValue { get; }

    private Maybe(TSource value)
    {
        HasValue = !Equals(value, null);
        Value = value;
    }

    /// <inheritdoc/>
    public bool Equals(Maybe<TSource> other)
    {
        if (HasValue != other.HasValue)
            return false;

        if (!HasValue)
            return true;

        return EqualityComparer<TSource>.Default.Equals(Value, other.Value);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj is Maybe<TSource> maybe && Equals(maybe);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HasValue
            ? HashCode.Combine(true, Value)
            : HashCode.Combine(false);
    }

    /// <summary>
    /// Returns a string that represents the current <see cref="Maybe{TSource}"/> instance.
    /// </summary>
    /// <returns>
    /// <c>"Maybe { Value = {Value}, HasValue = true }"</c> when a value is present;
    /// <c>"Maybe { HasValue = false }"</c> when the instance is <see cref="None"/>.
    /// </returns>
    public override string ToString()
    {
        return HasValue
            ? $"Maybe {{ Value = {Value}, HasValue = true }}"
            : "Maybe { HasValue = false }";
    }

    /// <summary>
    /// Represents the absence of a value (empty <see cref="Maybe{TSource}"/>).
    /// </summary>
    public static readonly Maybe<TSource> None = default;

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="TSource"/> to <see cref="Maybe{TSource}"/>.
    /// </summary>
    /// <param name="value">The value to wrap. If <see langword="null"/>, returns <see cref="None"/>.</param>
    public static implicit operator Maybe<TSource>(TSource value)
    {
        if (Equals(value, null))
            return default;

        return new(value);
    }

    /// <summary>
    /// Determines whether two <see cref="Maybe{TSource}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><see langword="true"/> if the instances are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Maybe<TSource> left, Maybe<TSource> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two <see cref="Maybe{TSource}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first instance to compare.</param>
    /// <param name="right">The second instance to compare.</param>
    /// <returns><see langword="true"/> if the instances are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Maybe<TSource> left, Maybe<TSource> right)
    {
        return !(left == right);
    }
}
