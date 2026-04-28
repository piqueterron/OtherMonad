namespace OtherMonad;

/// <summary>
/// The IEither type represents a value with two possible alternatives. By convention — matching
/// Haskell, fp-ts, and LanguageExt — <strong>Right represents the success case</strong> and
/// <strong>Left represents the failure/error case</strong>.
/// </summary>
/// <typeparam name="TLeft">Type that represents the failure/error case.</typeparam>
/// <typeparam name="TRight">Type that represents the success case.</typeparam>
public interface IEither<out TLeft, out TRight>
{
    /// <summary>
    /// <see langword="true"/> when the instance holds a Left (failure/error) value.
    /// </summary>
    bool IsLeft { get; }

    /// <summary>
    /// <see langword="true"/> when the instance holds a Right (success) value.
    /// </summary>
    bool IsRight { get; }

    /// <summary>
    /// The failure/error value of type <typeparamref name="TLeft"/>.
    /// </summary>
    TLeft Left { get; }

    /// <summary>
    /// The success value of type <typeparamref name="TRight"/>.
    /// </summary>
    TRight Right { get; }
}