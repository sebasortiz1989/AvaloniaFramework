using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace AvaloniaFramework.Threading;

/// <summary>
/// Await helpers that name their intent instead of spelling out <c>ConfigureAwait</c>.
/// <see cref="WithSync{T}(Task{T})"/> resumes on the captured context (the UI thread, in an
/// Avalonia app); <see cref="NoSync{T}(Task{T})"/> resumes anywhere.
/// </summary>
public static class AwaitExtensions
{
    /// <summary>Resumes on the synchronization context that was current at the await point.</summary>
    public static ConfiguredTaskAwaitable WithSync(this Task task) => task.ConfigureAwait(true);

    /// <inheritdoc cref="WithSync(Task)"/>
    public static ConfiguredTaskAwaitable<T> WithSync<T>(this Task<T> task) => task.ConfigureAwait(true);

    /// <inheritdoc cref="WithSync(Task)"/>
    public static ConfiguredValueTaskAwaitable WithSync(this ValueTask task) => task.ConfigureAwait(true);

    /// <inheritdoc cref="WithSync(Task)"/>
    public static ConfiguredValueTaskAwaitable<T> WithSync<T>(this ValueTask<T> task) => task.ConfigureAwait(true);

    /// <summary>Resumes on any thread pool thread, ignoring the captured context.</summary>
    public static ConfiguredTaskAwaitable NoSync(this Task task) => task.ConfigureAwait(false);

    /// <inheritdoc cref="NoSync(Task)"/>
    public static ConfiguredTaskAwaitable<T> NoSync<T>(this Task<T> task) => task.ConfigureAwait(false);

    /// <inheritdoc cref="NoSync(Task)"/>
    public static ConfiguredValueTaskAwaitable NoSync(this ValueTask task) => task.ConfigureAwait(false);

    /// <inheritdoc cref="NoSync(Task)"/>
    public static ConfiguredValueTaskAwaitable<T> NoSync<T>(this ValueTask<T> task) => task.ConfigureAwait(false);

    /// <summary>
    /// Explicitly abandons a task, so that fire-and-forget call sites read as deliberate rather
    /// than as a forgotten await. Faults are observed and rethrown on the thread pool.
    /// </summary>
    public static void Forget(this Task task)
    {
        ArgumentNullException.ThrowIfNull(task);

        if (task.IsCompletedSuccessfully)
            return;

        task.ContinueWith(
            static t => ExceptionDispatchInfo.Throw(t.Exception!),
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
            TaskScheduler.Default);
    }
}
