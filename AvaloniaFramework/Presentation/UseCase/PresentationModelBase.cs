using System.ComponentModel;
using System.Runtime.CompilerServices;
using AvaloniaFramework.Threading;

namespace AvaloniaFramework.Presentation.UseCase;

/// <summary>
/// Base class for view models. A run begins when the navigation controller pushes the hosting
/// view and does not complete until something calls <see cref="Finish"/> — typically the
/// controller popping this screen — so <c>await navigationController.PushAsync(...)</c> reads as
/// "show this screen and give me its result".
/// </summary>
/// <remarks>
/// The <see cref="PropertyChanged"/> event declared here is what PropertyChanged.Fody weaves
/// against in derived classes, so subclasses marked <c>[AddINotifyPropertyChangedInterface]</c>
/// can use plain auto-properties.
/// </remarks>
public abstract class PresentationModelBase<TInput, TResult> : PresentationModel<TInput, TResult>
{
    private readonly object runGate = new();
    private TaskCompletionSource<TResult>? taskResult;
    private string presenterTitle = string.Empty;

    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <inheritdoc />
    public event EventHandler? OnRunStarted;

    /// <inheritdoc />
    public event EventHandler? OnRunFinished;

    /// <inheritdoc />
    public string PresenterTitle
    {
        get => presenterTitle;
        protected set
        {
            if (presenterTitle == value)
                return;

            presenterTitle = value;
            OnPropertyChanged();
        }
    }

    /// <summary>The context of the current run; default outside of a run.</summary>
    protected PresentationExecutionContext Context { get; private set; }

    /// <summary>Cancelled when the current run ends, for work that should not outlive the screen.</summary>
    protected CancellationToken PresentationModelFinished { get; private set; }

    /// <inheritdoc />
    public async Task<TResult> RunAsync(
        TInput input,
        PresentationExecutionContext context,
        CancellationToken cancellationToken = default)
    {
        Task<TResult> pendingResult;
        CancellationTokenSource localCancellation;
        CancellationTokenSource linkedCancellation;

        lock (runGate)
        {
            if (taskResult is { Task.IsCompleted: false })
                throw new InvalidOperationException($"'{GetType().Name}' is already running.");

            taskResult = new TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously);
            localCancellation = new CancellationTokenSource();
            linkedCancellation = CancellationTokenSource.CreateLinkedTokenSource(localCancellation.Token, cancellationToken);
            PresentationModelFinished = linkedCancellation.Token;
            pendingResult = taskResult.Task;
            Context = context;
        }

        OnRunStarted?.Invoke(this, EventArgs.Empty);

        await OnRunStarting(input).WithSync();

        try
        {
            return await pendingResult.WithSync();
        }
        finally
        {
            await localCancellation.CancelAsync().WithSync();

            lock (runGate)
            {
                Context = default;
                PresentationModelFinished = default;
                taskResult = null;
            }

            await OnRunFinishing().WithSync();

            OnRunFinished?.Invoke(this, EventArgs.Empty);

            linkedCancellation.Dispose();
            localCancellation.Dispose();
        }
    }

    /// <summary>Completes the current run with <paramref name="result"/>. False if not running.</summary>
    public Task<bool> Finish(TResult result) =>
        Task.FromResult(taskResult?.TrySetResult(result) ?? false);

    /// <summary>Faults the current run. False if not running.</summary>
    public Task<bool> FinishWithError(Exception exception) =>
        Task.FromResult(taskResult?.TrySetException(exception) ?? false);

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>Raises <see cref="PropertyChanged"/>.</summary>
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>Called once the model has its input, before the run's task is awaited.</summary>
    protected abstract Task OnRunStarting(TInput input);

    /// <summary>Called after the run completes. Override to unsubscribe from anything long-lived.</summary>
    protected virtual Task OnRunFinishing()
    {
        Dispose();
        return Task.CompletedTask;
    }

    /// <summary>Cancels the current run. False if not running.</summary>
    protected Task<bool> Cancel(CancellationToken cancellationToken) =>
        Task.FromResult(taskResult?.TrySetCanceled(cancellationToken) ?? false);

    /// <inheritdoc cref="Dispose()" />
    protected virtual void Dispose(bool disposing)
    {
    }
}
