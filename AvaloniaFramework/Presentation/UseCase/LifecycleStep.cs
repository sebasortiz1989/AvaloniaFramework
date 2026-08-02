namespace AvaloniaFramework.Presentation.UseCase;

/// <summary>
/// One step of the presentation lifecycle: it is handed an input, runs until something finishes
/// it, and produces a result. Both presentation models and the views that host them implement it,
/// which is what lets a view be pushed onto the navigation stack and awaited as a unit.
/// </summary>
public interface LifecycleStep<TInput, TResult>
{
    /// <summary>
    /// Runs the step. The returned task completes when the step is finished — by
    /// <see cref="PresentationModelBase{TInput, TResult}.Finish"/>, by a fault, or by cancellation.
    /// </summary>
    Task<TResult> RunAsync(TInput input, PresentationExecutionContext context, CancellationToken cancellationToken = default);
}

/// <summary>Convenience overloads for steps that take no input.</summary>
public static class LifecycleStepExtensions
{
    /// <inheritdoc cref="LifecycleStep{TInput, TResult}.RunAsync"/>
    public static Task<TResult> RunAsync<TResult>(
        this LifecycleStep<Unit, TResult> lifecycleStep,
        PresentationExecutionContext context,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(lifecycleStep);
        return lifecycleStep.RunAsync(Unit.Value, context, cancellationToken);
    }
}
