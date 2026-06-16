namespace OtherMonad;

using System.Runtime.CompilerServices;

/// <summary>
/// Extension methods to Maybe Monad
/// </summary>
public static partial class Maybe
{
    /// <summary>
    /// Returns the current <see cref="Maybe{TSource}"/> if it has a value, otherwise returns a <see cref="Maybe{TSource}"/> containing the specified default value.
    /// </summary>
    /// <typeparam name="TSource">The type of the contained value.</typeparam>
    /// <param name="source">The <see cref="Maybe{TSource}"/> to check.</param>
    /// <param name="default">The default value to return if the source has no value.</param>
    /// <returns>The original <see cref="Maybe{TSource}"/> if it has a value; otherwise, a <see cref="Maybe{TSource}"/> containing <paramref name="default"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<TSource> OrElse<TSource>(this Maybe<TSource> source, TSource @default)
    {
        if (source.HasValue)
        {
            return source;
        }

        return @default;
    }

    /// <summary>
    /// Asynchronously waits for the <see cref="Maybe{TSource}"/> and returns it if it has a value, otherwise returns a <see cref="Maybe{TSource}"/> containing the specified default value.
    /// </summary>
    /// <typeparam name="TSource">The type of the contained value.</typeparam>
    /// <param name="source">A task that produces a <see cref="Maybe{TSource}"/>.</param>
    /// <param name="default">The default value to return if the resolved source has no value.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the resolved <see cref="Maybe{TSource}"/> if it has a value; otherwise, a <see cref="Maybe{TSource}"/> containing <paramref name="default"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Maybe<TSource>> OrElse<TSource>(this Task<Maybe<TSource>> source, TSource @default)
    {
        var maybe = await source;

        return maybe.OrElse(@default);
    }
}