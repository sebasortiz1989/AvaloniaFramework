using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class VToggleButton : ToggleButton
{
    public static readonly StyledProperty<IBrush> VIconPressedForegroundProperty =
        AvaloniaProperty.Register<VToggleButton, IBrush>(nameof(VIconPressedForeground));

    public static readonly StyledProperty<IBrush> VIconNormalForegroundProperty =
        AvaloniaProperty.Register<VToggleButton, IBrush>(nameof(VIconNormalForeground));

    public static readonly StyledProperty<Thickness> VTextMarginProperty =
        AvaloniaProperty.Register<VToggleButton, Thickness>(nameof(VTextMargin));

    public static readonly StyledProperty<HorizontalAlignment> VTextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VToggleButton, HorizontalAlignment>(nameof(VTextHorizontalAlignment));

    public static readonly StyledProperty<VerticalAlignment> VTextVerticalAlignmentProperty =
        AvaloniaProperty.Register<VToggleButton, VerticalAlignment>(nameof(VTextVerticalAlignment));

    public static readonly StyledProperty<Thickness> VIconOneMarginProperty =
        AvaloniaProperty.Register<VToggleButton, Thickness>(nameof(VIconOneMargin));

    public static readonly StyledProperty<Thickness> VIconTwoMarginProperty =
        AvaloniaProperty.Register<VToggleButton, Thickness>(nameof(VIconTwoMargin));

    public static readonly StyledProperty<HorizontalAlignment> VIconOneHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VToggleButton, HorizontalAlignment>(nameof(VIconOneHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VIconTwoHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VToggleButton, HorizontalAlignment>(nameof(VIconTwoHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconOneVerticalAlignmentProperty =
        AvaloniaProperty.Register<VToggleButton, VerticalAlignment>(nameof(VIconOneVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconTwoVerticalAlignmentProperty =
        AvaloniaProperty.Register<VToggleButton, VerticalAlignment>(nameof(VIconTwoVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<Geometry?> VPressedIconTwoProperty =
        AvaloniaProperty.Register<VToggleButton, Geometry?>(nameof(VPressedIconTwo));

    public static readonly StyledProperty<Geometry?> VPressedIconOneProperty =
        AvaloniaProperty.Register<VToggleButton, Geometry?>(nameof(VPressedIconOne));

    public static readonly StyledProperty<Geometry?> VNormalIconTwoProperty =
        AvaloniaProperty.Register<VToggleButton, Geometry?>(nameof(VNormalIconTwo));

    public static readonly StyledProperty<Geometry?> VNormalIconOneProperty =
        AvaloniaProperty.Register<VToggleButton, Geometry?>(nameof(VNormalIconOne));

    public static readonly StyledProperty<Thickness> VPaddingProperty =
        AvaloniaProperty.Register<VToggleButton, Thickness>(nameof(VPadding));

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<VToggleButton, Thickness>(nameof(VBorderThickness));

    public static readonly StyledProperty<IBrush> VNormalBorderBrushProperty =
        AvaloniaProperty.Register<VToggleButton, IBrush>(nameof(VNormalBorderBrush));

    public static readonly StyledProperty<IBrush> VPressedBorderBrushProperty =
        AvaloniaProperty.Register<VToggleButton, IBrush>(nameof(VPressedBorderBrush));

    public static readonly StyledProperty<CornerRadius> VCornerRadiusProperty =
        AvaloniaProperty.Register<VToggleButton, CornerRadius>(nameof(VCornerRadius), CornerRadius.Parse("8"));

    public static readonly StyledProperty<IBrush> VPressedForegroundProperty =
        AvaloniaProperty.Register<VToggleButton, IBrush>(nameof(VPressedForeground));

    public static readonly StyledProperty<IBrush> VNormalForegroundProperty =
        AvaloniaProperty.Register<VToggleButton, IBrush>(nameof(VNormalForeground));

    public static readonly StyledProperty<IBrush> VNormalBackgroundProperty =
        AvaloniaProperty.Register<VToggleButton, IBrush>(nameof(VNormalBackground));

    public static readonly StyledProperty<IBrush> VPressedBackgroundProperty =
        AvaloniaProperty.Register<VToggleButton, IBrush>(nameof(VPressedBackground));

    public static readonly StyledProperty<Thickness> VImageTwoMarginProperty =
        AvaloniaProperty.Register<VToggleButton, Thickness>(nameof(VImageTwoMargin));

    public static readonly StyledProperty<Thickness> VImageOneMarginProperty =
        AvaloniaProperty.Register<VToggleButton, Thickness>(nameof(VImageOneMargin));

    public static readonly StyledProperty<HorizontalAlignment> VImageTwoHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VToggleButton, HorizontalAlignment>(nameof(VImageTwoHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VImageOneHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VToggleButton, HorizontalAlignment>(nameof(VImageOneHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VImageTwoVerticalAlignmentProperty =
        AvaloniaProperty.Register<VToggleButton, VerticalAlignment>(nameof(VImageTwoVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VImageOneVerticalAlignmentProperty =
        AvaloniaProperty.Register<VToggleButton, VerticalAlignment>(nameof(VImageOneVerticalAlignment), VerticalAlignment.Center);

    public static readonly DirectProperty<VToggleButton, IImage?> VPressedImageOneProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, IImage?>(
            "VPressedImageOne",
            o => o.VPressedImageOne,
            (o, v) => o.VPressedImageOne = v);

    public static readonly DirectProperty<VToggleButton, IImage?> VPressedImageTwoProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, IImage?>(
            "VPressedImageTwo",
            o => o.VPressedImageTwo,
            (o, v) => o.VPressedImageTwo = v);

    public static readonly DirectProperty<VToggleButton, IImage?> VNormalImageOneProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, IImage?>(
            "VNormalImageOne",
            o => o.VNormalImageOne,
            (o, v) => o.VNormalImageOne = v);

    public static readonly DirectProperty<VToggleButton, IImage?> VNormalImageTwoProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, IImage?>(
            "VNormalImageTwo",
            o => o.VNormalImageTwo,
            (o, v) => o.VNormalImageTwo = v);

    public static readonly DirectProperty<VToggleButton, double> VImageOneWidthProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, double>(
            nameof(VImageOneWidth),
            o => o.VImageOneWidth,
            (o, v) => o.VImageOneWidth = v);

    public static readonly DirectProperty<VToggleButton, double> VImageTwoWidthProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, double>(
            nameof(VImageTwoWidth),
            o => o.VImageTwoWidth,
            (o, v) => o.VImageTwoWidth = v);

    public static readonly DirectProperty<VToggleButton, double> VImageOneHeightProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, double>(
            nameof(VImageOneHeight),
            o => o.VImageOneHeight,
            (o, v) => o.VImageOneHeight = v);

    public static readonly DirectProperty<VToggleButton, double> VImageTwoHeightProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, double>(
            nameof(VImageTwoHeight),
            o => o.VImageTwoHeight,
            (o, v) => o.VImageTwoHeight = v);

    public static readonly DirectProperty<VToggleButton, double> VIconTwoHeightProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, double>(
            "VIconTwoHeight",
            o => o.VIconTwoHeight,
            (o, v) => o.VIconTwoHeight = v);

    public static readonly DirectProperty<VToggleButton, VButtonEffects> VButtonEffectProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, VButtonEffects>(
            nameof(VButtonEffect),
            o => o.VButtonEffect,
            (o, v) => o.VButtonEffect = v);

    public static readonly DirectProperty<VToggleButton, string?> VTextCheckedProperty =
    AvaloniaProperty.RegisterDirect<VToggleButton, string?>(
        nameof(VTextChecked),
        o => o.VTextChecked,
        (o, v) => o.VTextChecked = v);

    public static readonly DirectProperty<VToggleButton, string?> VTextUncheckedProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, string?>(
            nameof(VTextUnchecked),
            o => o.VTextUnchecked,
            (o, v) => o.VTextUnchecked = v);

    public static readonly DirectProperty<VToggleButton, FontWeight> VFontWeightProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, FontWeight>(
            nameof(VFontWeight),
            o => o.VFontWeight,
            (o, v) => o.VFontWeight = v);

    public static readonly DirectProperty<VToggleButton, double> VFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, double>(
            nameof(VFontSize),
            o => o.VFontSize,
            (o, v) => o.VFontSize = v);

    public static readonly DirectProperty<VToggleButton, double> VIconTwoWidthProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, double>(
            nameof(VIconTwoWidth),
            o => o.VIconTwoWidth,
            (o, v) => o.VIconTwoWidth = v);

    public static readonly DirectProperty<VToggleButton, double> VIconOneWidthProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, double>(
            nameof(VIconOneWidth),
            o => o.VIconOneWidth,
            (o, v) => o.VIconOneWidth = v);

    public static readonly DirectProperty<VToggleButton, double> VIconOneHeightProperty =
        AvaloniaProperty.RegisterDirect<VToggleButton, double>(
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

    private double vFontSize = 22;

    private FontWeight vFontWeight = FontWeight.Bold;

    private string? vTextChecked = string.Empty;

    private string? vTextUnchecked = string.Empty;

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

    public string? VTextChecked
    {
        get => vTextChecked;
        set => SetAndRaise(VTextCheckedProperty, ref vTextChecked, value);
    }

    public string? VTextUnchecked
    {
        get => vTextUnchecked;
        set => SetAndRaise(VTextUncheckedProperty, ref vTextUnchecked, value);
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
        get => GetValue(VPressedIconOneProperty);
        set => SetValue(VPressedIconOneProperty, value);
    }

    public Geometry? VNormalIconTwo
    {
        get => GetValue(VPressedIconTwoProperty);
        set => SetValue(VPressedIconTwoProperty, value);
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

        if (string.IsNullOrEmpty(VTextChecked))
        {
            var t = e.NameScope.Find<TextBlock>("PART_MainText");
            if (t != null)
                t.IsEnabled = false;
        }
    }
}