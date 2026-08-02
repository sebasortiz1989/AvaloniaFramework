using Avalonia;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class VTextToggleButtonWithLeftIcon : VToggleButtonBase
{
    public static readonly StyledProperty<IBrush> VIconPressedForegroundProperty =
    AvaloniaProperty.Register<VTextToggleButtonWithLeftIcon, IBrush>(nameof(VIconPressedForeground));

    public static readonly StyledProperty<IBrush> VIconNormalForegroundProperty =
        AvaloniaProperty.Register<VTextToggleButtonWithLeftIcon, IBrush>(nameof(VIconNormalForeground));

    public static readonly StyledProperty<Thickness> VTextMarginProperty =
        AvaloniaProperty.Register<VTextToggleButtonWithLeftIcon, Thickness>(nameof(VTextMargin));

    public static readonly StyledProperty<HorizontalAlignment> VTextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VTextToggleButtonWithLeftIcon, HorizontalAlignment>(nameof(VTextHorizontalAlignment));

    public static readonly StyledProperty<VerticalAlignment> VTextVerticalAlignmentProperty =
        AvaloniaProperty.Register<VTextToggleButtonWithLeftIcon, VerticalAlignment>(nameof(VTextVerticalAlignment));

    public static readonly StyledProperty<Thickness> VIconMarginProperty =
        AvaloniaProperty.Register<VTextToggleButtonWithLeftIcon, Thickness>(nameof(VIconMargin));

    public static readonly StyledProperty<HorizontalAlignment> VIconHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VTextToggleButtonWithLeftIcon, HorizontalAlignment>(nameof(VIconHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconVerticalAlignmentProperty =
        AvaloniaProperty.Register<VTextToggleButtonWithLeftIcon, VerticalAlignment>(nameof(VIconVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<Geometry> VPressedIconProperty =
        AvaloniaProperty.Register<VTextToggleButtonWithLeftIcon, Geometry>(nameof(VPressedIcon));

    public static readonly StyledProperty<Geometry> VNormalIconProperty =
        AvaloniaProperty.Register<VTextToggleButtonWithLeftIcon, Geometry>(nameof(VNormalIcon));

    public static readonly DirectProperty<VTextToggleButtonWithLeftIcon, string?> VTextProperty =
    AvaloniaProperty.RegisterDirect<VTextToggleButtonWithLeftIcon, string?>(
        nameof(VText),
        o => o.VText,
        (o, v) => o.VText = v);

    public static readonly DirectProperty<VTextToggleButtonWithLeftIcon, FontWeight> VFontWeightProperty =
        AvaloniaProperty.RegisterDirect<VTextToggleButtonWithLeftIcon, FontWeight>(
            nameof(VFontWeight),
            o => o.VFontWeight,
            (o, v) => o.VFontWeight = v);

    public static readonly DirectProperty<VTextToggleButtonWithLeftIcon, double> VFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VTextToggleButtonWithLeftIcon, double>(
            nameof(VFontSize),
            o => o.VFontSize,
            (o, v) => o.VFontSize = v);

    public static readonly DirectProperty<VTextToggleButtonWithLeftIcon, double> VIconWidthProperty =
    AvaloniaProperty.RegisterDirect<VTextToggleButtonWithLeftIcon, double>(
        nameof(VIconWidth),
        o => o.VIconWidth,
        (o, v) => o.VIconWidth = v);

    public static readonly DirectProperty<VTextToggleButtonWithLeftIcon, double> VIconHeightProperty =
        AvaloniaProperty.RegisterDirect<VTextToggleButtonWithLeftIcon, double>(
            nameof(VIconHeight),
            o => o.VIconHeight,
            (o, v) => o.VIconHeight = v);

    public static readonly DirectProperty<VTextToggleButtonWithLeftIcon, bool> VIsCheckedProperty =
    AvaloniaProperty.RegisterDirect<VTextToggleButtonWithLeftIcon, bool>(
        nameof(VIsChecked),
        o => o.VIsChecked,
        (o, v) => o.VIsChecked = v);

    private double vIconWidth;

    private double vIconHeight;

    private double vFontSize = 22;

    private FontWeight vFontWeight = FontWeight.Bold;

    private string? vText = string.Empty;

    private bool vIsChecked;

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

    public Geometry VPressedIcon
    {
        get => GetValue(VPressedIconProperty);
        set => SetValue(VPressedIconProperty, value);
    }

    public Geometry VNormalIcon
    {
        get => GetValue(VPressedIconProperty);
        set => SetValue(VPressedIconProperty, value);
    }

    public bool VIsChecked
    {
        get => vIsChecked;
        set => SetAndRaise(VIsCheckedProperty, ref vIsChecked, value);
    }
}