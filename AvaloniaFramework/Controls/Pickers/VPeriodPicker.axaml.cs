using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaFramework.Presentation;

namespace AvaloniaFramework.Controls.Pickers;

/// <summary>
/// The inline month/year picker driven by a <see cref="PeriodPicker"/>. Place it directly under the
/// screen's period bar and bind <see cref="Picker"/> to that screen's picker.
/// </summary>
/// <remarks>
/// <para>
/// It hides its own content when the picker is closed, so a host only has to decide whether a
/// period control belongs on this screen at all. A host that does decide that binds
/// <c>IsVisible</c> on the instance — see the note in the markup about why the two conditions live
/// on different objects.
/// </para>
/// <para>
/// Appearance arrives through the <c>V*</c> properties, in the manner of
/// <see cref="Buttons.VButton"/>, so an app declares the whole look as one style.
/// </para>
/// </remarks>
public partial class VPeriodPicker : UserControl
{
    /// <summary>The screen's picker: its open state, its year and its cells.</summary>
    public static readonly StyledProperty<PeriodPicker?> PickerProperty =
        AvaloniaProperty.Register<VPeriodPicker, PeriodPicker?>(nameof(Picker));

    /// <summary>The fill of the panel and of an unselected cell.</summary>
    public static readonly StyledProperty<IBrush?> VSurfaceProperty =
        AvaloniaProperty.Register<VPeriodPicker, IBrush?>(nameof(VSurface), Brushes.White);

    /// <summary>The separator between the year row and the cells.</summary>
    public static readonly StyledProperty<IBrush?> VRuleProperty =
        AvaloniaProperty.Register<VPeriodPicker, IBrush?>(nameof(VRule), Brushes.Gainsboro);

    /// <summary>The colour of the year, the arrows and an unselected cell.</summary>
    public static readonly StyledProperty<IBrush?> VInkProperty =
        AvaloniaProperty.Register<VPeriodPicker, IBrush?>(nameof(VInk), Brushes.Black);

    /// <summary>The selected cell's text.</summary>
    public static readonly StyledProperty<IBrush?> VAccentProperty =
        AvaloniaProperty.Register<VPeriodPicker, IBrush?>(nameof(VAccent), Brushes.SteelBlue);

    /// <summary>The selected cell's fill.</summary>
    public static readonly StyledProperty<IBrush?> VAccentTintProperty =
        AvaloniaProperty.Register<VPeriodPicker, IBrush?>(nameof(VAccentTint), Brushes.AliceBlue);

    /// <summary>The glyph stepping back one year.</summary>
    public static readonly StyledProperty<Geometry?> VPreviousIconProperty =
        AvaloniaProperty.Register<VPeriodPicker, Geometry?>(nameof(VPreviousIcon), StreamGeometry.Parse("M15 5l-7 7 7 7"));

    /// <summary>The glyph stepping forward one year.</summary>
    public static readonly StyledProperty<Geometry?> VNextIconProperty =
        AvaloniaProperty.Register<VPeriodPicker, Geometry?>(nameof(VNextIcon), StreamGeometry.Parse("M9 5l7 7-7 7"));

    /// <summary>The face used by the year and the cells.</summary>
    public static readonly StyledProperty<FontFamily> VFontFamilyProperty =
        AvaloniaProperty.Register<VPeriodPicker, FontFamily>(nameof(VFontFamily), FontFamily.Default);

    /// <summary>The year's size.</summary>
    public static readonly StyledProperty<double> VYearFontSizeProperty =
        AvaloniaProperty.Register<VPeriodPicker, double>(nameof(VYearFontSize), 15d);

    /// <summary>A cell's size.</summary>
    public static readonly StyledProperty<double> VCellFontSizeProperty =
        AvaloniaProperty.Register<VPeriodPicker, double>(nameof(VCellFontSize), 14d);

    /// <summary>
    /// The space around the expanded panel.
    /// </summary>
    /// <remarks>
    /// It sits on the panel rather than on this control, so that a closed picker takes no room at
    /// all. A margin on the control itself would survive the panel collapsing and leave a gap in
    /// the host's stack with nothing in it.
    /// </remarks>
    public static readonly StyledProperty<Thickness> VContentMarginProperty =
        AvaloniaProperty.Register<VPeriodPicker, Thickness>(nameof(VContentMargin), new Thickness(35, 21, 35, 0));

    /// <summary>Initializes a new instance of the <see cref="VPeriodPicker"/> class.</summary>
    public VPeriodPicker()
    {
        InitializeComponent();
    }

    /// <summary>Gets or sets the screen's picker: its open state, its year and its cells.</summary>
    public PeriodPicker? Picker
    {
        get => GetValue(PickerProperty);
        set => SetValue(PickerProperty, value);
    }

    /// <summary>Gets or sets the fill of the panel and of an unselected cell.</summary>
    public IBrush? VSurface
    {
        get => GetValue(VSurfaceProperty);
        set => SetValue(VSurfaceProperty, value);
    }

    /// <summary>Gets or sets the separator between the year row and the cells.</summary>
    public IBrush? VRule
    {
        get => GetValue(VRuleProperty);
        set => SetValue(VRuleProperty, value);
    }

    /// <summary>Gets or sets the colour of the year, the arrows and an unselected cell.</summary>
    public IBrush? VInk
    {
        get => GetValue(VInkProperty);
        set => SetValue(VInkProperty, value);
    }

    /// <summary>Gets or sets the selected cell's text colour.</summary>
    public IBrush? VAccent
    {
        get => GetValue(VAccentProperty);
        set => SetValue(VAccentProperty, value);
    }

    /// <summary>Gets or sets the selected cell's fill.</summary>
    public IBrush? VAccentTint
    {
        get => GetValue(VAccentTintProperty);
        set => SetValue(VAccentTintProperty, value);
    }

    /// <summary>Gets or sets the glyph stepping back one year.</summary>
    public Geometry? VPreviousIcon
    {
        get => GetValue(VPreviousIconProperty);
        set => SetValue(VPreviousIconProperty, value);
    }

    /// <summary>Gets or sets the glyph stepping forward one year.</summary>
    public Geometry? VNextIcon
    {
        get => GetValue(VNextIconProperty);
        set => SetValue(VNextIconProperty, value);
    }

    /// <summary>Gets or sets the face used by the year and the cells.</summary>
    public FontFamily VFontFamily
    {
        get => GetValue(VFontFamilyProperty);
        set => SetValue(VFontFamilyProperty, value);
    }

    /// <summary>Gets or sets the year's size.</summary>
    public double VYearFontSize
    {
        get => GetValue(VYearFontSizeProperty);
        set => SetValue(VYearFontSizeProperty, value);
    }

    /// <summary>Gets or sets a cell's size.</summary>
    public double VCellFontSize
    {
        get => GetValue(VCellFontSizeProperty);
        set => SetValue(VCellFontSizeProperty, value);
    }

    /// <summary>Gets or sets the space around the expanded panel.</summary>
    public Thickness VContentMargin
    {
        get => GetValue(VContentMarginProperty);
        set => SetValue(VContentMarginProperty, value);
    }
}