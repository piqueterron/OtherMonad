namespace OtherMonad;

/// <summary>
/// The interface IEither represent a value which is either correct or an error; by convention, <typeparamref name="TLeft"/> represent <strong>success</strong> case and <typeparamref name="TRight"/> <strong>fail</strong> case
/// </summary>
/// <typeparam name="TLeft">Type represent success case</typeparam>
/// <typeparam name="TRight">Type represent fail case</typeparam>
public interface IEither<TLeft, TRight>
{
    /// <summary>
    /// Flag represent state of success case or not
    /// </summary>
    bool IsLeft { get; }

    /// <summary>
    /// <typeparam name="TLeft">Type represent success case</typeparam>
    /// </summary>
    TLeft Left { get; }

    /// <summary>
    /// <typeparam name="TRight">Type represent fail case</typeparam>
    /// </summary>
    TRight Right { get; }
}