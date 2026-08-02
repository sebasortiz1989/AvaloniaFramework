using Avalonia;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class VIconButton : VButtonBase
{
    public static readonly StyledProperty<IBrush> VIconPressedForegroundProperty =
    AvaloniaProperty.Register<VIconButton, IBrush>(nameof(VIconPressedForeground));

    public static readonly StyledProperty<IBrush> VIconPointoverForegroundProperty =
    AvaloniaProperty.Register<VIconButton, IBrush>(nameof(VIconPointoverForeground));

    public static readonly StyledProperty<IBrush> VIconNormalForegroundProperty =
        AvaloniaProperty.Register<VIconButton, IBrush>(nameof(VIconNormalForeground));

    public static readonly StyledProperty<Thickness> VIconMarginProperty =
        AvaloniaProperty.Register<VIconButton, Thickness>(nameof(VIconMargin));

    public static readonly StyledProperty<HorizontalAlignment> VIconHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VIconButton, HorizontalAlignment>(nameof(VIconHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconVerticalAlignmentProperty =
        AvaloniaProperty.Register<VIconButton, VerticalAlignment>(nameof(VIconVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<Geometry> VPressedIconProperty =
        AvaloniaProperty.Register<VIconButton, Geometry>(nameof(VPressedIcon));

    public static readonly StyledProperty<Geometry> VNormalIconProperty =
        AvaloniaProperty.Register<VIconButton, Geometry>(nameof(VNormalIcon));

    public static readonly DirectProperty<VIconButton, double> VIconWidthProperty =
    AvaloniaProperty.RegisterDirect<VIconButton, double>(
        nameof(VIconWidth),
        o => o.VIconWidth,
        (o, v) => o.VIconWidth = v);

    public static readonly DirectProperty<VIconButton, double> VIconHeightProperty =
        AvaloniaProperty.RegisterDirect<VIconButton, double>(
            nameof(VIconHeight),
            o => o.VIconHeight,
            (o, v) => o.VIconHeight = v);

    private double vIconWidth;

    private double vIconHeight;

    public IBrush VIconPressedForeground
    {
        get => GetValue(VIconPressedForegroundProperty);
        set => SetValue(VIconPressedForegroundProperty, value);
    }

    public IBrush VIconPointoverForeground
    {
        get => GetValue(VIconPointoverForegroundProperty);
        set => SetValue(VIconPointoverForegroundProperty, value);
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
}