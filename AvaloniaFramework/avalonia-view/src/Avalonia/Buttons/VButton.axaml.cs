using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class VButton : Button
{
    public static readonly StyledProperty<IBrush> VIconPressedForegroundProperty =
        AvaloniaProperty.Register<VButton, IBrush>(nameof(VIconPressedForeground));

    public static readonly StyledProperty<IBrush> VIconNormalForegroundProperty =
        AvaloniaProperty.Register<VButton, IBrush>(nameof(VIconNormalForeground));

    public static readonly StyledProperty<Thickness> VTextMarginProperty =
        AvaloniaProperty.Register<VButton, Thickness>(nameof(VTextMargin));

    public static readonly StyledProperty<HorizontalAlignment> VTextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VButton, HorizontalAlignment>(nameof(VTextHorizontalAlignment));

    public static readonly StyledProperty<VerticalAlignment> VTextVerticalAlignmentProperty =
        AvaloniaProperty.Register<VButton, VerticalAlignment>(nameof(VTextVerticalAlignment));

    public static readonly StyledProperty<Thickness> VIconOneMarginProperty =
        AvaloniaProperty.Register<VButton, Thickness>(nameof(VIconOneMargin));

    public static readonly StyledProperty<Thickness> VIconTwoMarginProperty =
        AvaloniaProperty.Register<VButton, Thickness>(nameof(VIconTwoMargin));

    public static readonly StyledProperty<HorizontalAlignment> VIconOneHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VButton, HorizontalAlignment>(nameof(VIconOneHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VIconTwoHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VButton, HorizontalAlignment>(nameof(VIconTwoHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconOneVerticalAlignmentProperty =
        AvaloniaProperty.Register<VButton, VerticalAlignment>(nameof(VIconOneVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconTwoVerticalAlignmentProperty =
        AvaloniaProperty.Register<VButton, VerticalAlignment>(nameof(VIconTwoVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<Geometry?> VPressedIconTwoProperty =
        AvaloniaProperty.Register<VButton, Geometry?>(nameof(VPressedIconTwo));

    public static readonly StyledProperty<Geometry?> VPressedIconOneProperty =
        AvaloniaProperty.Register<VButton, Geometry?>(nameof(VPressedIconOne));

    public static readonly StyledProperty<Geometry?> VNormalIconTwoProperty =
        AvaloniaProperty.Register<VButton, Geometry?>(nameof(VNormalIconTwo));

    public static readonly StyledProperty<Geometry?> VNormalIconOneProperty =
        AvaloniaProperty.Register<VButton, Geometry?>(nameof(VNormalIconOne));

    public static readonly StyledProperty<Thickness> VPaddingProperty =
        AvaloniaProperty.Register<VButton, Thickness>(nameof(VPadding));

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<VButton, Thickness>(nameof(VBorderThickness));

    public static readonly StyledProperty<IBrush> VNormalBorderBrushProperty =
        AvaloniaProperty.Register<VButton, IBrush>(nameof(VNormalBorderBrush));

    public static readonly StyledProperty<IBrush> VPressedBorderBrushProperty =
        AvaloniaProperty.Register<VButton, IBrush>(nameof(VPressedBorderBrush));

    public static readonly StyledProperty<CornerRadius> VCornerRadiusProperty =
        AvaloniaProperty.Register<VButton, CornerRadius>(nameof(VCornerRadius), CornerRadius.Parse("8"));

    public static readonly StyledProperty<IBrush> VPressedForegroundProperty =
        AvaloniaProperty.Register<VButton, IBrush>(nameof(VPressedForeground));

    public static readonly StyledProperty<IBrush> VNormalForegroundProperty =
        AvaloniaProperty.Register<VButton, IBrush>(nameof(VNormalForeground));

    public static readonly StyledProperty<IBrush> VNormalBackgroundProperty =
        AvaloniaProperty.Register<VButton, IBrush>(nameof(VNormalBackground));

    public static readonly StyledProperty<IBrush> VPressedBackgroundProperty =
        AvaloniaProperty.Register<VButton, IBrush>(nameof(VPressedBackground));

    public static readonly StyledProperty<Thickness> VImageTwoMarginProperty =
        AvaloniaProperty.Register<VButton, Thickness>(nameof(VImageTwoMargin));

    public static readonly StyledProperty<Thickness> VImageOneMarginProperty =
        AvaloniaProperty.Register<VButton, Thickness>(nameof(VImageOneMargin));

    public static readonly StyledProperty<HorizontalAlignment> VImageTwoHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VButton, HorizontalAlignment>(nameof(VImageTwoHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VImageOneHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VButton, HorizontalAlignment>(nameof(VImageOneHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VImageTwoVerticalAlignmentProperty =
        AvaloniaProperty.Register<VButton, VerticalAlignment>(nameof(VImageTwoVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VImageOneVerticalAlignmentProperty =
        AvaloniaProperty.Register<VButton, VerticalAlignment>(nameof(VImageOneVerticalAlignment), VerticalAlignment.Center);

    public static readonly DirectProperty<VButton, IImage?> VPressedImageOneProperty =
        AvaloniaProperty.RegisterDirect<VButton, IImage?>(
            "VPressedImageOne",
            o => o.VPressedImageOne,
            (o, v) => o.VPressedImageOne = v);

    public static readonly DirectProperty<VButton, IImage?> VPressedImageTwoProperty =
        AvaloniaProperty.RegisterDirect<VButton, IImage?>(
            "VPressedImageTwo",
            o => o.VPressedImageTwo,
            (o, v) => o.VPressedImageTwo = v);

    public static readonly DirectProperty<VButton, IImage?> VNormalImageOneProperty =
        AvaloniaProperty.RegisterDirect<VButton, IImage?>(
            "VNormalImageOne",
            o => o.VNormalImageOne,
            (o, v) => o.VNormalImageOne = v);

    public static readonly DirectProperty<VButton, IImage?> VNormalImageTwoProperty =
        AvaloniaProperty.RegisterDirect<VButton, IImage?>(
            "VNormalImageTwo",
            o => o.VNormalImageTwo,
            (o, v) => o.VNormalImageTwo = v);

    public static readonly DirectProperty<VButton, double> VImageOneWidthProperty =
        AvaloniaProperty.RegisterDirect<VButton, double>(
            nameof(VImageOneWidth),
            o => o.VImageOneWidth,
            (o, v) => o.VImageOneWidth = v);

    public static readonly DirectProperty<VButton, double> VImageTwoWidthProperty =
        AvaloniaProperty.RegisterDirect<VButton, double>(
            nameof(VImageTwoWidth),
            o => o.VImageTwoWidth,
            (o, v) => o.VImageTwoWidth = v);

    public static readonly DirectProperty<VButton, double> VImageOneHeightProperty =
        AvaloniaProperty.RegisterDirect<VButton, double>(
            nameof(VImageOneHeight),
            o => o.VImageOneHeight,
            (o, v) => o.VImageOneHeight = v);

    public static readonly DirectProperty<VButton, double> VImageTwoHeightProperty =
        AvaloniaProperty.RegisterDirect<VButton, double>(
            nameof(VImageTwoHeight),
            o => o.VImageTwoHeight,
            (o, v) => o.VImageTwoHeight = v);

    public static readonly DirectProperty<VButton, double> VIconTwoHeightProperty =
        AvaloniaProperty.RegisterDirect<VButton, double>(
            "VIconTwoHeight",
            o => o.VIconTwoHeight,
            (o, v) => o.VIconTwoHeight = v);

    public static readonly DirectProperty<VButton, VButtonEffects> VButtonEffectProperty =
        AvaloniaProperty.RegisterDirect<VButton, VButtonEffects>(
            nameof(VButtonEffect),
            o => o.VButtonEffect,
            (o, v) => o.VButtonEffect = v);

    public static readonly DirectProperty<VButton, string?> VTextProperty =
    AvaloniaProperty.RegisterDirect<VButton, string?>(
        nameof(VText),
        o => o.VText,
        (o, v) => o.VText = v);

    public static readonly StyledProperty<FontWeight> VFontWeightProperty =
        AvaloniaProperty.Register<VButton, FontWeight>(nameof(VFontWeight), FontWeight.Bold);

    public static readonly StyledProperty<double> VFontSizeProperty =
        AvaloniaProperty.Register<VButton, double>(nameof(VFontSize), 32);

    public static readonly DirectProperty<VButton, double> VIconTwoWidthProperty =
        AvaloniaProperty.RegisterDirect<VButton, double>(
            nameof(VIconTwoWidth),
            o => o.VIconTwoWidth,
            (o, v) => o.VIconTwoWidth = v);

    public static readonly DirectProperty<VButton, double> VIconOneWidthProperty =
        AvaloniaProperty.RegisterDirect<VButton, double>(
            nameof(VIconOneWidth),
            o => o.VIconOneWidth,
            (o, v) => o.VIconOneWidth = v);

    public static readonly DirectProperty<VButton, double> VIconOneHeightProperty =
        AvaloniaProperty.RegisterDirect<VButton, double>(
            nameof(VIconOneHeight),
            o => o.VIconOneHeight,
            (o, v) => o.VIconOneHeight = v);

    private IImage? vPressedImageTwo;

    private IImage? vNormalImageOne;

    private IImage? vNormalImageTwo;

    private IImage? vPressedImageOne;

    private double vImageOneWidth;

    private double vImageTwoWidth;

    private double vImageOneHeight;

    private double vImageTwoHeight;

    private double vIconTwoHeight;

    private VButtonEffects vButtonEffect = VButtonEffects.None;

    private double vIconTwoWidth;

    private double vIconOneWidth;

    private double vIconOneHeight;

    private string? vText = string.Empty;

    public IImage? VPressedImageOne
    {
        get => vPressedImageOne;
        set => SetAndRaise(VPressedImageOneProperty, ref vPressedImageOne, value);
    }

    public IImage? VPressedImageTwo
    {
        get => vPressedImageTwo;
        set => SetAndRaise(VPressedImageTwoProperty, ref vPressedImageTwo, value);
    }

    public IImage? VNormalImageOne
    {
        get => vNormalImageOne;
        set => SetAndRaise(VNormalImageOneProperty, ref vNormalImageOne, value);
    }

    public IImage? VNormalImageTwo
    {
        get => vNormalImageTwo;
        set => SetAndRaise(VNormalImageTwoProperty, ref vNormalImageTwo, value);
    }

    public double VImageTwoHeight
    {
        get => vImageTwoHeight;
        set => SetAndRaise(VImageTwoHeightProperty, ref vImageTwoHeight, value);
    }

    public double VImageOneHeight
    {
        get => vImageOneHeight;
        set => SetAndRaise(VImageOneHeightProperty, ref vImageOneHeight, value);
    }

    public double VImageTwoWidth
    {
        get => vImageTwoWidth;
        set => SetAndRaise(VImageTwoWidthProperty, ref vImageTwoWidth, value);
    }

    public double VImageOneWidth
    {
        get => vImageOneWidth;
        set => SetAndRaise(VImageOneWidthProperty, ref vImageOneWidth, value);
    }

    public Thickness VImageTwoMargin
    {
        get => GetValue(VImageTwoMarginProperty);
        set => SetValue(VImageTwoMarginProperty, value);
    }

    public Thickness VImageOneMargin
    {
        get => GetValue(VImageOneMarginProperty);
        set => SetValue(VImageOneMarginProperty, value);
    }

    public HorizontalAlignment VImageTwoHorizontalAlignment
    {
        get => GetValue(VImageTwoHorizontalAlignmentProperty);
        set => SetValue(VImageTwoHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VImageOneHorizontalAlignment
    {
        get => GetValue(VImageOneHorizontalAlignmentProperty);
        set => SetValue(VImageOneHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VImageTwoVerticalAlignment
    {
        get => GetValue(VImageTwoVerticalAlignmentProperty);
        set => SetValue(VImageTwoVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VImageOneVerticalAlignment
    {
        get => GetValue(VImageOneVerticalAlignmentProperty);
        set => SetValue(VImageOneVerticalAlignmentProperty, value);
    }

    public double VIconTwoHeight
    {
        get => vIconTwoHeight;
        set => SetAndRaise(VIconTwoHeightProperty, ref vIconTwoHeight, value);
    }

    public VButtonEffects VButtonEffect
    {
        get => vButtonEffect;
        set => SetAndRaise(VButtonEffectProperty, ref vButtonEffect, value);
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
        get => GetValue(VFontWeightProperty);
        set => SetValue(VFontWeightProperty, value);
    }

    public double VFontSize
    {
        get => GetValue(VFontSizeProperty);
        set => SetValue(VFontSizeProperty, value);
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

    public double VIconOneHeight
    {
        get => vIconOneHeight;
        set => SetAndRaise(VIconOneHeightProperty, ref vIconOneHeight, value);
    }

    public double VIconOneWidth
    {
        get => vIconOneWidth;
        set => SetAndRaise(VIconOneWidthProperty, ref vIconOneWidth, value);
    }

    public double VIconTwoWidth
    {
        get => vIconTwoWidth;
        set => SetAndRaise(VIconTwoWidthProperty, ref vIconTwoWidth, value);
    }

    public Thickness VIconOneMargin
    {
        get => GetValue(VIconOneMarginProperty);
        set => SetValue(VIconOneMarginProperty, value);
    }

    public Thickness VIconTwoMargin
    {
        get => GetValue(VIconTwoMarginProperty);
        set => SetValue(VIconTwoMarginProperty, value);
    }

    public HorizontalAlignment VIconOneHorizontalAlignment
    {
        get => GetValue(VIconOneHorizontalAlignmentProperty);
        set => SetValue(VIconOneHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VIconTwoHorizontalAlignment
    {
        get => GetValue(VIconTwoHorizontalAlignmentProperty);
        set => SetValue(VIconTwoHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VIconOneVerticalAlignment
    {
        get => GetValue(VIconOneVerticalAlignmentProperty);
        set => SetValue(VIconOneVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VIconTwoVerticalAlignment
    {
        get => GetValue(VIconTwoVerticalAlignmentProperty);
        set => SetValue(VIconTwoVerticalAlignmentProperty, value);
    }

    public Geometry? VPressedIconOne
    {
        get => GetValue(VPressedIconOneProperty);
        set => SetValue(VPressedIconOneProperty, value);
    }

    public Geometry? VPressedIconTwo
    {
        get => GetValue(VPressedIconTwoProperty);
        set => SetValue(VPressedIconTwoProperty, value);
    }

    public Geometry? VNormalIconOne
    {
        get => GetValue(VNormalIconOneProperty);
        set => SetValue(VNormalIconOneProperty, value);
    }

    public Geometry? VNormalIconTwo
    {
        get => GetValue(VNormalIconTwoProperty);
        set => SetValue(VNormalIconTwoProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (e == null)
        {
            return;
        }

        if (VNormalIconOne == null)
        {
            var icon = e.NameScope.Find<PathIcon>("PART_IconOne");
            if (icon != null)
                icon.IsEnabled = false;
        }

        if (VNormalIconTwo == null)
        {
            var icon = e.NameScope.Find<PathIcon>("PART_IconTwo");
            if (icon != null)
                icon.IsEnabled = false;
        }

        if (VNormalImageOne == null)
        {
            var image = e.NameScope.Find<Image>("PART_ImageOne");
            if (image != null)
                image.IsEnabled = false;
        }

        if (VPressedImageOne == null)
        {
            var image = e.NameScope.Find<Image>("PART_ImageTwo");
            if (image != null)
                image.IsEnabled = false;
        }

        if (string.IsNullOrEmpty(VText))
        {
            var t = e.NameScope.Find<TextBlock>("PART_MainText");
            if (t != null)
                t.IsEnabled = false;
        }
    }
}