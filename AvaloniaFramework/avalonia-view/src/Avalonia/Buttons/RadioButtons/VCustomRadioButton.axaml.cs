using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaFramework.Apresentacao.Utils.Enums;

namespace AvaloniaFramework.Apresentacao.Buttons.RadioButtons;

public class VCustomRadioButton : VBaseRadioButton
{
    public static readonly StyledProperty<IBrush> VNormalBackgroundProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VNormalBackground), defaultValue: Brushes.Black);

    public static readonly StyledProperty<IBrush> VPointeroverBackgroundProperty =
         AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VPointeroverBackground), defaultValue: Brushes.Aqua);

    public static readonly StyledProperty<IBrush> VCheckedBackgroundProperty =
         AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VCheckedBackground), defaultValue: Brushes.Teal);

    public static readonly StyledProperty<IBrush> VNormalBorderBrushProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VNormalBorderBrush), defaultValue: Brushes.Aqua);

    public static readonly StyledProperty<IBrush> VCheckedBorderBrushProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VCheckedBorderBrush), defaultValue: Brushes.Teal);

    public static readonly StyledProperty<IBrush> VPointeroverBorderBrushProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VPointeroverBorderBrush), defaultValue: Brushes.Aqua);

    public static readonly StyledProperty<IBrush> VNormalIconForegroundProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VNormalIconForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VCheckedIconForegroundProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VPointeroverIconForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VPointeroverIconForegroundProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VPointeroverIconForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VIconNormalForegroundProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VIconNormalForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VPointeroverForegroundProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VPointeroverForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VIconCheckedForegroundProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VIconCheckedForeground), defaultValue: Brushes.White);

    public static readonly StyledProperty<IBrush> VNormalTextForegroundProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VNormalTextForeground));

    public static readonly StyledProperty<IBrush> VPointoverTextForegroundProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VNormalTextForeground));

    public static readonly StyledProperty<IBrush> VCheckedTextForegroundProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IBrush>(nameof(VCheckedTextForeground));

    public static readonly StyledProperty<Thickness> VTextMarginProperty =
        AvaloniaProperty.Register<VCustomRadioButton, Thickness>(nameof(VTextMargin), defaultValue: default);

    public static readonly StyledProperty<HorizontalAlignment> VTextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VCustomRadioButton, HorizontalAlignment>(nameof(VTextHorizontalAlignment), defaultValue: HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VTextVerticalAlignmentProperty =
        AvaloniaProperty.Register<VCustomRadioButton, VerticalAlignment>(nameof(VTextVerticalAlignment), defaultValue: VerticalAlignment.Center);

    public static readonly StyledProperty<Thickness> VIconMarginProperty =
        AvaloniaProperty.Register<VCustomRadioButton, Thickness>(nameof(VIconMargin), defaultValue: default);

    public static readonly StyledProperty<HorizontalAlignment> VIconHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VCustomRadioButton, HorizontalAlignment>(nameof(VIconHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconVerticalAlignmentProperty =
        AvaloniaProperty.Register<VCustomRadioButton, VerticalAlignment>(nameof(VIconVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<Geometry?> VNormalIconProperty =
        AvaloniaProperty.Register<VCustomRadioButton, Geometry?>(nameof(VNormalIcon));

    public static readonly StyledProperty<Geometry?> VCheckedIconProperty =
        AvaloniaProperty.Register<VCustomRadioButton, Geometry?>(nameof(VCheckedIcon));

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<VCustomRadioButton, Thickness>(nameof(VBorderThickness), defaultValue: new Thickness(2));

    public static readonly StyledProperty<Thickness> VImageMarginProperty =
        AvaloniaProperty.Register<VCustomRadioButton, Thickness>(nameof(VImageMargin));

    public static readonly StyledProperty<HorizontalAlignment> VImageHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VCustomRadioButton, HorizontalAlignment>(nameof(VImageHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VImageVerticalAlignmentProperty =
        AvaloniaProperty.Register<VCustomRadioButton, VerticalAlignment>(nameof(VImageVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<IImage?> VNormalImageProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IImage?>(nameof(VNormalImage));

    public static readonly StyledProperty<IImage?> VCheckedImageProperty =
        AvaloniaProperty.Register<VCustomRadioButton, IImage?>(nameof(VCheckedImage));

    public static readonly DirectProperty<VCustomRadioButton, double> VImageWidthProperty =
        AvaloniaProperty.RegisterDirect<VCustomRadioButton, double>(
            nameof(VImageWidth),
            o => o.VImageWidth,
            (o, v) => o.VImageWidth = v);

    public static readonly DirectProperty<VCustomRadioButton, double> VImageHeightProperty =
        AvaloniaProperty.RegisterDirect<VCustomRadioButton, double>(
            nameof(VImageHeight),
            o => o.VImageHeight,
            (o, v) => o.VImageHeight = v);

    public static readonly DirectProperty<VCustomRadioButton, VButtonEffects> VButtonEffectProperty =
        AvaloniaProperty.RegisterDirect<VCustomRadioButton, VButtonEffects>(
            nameof(VButtonEffect),
            o => o.VButtonEffect,
            (o, v) => o.VButtonEffect = v);

    public static readonly DirectProperty<VCustomRadioButton, string?> VTextProperty =
    AvaloniaProperty.RegisterDirect<VCustomRadioButton, string?>(
        nameof(VText),
        o => o.VText,
        (o, v) => o.VText = v);

    public static readonly DirectProperty<VCustomRadioButton, FontWeight> VFontWeightProperty =
        AvaloniaProperty.RegisterDirect<VCustomRadioButton, FontWeight>(
            nameof(VFontWeight),
            o => o.VFontWeight,
            (o, v) => o.VFontWeight = v);

    public static readonly DirectProperty<VCustomRadioButton, double> VFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VCustomRadioButton, double>(
            nameof(VFontSize),
            o => o.VFontSize,
            (o, v) => o.VFontSize = v);

    public static readonly DirectProperty<VCustomRadioButton, double> VIconWidthProperty =
        AvaloniaProperty.RegisterDirect<VCustomRadioButton, double>(
            nameof(VIconWidth),
            o => o.VIconWidth,
            (o, v) => o.VIconWidth = v);

    public static readonly DirectProperty<VCustomRadioButton, double> VIconHeightProperty =
        AvaloniaProperty.RegisterDirect<VCustomRadioButton, double>(
            nameof(VIconHeight),
            o => o.VIconHeight,
            (o, v) => o.VIconHeight = v);

    private double vImageWidth = 50;

    private double vImageHeight = 50;

    private VButtonEffects vButtonEffect = VButtonEffects.None;

    private double vIconWidth = 50;

    private double vIconHeight = 50;

    private double vFontSize = 22;

    private FontWeight vFontWeight = FontWeight.Normal;

    private string? vText = string.Empty;

    static VCustomRadioButton()
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

    public IBrush VIconCheckedForeground
    {
        get => GetValue(VIconCheckedForegroundProperty);
        set => SetValue(VIconCheckedForegroundProperty, value);
    }

    public IBrush VPointeroverForeground
    {
        get => GetValue(VPointeroverForegroundProperty);
        set => SetValue(VPointeroverForegroundProperty, value);
    }

    public IBrush VCheckedIconForeground
    {
        get => GetValue(VCheckedIconForegroundProperty);
        set => SetValue(VCheckedIconForegroundProperty, value);
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

    public IBrush VCheckedBorderBrush
    {
        get => GetValue(VCheckedBorderBrushProperty);
        set => SetValue(VCheckedBorderBrushProperty, value);
    }

    public IBrush VPointeroverBackground
    {
        get => GetValue(VPointeroverBackgroundProperty);
        set => SetValue(VPointeroverBackgroundProperty, value);
    }

    public IBrush VCheckedBackground
    {
        get => GetValue(VCheckedBackgroundProperty);
        set => SetValue(VCheckedBackgroundProperty, value);
    }

    public IImage? VNormalImage
    {
        get => GetValue(VNormalImageProperty);
        set => SetValue(VNormalImageProperty, value);
    }

    public IImage? VCheckedImage
    {
        get => GetValue(VCheckedImageProperty);
        set => SetValue(VCheckedImageProperty, value);
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

    public IBrush VCheckedTextForeground
    {
        get => GetValue(VCheckedTextForegroundProperty);
        set => SetValue(VCheckedTextForegroundProperty, value);
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

    public Geometry? VCheckedIcon
    {
        get => GetValue(VCheckedIconProperty);
        set => SetValue(VCheckedIconProperty, value);
    }
}