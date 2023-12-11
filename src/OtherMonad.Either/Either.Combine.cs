namespace OtherMonad;

/// <summary>
/// Extension methods to Either Monad
/// </summary>
public static partial class Either
{
    /// <summary>
    /// <para>Method to combine two Either into one object</para>
    /// </summary>
    /// <typeparam name="TLeft1">Generic type</typeparam>
    /// <typeparam name="TRight1">Generic type</typeparam>
    /// <typeparam name="TLeft2">Generic type</typeparam>
    /// <typeparam name="TRight2">Generic type</typeparam>
    /// <param name="either1"><see cref="Either{TLeft1, TRight1}"/></param>
    /// <param name="either2"><see cref="Either{TLeft2, TRight2}"/></param>
    /// <returns>
    /// <see cref="Either"><![CDATA[Either<]]>(<typeparamref name="TLeft1"/>, <typeparamref name="TLeft2"/>), (<typeparamref name="TRight1"/>, <typeparamref name="TRight2"/>)<![CDATA[>]]></see>
    /// </returns>
    public static Either<(TLeft1 Left1, TLeft2 Left2), (TRight1 Right1, TRight2 Right2)> Combine<TLeft1, TRight1, TLeft2, TRight2>(
        Either<TLeft1, TRight1> either1,
        Either<TLeft2, TRight2> either2)
    {
        var error = false;

        HasError(either1, ref error);
        HasError(either2, ref error);

        if (error)
        {
            return InternalBuilders.CreateRight<TLeft1, TRight1, TLeft2, TRight2>(either1.Right, either2.Right);
        }

        return InternalBuilders.CreateLeft<TLeft1, TRight1, TLeft2, TRight2>(either1.Left, either2.Left);
    }

    /// <summary>
    /// <para>Method to combine three Either into one object</para>
    /// </summary>
    /// <typeparam name="TLeft1">Generic type</typeparam>
    /// <typeparam name="TRight1">Generic type</typeparam>
    /// <typeparam name="TLeft2">Generic type</typeparam>
    /// <typeparam name="TRight2">Generic type</typeparam>
    /// <typeparam name="TLeft3">Generic type</typeparam>
    /// <typeparam name="TRight3">Generic type</typeparam>
    /// <param name="either1"><see cref="Either{TLeft1, TRight1}"/></param>
    /// <param name="either2"><see cref="Either{TLeft2, TRight2}"/></param>
    /// <param name="either3"><see cref="Either{TLeft3, TRight3}"/></param>
    /// <returns>
    /// <see cref="Either"><![CDATA[Either<]]>(<typeparamref name="TLeft1"/>, <typeparamref name="TLeft2"/>, <typeparamref name="TLeft3"/>), (<typeparamref name="TRight1"/>, <typeparamref name="TRight2"/>, <typeparamref name="TRight3"/>)<![CDATA[>]]></see>
    /// </returns>
    public static Either<(TLeft1 Left1, TLeft2 Left2, TLeft3 Left3), (TRight1 Right1, TRight2 Right2, TRight3 Right3)> Combine<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3>(
        Either<TLeft1, TRight1> either1,
        Either<TLeft2, TRight2> either2,
        Either<TLeft3, TRight3> either3)
    {
        var error = false;

        HasError(either1, ref error);
        HasError(either2, ref error);
        HasError(either3, ref error);

        if (error)
        {
            return InternalBuilders.CreateRight<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3>(either1.Right, either2.Right, either3.Right);
        }

        return InternalBuilders.CreateLeft<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3>(either1.Left, either2.Left, either3.Left);
    }

    /// <summary>
    /// <para>Method to combine four Either into one object</para>
    /// </summary>
    /// <typeparam name="TLeft1">Generic type</typeparam>
    /// <typeparam name="TRight1">Generic type</typeparam>
    /// <typeparam name="TLeft2">Generic type</typeparam>
    /// <typeparam name="TRight2">Generic type</typeparam>
    /// <typeparam name="TLeft3">Generic type</typeparam>
    /// <typeparam name="TRight3">Generic type</typeparam>
    /// <typeparam name="TLeft4">Generic type</typeparam>
    /// <typeparam name="TRight4">Generic type</typeparam>
    /// <param name="either1"><see cref="Either{TLeft1, TRight1}"/></param>
    /// <param name="either2"><see cref="Either{TLeft2, TRight2}"/></param>
    /// <param name="either3"><see cref="Either{TLeft3, TRight3}"/></param>
    /// <param name="either4"><see cref="Either{TLeft4, TRight4}"/></param>
    /// <returns>
    /// <see cref="Either"><![CDATA[Either<]]>(<typeparamref name="TLeft1"/>, <typeparamref name="TLeft2"/>, <typeparamref name="TLeft3"/>, <typeparamref name="TLeft4"/>), (<typeparamref name="TRight1"/>, <typeparamref name="TRight2"/>, <typeparamref name="TRight3"/>, <typeparamref name="TRight4"/>)<![CDATA[>]]></see>
    /// </returns>
    public static Either<(TLeft1 Left1, TLeft2 Left2, TLeft3 Left3, TLeft4 Left4), (TRight1 Right1, TRight2 Right2, TRight3 Right3, TRight4 Right4)> Combine<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4>(
        Either<TLeft1, TRight1> either1,
        Either<TLeft2, TRight2> either2,
        Either<TLeft3, TRight3> either3,
        Either<TLeft4, TRight4> either4)
    {
        var error = false;

        HasError(either1, ref error);
        HasError(either2, ref error);
        HasError(either3, ref error);
        HasError(either4, ref error);

        if (error)
        {
            return InternalBuilders.CreateRight<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4>(either1.Right, either2.Right, either3.Right, either4.Right);
        }

        return InternalBuilders.CreateLeft<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4>(either1.Left, either2.Left, either3.Left, either4.Left);
    }

    /// <summary>
    /// <para>Method to combine five Either into one object</para>
    /// </summary>
    /// <typeparam name="TLeft1">Generic type</typeparam>
    /// <typeparam name="TRight1">Generic type</typeparam>
    /// <typeparam name="TLeft2">Generic type</typeparam>
    /// <typeparam name="TRight2">Generic type</typeparam>
    /// <typeparam name="TLeft3">Generic type</typeparam>
    /// <typeparam name="TRight3">Generic type</typeparam>
    /// <typeparam name="TLeft4">Generic type</typeparam>
    /// <typeparam name="TRight4">Generic type</typeparam>
    /// <typeparam name="TLeft5">Generic type</typeparam>
    /// <typeparam name="TRight5">Generic type</typeparam>
    /// <param name="either1"><see cref="Either{TLeft1, TRight1}"/></param>
    /// <param name="either2"><see cref="Either{TLeft2, TRight2}"/></param>
    /// <param name="either3"><see cref="Either{TLeft3, TRight3}"/></param>
    /// <param name="either4"><see cref="Either{TLeft4, TRight4}"/></param>
    /// <param name="either5"><see cref="Either{TLeft5, TRight5}"/></param>
    /// <returns>
    /// <see cref="Either"><![CDATA[Either<]]>(<typeparamref name="TLeft1"/>, <typeparamref name="TLeft2"/>, <typeparamref name="TLeft3"/>, <typeparamref name="TLeft4"/>, <typeparamref name="TLeft5"/>), (<typeparamref name="TRight1"/>, <typeparamref name="TRight2"/>, <typeparamref name="TRight3"/>, <typeparamref name="TRight4"/>, <typeparamref name="TRight5"/>)<![CDATA[>]]></see>
    /// </returns>
    public static Either<(TLeft1 Left1, TLeft2 Left2, TLeft3 Left3, TLeft4 Left4, TLeft5 Left5), (TRight1 Right1, TRight2 Right2, TRight3 Right3, TRight4 Right4, TRight5 Right5)> Combine<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4, TLeft5, TRight5>(
        Either<TLeft1, TRight1> either1,
        Either<TLeft2, TRight2> either2,
        Either<TLeft3, TRight3> either3,
        Either<TLeft4, TRight4> either4,
        Either<TLeft5, TRight5> either5)
    {
        var error = false;

        HasError(either1, ref error);
        HasError(either2, ref error);
        HasError(either3, ref error);
        HasError(either4, ref error);
        HasError(either5, ref error);

        if (error)
        {
            return InternalBuilders.CreateRight<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4, TLeft5, TRight5>(either1.Right, either2.Right, either3.Right, either4.Right, either5.Right);
        }

        return InternalBuilders.CreateLeft<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4, TLeft5, TRight5>(either1.Left, either2.Left, either3.Left, either4.Left, either5.Left);
    }

    /// <summary>
    /// <para>Method to combine six Either into one object</para>
    /// </summary>
    /// <typeparam name="TLeft1">Generic type</typeparam>
    /// <typeparam name="TRight1">Generic type</typeparam>
    /// <typeparam name="TLeft2">Generic type</typeparam>
    /// <typeparam name="TRight2">Generic type</typeparam>
    /// <typeparam name="TLeft3">Generic type</typeparam>
    /// <typeparam name="TRight3">Generic type</typeparam>
    /// <typeparam name="TLeft4">Generic type</typeparam>
    /// <typeparam name="TRight4">Generic type</typeparam>
    /// <typeparam name="TLeft5">Generic type</typeparam>
    /// <typeparam name="TRight5">Generic type</typeparam>
    /// <typeparam name="TRight6">Generic type</typeparam>
    /// <param name="either1"><see cref="Either{TLeft1, TRight1}"/></param>
    /// <param name="either2"><see cref="Either{TLeft2, TRight2}"/></param>
    /// <param name="either3"><see cref="Either{TLeft3, TRight3}"/></param>
    /// <param name="either4"><see cref="Either{TLeft4, TRight4}"/></param>
    /// <param name="either5"><see cref="Either{TLeft5, TRight5}"/></param>
    /// <param name="either6"><see cref="Either{TLeft6, TRight6}"/></param>
    /// <returns>
    /// <see cref="Either"><![CDATA[Either<]]>(<typeparamref name="TLeft1"/>, <typeparamref name="TLeft2"/>, <typeparamref name="TLeft3"/>, <typeparamref name="TLeft4"/>, <typeparamref name="TLeft5"/>, <typeparamref name="TLeft6"/>), (<typeparamref name="TRight1"/>, <typeparamref name="TRight2"/>, <typeparamref name="TRight3"/>, <typeparamref name="TRight4"/>, <typeparamref name="TRight5"/>, <typeparamref name="TRight6"/>)<![CDATA[>]]></see>
    /// </returns>
    public static Either<(TLeft1 Left1, TLeft2 Left2, TLeft3 Left3, TLeft4 Left4, TLeft5 Left5, TLeft6 Left6), (TRight1 Right1, TRight2 Right2, TRight3 Right3, TRight4 Right4, TRight5 Right5, TRight6 Right6)> Combine<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4, TLeft5, TRight5, TLeft6, TRight6>(
        Either<TLeft1, TRight1> either1,
        Either<TLeft2, TRight2> either2,
        Either<TLeft3, TRight3> either3,
        Either<TLeft4, TRight4> either4,
        Either<TLeft5, TRight5> either5,
        Either<TLeft6, TRight6> either6)
    {
        var error = false;

        HasError(either1, ref error);
        HasError(either2, ref error);
        HasError(either3, ref error);
        HasError(either4, ref error);
        HasError(either5, ref error);
        HasError(either6, ref error);

        if (error)
        {
            return InternalBuilders.CreateRight<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4, TLeft5, TRight5, TLeft6, TRight6>(either1.Right, either2.Right, either3.Right, either4.Right, either5.Right, either6.Right);
        }

        return InternalBuilders.CreateLeft<TLeft1, TRight1, TLeft2, TRight2, TLeft3, TRight3, TLeft4, TRight4, TLeft5, TRight5, TLeft6, TRight6>(either1.Left, either2.Left, either3.Left, either4.Left, either5.Left, either6.Left);
    }

    private static void HasError<TLeft, TRight>(Either<TLeft, TRight> either, ref bool error)
    {
        if (!either.IsLeft)
        {
            error = true;
        }
    }
}