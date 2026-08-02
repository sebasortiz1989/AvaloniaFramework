using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons.RadioButtons;

public class VRadioButton : RadioButton
{
    public static readonly DirectProperty<VRadioButton, double> VEllipseWidthProperty =
        AvaloniaProperty.RegisterDirect<VRadioButton, double>(
            nameof(VEllipseWidth),
            o => o.VEllipseWidth,
            (o, v) => o.VEllipseWidth = v);

    public static readonly DirectProperty<VRadioButton, double> VEllipseHeightProperty =
        AvaloniaProperty.RegisterDirect<VRadioButton, double>(
            nameof(VEllipseHeight),
            o => o.VEllipseHeight,
            (o, v) => o.VEllipseHeight = v);

    public static readonly DirectProperty<VRadioButton, Thickness> VEllipseThicknessProperty =
        AvaloniaProperty.RegisterDirect<VRadioButton, Thickness>(
            nameof(VEllipseThickness),
            o => o.VEllipseThickness,
            (o, v) => o.VEllipseThickness = v);

    public static readonly DirectProperty<VRadioButton, IBrush> VCheckIconForegroundProperty =
        AvaloniaProperty.RegisterDirect<VRadioButton, IBrush>(
            nameof(VCheckIconForeground),
            o => o.VCheckIconForeground,
            (o, v) => o.VCheckIconForeground = v);

    public static readonly DirectProperty<VRadioButton, IBrush> VInternalBorderBrushProperty =
        AvaloniaProperty.RegisterDirect<VRadioButton, IBrush>(
            nameof(VInternalBorderBrush),
            o => o.VInternalBorderBrush,
            (o, v) => o.VInternalBorderBrush = v);

    public static readonly DirectProperty<VRadioButton, Geometry?> VIconCheckedProperty =
        AvaloniaProperty.RegisterDirect<VRadioButton, Geometry?>(
            nameof(VIconChecked),
            o => o.VIconChecked,
            (o, v) => o.VIconChecked = v);

    public static readonly DirectProperty<VRadioButton, CornerRadius> VInnerBorderCornerRadiusProperty =
        AvaloniaProperty.RegisterDirect<VRadioButton, CornerRadius>(
            nameof(VInnerBorderCornerRadius),
            o => o.VInnerBorderCornerRadius,
            (o, v) => o.VInnerBorderCornerRadius = v);

    public static readonly DirectProperty<VRadioButton, double> VIconCheckedWidthProperty =
        AvaloniaProperty.RegisterDirect<VRadioButton, double>(
            nameof(VIconCheckedWidth),
            o => o.VIconCheckedWidth,
            (o, v) => o.VIconCheckedWidth = v);

    public static readonly DirectProperty<VRadioButton, double> VIconCheckedHeightProperty =
        AvaloniaProperty.RegisterDirect<VRadioButton, double>(
            nameof(VIconCheckedHeight),
            o => o.VIconCheckedHeight,
            (o, v) => o.VIconCheckedHeight = v);

    public static readonly DirectProperty<VRadioButton, IBrush> VCheckedBorderForegroundProperty =
        AvaloniaProperty.RegisterDirect<VRadioButton, IBrush>(
            nameof(VCheckedBorderForeground),
            o => o.VCheckedBorderForeground,
            (o, v) => o.VCheckedBorderForeground = v);

    public static readonly DirectProperty<VRadioButton, double> VShowBorderOnCkeckedProperty =
        AvaloniaProperty.RegisterDirect<VRadioButton, double>(
            nameof(VShowBorderOnCkecked),
            o => o.VShowBorderOnCkecked,
            (o, v) => o.VShowBorderOnCkecked = v);

    private double vEllipseHeight = 32;
    private Thickness vEllipseThickness = new Thickness(2);
    private double vEllipseWidth = 32;
    private IBrush vCheckIconForeground = Brush.Parse("#5DE4FF");
    private IBrush vInternalBorderBrush = Brush.Parse("#5DE4FF");
    private Geometry? vIconChecked = Geometry.Parse("M10 20.5C4.52381 20.5 0 15.9762 0 10.5C0 5.02381 4.51613 0.5 9.99232 0.5C15.4685 0.5 20 5.02381 20 10.5C20 15.9762 15.4762 20.5 10 20.5ZM8.77112 15.523C9.0553 15.523 9.30107 15.3925 9.48541 15.1083L14.6467 7.02074C14.7619 6.85177 14.8618 6.64439 14.8618 6.46006C14.8618 6.053 14.5008 5.79954 14.1244 5.79954C13.9017 5.79954 13.679 5.93011 13.51 6.19124L8.73272 13.7872L6.19048 10.5998C5.99078 10.3464 5.78341 10.2542 5.53763 10.2542C5.16129 10.2542 4.84639 10.5538 4.84639 10.9608C4.84639 11.1528 4.9232 11.3602 5.05376 11.5215L8.01843 15.116C8.25653 15.4078 8.48694 15.523 8.77112 15.523Z");
    private CornerRadius vInnerBorderCornerRadius = new(100);
    private double vIconCheckedHeight = 20;
    private double vIconCheckedWidth = 20;
    private IBrush vCheckedBorderForeground = Brush.Parse("#5DE4FF");
    private double vShowBorderOnCkecked;

    public double VEllipseWidth
    {
        get => vEllipseWidth;
        set => SetAndRaise(VEllipseWidthProperty, ref vEllipseWidth, value);
    }

    public double VEllipseHeight
    {
        get => vEllipseHeight;
        set => SetAndRaise(VEllipseHeightProperty, ref vEllipseHeight, value);
    }

    public double VIconCheckedHeight
    {
        get => vIconCheckedHeight;
        set => SetAndRaise(VIconCheckedHeightProperty, ref vIconCheckedHeight, value);
    }

    public double VIconCheckedWidth
    {
        get => vIconCheckedWidth;
        set => SetAndRaise(VIconCheckedWidthProperty, ref vIconCheckedWidth, value);
    }

    public Thickness VEllipseThickness
    {
        get => vEllipseThickness;
        set => SetAndRaise(VEllipseThicknessProperty, ref vEllipseThickness, value);
    }

    public IBrush VCheckIconForeground
    {
        get => vCheckIconForeground;
        set => SetAndRaise(VCheckIconForegroundProperty, ref vCheckIconForeground, value);
    }

    public IBrush VInternalBorderBrush
    {
        get => vInternalBorderBrush;
        set => SetAndRaise(VInternalBorderBrushProperty, ref vInternalBorderBrush, value);
    }

    public Geometry? VIconChecked
    {
        get => vIconChecked;
        set => SetAndRaise(VIconCheckedProperty, ref vIconChecked, value);
    }

    public CornerRadius VInnerBorderCornerRadius
    {
        get => vInnerBorderCornerRadius;
        set => SetAndRaise(VInnerBorderCornerRadiusProperty, ref vInnerBorderCornerRadius, value);
    }

    public IBrush VCheckedBorderForeground
    {
        get => vCheckedBorderForeground;
        set => SetAndRaise(VCheckedBorderForegroundProperty, ref vCheckedBorderForeground, value);
    }

    public double VShowBorderOnCkecked
    {
        get => vShowBorderOnCkecked;
        set => SetAndRaise(VShowBorderOnCkeckedProperty, ref vShowBorderOnCkecked, value);
    }
}