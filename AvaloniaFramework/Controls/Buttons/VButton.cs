using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Controls.Buttons;

/// <summary>
/// A text button whose normal and pressed appearance are separate styleable properties, so a
/// design system can declare a whole button variant as one style class:
/// <code>
/// &lt;Style Selector="buttons|VButton.BtnPrimary"&gt;
///     &lt;Setter Property="VNormalForeground" Value="{StaticResource ColorAccent}" /&gt;
///     &lt;Setter Property="VPressedBackground" Value="{StaticResource ColorAccentTint12}" /&gt;
/// &lt;/Style&gt;
/// </code>
/// </summary>
public class VButton : Button
{
    /// <summary>The button's caption.</summary>
    public static readonly StyledProperty<string?> VTextProperty =
        AvaloniaProperty.Register<VButton, string?>(nameof(VText));

    public static readonly StyledProperty<double> VFontSizeProperty =
        AvaloniaProperty.Register<VButton, double>(nameof(VFontSize), 14d);

    public static readonly StyledProperty<FontWeight> VFontWeightProperty =
        AvaloniaProperty.Register<VButton, FontWeight>(nameof(VFontWeight), FontWeight.Normal);

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<VButton, Thickness>(nameof(VBorderThickness), new Thickness(1));

    public static readonly StyledProperty<CornerRadius> VCornerRadiusProperty =
        AvaloniaProperty.Register<VButton, CornerRadius>(nameof(VCornerRadius), new CornerRadius(4));

    public static readonly StyledProperty<Thickness> VPaddingProperty =
        AvaloniaProperty.Register<VButton, Thickness>(nameof(VPadding), new Thickness(8, 4));

    public static readonly StyledProperty<IBrush?> VNormalBackgroundProperty =
        AvaloniaProperty.Register<VButton, IBrush?>(nameof(VNormalBackground), Brushes.Transparent);

    public static readonly StyledProperty<IBrush?> VNormalBorderBrushProperty =
        AvaloniaProperty.Register<VButton, IBrush?>(nameof(VNormalBorderBrush), Brushes.Transparent);

    public static readonly StyledProperty<IBrush?> VNormalForegroundProperty =
        AvaloniaProperty.Register<VButton, IBrush?>(nameof(VNormalForeground), Brushes.Black);

    public static readonly StyledProperty<IBrush?> VPressedBackgroundProperty =
        AvaloniaProperty.Register<VButton, IBrush?>(nameof(VPressedBackground), Brushes.Transparent);

    public static readonly StyledProperty<IBrush?> VPressedBorderBrushProperty =
        AvaloniaProperty.Register<VButton, IBrush?>(nameof(VPressedBorderBrush), Brushes.Transparent);

    public static readonly StyledProperty<IBrush?> VPressedForegroundProperty =
        AvaloniaProperty.Register<VButton, IBrush?>(nameof(VPressedForeground), Brushes.Black);

    public static readonly StyledProperty<IBrush?> VDisabledBackgroundProperty =
        AvaloniaProperty.Register<VButton, IBrush?>(nameof(VDisabledBackground), Brushes.Transparent);

    public static readonly StyledProperty<IBrush?> VDisabledForegroundProperty =
        AvaloniaProperty.Register<VButton, IBrush?>(nameof(VDisabledForeground), Brushes.Gray);

    public static readonly StyledProperty<HorizontalAlignment> VTextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VButton, HorizontalAlignment>(nameof(VTextHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VTextVerticalAlignmentProperty =
        AvaloniaProperty.Register<VButton, VerticalAlignment>(nameof(VTextVerticalAlignment), VerticalAlignment.Center);

    /// <inheritdoc cref="VTextProperty" />
    public string? VText
    {
        get => GetValue(VTextProperty);
        set => SetValue(VTextProperty, value);
    }

    public double VFontSize
    {
        get => GetValue(VFontSizeProperty);
        set => SetValue(VFontSizeProperty, value);
    }

    public FontWeight VFontWeight
    {
        get => GetValue(VFontWeightProperty);
        set => SetValue(VFontWeightProperty, value);
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

    public Thickness VPadding
    {
        get => GetValue(VPaddingProperty);
        set => SetValue(VPaddingProperty, value);
    }

    public IBrush? VNormalBackground
    {
        get => GetValue(VNormalBackgroundProperty);
        set => SetValue(VNormalBackgroundProperty, value);
    }

    public IBrush? VNormalBorderBrush
    {
        get => GetValue(VNormalBorderBrushProperty);
        set => SetValue(VNormalBorderBrushProperty, value);
    }

    public IBrush? VNormalForeground
    {
        get => GetValue(VNormalForegroundProperty);
        set => SetValue(VNormalForegroundProperty, value);
    }

    public IBrush? VPressedBackground
    {
        get => GetValue(VPressedBackgroundProperty);
        set => SetValue(VPressedBackgroundProperty, value);
    }

    public IBrush? VPressedBorderBrush
    {
        get => GetValue(VPressedBorderBrushProperty);
        set => SetValue(VPressedBorderBrushProperty, value);
    }

    public IBrush? VPressedForeground
    {
        get => GetValue(VPressedForegroundProperty);
        set => SetValue(VPressedForegroundProperty, value);
    }

    public IBrush? VDisabledBackground
    {
        get => GetValue(VDisabledBackgroundProperty);
        set => SetValue(VDisabledBackgroundProperty, value);
    }

    public IBrush? VDisabledForeground
    {
        get => GetValue(VDisabledForegroundProperty);
        set => SetValue(VDisabledForegroundProperty, value);
    }

    public HorizontalAlignment VTextHorizontalAlignment
    {
        get => GetValue(VTextHorizontalAlignmentProperty);
        set => SetValue(VTextHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VTextVerticalAlignment
    {
        get => GetValue(VTextVerticalAlignmentProperty);
        set => SetValue(VTextVerticalAlignmentProperty, value);
    }

    /// <summary>Keeps this control on its own theme rather than inheriting <see cref="Button"/>'s.</summary>
    protected override Type StyleKeyOverride => typeof(VButton);
}
