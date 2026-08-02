using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class VCustomButton : Button
{
    public static readonly StyledProperty<IBrush> VNormalBackgroundProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VNormalBackground), defaultValue: Brushes.Black);

    public static readonly StyledProperty<IBrush> VPointeroverBackgroundProperty =
         AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VPointeroverBackground), defaultValue: Brushes.Aqua);

    public static readonly StyledProperty<IBrush> VPressedBackgroundProperty =
         AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VPressedBackground), defaultValue: Brushes.Teal);

    public static readonly StyledProperty<IBrush> VNormalBorderBrushProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VNormalBorderBrush), defaultValue: Brushes.Aqua);

    public static readonly StyledProperty<IBrush> VPressedBorderBrushProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VPressedBorderBrush), defaultValue: Brushes.Teal);

    public static readonly StyledProperty<IBrush> VPointeroverBorderBrushProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VPointeroverBorderBrush), defaultValue: Brushes.Aqua);

    public static readonly StyledProperty<IBrush> VNormalIconForegroundProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VNormalIconForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VPressedIconForegroundProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VPointeroverIconForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VPointeroverIconForegroundProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VPointeroverIconForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VIconNormalForegroundProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VIconNormalForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VPointeroverForegroundProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VPointeroverForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VIconPressedForegroundProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VIconPressedForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VNormalTextForegroundProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VNormalTextForeground));

    public static readonly StyledProperty<IBrush> VPointoverTextForegroundProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VNormalTextForeground));

    public static readonly StyledProperty<IBrush> VPressedTextForegroundProperty =
        AvaloniaProperty.Register<VCustomButton, IBrush>(nameof(VPressedTextForeground));

    public static readonly StyledProperty<Thickness> VTextMarginProperty =
        AvaloniaProperty.Register<VCustomButton, Thickness>(nameof(VTextMargin), defaultValue: default);

    public static readonly StyledProperty<HorizontalAlignment> VTextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VCustomButton, HorizontalAlignment>(nameof(VTextHorizontalAlignment), defaultValue: HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VTextVerticalAlignmentProperty =
        AvaloniaProperty.Register<VCustomButton, VerticalAlignment>(nameof(VTextVerticalAlignment), defaultValue: VerticalAlignment.Center);

    public static readonly StyledProperty<Thickness> VIconMarginProperty =
        AvaloniaProperty.Register<VCustomButton, Thickness>(nameof(VIconMargin), defaultValue: default);

    public static readonly StyledProperty<HorizontalAlignment> VIconHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VCustomButton, HorizontalAlignment>(nameof(VIconHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconVerticalAlignmentProperty =
        AvaloniaProperty.Register<VCustomButton, VerticalAlignment>(nameof(VIconVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<Geometry?> VNormalIconProperty =
        AvaloniaProperty.Register<VCustomButton, Geometry?>(nameof(VNormalIcon));

    public static readonly StyledProperty<Geometry?> VPressedIconProperty =
        AvaloniaProperty.Register<VCustomButton, Geometry?>(nameof(VPressedIcon));

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<VCustomButton, Thickness>(nameof(VBorderThickness), defaultValue: new Thickness(2));

    public static readonly StyledProperty<Thickness> VImageMarginProperty =
        AvaloniaProperty.Register<VCustomButton, Thickness>(nameof(VImageMargin));

    public static readonly StyledProperty<HorizontalAlignment> VImageHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VCustomButton, HorizontalAlignment>(nameof(VImageHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VImageVerticalAlignmentProperty =
        AvaloniaProperty.Register<VCustomButton, VerticalAlignment>(nameof(VImageVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<IImage?> VNormalImageProperty =
        AvaloniaProperty.Register<VCustomButton, IImage?>(nameof(VNormalImage));

    public static readonly StyledProperty<IImage?> VPressedImageProperty =
        AvaloniaProperty.Register<VCustomButton, IImage?>(nameof(VPressedImage));

    public static readonly DirectProperty<VCustomButton, double> VImageWidthProperty =
        AvaloniaProperty.RegisterDirect<VCustomButton, double>(
            nameof(VImageWidth),
            o => o.VImageWidth,
            (o, v) => o.VImageWidth = v);

    public static readonly DirectProperty<VCustomButton, double> VImageHeightProperty =
        AvaloniaProperty.RegisterDirect<VCustomButton, double>(
            nameof(VImageHeight),
            o => o.VImageHeight,
            (o, v) => o.VImageHeight = v);

    public static readonly DirectProperty<VCustomButton, VButtonEffects> VButtonEffectProperty =
        AvaloniaProperty.RegisterDirect<VCustomButton, VButtonEffects>(
            nameof(VButtonEffect),
            o => o.VButtonEffect,
            (o, v) => o.VButtonEffect = v);

    public static readonly DirectProperty<VCustomButton, string?> VTextProperty =
    AvaloniaProperty.RegisterDirect<VCustomButton, string?>(
        nameof(VText),
        o => o.VText,
        (o, v) => o.VText = v);

    public static readonly DirectProperty<VCustomButton, FontWeight> VFontWeightProperty =
        AvaloniaProperty.RegisterDirect<VCustomButton, FontWeight>(
            nameof(VFontWeight),
            o => o.VFontWeight,
            (o, v) => o.VFontWeight = v);

    public static readonly DirectProperty<VCustomButton, double> VFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VCustomButton, double>(
            nameof(VFontSize),
            o => o.VFontSize,
            (o, v) => o.VFontSize = v);

    public static readonly DirectProperty<VCustomButton, double> VIconWidthProperty =
        AvaloniaProperty.RegisterDirect<VCustomButton, double>(
            nameof(VIconWidth),
            o => o.VIconWidth,
            (o, v) => o.VIconWidth = v);

    public static readonly DirectProperty<VCustomButton, double> VIconHeightProperty =
        AvaloniaProperty.RegisterDirect<VCustomButton, double>(
            nameof(VIconHeight),
            o => o.VIconHeight,
            (o, v) => o.VIconHeight = v);

    private double vImageWidth = 50;

    private double vImageHeight = 50;

    private VButtonEffects vButtonEffect = VButtonEffects.None;

    private double vIconWidth = 50;

    private double vIconHeight = 50;

    private double vFontSize = 32;

    private FontWeight vFontWeight = FontWeight.Normal;

    private string? vText = string.Empty;

    static VCustomButton()
    {
    }

    public IBrush VNormalIconForeground
    {
        get => GetValue(VNormalIconForegroundProperty);
        set => SetValue(VNormalIconForegroundProperty, value);
    }

    public IBrush VIconNormalForeground
    {
        get => GetValue(VIconNormalForegroundProperty);
        set => SetValue(VIconNormalForegroundProperty, value);
    }

    public IBrush VIconPressedForeground
    {
        get => GetValue(VIconPressedForegroundProperty);
        set => SetValue(VIconPressedForegroundProperty, value);
    }

    public IBrush VPointeroverForeground
    {
        get => GetValue(VPointeroverForegroundProperty);
        set => SetValue(VPointeroverForegroundProperty, value);
    }

    public IBrush VPressedIconForeground
    {
        get => GetValue(VPressedIconForegroundProperty);
        set => SetValue(VPressedIconForegroundProperty, value);
    }

    public IBrush VPointeroverIconForeground
    {
        get => GetValue(VPointeroverIconForegroundProperty);
        set => SetValue(VPointeroverIconForegroundProperty, value);
    }

    public IBrush VPointeroverBorderBrush
    {
        get => GetValue(VPointeroverBorderBrushProperty);
        set => SetValue(VPointeroverBorderBrushProperty, value);
    }

    public IBrush VPressedBorderBrush
    {
        get => GetValue(VPressedBorderBrushProperty);
        set => SetValue(VPressedBorderBrushProperty, value);
    }

    public IBrush VPointeroverBackground
    {
        get => GetValue(VPointeroverBackgroundProperty);
        set => SetValue(VPointeroverBackgroundProperty, value);
    }

    public IBrush VPressedBackground
    {
        get => GetValue(VPressedBackgroundProperty);
        set => SetValue(VPressedBackgroundProperty, value);
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

    public double VImageHeight
    {
        get => vImageHeight;
        set => SetAndRaise(VImageHeightProperty, ref vImageHeight, value);
    }

    public double VImageWidth
    {
        get => vImageWidth;
        set => SetAndRaise(VImageWidthProperty, ref vImageWidth, value);
    }

    public Thickness VImageMargin
    {
        get => GetValue(VImageMarginProperty);
        set => SetValue(VImageMarginProperty, value);
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

    public VButtonEffects VButtonEffect
    {
        get => vButtonEffect;
        set => SetAndRaise(VButtonEffectProperty, ref vButtonEffect, value);
    }

    public IBrush VNormalBorderBrush
    {
        get => GetValue(VNormalBorderBrushProperty);
        set => SetValue(VNormalBorderBrushProperty, value);
    }

    public IBrush VNormalBackground
    {
        get => GetValue(VNormalBackgroundProperty);
        set => SetValue(VNormalBackgroundProperty, value);
    }

    public Thickness VBorderThickness
    {
        get => GetValue(VBorderThicknessProperty);
        set => SetValue(VBorderThicknessProperty, value);
    }

    public IBrush VNormalTextForeground
    {
        get => GetValue(VNormalTextForegroundProperty);
        set => SetValue(VNormalTextForegroundProperty, value);
    }

    public IBrush VPointoverTextForeground
    {
        get => GetValue(VPointoverTextForegroundProperty);
        set => SetValue(VPointoverTextForegroundProperty, value);
    }

    public IBrush VPressedTextForeground
    {
        get => GetValue(VPressedTextForegroundProperty);
        set => SetValue(VPressedTextForegroundProperty, value);
    }

    public HorizontalAlignment VTextHorizontalAlignment
    {
        get => this.GetValue(VTextHorizontalAlignmentProperty);
        set => SetValue(VTextHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VTextVerticalAlignment
    {
        get => this.GetValue(VTextVerticalAlignmentProperty);
        set => SetValue(VTextVerticalAlignmentProperty, value);
    }

    public Thickness VTextMargin
    {
        get => this.GetValue(VTextMarginProperty);
        set => SetValue(VTextMarginProperty, value);
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

    public Thickness VIconMargin
    {
        get => GetValue(VIconMarginProperty);
        set => SetValue(VIconMarginProperty, value);
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

    public Geometry? VNormalIcon
    {
        get => GetValue(VNormalIconProperty);
        set => SetValue(VNormalIconProperty, value);
    }

    public Geometry? VPressedIcon
    {
        get => GetValue(VPressedIconProperty);
        set => SetValue(VPressedIconProperty, value);
    }
}