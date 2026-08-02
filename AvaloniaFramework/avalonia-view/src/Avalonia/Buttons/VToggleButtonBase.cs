using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class VToggleButtonBase : ToggleButton
{
    public static readonly StyledProperty<IBrush> VPointoverBorderBrushProperty =
    AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VPointoverBorderBrush), Brush.Parse("#323232"));

    public static readonly StyledProperty<IBrush> VPointoverForegroundProperty =
        AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VPointoverForeground), Brush.Parse("#040404"));

    public static readonly StyledProperty<IBrush> VPointoverBackgroundProperty =
        AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VPointoverBackground), Brush.Parse("#323232"));

    public static readonly StyledProperty<Thickness> VPaddingProperty =
            AvaloniaProperty.Register<VToggleButtonBase, Thickness>(nameof(VPadding));

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<VToggleButtonBase, Thickness>(nameof(VBorderThickness));

    public static readonly StyledProperty<IBrush> VNormalBorderBrushProperty =
        AvaloniaProperty.Register<VToggleButtonBase, IBrush>(nameof(VNormalBorderBrush));

    public static readonly StyledProperty<IBrush> VPressedBorderBrushProperty =
        AvaloniaProperty.Register<VToggleButtonBase, IBrush>(nameof(VPressedBorderBrush));

    public static readonly StyledProperty<CornerRadius> VCornerRadiusProperty =
        AvaloniaProperty.Register<VToggleButtonBase, CornerRadius>(nameof(VCornerRadius), CornerRadius.Parse("8"));

    public static readonly StyledProperty<IBrush> VPressedForegroundProperty =
        AvaloniaProperty.Register<VToggleButtonBase, IBrush>(nameof(VPressedForeground));

    public static readonly StyledProperty<IBrush> VNormalForegroundProperty =
        AvaloniaProperty.Register<VToggleButtonBase, IBrush>(nameof(VNormalForeground));

    public static readonly StyledProperty<IBrush> VNormalBackgroundProperty =
        AvaloniaProperty.Register<VToggleButtonBase, IBrush>(nameof(VNormalBackground));

    public static readonly StyledProperty<IBrush> VPressedBackgroundProperty =
        AvaloniaProperty.Register<VToggleButtonBase, IBrush>(nameof(VPressedBackground));

    public static readonly DirectProperty<VToggleButtonBase, VButtonEffects> VButtonEffectProperty =
        AvaloniaProperty.RegisterDirect<VToggleButtonBase, VButtonEffects>(
            nameof(VButtonEffect),
            o => o.VButtonEffect,
            (o, v) => o.VButtonEffect = v);

    private VButtonEffects vbuttonEffect = VButtonEffects.None;

    public IBrush VPointoverBorderBrush
    {
        get => this.GetValue(VPointoverBorderBrushProperty);
        set => SetValue(VPointoverBorderBrushProperty, value);
    }

    public IBrush VPointoverForeground
    {
        get => this.GetValue(VPointoverForegroundProperty);
        set => SetValue(VPointoverForegroundProperty, value);
    }

    public IBrush VPointoverBackground
    {
        get => this.GetValue(VPointoverForegroundProperty);
        set => SetValue(VPointoverForegroundProperty, value);
    }

    public VButtonEffects VButtonEffect
    {
        get => vbuttonEffect;
        set => SetAndRaise(VButtonEffectProperty, ref vbuttonEffect, value);
    }

    public IBrush VPressedBackground
    {
        get => GetValue(VPressedBackgroundProperty);
        set => SetValue(VPressedBackgroundProperty, value);
    }

    public IBrush VPressedBorderBrush
    {
        get => GetValue(VPressedBorderBrushProperty);
        set => SetValue(VPressedBorderBrushProperty, value);
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

    public Thickness VPadding
    {
        get => GetValue(VPaddingProperty);
        set => SetValue(VPaddingProperty, value);
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

    public IBrush VPressedForeground
    {
        get => GetValue(VPressedForegroundProperty);
        set => SetValue(VPressedForegroundProperty, value);
    }

    public IBrush VNormalForeground
    {
        get => GetValue(VNormalForegroundProperty);
        set => SetValue(VNormalForegroundProperty, value);
    }
}