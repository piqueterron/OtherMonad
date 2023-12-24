namespace OtherMonad;

/// <summary>
/// The IEither type represent a value which two posible values. By convention, left represent <strong>success</strong> case and right <strong>fail</strong> case
/// </summary>
/// <typeparam name="TLeft">Type represent success case</typeparam>
/// <typeparam name="TRight">Type represent fail case</typeparam>
public interface IEither<out TLeft, out TRight>
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