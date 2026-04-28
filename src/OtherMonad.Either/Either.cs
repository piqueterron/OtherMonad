namespace OtherMonad;

/// <summary>
/// The Either type represents a value with two possible alternatives. By convention — matching
/// Haskell, fp-ts, and LanguageExt — <strong>Right represents the success case</strong> and
/// <strong>Left represents the failure/error case</strong>.
/// </summary>
/// <typeparam name="TLeft">Type that represents the failure/error case.</typeparam>
/// <typeparam name="TRight">Type that represents the success case.</typeparam>
public readonly struct Either<TLeft, TRight> : IEither<TLeft, TRight>, IEquatable<Either<TLeft, TRight>>
{
    private readonly TLeft? _left;
    private readonly TRight? _right;
    private readonly bool _isLeft;

    /// <summary>
    /// The failure/error value of type <typeparamref name="TLeft"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the instance is in the Right (success) state. Check <see cref="IsLeft"/> before accessing.
    /// </exception>
    public TLeft Left => _isLeft
        ? _left!
        : throw new InvalidOperationException("Either is in a Right (success) state. Access Right instead.");

    /// <summary>
    /// The success value of type <typeparamref name="TRight"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the instance is in the Left (failure) state. Check <see cref="IsRight"/> before accessing.
    /// </exception>
    public TRight Right => !_isLeft
        ? _right!
        : throw new InvalidOperationException("Either is in a Left (failure) state. Access Left instead.");

    /// <summary>
    /// <see langword="true"/> when the instance holds a Left (failure/error) value.
    /// </summary>
    public bool IsLeft => _isLeft;

    /// <summary>
    /// <see langword="true"/> when the instance holds a Right (success) value.
    /// </summary>
    public bool IsRight => !_isLeft;

    private Either(TLeft? left, TRight? right, bool isLeft)
    {
        _left = left;
        _right = right;
        _isLeft = isLeft;
    }

    /// <summary>
    /// Explicit method for creating Either instances.
    /// </summary>
    public readonly struct Create
    {
        /// <summary>
        /// Creates an Either that holds a Left (failure/error) value.
        /// </summary>
        /// <param name="left">The failure/error value.</param>
        /// <returns><see cref="Either{TLeft,TRight}"/> in the Left state.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="left"/> is <see langword="null"/>.</exception>
        public static Either<TLeft, TRight> Left(TLeft left)
        {
            ArgumentNullException.ThrowIfNull(left);

            return new Either<TLeft, TRight>(left, default, true);
        }

        /// <summary>
        /// Creates an Either that holds a Right (success) value.
        /// </summary>
        /// <param name="right">The success value.</param>
        /// <returns><see cref="Either{TLeft,TRight}"/> in the Right state.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="right"/> is <see langword="null"/>.</exception>
        public static Either<TLeft, TRight> Right(TRight right)
        {
            ArgumentNullException.ThrowIfNull(right);

            return new Either<TLeft, TRight>(default, right, false);
        }
    }

    /// <inheritdoc/>
    public bool Equals(Either<TLeft, TRight> other)
    {
        if (_isLeft != other._isLeft)
            return false;

        return _isLeft
            ? EqualityComparer<TLeft>.Default.Equals(_left, other._left)
            : EqualityComparer<TRight>.Default.Equals(_right, other._right);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Either<TLeft, TRight> other && Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return _isLeft
            ? HashCode.Combine(true, _left)
            : HashCode.Combine(false, _right);
    }

    /// <summary>
    /// Equality operator for two <see cref="Either{TLeft,TRight}"/> instances.
    /// </summary>
    public static bool operator ==(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Inequality operator for two <see cref="Either{TLeft,TRight}"/> instances.
    /// </summary>
    public static bool operator !=(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
    {
        return !(left == right);
    }
}