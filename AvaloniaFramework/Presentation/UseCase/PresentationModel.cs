using System.ComponentModel;

namespace AvaloniaFramework.Presentation.UseCase;

/// <summary>
/// The contract a view binds to: a lifecycle step that also raises change notifications and
/// reports when its run starts and finishes.
/// </summary>
public interface PresentationModel<TInput, TResult>
    : LifecycleStep<TInput, TResult>, INotifyPropertyChanged, IDisposable
{
    /// <summary>Raised once the model has been handed its input and context.</summary>
    event EventHandler OnRunStarted;

    /// <summary>Raised after the run has completed and teardown has happened.</summary>
    event EventHandler OnRunFinished;

    /// <summary>A title the host chrome can display for this screen.</summary>
    string PresenterTitle { get; }
}
