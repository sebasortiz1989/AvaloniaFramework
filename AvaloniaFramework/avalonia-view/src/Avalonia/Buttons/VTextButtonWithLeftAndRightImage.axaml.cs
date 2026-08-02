using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class VTextButtonWithLeftAndRightImage : VButtonBase
{
    public static readonly StyledProperty<Thickness> VTextMarginProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, Thickness>(nameof(VTextMargin));

    public static readonly StyledProperty<HorizontalAlignment> VTextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, HorizontalAlignment>(nameof(VTextHorizontalAlignment));

    public static readonly StyledProperty<VerticalAlignment> VTextVerticalAlignmentProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, VerticalAlignment>(nameof(VTextVerticalAlignment));

    public static readonly StyledProperty<Thickness> VRightImageMarginProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, Thickness>(nameof(VRightImageMargin));

    public static readonly StyledProperty<Thickness> VLeftImageMarginProperty =
    AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, Thickness>(nameof(VLeftImageMargin));

    public static readonly StyledProperty<HorizontalAlignment> VRightImageHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, HorizontalAlignment>(nameof(VRightImageHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VLeftImageHorizontalAlignmentProperty =
    AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, HorizontalAlignment>(nameof(VLeftImageHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VRightImageVerticalAlignmentProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, VerticalAlignment>(nameof(VRightImageVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VLeftImageVerticalAlignmentProperty =
    AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, VerticalAlignment>(nameof(VLeftImageVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<IImage?> VRightPressedImageProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, IImage?>(nameof(VRightPressedImage));

    public static readonly StyledProperty<IImage?> VLeftPressedImageProperty =
    AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, IImage?>(nameof(VLeftPressedImage));

    public static readonly StyledProperty<IImage?> VRightNormalImageProperty =
        AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, IImage?>(nameof(VRightNormalImage));

    public static readonly StyledProperty<IImage?> VLeftNormalImageProperty =
    AvaloniaProperty.Register<VTextButtonWithLeftAndRightImage, IImage?>(nameof(VLeftNormalImage));

    public static readonly DirectProperty<VTextButtonWithLeftAndRightImage, string?> VTextProperty =
    AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightImage, string?>(
        nameof(VText),
        o => o.VText,
        (o, v) => o.VText = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightImage, FontWeight> VFontWeightProperty =
        AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightImage, FontWeight>(
            nameof(VFontWeight),
            o => o.VFontWeight,
            (o, v) => o.VFontWeight = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightImage, double> VFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightImage, double>(
            nameof(VFontSize),
            o => o.VFontSize,
            (o, v) => o.VFontSize = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightImage, double> VLeftImageWidthProperty =
    AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightImage, double>(
        nameof(VLeftImageWidth),
        o => o.VLeftImageWidth,
        (o, v) => o.VLeftImageWidth = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightImage, double> VRightImageWidthProperty =
    AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightImage, double>(
        nameof(VRightImageWidth),
        o => o.VRightImageWidth,
        (o, v) => o.VRightImageWidth = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightImage, double> VLeftImageHeightProperty =
        AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightImage, double>(
            nameof(VLeftImageHeight),
            o => o.VLeftImageHeight,
            (o, v) => o.VLeftImageHeight = v);

    public static readonly DirectProperty<VTextButtonWithLeftAndRightImage, double> VRightImageHeightProperty =
    AvaloniaProperty.RegisterDirect<VTextButtonWithLeftAndRightImage, double>(
        nameof(VRightImageHeight),
        o => o.VRightImageHeight,
        (o, v) => o.VRightImageHeight = v);

    private double vLeftImageWidth;

    private double vRightImageWidth;

    private double vLeftImageHeight;

    private double vRightImageHeight;

    private double vFontSize = 22;

    private FontWeight vFontWeight = FontWeight.Bold;

    private string? vText = string.Empty;

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

    public double VRightImageHeight
    {
        get => vRightImageHeight;
        set => SetAndRaise(VRightImageHeightProperty, ref vRightImageHeight, value);
    }

    public double VLeftImageHeight
    {
        get => vLeftImageHeight;
        set => SetAndRaise(VLeftImageHeightProperty, ref vLeftImageHeight, value);
    }

    public double VRightImageWidth
    {
        get => vRightImageWidth;
        set => SetAndRaise(VRightImageWidthProperty, ref vRightImageWidth, value);
    }

    public double VLeftImageWidth
    {
        get => vLeftImageWidth;
        set => SetAndRaise(VLeftImageWidthProperty, ref vLeftImageWidth, value);
    }

    public Thickness VRightImageMargin
    {
        get => GetValue(VRightImageMarginProperty);
        set => SetValue(VRightImageMarginProperty, value);
    }

    public Thickness VLeftImageMargin
    {
        get => GetValue(VLeftImageMarginProperty);
        set => SetValue(VLeftImageMarginProperty, value);
    }

    public HorizontalAlignment VRightImageHorizontalAlignment
    {
        get => GetValue(VRightImageHorizontalAlignmentProperty);
        set => SetValue(VRightImageHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VLeftImageHorizontalAlignment
    {
        get => GetValue(VLeftImageHorizontalAlignmentProperty);
        set => SetValue(VLeftImageHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VRightImageVerticalAlignment
    {
        get => GetValue(VRightImageVerticalAlignmentProperty);
        set => SetValue(VRightImageVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VLeftImageVerticalAlignment
    {
        get => GetValue(VLeftImageVerticalAlignmentProperty);
        set => SetValue(VLeftImageVerticalAlignmentProperty, value);
    }

    public IImage? VLeftPressedImage
    {
        get => GetValue(VLeftPressedImageProperty);
        set => SetValue(VLeftPressedImageProperty, value);
    }

    public IImage? VRightPressedImage
    {
        get => GetValue(VRightPressedImageProperty);
        set => SetValue(VRightPressedImageProperty, value);
    }

    public IImage? VLeftNormalImage
    {
        get => GetValue(VLeftPressedImageProperty);
        set => SetValue(VLeftPressedImageProperty, value);
    }

    public IImage? VRightNormalImage
    {
        get => GetValue(VRightPressedImageProperty);
        set => SetValue(VRightPressedImageProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        VLeftPressedImage ??= VLeftNormalImage;
        VRightPressedImage ??= VRightNormalImage;
    }
}