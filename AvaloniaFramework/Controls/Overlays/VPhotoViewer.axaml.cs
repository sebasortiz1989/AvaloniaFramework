using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaFramework.Hosting;
using System.Windows.Input;

namespace AvaloniaFramework.Controls.Overlays;

/// <summary>
/// One photo at the resolution it was stored at, over a scrim covering the screen that hosts it.
/// Place it as the last child of a screen's root Grid.
/// </summary>
/// <remarks>
/// <para>
/// A list or detail screen decodes photos down to the size it draws them at, which is what keeps it
/// scrolling. This is the one place that pays for the full decode, and it only pays while it is
/// open — see <see cref="ActivePath"/>.
/// </para>
/// <para>
/// Appearance arrives through the <c>V*</c> properties, in the manner of
/// <see cref="Buttons.VButton"/>, so an app declares the whole look as one style:
/// <code>
/// &lt;Style Selector="overlays|VPhotoViewer"&gt;
///     &lt;Setter Property="VScrim" Value="{DynamicResource Scrim}" /&gt;
///     &lt;Setter Property="VSurface" Value="{DynamicResource SurfaceRaised}" /&gt;
/// &lt;/Style&gt;
/// </code>
/// The defaults are deliberately plain rather than pretty: a control that renders legibly with no
/// styling at all is one an app can drop in and judge before committing to it.
/// </para>
/// <para>
/// A UserControl rather than a TemplatedControl. The visual tree here is a composed overlay, not a
/// primitive with states worth re-templating, and every part of it that an app would want to change
/// is already a property. Reach for <c>ControlTheme</c> when the arrangement itself is the thing in
/// question.
/// </para>
/// </remarks>
public partial class VPhotoViewer : UserControl
{
    /// <summary>Whether the photo covers its host screen.</summary>
    public static readonly StyledProperty<bool> IsOpenProperty =
        AvaloniaProperty.Register<VPhotoViewer, bool>(nameof(IsOpen));

    /// <summary>The absolute path of the photo to show.</summary>
    public static readonly StyledProperty<string?> PathProperty =
        AvaloniaProperty.Register<VPhotoViewer, string?>(nameof(Path));

    /// <summary>What dismissing the photo runs.</summary>
    public static readonly StyledProperty<ICommand?> CloseCommandProperty =
        AvaloniaProperty.Register<VPhotoViewer, ICommand?>(nameof(CloseCommand));

    /// <summary>The wash covering the screen behind the photo.</summary>
    public static readonly StyledProperty<IBrush?> VScrimProperty =
        AvaloniaProperty.Register<VPhotoViewer, IBrush?>(nameof(VScrim), new SolidColorBrush(Color.FromArgb(0xA6, 0, 0, 0)));

    /// <summary>The fill of the close button and the caption, both of which sit on the scrim.</summary>
    public static readonly StyledProperty<IBrush?> VSurfaceProperty =
        AvaloniaProperty.Register<VPhotoViewer, IBrush?>(nameof(VSurface), Brushes.White);

    /// <summary>The hairline edge around those two.</summary>
    public static readonly StyledProperty<IBrush?> VStrokeProperty =
        AvaloniaProperty.Register<VPhotoViewer, IBrush?>(nameof(VStroke), Brushes.Gainsboro);

    /// <summary>The colour of the cross and the caption's text.</summary>
    public static readonly StyledProperty<IBrush?> VInkProperty =
        AvaloniaProperty.Register<VPhotoViewer, IBrush?>(nameof(VInk), Brushes.Black);

    /// <summary>The dismiss glyph, stroked rather than filled.</summary>
    public static readonly StyledProperty<Geometry?> VCloseIconProperty =
        AvaloniaProperty.Register<VPhotoViewer, Geometry?>(nameof(VCloseIcon), StreamGeometry.Parse("M18 6L6 18 M6 6l12 12"));

    /// <summary>The caption under the photo. Empty hides it.</summary>
    public static readonly StyledProperty<string?> VHintProperty =
        AvaloniaProperty.Register<VPhotoViewer, string?>(nameof(VHint));

    /// <summary>The caption's face.</summary>
    public static readonly StyledProperty<FontFamily> VFontFamilyProperty =
        AvaloniaProperty.Register<VPhotoViewer, FontFamily>(nameof(VFontFamily), FontFamily.Default);

    /// <summary>The caption's size.</summary>
    public static readonly StyledProperty<double> VHintFontSizeProperty =
        AvaloniaProperty.Register<VPhotoViewer, double>(nameof(VHintFontSize), 14d);

    /// <summary>The space kept clear around the photo.</summary>
    public static readonly StyledProperty<Thickness> VPhotoMarginProperty =
        AvaloniaProperty.Register<VPhotoViewer, Thickness>(nameof(VPhotoMargin), new Thickness(35, 126, 35, 35));

    private static readonly StyledProperty<string?> ActivePathProperty =
        AvaloniaProperty.Register<VPhotoViewer, string?>(nameof(ActivePath));

    /// <summary>Initializes a new instance of the <see cref="VPhotoViewer"/> class.</summary>
    public VPhotoViewer()
    {
        InitializeComponent();
    }

    /// <summary>Gets or sets a value indicating whether the photo covers its host screen.</summary>
    public bool IsOpen
    {
        get => GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>Gets or sets the absolute path of the photo to show.</summary>
    public string? Path
    {
        get => GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }

    /// <summary>Gets or sets what dismissing the photo runs. Expected to close the viewer.</summary>
    public ICommand? CloseCommand
    {
        get => GetValue(CloseCommandProperty);
        set => SetValue(CloseCommandProperty, value);
    }

    /// <summary>Gets or sets the wash covering the screen behind the photo.</summary>
    public IBrush? VScrim
    {
        get => GetValue(VScrimProperty);
        set => SetValue(VScrimProperty, value);
    }

    /// <summary>Gets or sets the fill of the close button and the caption.</summary>
    public IBrush? VSurface
    {
        get => GetValue(VSurfaceProperty);
        set => SetValue(VSurfaceProperty, value);
    }

    /// <summary>Gets or sets the hairline edge around the close button and the caption.</summary>
    public IBrush? VStroke
    {
        get => GetValue(VStrokeProperty);
        set => SetValue(VStrokeProperty, value);
    }

    /// <summary>Gets or sets the colour of the cross and the caption's text.</summary>
    public IBrush? VInk
    {
        get => GetValue(VInkProperty);
        set => SetValue(VInkProperty, value);
    }

    /// <summary>Gets or sets the dismiss glyph.</summary>
    public Geometry? VCloseIcon
    {
        get => GetValue(VCloseIconProperty);
        set => SetValue(VCloseIconProperty, value);
    }

    /// <summary>Gets or sets the caption under the photo. Empty or null hides it.</summary>
    public string? VHint
    {
        get => GetValue(VHintProperty);
        set => SetValue(VHintProperty, value);
    }

    /// <summary>Gets or sets the caption's face.</summary>
    public FontFamily VFontFamily
    {
        get => GetValue(VFontFamilyProperty);
        set => SetValue(VFontFamilyProperty, value);
    }

    /// <summary>Gets or sets the caption's size.</summary>
    public double VHintFontSize
    {
        get => GetValue(VHintFontSizeProperty);
        set => SetValue(VHintFontSizeProperty, value);
    }

    /// <summary>Gets or sets the space kept clear around the photo.</summary>
    public Thickness VPhotoMargin
    {
        get => GetValue(VPhotoMarginProperty);
        set => SetValue(VPhotoMarginProperty, value);
    }

    /// <summary>
    /// Gets the path the image actually loads from: <see cref="Path"/> while open, null otherwise.
    /// </summary>
    /// <remarks>
    /// Binding the image straight to <see cref="Path"/> would decode a full-resolution photo as
    /// soon as the host screen loaded, whether or not the viewer was ever opened — which is the
    /// cost this control exists to confine. <see cref="Visual.IsVisible"/> does not help: a hidden
    /// control still evaluates its bindings.
    /// </remarks>
    public string? ActivePath => GetValue(ActivePathProperty);

    /// <inheritdoc/>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == IsOpenProperty || change.Property == PathProperty)
        {
            SetValue(ActivePathProperty, IsOpen ? Path : null);
        }

        if (change.Property == IsOpenProperty)
        {
            ScreenOverlay.Current.Set(this, IsOpen);
        }
    }

    /// <inheritdoc/>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        ScreenOverlay.Current.Set(this, IsOpen);
    }

    /// <inheritdoc/>
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        ScreenOverlay.Current.Set(this, false);
    }
}