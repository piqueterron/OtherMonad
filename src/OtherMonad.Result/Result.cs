namespace OtherMonad;

/// <summary>
/// Represents the result of an operation that either succeeded with a value of type
/// <typeparamref name="T"/> (Ok) or failed with an <see cref="Exception"/> (Err).
/// <para>
/// <c>Result&lt;T&gt;</c> is a semantic specialisation of <see cref="Either{TLeft,TRight}"/>
/// where <c>TLeft = Exception</c> and <c>TRight = T</c>. It wraps an
/// <see cref="Either{TLeft,TRight}"/> internally and re-exposes its behaviour under
/// the idiomatic <c>Ok</c> / <c>Err</c> vocabulary.
/// </para>
/// </summary>
/// <typeparam name="T">The type of the success value.</typeparam>
public readonly struct Result<T> : IResult<T>, IEquatable<Result<T>>
{
    private readonly Either<Exception, T> _either;

    /// <summary>
    /// <see langword="true"/> when the operation succeeded and the instance holds a value.
    /// </summary>
    public bool IsOk => _either.IsRight;

    /// <summary>
    /// <see langword="true"/> when the operation failed and the instance holds an <see cref="Exception"/>.
    /// </summary>
    public bool IsErr => _either.IsLeft;

    /// <summary>
    /// The success value of type <typeparamref name="T"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the instance is in the Err state. Check <see cref="IsOk"/> before accessing.
    /// </exception>
    public T Value => _either.IsRight
        ? _either.Right
        : throw new InvalidOperationException("Result is in an Err state. Access Error instead.");

    /// <summary>
    /// The failure <see cref="Exception"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the instance is in the Ok state. Check <see cref="IsErr"/> before accessing.
    /// </exception>
    public Exception Error => _either.IsLeft
        ? _either.Left
        : throw new InvalidOperationException("Result is in an Ok state. Access Value instead.");

    bool IEither<Exception, T>.IsLeft => _either.IsLeft;
    bool IEither<Exception, T>.IsRight => _either.IsRight;
    Exception IEither<Exception, T>.Left => _either.Left;
    T IEither<Exception, T>.Right => _either.Right;

    private Result(Either<Exception, T> either) => _either = either;

    /// <summary>
    /// Implicitly converts a <see cref="Result{T}"/> to the underlying
    /// <see cref="Either{TLeft,TRight}"/> representation.
    /// </summary>
    public static implicit operator Either<Exception, T>(Result<T> result) => result._either;

    /// <summary>
    /// Implicitly converts an <see cref="Either{TLeft,TRight}"/> (with
    /// <c>TLeft = Exception</c>) to a <see cref="Result{T}"/>.
    /// </summary>
    public static implicit operator Result<T>(Either<Exception, T> either) => new(either);

    /// <summary>
    /// Explicit factory methods for creating <see cref="Result{T}"/> instances.
    /// </summary>
    public readonly struct Create
    {
        /// <summary>
        /// Creates a <see cref="Result{T}"/> that represents a successful operation.
        /// </summary>
        /// <param name="value">The success value.</param>
        /// <returns><see cref="Result{T}"/> in the Ok state.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
        public static Result<T> Ok(T value) => Either<Exception, T>.Create.Right(value);

        /// <summary>
        /// Creates a <see cref="Result{T}"/> that represents a failed operation.
        /// </summary>
        /// <param name="exception">The exception describing the failure.</param>
        /// <returns><see cref="Result{T}"/> in the Err state.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is <see langword="null"/>.</exception>
        public static Result<T> Err(Exception exception) => Either<Exception, T>.Create.Left(exception);
    }

    /// <inheritdoc/>
    public bool Equals(Result<T> other) => _either.Equals(other._either);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Result<T> other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => _either.GetHashCode();

    /// <summary>
    /// Equality operator for two <see cref="Result{T}"/> instances.
    /// </summary>
    public static bool operator ==(Result<T> left, Result<T> right) => left.Equals(right);

    /// <summary>
    /// Inequality operator for two <see cref="Result{T}"/> instances.
    /// </summary>
    public static bool operator !=(Result<T> left, Result<T> right) => !(left == right);

    /// <summary>
    /// Returns a string that represents the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <returns>
    /// <c>"Result { Ok = {Value} }"</c> when in the Ok state;
    /// <c>"Result { Err = {Error} }"</c> when in the Err state.
    /// </returns>
    public override string ToString() =>
        IsOk ? $"Result {{ Ok = {Value} }}" : $"Result {{ Err = {Error} }}";
}
