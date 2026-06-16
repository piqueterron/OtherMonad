namespace OtherMonad;

using System.Runtime.CompilerServices;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary> 
    /// Wraps a value of type <typeparamref name="TSource"/> into a <see cref="Maybe{TSource}"/>.
    /// </summary>
    /// <typeparam name="TSource">The type of the value to wrap.</typeparam>
    /// <param name="source">The value to wrap. If <see langword="null"/>, returns <see cref="Maybe{TSource}.None"/>.</param>
    /// <returns>A <see cref="Maybe{TSource}"/> containing the value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<TSource> Wrap<TSource>(this TSource source)
    {
        return source;
    }

    /// <summary>
    /// Unwraps a <see cref="Maybe{TSource}"/> and returns its value.
    /// </summary>
    /// <typeparam name="TSource">The type of the contained value.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> to unwrap.</param>
    /// <returns>The contained value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="source"/> has no value (<see cref="Maybe{TSource}.HasValue"/> is <see langword="false"/>). Use <see cref="Unwrap{TSource}(Maybe{TSource}, TSource)"/> to provide a fallback value.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TSource Unwrap<TSource>(this Maybe<TSource> source)
    {
        if (!source.HasValue)
            throw new InvalidOperationException("Maybe has no value. Use Unwrap(defaultValue) to provide a fallback.");

        return source.Value;
    }

    /// <summary>
    /// Unwraps a <see cref="Maybe{TSource}"/> and returns its value, or a default value if empty.
    /// </summary>
    /// <typeparam name="TSource">The type of the contained value.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> to unwrap.</param>
    /// <param name="default">The default value to return if the <see cref="Maybe{TSource}"/> has no value.</param>
    /// <returns>The contained value if present; otherwise, <paramref name="default"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TSource Unwrap<TSource>(this Maybe<TSource> source, TSource @default)
    {
        if (source.HasValue)
        {
            return source.Unwrap();
        }

        return @default;
    }
}