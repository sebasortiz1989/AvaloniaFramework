using System.Runtime.CompilerServices;

namespace AvaloniaFramework.Threading;

/// <summary>
/// Lets an async method hop onto a <see cref="SynchronizationContext"/> mid-body:
/// <c>synchronizationContext.SwitchTo();</c> inside a callback running off the UI thread
/// moves the rest of the callback onto it.
/// </summary>
public static class SynchronizationContextExtensions
{
    /// <summary>
    /// Posts the remainder of the calling method to <paramref name="context"/>.
    /// Awaiting the result is optional — the awaiter is also usable as a statement, in which
    /// case the continuation is scheduled the moment the enclosing async method yields.
    /// </summary>
    public static SynchronizationContextAwaitable SwitchTo(this SynchronizationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return new SynchronizationContextAwaitable(context);
    }

    /// <summary>
    /// Runs <paramref name="action"/> on <paramref name="context"/>, inline when the caller is
    /// already there. This is the form to use from a synchronous callback — unlike
    /// <see cref="SwitchTo"/>, it does not need to be awaited to take effect.
    /// </summary>
    public static void Run(this SynchronizationContext context, Action action)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(action);

        if (SynchronizationContext.Current == context)
            action();
        else
            context.Post(static state => ((Action)state!)(), action);
    }

    /// <summary>Awaitable returned by <see cref="SwitchTo"/>.</summary>
    public readonly struct SynchronizationContextAwaitable(SynchronizationContext context)
    {
        public SynchronizationContextAwaiter GetAwaiter() => new(context);
    }

    /// <summary>Awaiter that resumes the continuation on the target context.</summary>
    public readonly struct SynchronizationContextAwaiter(SynchronizationContext context)
        : INotifyCompletion
    {
        /// <summary>True when the caller is already on the target context, so no hop is needed.</summary>
        public bool IsCompleted => SynchronizationContext.Current == context;

        public void OnCompleted(Action continuation) =>
            context.Post(static state => ((Action)state!)(), continuation);

        public void GetResult()
        {
        }
    }
}
