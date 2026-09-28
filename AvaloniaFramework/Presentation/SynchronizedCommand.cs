using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Windows.Input;
using AvaloniaFramework.Threading;

namespace AvaloniaFramework.Presentation;

/// <summary>
/// An <see cref="ICommand"/> that will not run twice at once. A second invocation arriving while
/// the first is still in flight is either queued or dropped, depending on the
/// <see cref="SynchronizationBehavior"/> given at construction — which is what stops a double tap
/// from pushing the same screen twice.
/// </summary>
/// <example>
/// <code>
/// SaveCommand = new SynchronizedCommand(SaveAsync, SynchronizationBehavior.Discard, true);
/// </code>
/// </example>
[DebuggerDisplay("CanExecute={CanExecute}")]
public sealed class SynchronizedCommand : ICommand, INotifyPropertyChanged, IDisposable
{
    private readonly Delegate target;
    private readonly SynchronizationGate executionGate;
    private readonly Queue<PendingExecution>? waiting;
    private bool canExecute;
    private bool isRunning;

    /// <summary>Wraps a synchronous parameterless handler.</summary>
    public SynchronizedCommand(Action target, SynchronizationBehavior behavior, bool canExecute)
        : this((Delegate)target, null, behavior, canExecute)
    {
    }

    /// <inheritdoc cref="SynchronizedCommand(Action, SynchronizationBehavior, bool)" />
    public SynchronizedCommand(Action target, SynchronizationGate? gate, SynchronizationBehavior behavior, bool canExecute)
        : this((Delegate)target, gate, behavior, canExecute)
    {
    }

    /// <summary>Wraps a synchronous handler that receives the command parameter.</summary>
    public SynchronizedCommand(Action<object?> target, SynchronizationBehavior behavior, bool canExecute)
        : this((Delegate)target, null, behavior, canExecute)
    {
    }

    /// <inheritdoc cref="SynchronizedCommand(Action{object}, SynchronizationBehavior, bool)" />
    public SynchronizedCommand(Action<object?> target, SynchronizationGate? gate, SynchronizationBehavior behavior, bool canExecute)
        : this((Delegate)target, gate, behavior, canExecute)
    {
    }

    /// <summary>Wraps an asynchronous parameterless handler.</summary>
    public SynchronizedCommand(Func<Task> target, SynchronizationBehavior behavior, bool canExecute)
        : this((Delegate)target, null, behavior, canExecute)
    {
    }

    /// <inheritdoc cref="SynchronizedCommand(Func{Task}, SynchronizationBehavior, bool)" />
    public SynchronizedCommand(Func<Task> target, SynchronizationGate? gate, SynchronizationBehavior behavior, bool canExecute)
        : this((Delegate)target, gate, behavior, canExecute)
    {
    }

    /// <summary>Wraps an asynchronous handler that receives the command parameter.</summary>
    public SynchronizedCommand(Func<object?, Task> target, SynchronizationBehavior behavior, bool canExecute)
        : this((Delegate)target, null, behavior, canExecute)
    {
    }

    /// <inheritdoc cref="SynchronizedCommand(Func{object, Task}, SynchronizationBehavior, bool)" />
    public SynchronizedCommand(Func<object?, Task> target, SynchronizationGate? gate, SynchronizationBehavior behavior, bool canExecute)
        : this((Delegate)target, gate, behavior, canExecute)
    {
    }

    private SynchronizedCommand(Delegate target, SynchronizationGate? gate, SynchronizationBehavior behavior, bool canExecute)
    {
        ArgumentNullException.ThrowIfNull(target);

        this.target = target;
        this.canExecute = canExecute;
        executionGate = gate ?? new SynchronizationGate();

        waiting = behavior switch
        {
            SynchronizationBehavior.Enqueue => new Queue<PendingExecution>(),
            SynchronizationBehavior.Discard => null,
            _ => throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null),
        };
    }

    /// <inheritdoc />
    public event EventHandler? CanExecuteChanged;

    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raised once for every fault a target throws, on the context the press was made from (the UI
    /// thread, for a button), before the command is released. This is where a press made through <see cref="Execute"/> reports its
    /// fault: a button has nothing to await, so without a handler the fault goes where
    /// <see cref="AwaitExtensions.Forget(Task)"/> sends it, which is nowhere a user can see.
    /// </summary>
    /// <remarks>
    /// A cancellation (<see cref="OperationCanceledException"/>) is not a fault and is not raised.
    /// <see cref="ExecuteAsync"/> still faults with the first fault as well, so a caller that awaits
    /// it sees the fault twice if it also handles this.
    /// A handler that throws is a bug in the handler, and its exception is not swallowed. Presses
    /// queued behind the fault still run; then <see cref="ExecuteAsync"/>'s task faults with the
    /// handler's exception, and from <see cref="Execute"/> it goes where
    /// <see cref="AwaitExtensions.Forget(Task)"/> sends it.
    /// </remarks>
    public event EventHandler<CommandFaultedEventArgs>? Faulted;

    /// <summary>Whether the command is currently enabled. Setting it re-queries bound controls.</summary>
    public bool CanExecute
    {
        get => canExecute;

        set
        {
            lock (executionGate)
            {
                if (canExecute == value)
                    return;

                canExecute = value;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanExecute)));
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    bool ICommand.CanExecute(object? parameter) => canExecute;

    /// <inheritdoc />
    public void Execute(object? parameter)
    {
        // A button has nothing to await, so this task is abandoned, and it faults only with the first
        // fault no Faulted handler carried: a target's fault when there is no handler, or a handler's
        // own exception when one throws. A fault a handler did carry is not reported a second time,
        // as an unobserved task exception.
        if (TryBeginExecution(parameter))
            InvokeCurrentAndQueued(parameter, includeReportedFaults: false).Forget();
    }

    /// <summary>
    /// The awaitable form of <see cref="Execute"/>. The returned task completes once this
    /// invocation — and anything it caused to be queued — has finished.
    /// </summary>
    /// <remarks>
    /// A target that faults releases the command, and invocations queued behind it still run.
    /// Once the queue has drained, the returned task faults with the first fault no
    /// <see cref="Faulted"/> handler was given — a handler's own exception, or a fault met while no
    /// handler was subscribed — and otherwise with the first fault. It ends cancelled only when
    /// nothing faulted, so a cancellation does not hide a fault queued after it.
    /// </remarks>
    public Task ExecuteAsync(object? parameter)
    {
        return TryBeginExecution(parameter)
            ? InvokeCurrentAndQueued(parameter, includeReportedFaults: true)
            : Task.CompletedTask;
    }

    /// <inheritdoc />
    public void Dispose() => waiting?.Clear();

    private static Task InvokeTarget(Delegate target, object? parameter)
    {
        switch (target)
        {
            case Action action:
                action();
                return Task.CompletedTask;
            case Action<object?> action:
                action(parameter);
                return Task.CompletedTask;
            case Func<Task> function:
                return function();
            case Func<object?, Task> function:
                return function(parameter);
            default:
                throw new InvalidOperationException($"Unsupported command target: {target.GetType()}");
        }
    }

    private bool TryBeginExecution(object? parameter)
    {
        lock (executionGate)
        {
            if (!canExecute)
                return false;

            if (isRunning)
            {
                waiting?.Enqueue(new PendingExecution(target, parameter));
                return false;
            }

            isRunning = true;
            return true;
        }
    }

    private async Task InvokeCurrentAndQueued(object? parameter, bool includeReportedFaults)
    {
        var faults = new FaultLog();
        var released = false;

        try
        {
            await InvokeObservingFault(new PendingExecution(target, parameter), faults).WithSync();

            while (true)
            {
                PendingExecution[] captured;

                lock (executionGate)
                {
                    // Released under the same lock that finds the queue empty, so a press arriving
                    // between the two cannot be queued behind a run that has already ended.
                    if (waiting is not { Count: > 0 })
                    {
                        isRunning = false;
                        released = true;
                        break;
                    }

                    captured = [.. waiting];
                    waiting.Clear();
                }

                // Every queued press runs even after a fault: each one is a press the user made.
                foreach (var pending in captured)
                    await InvokeObservingFault(pending, faults).WithSync();
            }
        }
        finally
        {
            // InvokeObservingFault catches a target's fault and a Faulted handler's alike, so no
            // known path reaches here un-released. If one ever does, the command must still not
            // stay running, or the button is dead for the rest of its life.
            if (!released)
            {
                lock (executionGate)
                    isRunning = false;
            }
        }

        faults.Rethrow(includeReportedFaults);
    }

    private async Task InvokeObservingFault(PendingExecution pending, FaultLog faults)
    {
        try
        {
            await pending.Execute().WithSync();
        }
#pragma warning disable CA1031 // Deliberately general: the fault is captured and rethrown once the queue drains.
        catch (Exception exception)
#pragma warning restore CA1031
        {
            if (exception is OperationCanceledException)
            {
                faults.AddCancellation(exception);
                return;
            }

            var handler = Faulted;
            faults.AddFault(exception, reported: handler is not null);

            if (handler is null)
                return;

            try
            {
                handler(this, new CommandFaultedEventArgs(exception));
            }
#pragma warning disable CA1031 // Deliberately general: a handler's own exception must not strand the presses queued behind it.
            catch (Exception handlerException)
#pragma warning restore CA1031
            {
                faults.AddHandlerFault(handlerException);
            }
        }
    }

    private readonly struct PendingExecution(Delegate target, object? parameter)
    {
        public Task Execute() => InvokeTarget(target, parameter);
    }

    /// <summary>What one execution and the presses queued behind it threw, kept until the queue drains.</summary>
    private sealed class FaultLog
    {
        private ExceptionDispatchInfo? firstFault;
        private ExceptionDispatchInfo? firstUnreported;
        private ExceptionDispatchInfo? firstCancellation;

        public void AddCancellation(Exception exception) =>
            firstCancellation ??= ExceptionDispatchInfo.Capture(exception);

        /// <summary>A target's fault. It is reported when a <see cref="Faulted"/> handler was given it.</summary>
        public void AddFault(Exception exception, bool reported)
        {
            var fault = ExceptionDispatchInfo.Capture(exception);
            firstFault ??= fault;

            if (!reported)
                firstUnreported ??= fault;
        }

        /// <summary>A <see cref="Faulted"/> handler's own exception, which nothing else will carry.</summary>
        public void AddHandlerFault(Exception exception) =>
            firstUnreported ??= ExceptionDispatchInfo.Capture(exception);

        /// <summary>
        /// Throws what the caller is owed. An awaiting caller gets the first unreported fault, else
        /// the first fault, and a cancellation only when nothing faulted. An abandoned press gets
        /// the unreported fault alone.
        /// </summary>
        public void Rethrow(bool includeReportedFaults)
        {
            if (!includeReportedFaults)
            {
                firstUnreported?.Throw();
                return;
            }

            (firstUnreported ?? firstFault ?? firstCancellation)?.Throw();
        }
    }
}
