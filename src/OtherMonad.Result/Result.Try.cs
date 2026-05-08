namespace OtherMonad;

/// <summary>
/// Static factory and extension methods for <see cref="Result{T}"/>.
/// </summary>
public static partial class Result
{
    /// <summary>
    /// Executes <paramref name="factory"/> and returns <see cref="Result{T}.Create.Ok(T)"/> on success,
    /// or <see cref="Result{T}.Create.Err(Exception)"/> if the factory throws.
    /// </summary>
    /// <typeparam name="T">The success type.</typeparam>
    /// <param name="factory">A delegate whose return value becomes the success value.</param>
    /// <returns><see cref="Result{T}"/></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory"/> is <see langword="null"/>.</exception>
    public static Result<T> Try<T>(Func<T> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);

        try
        {
            return Result<T>.Create.Ok(factory());
        }
        catch (Exception ex)
        {
            return Result<T>.Create.Err(ex);
        }
    }

    /// <summary>
    /// Executes <paramref name="factory"/> asynchronously and returns <see cref="Result{T}.Create.Ok(T)"/> on success,
    /// or <see cref="Result{T}.Create.Err(Exception)"/> if the factory throws.
    /// </summary>
    /// <typeparam name="T">The success type.</typeparam>
    /// <param name="factory">An async delegate whose return value becomes the success value.</param>
    /// <param name="cancellation">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns><see cref="Task{T}"><![CDATA[Task<]]><see cref="Result{T}"/><![CDATA[>]]></see></returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory"/> is <see langword="null"/>.</exception>
    public static async Task<Result<T>> Try<T>(
        Func<CancellationToken, Task<T>> factory,
        CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(factory);

        try
        {
            return Result<T>.Create.Ok(await factory(cancellation).ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return Result<T>.Create.Err(ex);
        }
    }
}
