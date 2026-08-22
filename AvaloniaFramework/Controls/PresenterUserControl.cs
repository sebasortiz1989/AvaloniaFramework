using Avalonia;
using Avalonia.Interactivity;
using AvaloniaFramework.Hosting;
using AvaloniaFramework.Presentation;
using AvaloniaFramework.Presentation.UseCase;

namespace AvaloniaFramework.Controls;

/// <summary>
/// Base class for a screen. It resolves <typeparamref name="TModel"/> from the ambient container,
/// makes it the <see cref="StyledElement.DataContext"/>, and forwards the lifecycle to it — so a
/// derived view's constructor only needs to call <c>InitializeComponent()</c>.
/// </summary>
/// <remarks>
/// On platforms with a soft keyboard, the content is shifted up when the keyboard would cover the
/// focused field — see <see cref="KeyboardAvoidingUserControl"/>.
/// </remarks>
/// <example>
/// <code>
/// public partial class LoginView : PresenterUserControl&lt;LoginViewModel, Unit, Unit&gt;
/// {
///     public LoginView() => InitializeComponent();
/// }
/// </code>
/// </example>
public abstract class PresenterUserControl<TModel, TInput, TResult>
    : KeyboardAvoidingUserControl, PresenterBase<TModel, TInput, TResult>, IDisposable
    where TModel : PresentationModelBase<TInput, TResult>
{
    protected PresenterUserControl()
    {
        PresentationModel = ViewContainer.Required.Resolve<TModel>();
        DataContext = PresentationModel;
    }

    /// <summary>The view model this screen is bound to.</summary>
    public TModel PresentationModel { get; }

    /// <inheritdoc />
    public bool Hosts(object model) => ReferenceEquals(PresentationModel, model);

    /// <inheritdoc />
    public bool AbandonRun() => PresentationModel.Abandon();

    /// <inheritdoc />
    public virtual Task<TResult> RunAsync(
        TInput input,
        PresentationExecutionContext context,
        CancellationToken cancellationToken = default) =>
        PresentationModel.RunAsync(input, context, cancellationToken);

    /// <inheritdoc />
    public virtual void Dispose() => GC.SuppressFinalize(this);

    /// <inheritdoc />
    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        Dispose();
    }
}
