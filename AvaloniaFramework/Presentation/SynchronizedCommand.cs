using System.ComponentModel;
using System.Diagnostics;
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
    public void Execute(object? parameter) => ExecuteAsync(parameter).Forget();

    /// <summary>
    /// The awaitable form of <see cref="Execute"/>. The returned task completes once this
    /// invocation — and anything it caused to be queued — has finished.
    /// </summary>
    public Task ExecuteAsync(object? parameter)
    {
        return TryBeginExecution(parameter)
            ? InvokeCurrentAndQueued(parameter)
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

    private async Task InvokeCurrentAndQueued(object? parameter)
    {
        await InvokeTarget(target, parameter).WithSync();

        while (true)
        {
            PendingExecution[] captured;

            lock (executionGate)
            {
                if (waiting is not { Count: > 0 })
                {
                    isRunning = false;
                    return;
                }

                captured = [.. waiting];
                waiting.Clear();
            }

            foreach (var pending in captured)
                await pending.Execute().WithSync();
        }
    }

    private readonly struct PendingExecution(Delegate target, object? parameter)
    {
        public Task Execute() => InvokeTarget(target, parameter);
    }
}
