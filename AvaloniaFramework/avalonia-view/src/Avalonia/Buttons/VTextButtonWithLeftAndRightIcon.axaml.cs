using Avalonia;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class VTextButtonWithLeftAndRightIcon : VButtonBase
{
    public static readonly StyledProperty<IBrush> VIconPressedForegroundProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, IBrush>(nameof(VIconPressedForeground));

    public static readonly StyledProperty<IBrush> VIconNormalForegroundProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, IBrush>(nameof(VIconNormalForeground));

    public static readonly StyledProperty<Thickness> VTextMarginProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, Thickness>(nameof(VTextMargin));

    public static readonly StyledProperty<HorizontalAlignment> VTextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, HorizontalAlignment>(nameof(VTextHorizontalAlignment));

    public static readonly StyledProperty<VerticalAlignment> VTextVerticalAlignmentProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, VerticalAlignment>(nameof(VTextVerticalAlignment));

    public static readonly StyledProperty<Thickness> VRightIconMarginProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, Thickness>(nameof(VRightIconMargin));

    public static readonly StyledProperty<Thickness> VLeftIconMarginProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, Thickness>(nameof(VLeftIconMargin));

    public static readonly StyledProperty<HorizontalAlignment> VRightIconHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, HorizontalAlignment>(nameof(VRightIconHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VLeftIconHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, HorizontalAlignment>(nameof(VLeftIconHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VRightIconVerticalAlignmentProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, VerticalAlignment>(nameof(VRightIconVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VLeftIconVerticalAlignmentProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, VerticalAlignment>(nameof(VLeftIconVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<Geometry> VRightPressedIconProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, Geometry>(nameof(VRightPressedIcon));

    public static readonly StyledProperty<Geometry> VLeftPressedIconProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, Geometry>(nameof(VLeftPressedIcon));

    public static readonly StyledProperty<Geometry> VRightNormalIconProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, Geometry>(nameof(VRightNormalIcon));

    public static readonly StyledProperty<Geometry> VLeftNormalIconProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightIcon, Geometry>(nameof(VLeftNormalIcon));

    public static readonly DirectProperty<VTextButtonWithLeftAndRightIcon, string?> VTextProperty =
        AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightIcon, string?>(
            nameof(VText),
            o => o.VText,
            (o, v) => o.VText = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightIcon, FontWeight> VFontWeightProperty =
        AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightIcon, FontWeight>(
            nameof(VFontWeight),
            o => o.VFontWeight,
            (o, v) => o.VFontWeight = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightIcon, double> VFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightIcon, double>(
            nameof(VFontSize),
            o => o.VFontSize,
            (o, v) => o.VFontSize = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightIcon, double> VLeftIconWidthProperty =
        AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightIcon, double>(
            nameof(VLeftIconWidth),
            o => o.VLeftIconWidth,
            (o, v) => o.VLeftIconWidth = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightIcon, double> VRightIconWidthProperty =
        AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightIcon, double>(
            nameof(VRightIconWidth),
            o => o.VRightIconWidth,
            (o, v) => o.VRightIconWidth = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightIcon, double> VLeftIconHeightProperty =
        AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightIcon, double>(
            nameof(VLeftIconHeight),
            o => o.VLeftIconHeight,
            (o, v) => o.VLeftIconHeight = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightIcon, double> VRightIconHeightProperty =
        AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightIcon, double>(
            nameof(VRightIconHeight),
            o => o.VRightIconHeight,
            (o, v) => o.VRightIconHeight = v);

    private double vLeftIconWidth;

    private double vRightIconWidth;

    private double vLeftIconHeight;

    private double vRightIconHeight;

    private double vFontSize = 22;

    private FontWeight vFontWeight = FontWeight.Bold;

    private string? vText = string.Empty;

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

    public double VRightIconHeight
    {
        get => vRightIconHeight;
        set => SetAndRaise(VRightIconHeightProperty, ref vRightIconHeight, value);
    }

    public double VLeftIconHeight
    {
        get => vLeftIconHeight;
        set => SetAndRaise(VLeftIconHeightProperty, ref vLeftIconHeight, value);
    }

    public double VRightIconWidth
    {
        get => vRightIconWidth;
        set => SetAndRaise(VRightIconWidthProperty, ref vRightIconWidth, value);
    }

    public double VLeftIconWidth
    {
        get => vLeftIconWidth;
        set => SetAndRaise(VLeftIconWidthProperty, ref vLeftIconWidth, value);
    }

    public Thickness VRightIconMargin
    {
        get => GetValue(VRightIconMarginProperty);
        set => SetValue(VRightIconMarginProperty, value);
    }

    public Thickness VLeftIconMargin
    {
        get => GetValue(VLeftIconMarginProperty);
        set => SetValue(VLeftIconMarginProperty, value);
    }

    public HorizontalAlignment VRightIconHorizontalAlignment
    {
        get => GetValue(VRightIconHorizontalAlignmentProperty);
        set => SetValue(VRightIconHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VLeftIconHorizontalAlignment
    {
        get => GetValue(VLeftIconHorizontalAlignmentProperty);
        set => SetValue(VLeftIconHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VRightIconVerticalAlignment
    {
        get => GetValue(VRightIconVerticalAlignmentProperty);
        set => SetValue(VRightIconVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VLeftIconVerticalAlignment
    {
        get => GetValue(VLeftIconVerticalAlignmentProperty);
        set => SetValue(VLeftIconVerticalAlignmentProperty, value);
    }

    public Geometry VLeftPressedIcon
    {
        get => GetValue(VLeftPressedIconProperty);
        set => SetValue(VLeftPressedIconProperty, value);
    }

    public Geometry VRightPressedIcon
    {
        get => GetValue(VRightPressedIconProperty);
        set => SetValue(VRightPressedIconProperty, value);
    }

    public Geometry VLeftNormalIcon
    {
        get => GetValue(VLeftPressedIconProperty);
        set => SetValue(VLeftPressedIconProperty, value);
    }

    public Geometry VRightNormalIcon
    {
        get => GetValue(VRightPressedIconProperty);
        set => SetValue(VRightPressedIconProperty, value);
    }
}