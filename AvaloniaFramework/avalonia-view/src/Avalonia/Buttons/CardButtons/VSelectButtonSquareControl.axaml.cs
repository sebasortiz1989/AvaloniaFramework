using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons.CardButtons;

public class VSelectButtonSquareControl : ToggleButton
{
    public static readonly DirectProperty<VSelectButtonSquareControl, double> VToggleButtonWidthProperty =
        AvaloniaProperty.RegisterDirect<VSelectButtonSquareControl, double>(
            nameof(VToggleButtonWidth),
            o => o.VToggleButtonWidth,
            (o, v) => o.VToggleButtonWidth = v);

    public static readonly DirectProperty<VSelectButtonSquareControl, double> VToggleButtonHeightProperty =
        AvaloniaProperty.RegisterDirect<VSelectButtonSquareControl, double>(
            nameof(VToggleButtonHeight),
            o => o.VToggleButtonHeight,
            (o, v) => o.VToggleButtonHeight = v);

    public static readonly DirectProperty<VSelectButtonSquareControl, double> VIconWidthProperty =
        AvaloniaProperty.RegisterDirect<VSelectButtonSquareControl, double>(
            nameof(VIconWidth),
            o => o.VIconWidth,
            (o, v) => o.VIconWidth = v);

    public static readonly DirectProperty<VSelectButtonSquareControl, double> VIconHeightProperty =
        AvaloniaProperty.RegisterDirect<VSelectButtonSquareControl, double>(
            nameof(VIconHeight),
            o => o.VIconHeight,
            (o, v) => o.VIconHeight = v);

    public static readonly DirectProperty<VSelectButtonSquareControl, VerticalAlignment> VIconVerticalAlignmentProperty =
        AvaloniaProperty.RegisterDirect<VSelectButtonSquareControl, VerticalAlignment>(
            nameof(VIconVerticalAlignment),
            o => o.VIconVerticalAlignment,
            (o, v) => o.VIconVerticalAlignment = v);

    public static readonly DirectProperty<VSelectButtonSquareControl, HorizontalAlignment> VIconHorizontalAlignmentProperty =
        AvaloniaProperty.RegisterDirect<VSelectButtonSquareControl, HorizontalAlignment>(
            nameof(VIconHorizontalAlignment),
            o => o.VIconHorizontalAlignment,
            (o, v) => o.VIconHorizontalAlignment = v);

    public static readonly DirectProperty<VSelectButtonSquareControl, VerticalAlignment> VTextVerticalAlignmentProperty =
    AvaloniaProperty.RegisterDirect<VSelectButtonSquareControl, VerticalAlignment>(
        nameof(VTextVerticalAlignment),
        o => o.VTextVerticalAlignment,
        (o, v) => o.VTextVerticalAlignment = v);

    public static readonly DirectProperty<VSelectButtonSquareControl, HorizontalAlignment> VTextHorizontalAlignmentProperty =
        AvaloniaProperty.RegisterDirect<VSelectButtonSquareControl, HorizontalAlignment>(
            nameof(VTextHorizontalAlignment),
            o => o.VTextHorizontalAlignment,
            (o, v) => o.VTextHorizontalAlignment = v);

    public static readonly DirectProperty<VSelectButtonSquareControl, string?> VTextProperty =
        AvaloniaProperty.RegisterDirect<VSelectButtonSquareControl, string?>(
            nameof(VText),
            o => o.VText,
            (o, v) => o.VText = v);

    public static readonly StyledProperty<Geometry> VCheckedIconProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, Geometry>(nameof(VCheckedIcon));

    public static readonly StyledProperty<Geometry> VUncheckedIconProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, Geometry>(nameof(VUncheckedIcon));

    public static readonly StyledProperty<IBrush> VIconForegroundCheckedProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, IBrush>(nameof(VIconForegroundChecked));

    public static readonly StyledProperty<IBrush> VIconForegroundUncheckedProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, IBrush>(nameof(VIconForegroundUnchecked));

    public static readonly StyledProperty<IBrush> VCheckedTextForegroundProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, IBrush>(nameof(VCheckedTextForeground));

    public static readonly StyledProperty<IBrush> VUncheckedTextForegroundProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, IBrush>(nameof(VUncheckedTextForeground));

    public static readonly StyledProperty<Thickness> VTextMarginProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, Thickness>(nameof(VTextMargin));

    public static readonly StyledProperty<IBrush> VCheckedBackgroundProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, IBrush>(nameof(VCheckedBackground));

    public static readonly StyledProperty<IBrush> VUncheckedBackgroundProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, IBrush>(nameof(VUncheckedBackground));

    public static readonly StyledProperty<IBrush> VUncheckedBorderBrushProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, IBrush>(nameof(VUncheckedBorderBrush));

    public static readonly StyledProperty<IBrush> VCheckedBorderBrushProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, IBrush>(nameof(VCheckedBorderBrush));

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, Thickness>(nameof(VBorderThickness));

    public static readonly StyledProperty<CornerRadius> VCornerRadiusProperty =
        AvaloniaProperty.Register<VSelectButtonSquareControl, CornerRadius>(nameof(VCornerRadius), CornerRadius.Parse("16"));

    private double vIconWidth;
    private double vIconHeight;
    private double vToggleButtonWidth = 188;
    private double vToggleButtonHeight = 212;
    private VerticalAlignment vIconVerticalAlignment;
    private HorizontalAlignment vIconHorizontalAlignment = HorizontalAlignment.Center;
    private VerticalAlignment vTextVerticalAlignment = VerticalAlignment.Center;
    private HorizontalAlignment vTextHorizontalAlignment = HorizontalAlignment.Center;
    private string? vText = string.Empty;

    public double VIconWidth
    {
        get => vIconWidth;
        set => SetAndRaise(VIconWidthProperty, ref vIconWidth, value);
    }

    public double VIconHeight
    {
        get => vIconHeight;
        set => SetAndRaise(VIconHeightProperty, ref vIconHeight, value);
    }

    public double VToggleButtonWidth
    {
        get => vToggleButtonWidth;
        set => SetAndRaise(VToggleButtonWidthProperty, ref vToggleButtonWidth, value);
    }

    public double VToggleButtonHeight
    {
        get => vToggleButtonHeight;
        set => SetAndRaise(VToggleButtonHeightProperty, ref vToggleButtonHeight, value);
    }

    public Geometry VCheckedIcon
    {
        get => GetValue(VCheckedIconProperty);
        set => SetValue(VCheckedIconProperty, value);
    }

    public Geometry VUncheckedIcon
    {
        get => GetValue(VUncheckedIconProperty);
        set => SetValue(VUncheckedIconProperty, value);
    }

    public IBrush VIconForegroundChecked
    {
        get => GetValue(VIconForegroundCheckedProperty);
        set => SetValue(VIconForegroundCheckedProperty, value);
    }

    public IBrush VIconForegroundUnchecked
    {
        get => GetValue(VIconForegroundUncheckedProperty);
        set => SetValue(VIconForegroundUncheckedProperty, value);
    }

    public VerticalAlignment VIconVerticalAlignment
    {
        get => vIconVerticalAlignment;
        set => SetAndRaise(VIconVerticalAlignmentProperty, ref vIconVerticalAlignment, value);
    }

    public HorizontalAlignment VIconHorizontalAlignment
    {
        get => vIconHorizontalAlignment;
        set => SetAndRaise(VIconHorizontalAlignmentProperty, ref vIconHorizontalAlignment, value);
    }

    public IBrush VCheckedTextForeground
    {
        get => GetValue(VCheckedTextForegroundProperty);
        set => SetValue(VCheckedTextForegroundProperty, value);
    }

    public IBrush VUncheckedTextForeground
    {
        get => GetValue(VUncheckedTextForegroundProperty);
        set => SetValue(VUncheckedTextForegroundProperty, value);
    }

    public VerticalAlignment VTextVerticalAlignment
    {
        get => vTextVerticalAlignment;
        set => SetAndRaise(VTextVerticalAlignmentProperty, ref vTextVerticalAlignment, value);
    }

    public HorizontalAlignment VTextHorizontalAlignment
    {
        get => vTextHorizontalAlignment;
        set => SetAndRaise(VTextHorizontalAlignmentProperty, ref vTextHorizontalAlignment, value);
    }

    public Thickness VTextMargin
    {
        get => GetValue(VTextMarginProperty);
        set => SetValue(VTextMarginProperty, value);
    }

    public IBrush VCheckedBackground
    {
        get => GetValue(VCheckedBackgroundProperty);
        set => SetValue(VCheckedBackgroundProperty, value);
    }

    public IBrush VUncheckedBackground
    {
        get => GetValue(VUncheckedBackgroundProperty);
        set => SetValue(VUncheckedBackgroundProperty, value);
    }

    public IBrush VCheckedBorderBrush
    {
        get => GetValue(VCheckedBorderBrushProperty);
        set => SetValue(VCheckedBorderBrushProperty, value);
    }

    public IBrush VUncheckedBorderBrush
    {
        get => GetValue(VUncheckedBorderBrushProperty);
        set => SetValue(VUncheckedBorderBrushProperty, value);
    }

    public Thickness VBorderThickness
    {
        get => GetValue(VBorderThicknessProperty);
        set => SetValue(VBorderThicknessProperty, value);
    }

    public CornerRadius VCornerRadius
    {
        get => GetValue(VCornerRadiusProperty);
        set => SetValue(VCornerRadiusProperty, value);
    }

    public string? VText
    {
        get => vText;
        set => SetAndRaise(VTextProperty, ref vText, value);
    }
}