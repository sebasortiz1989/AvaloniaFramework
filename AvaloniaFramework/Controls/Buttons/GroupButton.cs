using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaFramework.Controls.Buttons;

/// <summary>
/// An icon button that behaves as one option in a mutually exclusive set — a tab bar item, in
/// practice. Being a <see cref="RadioButton"/>, buttons sharing a <c>GroupName</c> uncheck each
/// other, and the checked state swaps both the background and the image.
/// </summary>
public class GroupButton : RadioButton
{
    public static readonly StyledProperty<IImage?> VNormalImageOneProperty =
        AvaloniaProperty.Register<GroupButton, IImage?>(nameof(VNormalImageOne));

    public static readonly StyledProperty<IImage?> VCheckedImageOneProperty =
        AvaloniaProperty.Register<GroupButton, IImage?>(nameof(VCheckedImageOne));

    public static readonly StyledProperty<double> VImageOneWidthProperty =
        AvaloniaProperty.Register<GroupButton, double>(nameof(VImageOneWidth), double.NaN);

    public static readonly StyledProperty<double> VImageOneHeightProperty =
        AvaloniaProperty.Register<GroupButton, double>(nameof(VImageOneHeight), double.NaN);

    public static readonly StyledProperty<IBrush?> VNormalBackgroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush?>(nameof(VNormalBackground), Brushes.Transparent);

    public static readonly StyledProperty<IBrush?> VCheckedBackgroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush?>(nameof(VCheckedBackground), Brushes.Transparent);

    public static readonly StyledProperty<IBrush?> VPointeroverBackgroundColorProperty =
        AvaloniaProperty.Register<GroupButton, IBrush?>(nameof(VPointeroverBackgroundColor), Brushes.Transparent);

    public static readonly StyledProperty<IBrush?> VNormalBorderBrushProperty =
        AvaloniaProperty.Register<GroupButton, IBrush?>(nameof(VNormalBorderBrush), Brushes.Transparent);

    public static readonly StyledProperty<IBrush?> VCheckedBorderBrushProperty =
        AvaloniaProperty.Register<GroupButton, IBrush?>(nameof(VCheckedBorderBrush), Brushes.Transparent);

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<GroupButton, Thickness>(nameof(VBorderThickness), default);

    public static readonly StyledProperty<CornerRadius> VCornerRadiusProperty =
        AvaloniaProperty.Register<GroupButton, CornerRadius>(nameof(VCornerRadius), default);

    /// <summary>The image shown while unchecked.</summary>
    public IImage? VNormalImageOne
    {
        get => GetValue(VNormalImageOneProperty);
        set => SetValue(VNormalImageOneProperty, value);
    }

    /// <summary>The image shown while checked.</summary>
    public IImage? VCheckedImageOne
    {
        get => GetValue(VCheckedImageOneProperty);
        set => SetValue(VCheckedImageOneProperty, value);
    }

    public double VImageOneWidth
    {
        get => GetValue(VImageOneWidthProperty);
        set => SetValue(VImageOneWidthProperty, value);
    }

    public double VImageOneHeight
    {
        get => GetValue(VImageOneHeightProperty);
        set => SetValue(VImageOneHeightProperty, value);
    }

    public IBrush? VNormalBackground
    {
        get => GetValue(VNormalBackgroundProperty);
        set => SetValue(VNormalBackgroundProperty, value);
    }

    public IBrush? VCheckedBackground
    {
        get => GetValue(VCheckedBackgroundProperty);
        set => SetValue(VCheckedBackgroundProperty, value);
    }

    public IBrush? VPointeroverBackgroundColor
    {
        get => GetValue(VPointeroverBackgroundColorProperty);
        set => SetValue(VPointeroverBackgroundColorProperty, value);
    }

    public IBrush? VNormalBorderBrush
    {
        get => GetValue(VNormalBorderBrushProperty);
        set => SetValue(VNormalBorderBrushProperty, value);
    }

    public IBrush? VCheckedBorderBrush
    {
        get => GetValue(VCheckedBorderBrushProperty);
        set => SetValue(VCheckedBorderBrushProperty, value);
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

    /// <summary>Keeps this control on its own theme rather than inheriting <see cref="RadioButton"/>'s.</summary>
    protected override Type StyleKeyOverride => typeof(GroupButton);
}
