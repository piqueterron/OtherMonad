namespace OtherMonad;

/// <summary>
/// The Either type is sometimes used to represent a value which is either correct or an error; by convention, <typeparamref name="TLeft"/> represent <strong>success</strong> case and <typeparamref name="TRight"/> <strong>fail</strong> case
/// </summary>
/// <typeparam name="TLeft">Type represent success case</typeparam>
/// <typeparam name="TRight">Type represent fail case</typeparam>
public readonly struct Either<TLeft, TRight> : IEither<TLeft, TRight>
{
    private readonly TLeft _left;
    private readonly TRight _right;
    private readonly bool _isLeft;

    /// <summary>
    /// <typeparam name="TLeft">Type represent success case</typeparam>
    /// </summary>
    public TLeft Left => _left;

    /// <summary>
    /// <typeparam name="TRight">Type represent fail case</typeparam>
    /// </summary>
    public TRight Right => _right;

    /// <summary>
    /// Flag represent state of success case or not
    /// </summary>
    public bool IsLeft => _isLeft;

    private Either(TLeft left)
    {
        _left = left;
        _right = default;
        _isLeft = true;
    }

    private Either(TRight right)
    {
        _left = default;
        _right = right;
        _isLeft = false;
    }

    /// <summary>
    /// Implicit operators are used when the conversion is guaranteed to succeed without data loss.
    /// </summary>
    /// <param name="left"><typeparamref name="TLeft"/></param>
    public static implicit operator Either<TLeft, TRight>(TLeft left) => new(left);

    /// <summary>
    /// Implicit operators are used when the conversion is guaranteed to succeed without data loss.
    /// </summary>
    /// <param name="right"><typeparamref name="TRight"/></param>
    public static implicit operator Either<TLeft, TRight>(TRight right) => new(right);
}