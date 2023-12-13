namespace OtherMonad;

/// <summary>
/// Extension methods to Either Monad
/// </summary>
public static partial class Result
{
    /// <summary>
    /// <para>Method to combine two Results into one struct. If have errors return <see cref="AggregateException"/> with list of exceptions</para>
    /// </summary>
    /// <typeparam name="TLeft1">Generic type</typeparam>
    /// <typeparam name="TLeft2">Generic type</typeparam>
    /// <param name="result1"><see cref="Result{TLeft1}"/></param>
    /// <param name="result2"><see cref="Result{TLeft2}"/></param>
    /// <returns>
    /// <see cref="Result"><![CDATA[Result<]]>(<typeparamref name="TLeft1"/>, <typeparamref name="TLeft2"/>))<![CDATA[>]]></see>
    /// </returns>
    public static Result<(TLeft1 Result1, TLeft2 Result2)> Combine<TLeft1, TLeft2>(
        Result<TLeft1> result1,
        Result<TLeft2> result2)
    {
        var errors = new List<Exception>();

        HasError(result1, errors);
        HasError(result2, errors);

        if (errors.Count != 0)
            return new AggregateException(errors);

        return (result1.Left, result2.Left);
    }

    /// <summary>
    /// <para>Method to combine three Results into one struct. If have errors return <see cref="AggregateException"/> with list of exceptions</para>
    /// </summary>
    /// <typeparam name="TLeft1"><see cref="Result{TLeft1}"/></typeparam>
    /// <typeparam name="TLeft2"><see cref="Result{TLeft2}"/></typeparam>
    /// <typeparam name="TLeft3"><see cref="Result{TLeft3}"/></typeparam>
    /// <param name="result1"><see cref="Result{TLeft1}"/></param>
    /// <param name="result2"><see cref="Result{TLeft2}"/></param>
    /// <param name="result3"><see cref="Result{TLeft3}"/></param>
    /// <returns>
    /// <see cref="Result"><![CDATA[Result<]]>(<typeparamref name="TLeft1"/>, <typeparamref name="TLeft2"/>, <typeparamref name="TLeft3"/>))<![CDATA[>]]></see>
    /// </returns>
    public static Result<(TLeft1 Result1, TLeft2 Result2, TLeft3 Result3)> Combine<TLeft1, TLeft2, TLeft3>(
        Result<TLeft1> result1,
        Result<TLeft2> result2,
        Result<TLeft3> result3)
    {
        var errors = new List<Exception>();

        HasError(result1, errors);
        HasError(result2, errors);
        HasError(result3, errors);

        if (errors.Count != 0)
            return new AggregateException(errors);

        return (result1.Left, result2.Left, result3.Left);
    }

    /// <summary>
    /// <para>Method to combine four Results into one struct. If have errors return <see cref="AggregateException"/> with list of exceptions</para>
    /// </summary>
    /// <typeparam name="TLeft1"><see cref="Result{TLeft1}"/></typeparam>
    /// <typeparam name="TLeft2"><see cref="Result{TLeft2}"/></typeparam>
    /// <typeparam name="TLeft3"><see cref="Result{TLeft3}"/></typeparam>
    /// <typeparam name="TLeft4"><see cref="Result{TLeft4}"/></typeparam>
    /// <param name="result1"><see cref="Result{TLeft1}"/></param>
    /// <param name="result2"><see cref="Result{TLeft2}"/></param>
    /// <param name="result3"><see cref="Result{TLeft3}"/></param>
    /// <param name="result4"><see cref="Result{TLeft4}"/></param>
    /// <returns>
    /// <see cref="Result"><![CDATA[Result<]]>(<typeparamref name="TLeft1"/>, <typeparamref name="TLeft2"/>, <typeparamref name="TLeft3"/>, <typeparamref name="TLeft4"/>))<![CDATA[>]]></see>
    /// </returns>
    public static Result<(TLeft1 Result1, TLeft2 Result2, TLeft3 Result3, TLeft4 Result4)> Combine<TLeft1, TLeft2, TLeft3, TLeft4>(
        Result<TLeft1> result1,
        Result<TLeft2> result2,
        Result<TLeft3> result3,
        Result<TLeft4> result4)
    {
        var errors = new List<Exception>();

        HasError(result1, errors);
        HasError(result2, errors);
        HasError(result3, errors);
        HasError(result4, errors);

        if (errors.Count != 0)
            return new AggregateException(errors);

        return (result1.Left, result2.Left, result3.Left, result4.Left);
    }

    /// <summary>
    /// <para>Method to combine five Results into one struct. If have errors return <see cref="AggregateException"/> with list of exceptions</para>
    /// </summary>
    /// <typeparam name="TLeft1"><see cref="Result{TLeft1}"/></typeparam>
    /// <typeparam name="TLeft2"><see cref="Result{TLeft2}"/></typeparam>
    /// <typeparam name="TLeft3"><see cref="Result{TLeft3}"/></typeparam>
    /// <typeparam name="TLeft4"><see cref="Result{TLeft4}"/></typeparam>
    /// <typeparam name="TLeft5"><see cref="Result{TLeft5}"/></typeparam>
    /// <param name="result1"><see cref="Result{TLeft1}"/></param>
    /// <param name="result2"><see cref="Result{TLeft2}"/></param>
    /// <param name="result3"><see cref="Result{TLeft3}"/></param>
    /// <param name="result4"><see cref="Result{TLeft4}"/></param>
    /// <param name="result5"><see cref="Result{TLeft5}"/></param>
    /// <returns>
    /// <see cref="Result"><![CDATA[Result<]]>(<typeparamref name="TLeft1"/>, <typeparamref name="TLeft2"/>, <typeparamref name="TLeft3"/>, <typeparamref name="TLeft4"/>, <typeparamref name="TLeft5"/>))<![CDATA[>]]></see>
    /// </returns>
    public static Result<(TLeft1 Result1, TLeft2 Result2, TLeft3 Result3, TLeft4 Result4, TLeft5 Result5)> Combine<TLeft1, TLeft2, TLeft3, TLeft4, TLeft5>(
        Result<TLeft1> result1,
        Result<TLeft2> result2,
        Result<TLeft3> result3,
        Result<TLeft4> result4,
        Result<TLeft5> result5)
    {
        var errors = new List<Exception>();

        HasError(result1, errors);
        HasError(result2, errors);
        HasError(result3, errors);
        HasError(result4, errors);
        HasError(result5, errors);

        if (errors.Count != 0)
            return new AggregateException(errors);

        return (result1.Left, result2.Left, result3.Left, result4.Left, result5.Left);
    }

    /// <summary>
    /// <para>Method to combine six Results into one struct. If have errors return <see cref="AggregateException"/> with list of exceptions</para>
    /// </summary>
    /// <typeparam name="TLeft1"><see cref="Result{TLeft1}"/></typeparam>
    /// <typeparam name="TLeft2"><see cref="Result{TLeft2}"/></typeparam>
    /// <typeparam name="TLeft3"><see cref="Result{TLeft3}"/></typeparam>
    /// <typeparam name="TLeft4"><see cref="Result{TLeft4}"/></typeparam>
    /// <typeparam name="TLeft5"><see cref="Result{TLeft5}"/></typeparam>
    /// <typeparam name="TLeft6"><see cref="Result{TLeft6}"/></typeparam>
    /// <param name="result1"><see cref="Result{TLeft1}"/></param>
    /// <param name="result2"><see cref="Result{TLeft2}"/></param>
    /// <param name="result3"><see cref="Result{TLeft3}"/></param>
    /// <param name="result4"><see cref="Result{TLeft4}"/></param>
    /// <param name="result5"><see cref="Result{TLeft5}"/></param>
    /// <param name="result6"><see cref="Result{TLeft6}"/></param>
    /// <returns>
    /// <see cref="Result"><![CDATA[Result<]]>(<typeparamref name="TLeft1"/>, <typeparamref name="TLeft2"/>, <typeparamref name="TLeft3"/>, <typeparamref name="TLeft4"/>, <typeparamref name="TLeft5"/>, <typeparamref name="TLeft6"/>))<![CDATA[>]]></see>
    /// </returns>
    public static Result<(TLeft1 Result1, TLeft2 Result2, TLeft3 Result3, TLeft4 Result4, TLeft5 Result5, TLeft6 Result6)> Combine<TLeft1, TLeft2, TLeft3, TLeft4, TLeft5, TLeft6>(
        Result<TLeft1> result1,
        Result<TLeft2> result2,
        Result<TLeft3> result3,
        Result<TLeft4> result4,
        Result<TLeft5> result5,
        Result<TLeft6> result6)
    {
        var errors = new List<Exception>();

        HasError(result1, errors);
        HasError(result2, errors);
        HasError(result3, errors);
        HasError(result4, errors);
        HasError(result5, errors);
        HasError(result6, errors);

        if (errors.Count != 0)
            return new AggregateException(errors);

        return (result1.Left, result2.Left, result3.Left, result4.Left, result5.Left, result6.Left);
    }

    private static void HasError<T>(Result<T> either, List<Exception> errors)
    {
        if (!either.IsLeft)
            errors.Add(either.Right);
    }
}