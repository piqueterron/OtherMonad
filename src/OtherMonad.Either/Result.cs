namespace OtherMonad;

/// <summary>
/// The Either type is sometimes used to represent a value which is either correct or an error; by convention, <typeparamref name="TLeft"/> represent <strong>success</strong> case and <typeparamref name="TRight"/> <strong>fail</strong> case
/// </summary>
/// <typeparam name="TLeft">Type represent success case</typeparam>
/// <typeparam name="Exception">Type represent fail case</typeparam>
public readonly struct Result<TLeft> : IEither<TLeft, Exception>
{
    private readonly TLeft _left;
    private readonly Exception _right;
    private readonly bool _isLeft;

    /// <summary>
    /// <typeparam name="TLeft">Type represent success case</typeparam>
    /// </summary>
    public TLeft Left => _left;

    /// <summary>
    /// <typeparam name="Exception">Type represent fail case</typeparam>
    /// </summary>
    public Exception Right => _right;

    /// <summary>
    /// Flag represent state of success case or not
    /// </summary>
    public bool IsLeft => _isLeft;

    private Result(TLeft left)
    {
        _left = left;
        _right = default;
        _isLeft = true;
    }

    private Result(Exception right)
    {
        _left = default;
        _right = right;
        _isLeft = false;
    }

    /// <summary>
    /// Implicit operators are used when the conversion is guaranteed to succeed without data loss.
    /// </summary>
    /// <param name="left"><typeparamref name="TLeft"/></param>
    public static implicit operator Result<TLeft>(TLeft left) => new(left);

    /// <summary>
    /// Implicit operators are used when the conversion is guaranteed to succeed without data loss.
    /// </summary>
    /// <param name="right"><typeparamref name="Exception"/></param>
    public static implicit operator Result<TLeft>(Exception right) => new(right);
}