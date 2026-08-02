using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class VButtonBase : Button
{
    public static readonly StyledProperty<IBrush> VPointoverBorderBrushProperty =
        AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VPointoverBorderBrush));

    public static readonly StyledProperty<IBrush> VPointoverForegroundProperty =
        AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VPointoverForeground));

    public static readonly StyledProperty<IBrush> VPointoverBackgroundProperty =
        AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VPointoverBackground));

    public static readonly StyledProperty<Thickness> VPaddingProperty =
        AvaloniaProperty.Register<VButtonBase, Thickness>(nameof(VPadding));

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<VButtonBase, Thickness>(nameof(VBorderThickness));

    public static readonly StyledProperty<IBrush> VNormalBorderBrushProperty =
        AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VNormalBorderBrush));

    public static readonly StyledProperty<IBrush> VPressedBorderBrushProperty =
        AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VPressedBorderBrush));

    public static readonly StyledProperty<CornerRadius> VCornerRadiusProperty =
        AvaloniaProperty.Register<VButtonBase, CornerRadius>(nameof(VCornerRadius), CornerRadius.Parse("8"));

    public static readonly StyledProperty<IBrush> VPressedForegroundProperty =
        AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VPressedForeground));

    public static readonly StyledProperty<IBrush> VNormalForegroundProperty =
        AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VNormalForeground));

    public static readonly StyledProperty<IBrush> VNormalBackgroundProperty =
        AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VNormalBackground));

    public static readonly StyledProperty<IBrush> VPressedBackgroundProperty =
        AvaloniaProperty.Register<VButtonBase, IBrush>(nameof(VPressedBackground));

    public static readonly DirectProperty<VButtonBase, VButtonEffects> VButtonEffectProperty =
        AvaloniaProperty.RegisterDirect<VButtonBase, VButtonEffects>(
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