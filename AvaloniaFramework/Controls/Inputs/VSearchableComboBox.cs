using System;
using Avalonia.Controls;
using Avalonia.Input;

namespace AvaloniaFramework.Controls.Inputs;

/// <summary>
/// A picker for lists too long to scroll comfortably: focusing it drops down every item, and typing
/// narrows that list to the entries containing what was typed.
/// </summary>
/// <remarks>
/// <para>
/// This is <see cref="AutoCompleteBox"/> with the two defaults a picker wants — a case-insensitive
/// <c>Contains</c> filter and a zero-length prefix, so an empty box still populates — plus the one
/// behaviour it lacks: a click shows the <i>whole</i> list rather than nothing.
/// </para>
/// <para>
/// Getting there takes a little care, because the text and the selection are the same field. Once an
/// item is chosen the box holds that item's label, and filtering on it would match only the item
/// already picked. So focus blanks the text to reveal the full list, remembering the selection; if
/// focus leaves without a new choice being made, the remembered one is put back. Tabbing through a
/// form therefore never silently empties a picker.
/// </para>
/// <para>
/// It keeps <see cref="AutoCompleteBox"/>'s own theme via <see cref="StyleKeyOverride"/>, so consumers
/// style it with their existing <c>AutoCompleteBox</c> rules and no theme is added to
/// <c>LayoutStyles.axaml</c>.
/// </para>
/// </remarks>
public class VSearchableComboBox : AutoCompleteBox
{
    /// <summary>
    /// What was selected when the box took focus, held only for as long as the box has focus, so a
    /// blanked text field can be undone if the user picks nothing.
    /// </summary>
    private object? selectionBeforeFocus;

    /// <summary>Initializes a new instance of the <see cref="VSearchableComboBox"/> class.</summary>
    public VSearchableComboBox()
    {
        // A zero prefix is what makes the unfiltered list appear; Contains is case-insensitive and
        // matches mid-word, which is how someone hunting a name in a long list actually types.
        FilterMode = AutoCompleteFilterMode.Contains;
        MinimumPrefixLength = 0;
    }

    /// <summary>Keeps <see cref="AutoCompleteBox"/>'s theme rather than looking for one of its own.</summary>
    protected override Type StyleKeyOverride => typeof(AutoCompleteBox);

    /// <inheritdoc />
    protected override void OnGotFocus(FocusChangedEventArgs e)
    {
        base.OnGotFocus(e);
        ShowEveryItem();
    }

    /// <inheritdoc />
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        // A tap on an already-focused box has no GotFocus to ride on, so re-open from here too.
        ShowEveryItem();
    }

    /// <inheritdoc />
    protected override void OnLostFocus(FocusChangedEventArgs e)
    {
        base.OnLostFocus(e);

        // Blanking the text to show the list nulls the selection as a side effect. Only put the old
        // one back when the user left without choosing — a real new choice must survive.
        if (SelectedItem is null && selectionBeforeFocus is not null)
        {
            SelectedItem = selectionBeforeFocus;
        }

        selectionBeforeFocus = null;
    }

    /// <summary>
    /// Clears the filter text and opens the drop-down, so the list on screen is the full one.
    /// </summary>
    private void ShowEveryItem()
    {
        if (SelectedItem is not null)
        {
            selectionBeforeFocus = SelectedItem;
            Text = string.Empty;
        }

        IsDropDownOpen = true;
    }
}
