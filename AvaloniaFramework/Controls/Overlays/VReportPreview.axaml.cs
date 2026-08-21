using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaFramework.Hosting;
using System.Windows.Input;

namespace AvaloniaFramework.Controls.Overlays;

/// <summary>
/// A rendered document over a scrim, with a share action and a save action beneath it. Place it as
/// the last child of a screen's root Grid.
/// </summary>
/// <remarks>
/// <para>
/// The order is the one a phone uses for a screenshot: show the result first, then offer what to do
/// with it. Sharing needs nothing saved, so the common errand costs the user no file they have to
/// clean up later.
/// </para>
/// <para>
/// Both captions are properties with no default, because a library cannot know the app's language.
/// A caption left unset renders an empty button, which is a loud enough mistake to catch on first
/// run — louder than an English word appearing in a Portuguese app.
/// </para>
/// <para>
/// Appearance arrives through the <c>V*</c> properties, in the manner of
/// <see cref="Buttons.VButton"/>. See <see cref="VPhotoViewer"/> for the same arrangement and the
/// reason this is a UserControl rather than a TemplatedControl.
/// </para>
/// </remarks>
public partial class VReportPreview : UserControl
{
    /// <summary>Whether the preview covers its host screen.</summary>
    public static readonly StyledProperty<bool> IsOpenProperty =
        AvaloniaProperty.Register<VReportPreview, bool>(nameof(IsOpen));

    /// <summary>The rendered image's path.</summary>
    public static readonly StyledProperty<string?> ImagePathProperty =
        AvaloniaProperty.Register<VReportPreview, string?>(nameof(ImagePath));

    /// <summary>Whether the platform offers a share sheet.</summary>
    public static readonly StyledProperty<bool> CanShareProperty =
        AvaloniaProperty.Register<VReportPreview, bool>(nameof(CanShare));

    /// <summary>What happened to the document, shown above the actions.</summary>
    public static readonly StyledProperty<string> MessageProperty =
        AvaloniaProperty.Register<VReportPreview, string>(nameof(Message), string.Empty);

    /// <summary>Whether there is a message to show.</summary>
    public static readonly StyledProperty<bool> HasMessageProperty =
        AvaloniaProperty.Register<VReportPreview, bool>(nameof(HasMessage));

    /// <summary>What the share action runs.</summary>
    public static readonly StyledProperty<ICommand?> ShareCommandProperty =
        AvaloniaProperty.Register<VReportPreview, ICommand?>(nameof(ShareCommand));

    /// <summary>What the save action runs.</summary>
    public static readonly StyledProperty<ICommand?> SaveCommandProperty =
        AvaloniaProperty.Register<VReportPreview, ICommand?>(nameof(SaveCommand));

    /// <summary>What dismissing runs.</summary>
    public static readonly StyledProperty<ICommand?> CloseCommandProperty =
        AvaloniaProperty.Register<VReportPreview, ICommand?>(nameof(CloseCommand));

    /// <summary>The share action's caption.</summary>
    public static readonly StyledProperty<string> VShareTextProperty =
        AvaloniaProperty.Register<VReportPreview, string>(nameof(VShareText), string.Empty);

    /// <summary>The save action's caption.</summary>
    public static readonly StyledProperty<string> VSaveTextProperty =
        AvaloniaProperty.Register<VReportPreview, string>(nameof(VSaveText), string.Empty);

    /// <summary>The wash covering the screen behind the document.</summary>
    public static readonly StyledProperty<IBrush?> VScrimProperty =
        AvaloniaProperty.Register<VReportPreview, IBrush?>(nameof(VScrim), new SolidColorBrush(Color.FromArgb(0xA6, 0, 0, 0)));

    /// <summary>The fill behind the document and under the actions.</summary>
    public static readonly StyledProperty<IBrush?> VSurfaceProperty =
        AvaloniaProperty.Register<VReportPreview, IBrush?>(nameof(VSurface), Brushes.White);

    /// <summary>The hairline edge around the close button and the action group.</summary>
    public static readonly StyledProperty<IBrush?> VStrokeProperty =
        AvaloniaProperty.Register<VReportPreview, IBrush?>(nameof(VStroke), Brushes.Gainsboro);

    /// <summary>The separator between the two actions.</summary>
    public static readonly StyledProperty<IBrush?> VRuleProperty =
        AvaloniaProperty.Register<VReportPreview, IBrush?>(nameof(VRule), Brushes.Gainsboro);

    /// <summary>The colour of the cross, the message and both captions.</summary>
    public static readonly StyledProperty<IBrush?> VInkProperty =
        AvaloniaProperty.Register<VReportPreview, IBrush?>(nameof(VInk), Brushes.Black);

    /// <summary>The dismiss glyph, stroked rather than filled.</summary>
    public static readonly StyledProperty<Geometry?> VCloseIconProperty =
        AvaloniaProperty.Register<VReportPreview, Geometry?>(nameof(VCloseIcon), StreamGeometry.Parse("M18 6L6 18 M6 6l12 12"));

    /// <summary>The face used by the message and both captions.</summary>
    public static readonly StyledProperty<FontFamily> VFontFamilyProperty =
        AvaloniaProperty.Register<VReportPreview, FontFamily>(nameof(VFontFamily), FontFamily.Default);

    /// <summary>The message's size.</summary>
    public static readonly StyledProperty<double> VMessageFontSizeProperty =
        AvaloniaProperty.Register<VReportPreview, double>(nameof(VMessageFontSize), 12d);

    /// <summary>Both captions' size.</summary>
    public static readonly StyledProperty<double> VActionFontSizeProperty =
        AvaloniaProperty.Register<VReportPreview, double>(nameof(VActionFontSize), 15d);

    /// <summary>Initializes a new instance of the <see cref="VReportPreview"/> class.</summary>
    public VReportPreview()
    {
        InitializeComponent();
    }

    /// <summary>Gets or sets a value indicating whether the preview covers its host screen.</summary>
    public bool IsOpen
    {
        get => GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>Gets or sets the rendered image's path.</summary>
    public string? ImagePath
    {
        get => GetValue(ImagePathProperty);
        set => SetValue(ImagePathProperty, value);
    }

    /// <summary>Gets or sets a value indicating whether the platform offers a share sheet.</summary>
    public bool CanShare
    {
        get => GetValue(CanShareProperty);
        set => SetValue(CanShareProperty, value);
    }

    /// <summary>Gets or sets what happened to the document, shown above the actions.</summary>
    public string Message
    {
        get => GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    /// <summary>Gets or sets a value indicating whether there is a message to show.</summary>
    public bool HasMessage
    {
        get => GetValue(HasMessageProperty);
        set => SetValue(HasMessageProperty, value);
    }

    /// <summary>Gets or sets what the share action runs.</summary>
    public ICommand? ShareCommand
    {
        get => GetValue(ShareCommandProperty);
        set => SetValue(ShareCommandProperty, value);
    }

    /// <summary>Gets or sets what the save action runs.</summary>
    public ICommand? SaveCommand
    {
        get => GetValue(SaveCommandProperty);
        set => SetValue(SaveCommandProperty, value);
    }

    /// <summary>Gets or sets what dismissing runs. Expected to close the preview.</summary>
    public ICommand? CloseCommand
    {
        get => GetValue(CloseCommandProperty);
        set => SetValue(CloseCommandProperty, value);
    }

    /// <summary>Gets or sets the share action's caption.</summary>
    public string VShareText
    {
        get => GetValue(VShareTextProperty);
        set => SetValue(VShareTextProperty, value);
    }

    /// <summary>Gets or sets the save action's caption.</summary>
    public string VSaveText
    {
        get => GetValue(VSaveTextProperty);
        set => SetValue(VSaveTextProperty, value);
    }

    /// <summary>Gets or sets the wash covering the screen behind the document.</summary>
    public IBrush? VScrim
    {
        get => GetValue(VScrimProperty);
        set => SetValue(VScrimProperty, value);
    }

    /// <summary>Gets or sets the fill behind the document and under the actions.</summary>
    public IBrush? VSurface
    {
        get => GetValue(VSurfaceProperty);
        set => SetValue(VSurfaceProperty, value);
    }

    /// <summary>Gets or sets the hairline edge around the close button and the action group.</summary>
    public IBrush? VStroke
    {
        get => GetValue(VStrokeProperty);
        set => SetValue(VStrokeProperty, value);
    }

    /// <summary>Gets or sets the separator between the two actions.</summary>
    public IBrush? VRule
    {
        get => GetValue(VRuleProperty);
        set => SetValue(VRuleProperty, value);
    }

    /// <summary>Gets or sets the colour of the cross, the message and both captions.</summary>
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

    /// <summary>Gets or sets the face used by the message and both captions.</summary>
    public FontFamily VFontFamily
    {
        get => GetValue(VFontFamilyProperty);
        set => SetValue(VFontFamilyProperty, value);
    }

    /// <summary>Gets or sets the message's size.</summary>
    public double VMessageFontSize
    {
        get => GetValue(VMessageFontSizeProperty);
        set => SetValue(VMessageFontSizeProperty, value);
    }

    /// <summary>Gets or sets both captions' size.</summary>
    public double VActionFontSize
    {
        get => GetValue(VActionFontSizeProperty);
        set => SetValue(VActionFontSizeProperty, value);
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

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