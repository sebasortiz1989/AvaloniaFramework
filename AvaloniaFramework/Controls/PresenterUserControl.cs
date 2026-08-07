using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Platform;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.VisualTree;
using AvaloniaFramework.Controls.Inputs;
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
/// focused field.
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
    : UserControl, PresenterBase<TModel, TInput, TResult>, IDisposable
    where TModel : PresentationModelBase<TInput, TResult>
{
    private Point focusedInputPosition;

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

    /// <summary>
    /// How far up the design canvas the focused field should sit when the keyboard is open,
    /// as a fraction of the view's height.
    /// </summary>
    public virtual double DistanceToMoveWithKeyboard => 0.2d;

    /// <summary>
    /// The height of the design canvas the layout is authored against. Used to map a field's
    /// position into the view's actual height when deciding how far to shift.
    /// </summary>
    protected virtual double DesignCanvasHeight => 1560d;

    /// <inheritdoc />
    public virtual Task<TResult> RunAsync(
        TInput input,
        PresentationExecutionContext context,
        CancellationToken cancellationToken = default) =>
        PresentationModel.RunAsync(input, context, cancellationToken);

    /// <inheritdoc />
    public virtual void Dispose() => GC.SuppressFinalize(this);

    /// <inheritdoc />
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        var pane = TopLevel.GetTopLevel(this)?.InputPane;
        if (pane is not null)
            pane.StateChanged += OnInputPaneStateChanged;

        foreach (var input in InputElements())
            input.GotFocus += OnInputGotFocus;
    }

    /// <inheritdoc />
    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromLogicalTree(e);

        var pane = TopLevel.GetTopLevel(this)?.InputPane;
        if (pane is not null)
            pane.StateChanged -= OnInputPaneStateChanged;

        foreach (var input in InputElements())
            input.GotFocus -= OnInputGotFocus;
    }

    /// <inheritdoc />
    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        Dispose();
    }

    private static Point AbsolutePosition(Control? control)
    {
        var position = new Point(0, 0);

        while (control is not null)
        {
            position = new Point(position.X + control.Bounds.X, position.Y + control.Bounds.Y);
            control = control.Parent as Control;
        }

        return position;
    }

    private IEnumerable<Control> InputElements() =>
        this.GetVisualDescendants().OfType<Control>().Where(control => control is TextBox or VTextBoxWithLabel);

    private void OnInputGotFocus(object? sender, FocusChangedEventArgs e)
    {
        if (sender is Control control)
            focusedInputPosition = AbsolutePosition(control);
    }

    private void OnInputPaneStateChanged(object? sender, InputPaneStateEventArgs e)
    {
        if (Content is not Control content)
            return;

        var offset = (Bounds.Height / 2)
            - (focusedInputPosition.Y * Bounds.Height / DesignCanvasHeight)
            - (Bounds.Height * DistanceToMoveWithKeyboard);

        // A positive offset would push the field further under the keyboard, so leave it alone.
        if (offset > 0)
            return;

        content.RenderTransform = e.NewState == InputPaneState.Open
            ? new TranslateTransform(0, Math.Max(offset, -Bounds.Height / 2))
            : null;
    }
}
