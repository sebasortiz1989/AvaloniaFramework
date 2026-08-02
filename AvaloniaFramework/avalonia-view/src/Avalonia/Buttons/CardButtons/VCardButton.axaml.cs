using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaFramework.Apresentacao.Utils;

namespace AvaloniaFramework.Apresentacao.Buttons.CardButtons;

public class VCardButton : Button
{
    public static readonly StyledProperty<double> VImageHeightProperty =
        AvaloniaProperty.Register<VCardButton, double>(nameof(VImageHeight), 90);

    public static readonly StyledProperty<double> VImageWidthProperty =
        AvaloniaProperty.Register<VCardButton, double>(nameof(VImageWidth), 90);

    public static readonly StyledProperty<HorizontalAlignment> VImageHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VCardButton, HorizontalAlignment>(nameof(VImageHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VImageVerticalAlignmentProperty =
        AvaloniaProperty.Register<VCardButton, VerticalAlignment>(nameof(VImageVerticalAlignment), VerticalAlignment.Bottom);

    public static readonly StyledProperty<Thickness> VImageMarginProperty =
        AvaloniaProperty.Register<VCardButton, Thickness>(nameof(VImageMargin), new Thickness(0));

    public static readonly StyledProperty<IImage?> VNormalImageProperty =
        AvaloniaProperty.Register<VCardButton, IImage?>(nameof(VNormalImage));

    public static readonly StyledProperty<IImage?> VPressedImageProperty =
        AvaloniaProperty.Register<VCardButton, IImage?>(nameof(VPressedImage));

    public static readonly StyledProperty<bool> VIsDivisionLineVisibleProperty =
        AvaloniaProperty.Register<VCardButton, bool>(nameof(VIsDivisionLineVisible), true);

    public static readonly StyledProperty<Thickness> VIconOneMarginProperty =
        AvaloniaProperty.Register<VCardButton, Thickness>(nameof(VIconOneMargin));

    public static readonly StyledProperty<HorizontalAlignment> VIconHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VCardButton, HorizontalAlignment>(nameof(VIconHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconVerticalAlignmentProperty =
        AvaloniaProperty.Register<VCardButton, VerticalAlignment>(nameof(VIconVerticalAlignment), VerticalAlignment.Bottom);

    public static readonly StyledProperty<Geometry?> VPressedIconProperty =
        AvaloniaProperty.Register<VCardButton, Geometry?>(nameof(VPressedIcon));

    public static readonly StyledProperty<Geometry?> VNormalIconProperty =
        AvaloniaProperty.Register<VCardButton, Geometry?>(nameof(VNormalIcon));

    public static readonly StyledProperty<Thickness> VPaddingProperty =
        AvaloniaProperty.Register<VCardButton, Thickness>(nameof(VPadding));

    public static readonly StyledProperty<IBrush> VPressedTextColorProperty =
        AvaloniaProperty.Register<VCardButton, IBrush>(nameof(VPressedTextColor), new SolidColorBrush(AvaloniaViewExtensions.HexToColor("#000000")));

    public static readonly StyledProperty<IBrush> VIconPressedColorProperty =
        AvaloniaProperty.Register<VCardButton, IBrush>(nameof(VIconPressedColor), new SolidColorBrush(AvaloniaViewExtensions.HexToColor("#000000")));

    public static readonly StyledProperty<IBrush> VIconNormalColorProperty =
        AvaloniaProperty.Register<VCardButton, IBrush>(nameof(VIconNormalColor), new SolidColorBrush(AvaloniaViewExtensions.HexToColor("#FFFFFF")));

    public static readonly StyledProperty<IBrush> VNormalTextColorProperty =
        AvaloniaProperty.Register<VCardButton, IBrush>(nameof(VNormalTextColor), new SolidColorBrush(AvaloniaViewExtensions.HexToColor("#FFFFFF")));

    public static readonly StyledProperty<IBrush> VNormalBorderBrushProperty =
        AvaloniaProperty.Register<VCardButton, IBrush>(nameof(VNormalBorderBrush), new SolidColorBrush(AvaloniaViewExtensions.HexToColor("#5DE4FF")));

    public static readonly StyledProperty<IBrush> VPressedBorderBrushProperty =
        AvaloniaProperty.Register<VCardButton, IBrush>(nameof(VPressedBorderBrush), new SolidColorBrush(AvaloniaViewExtensions.HexToColor("#000000")));

    public static readonly StyledProperty<IBrush> VNormalBackgroundProperty =
        AvaloniaProperty.Register<VCardButton, IBrush>(nameof(VNormalBackground), new SolidColorBrush(AvaloniaViewExtensions.HexToColor("#000000")));

    public static readonly StyledProperty<IBrush> VPressedBackgroundProperty =
        AvaloniaProperty.Register<VCardButton, IBrush>(nameof(VPressedBackground), new SolidColorBrush(AvaloniaViewExtensions.HexToColor("#43B6DB")));

    public static readonly DirectProperty<VCardButton, string?> VTextProperty =
    AvaloniaProperty.RegisterDirect<VCardButton, string?>(
        nameof(VText),
        o => o.VText,
        (o, v) => o.VText = v);

    public static readonly DirectProperty<VCardButton, FontWeight> VFontWeightProperty =
        AvaloniaProperty.RegisterDirect<VCardButton, FontWeight>(
            nameof(VFontWeight),
            o => o.VFontWeight,
            (o, v) => o.VFontWeight = v);

    public static readonly DirectProperty<VCardButton, double> VFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VCardButton, double>(
            nameof(VFontSize),
            o => o.VFontSize,
            (o, v) => o.VFontSize = v);

    public static readonly DirectProperty<VCardButton, double> VIconWidthProperty =
        AvaloniaProperty.RegisterDirect<VCardButton, double>(
            nameof(VIconWidth),
            o => o.VIconWidth,
            (o, v) => o.VIconWidth = v);

    public static readonly DirectProperty<VCardButton, double> VIconHeightProperty =
        AvaloniaProperty.RegisterDirect<VCardButton, double>(
            nameof(VIconHeight),
            o => o.VIconHeight,
            (o, v) => o.VIconHeight = v);

    private double vIconWidth = 100;

    private double vIconHeight = 100;

    private double vFontSize = 32;

    private FontWeight vFontWeight = FontWeight.Bold;

    private string? vText = string.Empty;

    public VCardButton()
    {
        CornerRadius = new CornerRadius(16);
        BorderThickness = new Thickness(3);
        Height = 230;
        Width = 250;
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

    public HorizontalAlignment VImageHorizontalAlignment
    {
        get => GetValue(VImageHorizontalAlignmentProperty);
        set => SetValue(VImageHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VImageVerticalAlignment
    {
        get => GetValue(VImageVerticalAlignmentProperty);
        set => SetValue(VImageVerticalAlignmentProperty, value);
    }

    public Thickness VImageMargin
    {
        get => GetValue(VImageMarginProperty);
        set => SetValue(VImageMarginProperty, value);
    }

    public IImage? VNormalImage
    {
        get => GetValue(VNormalImageProperty);
        set => SetValue(VNormalImageProperty, value);
    }

    public IImage? VPressedImage
    {
        get => GetValue(VPressedImageProperty);
        set => SetValue(VPressedImageProperty, value);
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

    public IBrush VIconPressedColor
    {
        get => GetValue(VIconPressedColorProperty);
        set => SetValue(VIconPressedColorProperty, value);
    }

    public bool VIsDivisionLineVisible
    {
        get => GetValue(VIsDivisionLineVisibleProperty);
        set => SetValue(VIsDivisionLineVisibleProperty, value);
    }

    public IBrush VIconNormalColor
    {
        get => GetValue(VIconNormalColorProperty);
        set => SetValue(VIconNormalColorProperty, value);
    }

    public double VIconHeight
    {
        get => vIconHeight;
        set => SetAndRaise(VIconHeightProperty, ref vIconHeight, value);
    }

    public double VIconWidth
    {
        get => vIconWidth;
        set => SetAndRaise(VIconWidthProperty, ref vIconWidth, value);
    }

    public Thickness VIconOneMargin
    {
        get => GetValue(VIconOneMarginProperty);
        set => SetValue(VIconOneMarginProperty, value);
    }

    public HorizontalAlignment VIconHorizontalAlignment
    {
        get => GetValue(VIconHorizontalAlignmentProperty);
        set => SetValue(VIconHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VIconVerticalAlignment
    {
        get => GetValue(VIconVerticalAlignmentProperty);
        set => SetValue(VIconVerticalAlignmentProperty, value);
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
}