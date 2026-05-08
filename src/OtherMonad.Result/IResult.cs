namespace OtherMonad;

/// <summary>
/// Represents an operation that either succeeded with a value of type <typeparamref name="T"/>
/// or failed with an <see cref="Exception"/>.
/// Inherits from <see cref="IEither{TLeft,TRight}"/> with <c>TLeft = Exception</c> (error) and
/// <c>TRight = T</c> (success), establishing <strong>Result as a specialisation of Either</strong>.
/// </summary>
/// <typeparam name="T">The type of the success value.</typeparam>
public interface IResult<out T> : IEither<Exception, T>
{
    /// <summary>
    /// <see langword="true"/> when the operation succeeded and the instance holds a value.
    /// Equivalent to <see cref="IEither{TLeft,TRight}.IsRight"/>.
    /// </summary>
    bool IsOk { get; }

    /// <summary>
    /// <see langword="true"/> when the operation failed and the instance holds an <see cref="Exception"/>.
    /// Equivalent to <see cref="IEither{TLeft,TRight}.IsLeft"/>.
    /// </summary>
    bool IsErr { get; }

    /// <summary>
    /// The success value of type <typeparamref name="T"/>.
    /// Equivalent to <see cref="IEither{TLeft,TRight}.Right"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the instance is in the Err state.</exception>
    T Value { get; }

    /// <summary>
    /// The failure <see cref="Exception"/>.
    /// Equivalent to <see cref="IEither{TLeft,TRight}.Left"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the instance is in the Ok state.</exception>
    Exception Error { get; }
}
