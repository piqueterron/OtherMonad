namespace OtherMonad;

/// <summary>
/// Extension methods for <see cref="Result{T}"/>.
/// </summary>
public static partial class Result
{
    /// <summary>
    /// <para>Combines two <see cref="Result{T}"/> instances:
    /// applies <paramref name="selectorOk"/> when both are in the Ok (success) state,
    /// or returns an Err (failure) otherwise.</para>
    /// <para>When both are Err, the exceptions are aggregated into an <see cref="AggregateException"/>.
    /// When states are mixed, the available exception is propagated.</para>
    /// <para>Delegates to <see cref="Either.Combine{TSourceLeft,TSourceRight,TOtherLeft,TOtherRight,TLeft,TRight}"/>
    /// with automatic exception aggregation.</para>
    /// </summary>
    /// <typeparam name="T">The success type of the source.</typeparam>
    /// <typeparam name="TOther">The success type of the other Result.</typeparam>
    /// <typeparam name="TResult">The success type of the combined result.</typeparam>
    /// <param name="source">The first Result to combine.</param>
    /// <param name="other">The second Result to combine.</param>
    /// <param name="selectorOk">Combines two success values into one success result.</param>
    /// <returns><see cref="Result{TResult}"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="selectorOk"/> is <see langword="null"/>.</exception>
    public static Result<TResult> Combine<T, TOther, TResult>(
        this Result<T> source,
        Result<TOther> other,
        Func<T, TOther, TResult> selectorOk)
    {
        ArgumentNullException.ThrowIfNull(selectorOk);

        Either<Exception, TResult> combined = ((Either<Exception, T>)source).Combine(
            (Either<Exception, TOther>)other,
            (e1, e2) => e1 is not null && e2 is not null
                ? new AggregateException(e1, e2)
                : (Exception)(e1 ?? e2)!,
            selectorOk);

        return combined;
    }
}
