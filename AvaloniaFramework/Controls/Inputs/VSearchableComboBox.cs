using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Media;

namespace AvaloniaFramework.Controls.Inputs;

/// <summary>
/// A picker for lists too long to scroll comfortably: focusing it shows every item, and typing
/// narrows that list to the entries containing what was typed.
/// </summary>
/// <remarks>
/// <para>
/// The list is drawn **inline**, in the ordinary layout below the field, rather than in a popup.
/// That is the whole reason this control exists instead of a styled <see cref="AutoCompleteBox"/>.
/// A popup is laid out in its own visual root, so it does not inherit a
/// <see cref="Visual.RenderTransform"/> applied further up the tree — and a host that scales a
/// fixed design canvas to the device does exactly that. The field shrinks with the canvas while its
/// drop-down keeps the canvas's own units and renders at full size, giving a list several times
/// wider than the control it belongs to. Staying in the layout keeps the two in the same
/// coordinate space, whatever the host does with it.
/// </para>
/// <para>
/// The cost is that opening the list pushes what follows it down the page rather than covering it,
/// so place one where that is acceptable — inside a scrolling form, not over a fixed layout.
/// </para>
/// <para>
/// Items are matched and displayed by their <see cref="object.ToString"/>, so give the option type
/// a <c>ToString</c> that returns what the user should read and search on.
/// </para>
/// </remarks>
public class VSearchableComboBox : TemplatedControl
{
    /// <summary>The items to choose from.</summary>
    public static readonly StyledProperty<IEnumerable?> ItemsSourceProperty =
        AvaloniaProperty.Register<VSearchableComboBox, IEnumerable?>(nameof(ItemsSource));

    /// <summary>The chosen item. Binds two-way by default.</summary>
    public static readonly StyledProperty<object?> SelectedItemProperty =
        AvaloniaProperty.Register<VSearchableComboBox, object?>(
            nameof(SelectedItem),
            defaultBindingMode: BindingMode.TwoWay);

    /// <summary>Shown while nothing has been chosen.</summary>
    public static readonly StyledProperty<string?> PlaceholderTextProperty =
        AvaloniaProperty.Register<VSearchableComboBox, string?>(nameof(PlaceholderText));

    /// <summary>What has been typed. The list filters on it.</summary>
    public static readonly StyledProperty<string?> SearchTextProperty =
        AvaloniaProperty.Register<VSearchableComboBox, string?>(nameof(SearchText));

    /// <summary>Whether the list is showing.</summary>
    public static readonly StyledProperty<bool> IsListOpenProperty =
        AvaloniaProperty.Register<VSearchableComboBox, bool>(nameof(IsListOpen));

    /// <summary>Fill behind the list.</summary>
    public static readonly StyledProperty<IBrush?> VListBackgroundProperty =
        AvaloniaProperty.Register<VSearchableComboBox, IBrush?>(nameof(VListBackground), Brushes.White);

    /// <summary>How tall the list may grow before it scrolls.</summary>
    public static readonly StyledProperty<double> VListMaxHeightProperty =
        AvaloniaProperty.Register<VSearchableComboBox, double>(nameof(VListMaxHeight), 300d);

    /// <summary>Padding inside each row of the list.</summary>
    public static readonly StyledProperty<Thickness> VItemPaddingProperty =
        AvaloniaProperty.Register<VSearchableComboBox, Thickness>(nameof(VItemPadding), new Thickness(12, 8));

    /// <summary>Colour of <see cref="PlaceholderText"/>.</summary>
    public static readonly StyledProperty<IBrush?> VPlaceholderForegroundProperty =
        AvaloniaProperty.Register<VSearchableComboBox, IBrush?>(nameof(VPlaceholderForeground), Brushes.Gray);

    /// <summary>Gap between the field and the list.</summary>
    public static readonly StyledProperty<double> VListSpacingProperty =
        AvaloniaProperty.Register<VSearchableComboBox, double>(nameof(VListSpacing), 4d);

    private readonly ObservableCollection<object> matches = [];

    /// <summary>
    /// Set while the control is writing <see cref="SearchText"/> itself, so the change handler can
    /// tell a user's keystroke — which should filter and open the list — from its own tidying up,
    /// which should not.
    /// </summary>
    private bool writingSearchText;

    private ListBox? list;
    private TextBox? textBox;

    /// <inheritdoc cref="ItemsSourceProperty" />
    public IEnumerable? ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <inheritdoc cref="SelectedItemProperty" />
    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    /// <inheritdoc cref="PlaceholderTextProperty" />
    public string? PlaceholderText
    {
        get => GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    /// <inheritdoc cref="SearchTextProperty" />
    public string? SearchText
    {
        get => GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }

    /// <inheritdoc cref="IsListOpenProperty" />
    public bool IsListOpen
    {
        get => GetValue(IsListOpenProperty);
        set => SetValue(IsListOpenProperty, value);
    }

    /// <inheritdoc cref="VListBackgroundProperty" />
    public IBrush? VListBackground
    {
        get => GetValue(VListBackgroundProperty);
        set => SetValue(VListBackgroundProperty, value);
    }

    /// <inheritdoc cref="VListMaxHeightProperty" />
    public double VListMaxHeight
    {
        get => GetValue(VListMaxHeightProperty);
        set => SetValue(VListMaxHeightProperty, value);
    }

    /// <inheritdoc cref="VItemPaddingProperty" />
    public Thickness VItemPadding
    {
        get => GetValue(VItemPaddingProperty);
        set => SetValue(VItemPaddingProperty, value);
    }

    /// <inheritdoc cref="VPlaceholderForegroundProperty" />
    public IBrush? VPlaceholderForeground
    {
        get => GetValue(VPlaceholderForegroundProperty);
        set => SetValue(VPlaceholderForegroundProperty, value);
    }

    /// <inheritdoc cref="VListSpacingProperty" />
    public double VListSpacing
    {
        get => GetValue(VListSpacingProperty);
        set => SetValue(VListSpacingProperty, value);
    }

    /// <summary>Gets the items matching what has been typed. The template binds the list to this.</summary>
    public IEnumerable Matches => this.matches;

    /// <inheritdoc/>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (this.list != null)
        {
            this.list.SelectionChanged -= OnListSelectionChanged;
        }

        if (this.textBox != null)
        {
            this.textBox.GotFocus -= OnTextBoxGotFocus;
        }

        this.textBox = e.NameScope.Find<TextBox>("PART_TextBox");
        this.list = e.NameScope.Find<ListBox>("PART_List");

        if (this.textBox != null)
        {
            this.textBox.GotFocus += OnTextBoxGotFocus;
        }

        if (this.list != null)
        {
            this.list.SelectionChanged += OnListSelectionChanged;
        }

        ShowTextFor(SelectedItem);
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SearchTextProperty && !this.writingSearchText)
        {
            // A keystroke: anything previously chosen no longer describes what is in the box, so it
            // is dropped rather than left behind to be re-committed on blur.
            SelectedItem = null;
            Repopulate();
            IsListOpen = true;
        }
        else if (change.Property == ItemsSourceProperty)
        {
            Repopulate();
        }
        else if (change.Property == SelectedItemProperty && !IsListOpen)
        {
            // Set from outside rather than by a tap on the list — reflect it in the box.
            ShowTextFor(change.NewValue);
        }
    }

    /// <inheritdoc/>
    protected override void OnLostFocus(FocusChangedEventArgs e)
    {
        base.OnLostFocus(e);

        // Focus moving between the field and a row of the list is still inside this control, and
        // must not be mistaken for the user leaving it.
        if (this.IsKeyboardFocusWithin)
        {
            return;
        }

        IsListOpen = false;

        // Half-typed text describes nothing. Put back whatever is actually chosen, so the box never
        // shows a name that was not picked.
        ShowTextFor(SelectedItem);
    }

    private void OnTextBoxGotFocus(object? sender, FocusChangedEventArgs e)
    {
        // Clearing the text is what makes the whole list appear: with the chosen item's own name
        // sitting in the box, filtering on it would match only that one item.
        SetSearchText(string.Empty);
        Repopulate();
        IsListOpen = true;
    }

    private void OnListSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (this.list?.SelectedItem is not { } chosen)
        {
            return;
        }

        SelectedItem = chosen;
        IsListOpen = false;
        ShowTextFor(chosen);

        // Cleared so picking the same row again still raises a change, and so the highlight does
        // not linger on a list that is about to be rebuilt.
        this.list.SelectedItem = null;
    }

    /// <summary>Writes the box's text without it being taken for a keystroke.</summary>
    private void SetSearchText(string? text)
    {
        this.writingSearchText = true;
        try
        {
            SearchText = text;
        }
        finally
        {
            this.writingSearchText = false;
        }
    }

    private void ShowTextFor(object? item) => SetSearchText(item?.ToString() ?? string.Empty);

    /// <summary>Rebuilds the visible list from what has been typed.</summary>
    private void Repopulate()
    {
        this.matches.Clear();

        if (ItemsSource == null)
        {
            return;
        }

        var typed = SearchText;
        var all = ItemsSource.Cast<object>();

        var kept = string.IsNullOrWhiteSpace(typed)
            ? all
            : all.Where(item => (item?.ToString() ?? string.Empty)
                .Contains(typed, StringComparison.CurrentCultureIgnoreCase));

        foreach (var item in kept)
        {
            this.matches.Add(item);
        }
    }
}
