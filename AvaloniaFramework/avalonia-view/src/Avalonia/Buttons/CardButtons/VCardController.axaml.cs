using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaFramework.Apresentacao.Converters;

namespace AvaloniaFramework.Apresentacao.Buttons.CardButtons;

public class VCardController : Button
{
    public static readonly StyledProperty<IBrush> VIconPressedForegroundProperty =
        AvaloniaProperty.Register<VCardController, IBrush>(nameof(VIconPressedForeground));

    public static readonly StyledProperty<IBrush> VIconNormalForegroundProperty =
        AvaloniaProperty.Register<VCardController, IBrush>(nameof(VIconNormalForeground));

    public static readonly StyledProperty<Thickness> VIconOneMarginProperty =
        AvaloniaProperty.Register<VCardController, Thickness>(nameof(VIconOneMargin), defaultValue: new Thickness(0));

    public static readonly StyledProperty<HorizontalAlignment> VIconOneHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VCardController, HorizontalAlignment>(nameof(VIconOneHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconOneVerticalAlignmentProperty =
        AvaloniaProperty.Register<VCardController, VerticalAlignment>(nameof(VIconOneVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<Geometry?> VPressedIconProperty =
        AvaloniaProperty.Register<VCardController, Geometry?>(nameof(VPressedIcon));

    public static readonly StyledProperty<Geometry?> VNormalIconProperty =
        AvaloniaProperty.Register<VCardController, Geometry?>(nameof(VNormalIcon));

    public static readonly StyledProperty<Thickness> VPaddingProperty =
        AvaloniaProperty.Register<VCardController, Thickness>(nameof(VPadding), new Thickness(0));

    public static readonly StyledProperty<IBrush> VPressedTextColorProperty =
        AvaloniaProperty.Register<VCardController, IBrush>(nameof(VPressedTextColor));

    public static readonly StyledProperty<IBrush> VNormalTextColorProperty =
        AvaloniaProperty.Register<VCardController, IBrush>(nameof(VNormalTextColor));

    public static readonly StyledProperty<IBrush> VNormalBorderBrushProperty =
        AvaloniaProperty.Register<VCardController, IBrush>(nameof(VNormalBorderBrush));

    public static readonly StyledProperty<IBrush> VPressedBorderBrushProperty =
        AvaloniaProperty.Register<VCardController, IBrush>(nameof(VNormalBorderBrush));

    public static readonly StyledProperty<IBrush> VNormalBackgroundProperty =
        AvaloniaProperty.Register<VCardController, IBrush>(nameof(VNormalBackground));

    public static readonly StyledProperty<IBrush> VPressedBackgroundProperty =
        AvaloniaProperty.Register<VCardController, IBrush>(nameof(VPressedBackground));

    public static readonly StyledProperty<double> VImageHeightProperty =
        AvaloniaProperty.Register<VCardController, double>(nameof(VImageHeight), defaultValue: 50);

    public static readonly StyledProperty<double> VImageWidthProperty =
        AvaloniaProperty.Register<VCardController, double>(nameof(VImageWidth), defaultValue: 50);

    public static readonly DirectProperty<VCardController, string?> VTextProperty =
    AvaloniaProperty.RegisterDirect<VCardController, string?>(
        nameof(VText),
        o => o.VText,
        (o, v) => o.VText = v);

    public static readonly DirectProperty<VCardController, FontWeight> VFontWeightProperty =
        AvaloniaProperty.RegisterDirect<VCardController, FontWeight>(
            nameof(VFontWeight),
            o => o.VFontWeight,
            (o, v) => o.VFontWeight = v);

    public static readonly DirectProperty<VCardController, double> VFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VCardController, double>(
            nameof(VFontSize),
            o => o.VFontSize,
            (o, v) => o.VFontSize = v);

    public static readonly DirectProperty<VCardController, double> VIconOneWidthProperty =
        AvaloniaProperty.RegisterDirect<VCardController, double>(
            nameof(VIconOneWidth),
            o => o.VIconOneWidth,
            (o, v) => o.VIconOneWidth = v);

    public static readonly DirectProperty<VCardController, double> VIconOneHeightProperty =
        AvaloniaProperty.RegisterDirect<VCardController, double>(
            nameof(VIconOneHeight),
            o => o.VIconOneHeight,
            (o, v) => o.VIconOneHeight = v);

    public static readonly StyledProperty<IImage> VPressedImageProperty =
        AvaloniaProperty.Register<VCardController, IImage>(nameof(VPressedImage));

    public static readonly StyledProperty<IImage> VNormalImageProperty =
        AvaloniaProperty.Register<VCardController, IImage>(nameof(VNormalImage));

    private double vIconOneWidth = 40;

    private double vIconOneHeight = 40;

    private double vFontSize = 16;

    private FontWeight vFontWeight = FontWeight.Bold;

    private string? vText = string.Empty;

    public VCardController()
    {
        Width = 180;
        CornerRadius = new CornerRadius(30);
        CornerRadius = new CornerRadius(30);
        Height = 200;
        VIconNormalForeground = new SolidColorBrush(VerionAvaloniaConverters.HexToArgb("#5DE4FF"));
        VIconPressedForeground = new SolidColorBrush(VerionAvaloniaConverters.HexToArgb("#002459"));
        VNormalBackground = new SolidColorBrush(VerionAvaloniaConverters.HexToArgb("#FF040404"));
        VPressedBackground = new SolidColorBrush(VerionAvaloniaConverters.HexToArgb("#5DE4FF"));
        VNormalTextColor = Brushes.White;
        VPressedTextColor = new SolidColorBrush(VerionAvaloniaConverters.HexToArgb("#002459"));
        VImageWidth = 50;
        VImageHeight = 50;
        VText = "Test";
    }

    public IBrush VPressedBackground
    {
        get => GetValue(VPressedBackgroundProperty);
        set => SetValue(VPressedBackgroundProperty, value);
    }

    public IBrush VNormalBackground
    {
        get => GetValue(VNormalBackgroundProperty);
        set => SetValue(VNormalBackgroundProperty, value);
    }

    public Thickness VPadding
    {
        get => GetValue(VPaddingProperty);
        set => SetValue(VPaddingProperty, value);
    }

    public IBrush VPressedTextColor
    {
        get => GetValue(VPressedTextColorProperty);
        set => SetValue(VPressedTextColorProperty, value);
    }

    public IBrush VNormalTextColor
    {
        get => GetValue(VNormalTextColorProperty);
        set => SetValue(VNormalTextColorProperty, value);
    }

    public IBrush VNormalBorderBrush
    {
        get => GetValue(VNormalBorderBrushProperty);
        set => SetValue(VNormalBorderBrushProperty, value);
    }

    public IBrush VPressedBorderBrush
    {
        get => GetValue(VPressedBorderBrushProperty);
        set => SetValue(VPressedBorderBrushProperty, value);
    }

    public string? VText
    {
        get => vText;
        set => SetAndRaise(VTextProperty, ref vText, value);
    }

    public FontWeight VFontWeight
    {
        get => vFontWeight;
        set => SetAndRaise(VFontWeightProperty, ref vFontWeight, value);
    }

    public double VFontSize
    {
        get => vFontSize;
        set => SetAndRaise(VFontSizeProperty, ref vFontSize, value);
    }

    public IBrush VIconPressedForeground
    {
        get => GetValue(VIconPressedForegroundProperty);
        set => SetValue(VIconPressedForegroundProperty, value);
    }

    public IBrush VIconNormalForeground
    {
        get => GetValue(VIconNormalForegroundProperty);
        set => SetValue(VIconNormalForegroundProperty, value);
    }

    public double VIconOneHeight
    {
        get => vIconOneHeight;
        set => SetAndRaise(VIconOneHeightProperty, ref vIconOneHeight, value);
    }

    public double VIconOneWidth
    {
        get => vIconOneWidth;
        set => SetAndRaise(VIconOneWidthProperty, ref vIconOneWidth, value);
    }

    public double VImageHeight
    {
        get => GetValue(VImageHeightProperty);
        set => SetValue(VImageHeightProperty, value);
    }

    public double VImageWidth
    {
        get => GetValue(VImageWidthProperty);
        set => SetValue(VImageWidthProperty, value);
    }

    public Thickness VIconOneMargin
    {
        get => GetValue(VIconOneMarginProperty);
        set => SetValue(VIconOneMarginProperty, value);
    }

    public HorizontalAlignment VIconOneHorizontalAlignment
    {
        get => GetValue(VIconOneHorizontalAlignmentProperty);
        set => SetValue(VIconOneHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VIconOneVerticalAlignment
    {
        get => GetValue(VIconOneVerticalAlignmentProperty);
        set => SetValue(VIconOneVerticalAlignmentProperty, value);
    }

    public Geometry? VPressedIcon
    {
        get => GetValue(VPressedIconProperty);
        set => SetValue(VPressedIconProperty, value);
    }

    public Geometry? VNormalIcon
    {
        get => GetValue(VNormalIconProperty);
        set => SetValue(VNormalIconProperty, value);
    }

    public IImage VPressedImage
    {
        get => GetValue(VPressedImageProperty);
        set => SetValue(VPressedImageProperty, value);
    }

    public IImage VNormalImage
    {
        get => GetValue(VNormalImageProperty);
        set => SetValue(VNormalImageProperty, value);
    }
}