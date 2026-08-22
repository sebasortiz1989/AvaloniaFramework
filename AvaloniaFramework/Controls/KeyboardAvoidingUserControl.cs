using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Platform;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.VisualTree;
using AvaloniaFramework.Controls.Inputs;

namespace AvaloniaFramework.Controls;

/// <summary>
/// A screen that lifts its content out from under the soft keyboard. Split out of
/// <see cref="PresenterUserControl{TModel,TInput,TResult}"/> so the shift can find the nearest
/// screen above a focused field without knowing its type arguments.
/// </summary>
/// <remarks>
/// The shift is a <see cref="TranslateTransform"/> on <see cref="ContentControl.Content"/> and is
/// undone whenever the keyboard closes or the screen is shown. Deciding to undo it from the current
/// offset instead is what used to leave a screen stuck near the top of the window.
/// </remarks>
public abstract class KeyboardAvoidingUserControl : UserControl
{
    private IInputPane? pane;
    private Point focusedInputPosition;

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
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        // A screen is shown with the keyboard closed, so it starts unshifted. Screens outlive the
        // navigation that shows them — a tab presenter is built once and reused — so one that was
        // shifted and never told the keyboard had gone would come back still sitting high.
        ClearOffset();

        // Detach first: Loaded can be raised again without a matching detach, and a screen
        // subscribed twice would shift twice.
        DetachFromInput();

        pane = TopLevel.GetTopLevel(this)?.InputPane;
        if (pane is not null)
            pane.StateChanged += OnInputPaneStateChanged;

        // One handler on the screen rather than one per field: rows and forms built after the
        // screen loads are caught too, and hooking each field once at load caught none of them.
        AddHandler(GotFocusEvent, OnInputGotFocus, RoutingStrategies.Bubble);
    }

    /// <inheritdoc />
    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        DetachFromInput();
        ClearOffset();
    }

    /// <inheritdoc />
    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromLogicalTree(e);
        DetachFromInput();
    }

    private void DetachFromInput()
    {
        if (pane is not null)
        {
            pane.StateChanged -= OnInputPaneStateChanged;
            pane = null;
        }

        RemoveHandler(GotFocusEvent, OnInputGotFocus);
    }

    private void ClearOffset()
    {
        focusedInputPosition = default;

        if (Content is Control content)
            content.RenderTransform = null;
    }

    private void OnInputGotFocus(object? sender, FocusChangedEventArgs e)
    {
        if (e.Source is not Control control || control is not (TextBox or VTextBoxWithLabel))
            return;

        // Screens nest — the field belongs to a tab, which sits inside the screen holding the
        // navigation bar — and the event passes through every one of them. Each shifting its own
        // content would move the field several times over, so only the closest screen acts.
        if (control.GetVisualAncestors().OfType<KeyboardAvoidingUserControl>().FirstOrDefault() != this)
            return;

        focusedInputPosition = control.TranslatePoint(default, this) ?? default;

        // The keyboard is already up when the next field is tapped, so no state change is coming
        // to work the shift out again.
        if (pane?.State == InputPaneState.Open)
            ApplyOffset();
    }

    private void OnInputPaneStateChanged(object? sender, InputPaneStateEventArgs e)
    {
        if (e.NewState == InputPaneState.Open)
            ApplyOffset();
        else
            ClearOffset();
    }

    private void ApplyOffset()
    {
        if (Content is not Control content)
            return;

        var offset = (Bounds.Height / 2)
            - (focusedInputPosition.Y * Bounds.Height / DesignCanvasHeight)
            - (Bounds.Height * DistanceToMoveWithKeyboard);

        // A positive offset would push the field further under the keyboard. Clear rather than
        // return: the last field may have needed a shift where this one does not.
        content.RenderTransform = offset > 0
            ? null
            : new TranslateTransform(0, Math.Max(offset, -Bounds.Height / 2));
    }
}
