using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using AvaloniaFramework.Hosting;
using System;
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
/// The document can be magnified, because a page laid out to be read on paper is not readable at
/// phone width: pinch to zoom, double tap to jump in and back out, drag to move around, and the
/// mouse wheel where there is one. It resets to fitted whenever a different document is shown, so
/// one report's magnification is never inherited by the next.
/// </para>
/// <para>
/// Appearance arrives through the <c>V*</c> properties, in the manner of
/// <see cref="Buttons.VButton"/>. See <see cref="VPhotoViewer"/> for the same arrangement and the
/// reason this is a UserControl rather than a TemplatedControl.
/// </para>
/// </remarks>
public partial class VReportPreview : UserControl
{
    /// <summary>
    /// The most the document may be magnified.
    /// </summary>
    /// <remarks>
    /// Eight times fitted width puts a report rendered at 1120 points well past the resolution it
    /// was drawn at, which is as far as magnifying can usefully go — beyond it the reader is
    /// looking at the renderer's own pixels.
    /// </remarks>
    private const double MaxZoom = 8;

    /// <summary>What a double tap jumps to, and back from.</summary>
    /// <remarks>
    /// Enough to read a table cell on a phone in one gesture. Pinching still reaches anything
    /// between this and <see cref="MaxZoom"/>; the tap is the shortcut for the common case.
    /// </remarks>
    private const double TapZoom = 3;

    /// <summary>How much one wheel notch magnifies by.</summary>
    private const double WheelStep = 1.2;

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

    /// <summary>
    /// The two halves of the image's render transform, mutated rather than replaced.
    /// </summary>
    /// <remarks>
    /// Scale first, then translate: the group applies its children in order, so the offset is in
    /// viewport units and does not itself grow with the magnification.
    /// </remarks>
    private readonly ScaleTransform magnification = new();

    private readonly TranslateTransform pan = new();

    /// <summary>How much the document is magnified. One is fitted to the viewport.</summary>
    private double zoom = 1;

    /// <summary>
    /// The magnification a pinch started from, or null when none is under way.
    /// </summary>
    /// <remarks>
    /// <see cref="PinchEventArgs.Scale"/> is measured against the distance the fingers started at,
    /// not against the previous event, so it has to be applied to the magnification as it was when
    /// the gesture began rather than compounded frame by frame.
    /// </remarks>
    private double? pinchedFrom;

    /// <summary>
    /// The pointer a drag is following, or null when nothing is dragging.
    /// </summary>
    /// <remarks>
    /// Held so a second finger can be told from the one already down. Only the first is ever
    /// captured — see <see cref="OnPointerPressed"/> for why capturing the second kills the pinch.
    /// </remarks>
    private IPointer? draggingPointer;

    /// <summary>Where a drag last was, in viewport coordinates, or null when nothing is dragging.</summary>
    private Point? draggingFrom;

    /// <summary>
    /// Whether the markup's controls exist yet.
    /// </summary>
    /// <remarks>
    /// A property change can reach this control before its own template has been built, and the
    /// magnification handling reads the image and the viewport by name. Cheaper and clearer than
    /// null-checking two fields that the generated code declares as non-nullable.
    /// </remarks>
    private bool ready;

    /// <summary>Initializes a new instance of the <see cref="VReportPreview"/> class.</summary>
    public VReportPreview()
    {
        InitializeComponent();

        // Top left, so the transform's arithmetic is in the image's own coordinates with no
        // half-size offsets threaded through it. The default is the centre.
        Page.RenderTransformOrigin = RelativePoint.TopLeft;
        Page.RenderTransform = new TransformGroup { Children = { magnification, pan } };

        // Added here rather than in the markup, so the gesture and the handler that answers it
        // are in one place. The recognizer reports touch and pen only, which is why the wheel and
        // the double tap are wired separately below.
        Viewport.GestureRecognizers.Add(new PinchGestureRecognizer());
        Viewport.Pinch += OnPinch;
        Viewport.PinchEnded += OnPinchEnded;

        Viewport.DoubleTapped += OnDoubleTapped;
        Viewport.PointerWheelChanged += OnWheel;
        Viewport.PointerPressed += OnPointerPressed;
        Viewport.PointerMoved += OnPointerMoved;
        Viewport.PointerReleased += OnPointerReleased;
        Viewport.PointerCaptureLost += OnPointerCaptureLost;

        // The fitted size is only known once the viewport has been given one, and it changes with
        // the window. Re-clamping keeps a magnified document from being left outside its own edges.
        Viewport.SizeChanged += (_, _) => ApplyTransform();
        Page.SizeChanged += (_, _) => ApplyTransform();

        ready = true;
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
        ArgumentNullException.ThrowIfNull(change);
        base.OnPropertyChanged(change);

        if (change.Property == IsOpenProperty)
        {
            ScreenOverlay.Current.Set(this, IsOpen);
        }

        // A different document, or this one being put away. Either way the next thing the user
        // sees has to start fitted: inheriting the last report's magnification would open the new
        // one somewhere in the middle of a page it has never seen.
        if (change.Property == ImagePathProperty || change.Property == IsOpenProperty)
        {
            ResetZoom();
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

    /// <summary>
    /// Holds one axis of the offset inside the viewport.
    /// </summary>
    /// <param name="offset">The offset asked for.</param>
    /// <param name="origin">Where the unmagnified image sits on this axis.</param>
    /// <param name="size">How long the magnified image is on this axis.</param>
    /// <param name="viewport">How long the viewport is on this axis.</param>
    /// <returns>The offset to use: centred when the image is the smaller of the two, otherwise as
    /// asked for but never past the image's own edge.</returns>
    private static double Clamp(double offset, double origin, double size, double viewport)
    {
        if (size <= viewport)
        {
            return ((viewport - size) / 2) - origin;
        }

        return Math.Clamp(offset, viewport - size - origin, -origin);
    }

    /// <summary>
    /// Magnifies about a fixed point in the viewport, so what is under the fingers stays there.
    /// </summary>
    /// <param name="target">The magnification wanted, before clamping.</param>
    /// <param name="anchor">The viewport point to hold still.</param>
    private void ZoomTo(double target, Point anchor)
    {
        var wanted = Math.Clamp(target, 1, MaxZoom);
        if (!ready || Math.Abs(wanted - zoom) < 0.0001)
        {
            return;
        }

        // Where the anchor sits in the image's own coordinates, which is what has to come back out
        // to the same place once the scale has changed.
        var bounds = Page.Bounds;
        var local = new Point(
            (anchor.X - bounds.X - pan.X) / zoom,
            (anchor.Y - bounds.Y - pan.Y) / zoom);

        zoom = wanted;
        pan.X = anchor.X - bounds.X - (local.X * zoom);
        pan.Y = anchor.Y - bounds.Y - (local.Y * zoom);
        ApplyTransform();
    }

    /// <summary>Puts the document back to fitted, with no offset.</summary>
    private void ResetZoom()
    {
        zoom = 1;
        pinchedFrom = null;
        EndDrag();
        pan.X = 0;
        pan.Y = 0;
        ApplyTransform();
    }

    /// <summary>
    /// Writes the current magnification and offset onto the image, keeping it inside its viewport.
    /// </summary>
    /// <remarks>
    /// Everything that changes either value ends here, so the clamping lives in one place: a
    /// document smaller than the viewport is centred in it, and a larger one may be moved only as
    /// far as its own edges. Without it a flick could throw the page off screen with nothing left
    /// to drag back.
    /// </remarks>
    private void ApplyTransform()
    {
        if (!ready)
        {
            return;
        }

        magnification.ScaleX = zoom;
        magnification.ScaleY = zoom;

        // Nothing has been laid out yet — the first SizeChanged will bring this straight back.
        var bounds = Page.Bounds;
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        pan.X = Clamp(pan.X, bounds.X, bounds.Width * zoom, Viewport.Bounds.Width);
        pan.Y = Clamp(pan.Y, bounds.Y, bounds.Height * zoom, Viewport.Bounds.Height);
    }

    /// <summary>Forgets the drag under way, leaving the magnification where it is.</summary>
    private void EndDrag()
    {
        draggingPointer = null;
        draggingFrom = null;
    }

    /// <summary>Pinching. Scale is measured from where the fingers started, not the last frame.</summary>
    private void OnPinch(object? sender, PinchEventArgs e)
    {
        // Two fingers down ends any drag that was under way; the recognizer has taken the pointers
        // and the drag would otherwise resume from a position that is now meaningless.
        EndDrag();

        pinchedFrom ??= zoom;
        ZoomTo(pinchedFrom.Value * e.Scale, e.ScaleOrigin);
        e.Handled = true;
    }

    /// <summary>The gesture is over, and so is anything it interrupted.</summary>
    /// <remarks>
    /// Two fingers that land and lift without moving produce no Pinch event at all, so this is the
    /// only place the drag the first of them started gets forgotten. Left behind, it reads to
    /// <see cref="OnPointerPressed"/> as a finger still down and refuses the next drag.
    /// </remarks>
    private void OnPinchEnded(object? sender, PinchEndedEventArgs e)
    {
        pinchedFrom = null;
        EndDrag();
    }

    /// <summary>Double tapping jumps in on what was tapped, and back out again.</summary>
    private void OnDoubleTapped(object? sender, TappedEventArgs e) =>
        ZoomTo(zoom > 1 ? 1 : TapZoom, e.GetPosition(Viewport));

    /// <summary>The mouse equivalent of a pinch, about the pointer.</summary>
    private void OnWheel(object? sender, PointerWheelEventArgs e)
    {
        ZoomTo(e.Delta.Y > 0 ? zoom * WheelStep : zoom / WheelStep, e.GetPosition(Viewport));
        e.Handled = true;
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        // Nothing to drag while the whole page is already on screen, and a pointer captured for
        // a pan that cannot move is one the double tap never gets to see.
        if (zoom <= 1)
        {
            return;
        }

        // A second finger down starts a pinch, not a second drag, and is left alone.
        if (draggingPointer != null)
        {
            return;
        }

        draggingPointer = e.Pointer;
        draggingFrom = e.GetPosition(Viewport);

        // The mouse only, and this is the whole reason pinching used to work exactly once.
        //
        // A touch pointer is already captured, implicitly, by the platform: TouchDevice captures
        // it to whatever was under the finger the moment it lands, so its moves reach this
        // control by bubbling and a drag needs nothing further. Capturing it again here took it
        // off the pinch recognizer, which grabs both contacts as the second finger arrives — and
        // this handler runs after it, gesture recognizers being driven from a class handler on
        // InputElement. The recognizer saw a capture lost, dropped the contact and raised
        // PinchEnded before producing a single Pinch event, leaving one finger driving a pan.
        //
        // It only ever showed from the second gesture onwards, because the first starts at fitted
        // where this handler returns above — so a reader could magnify a report once and then
        // never again, in either direction.
        //
        // A mouse has no implicit capture and needs this one, or a drag stops the moment the
        // pointer leaves the viewport. It also never reaches the recognizer, which answers to
        // touch and pen alone.
        if (e.Pointer.Type == PointerType.Mouse)
        {
            e.Pointer.Capture(Viewport);
        }
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (draggingFrom is not { } from || e.Pointer != draggingPointer)
        {
            return;
        }

        var to = e.GetPosition(Viewport);
        draggingFrom = to;
        pan.X += to.X - from.X;
        pan.Y += to.Y - from.Y;
        ApplyTransform();
    }

    /// <summary>
    /// Ends the drag, and only the drag this control started.
    /// </summary>
    /// <remarks>
    /// Releasing a capture it never took would clear somebody else's — the pinch recognizer's,
    /// while the other finger is still in the middle of a gesture.
    /// </remarks>
    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);

        if (e.Pointer != draggingPointer)
        {
            return;
        }

        EndDrag();

        // Only the capture this control took — see OnPointerPressed.
        if (e.Pointer.Type == PointerType.Mouse)
        {
            e.Pointer.Capture(null);
        }
    }

    /// <summary>
    /// The pinch recognizer taking the pointer, which is exactly when the drag gives way to it.
    /// </summary>
    private void OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);

        if (e.Pointer == draggingPointer)
        {
            EndDrag();
        }
    }
}
