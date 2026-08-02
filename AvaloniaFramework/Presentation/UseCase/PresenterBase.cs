namespace AvaloniaFramework.Presentation.UseCase;

/// <summary>
/// A view that hosts <typeparamref name="TModel"/> and forwards the lifecycle to it. Views are
/// resolved as this interface, so navigation deals in presenters without knowing the concrete
/// control type.
/// </summary>
public interface PresenterBase<TModel, TInput, TResult> : LifecycleStep<TInput, TResult>, PresenterHandle
    where TModel : PresentationModelBase<TInput, TResult>
{
}
