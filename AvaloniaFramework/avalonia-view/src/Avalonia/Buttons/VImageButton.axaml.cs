using Avalonia;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class VImageButton : VButtonBase
{
    public static readonly StyledProperty<Thickness> VImageMarginProperty =
        AvaloniaProperty.Register<VImageButton, Thickness>(nameof(VImageMargin));

    public static readonly StyledProperty<HorizontalAlignment> VImageHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VImageButton, HorizontalAlignment>(nameof(VImageHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VImageVerticalAlignmentProperty =
        AvaloniaProperty.Register<VImageButton, VerticalAlignment>(nameof(VImageVerticalAlignment), VerticalAlignment.Center);

    public static readonly DirectProperty<VImageButton, IImage?> VSourceImageProperty = AvaloniaProperty.RegisterDirect<VImageButton, IImage?>(
        "VSourceImage", o => o.VSourceImage, (o, v) => o.VSourceImage = v);

    public static readonly DirectProperty<VImageButton, double> VImageWidthProperty =
    AvaloniaProperty.RegisterDirect<VImageButton, double>(
        nameof(VImageWidth),
        o => o.VImageWidth,
        (o, v) => o.VImageWidth = v);

    public static readonly DirectProperty<VImageButton, double> VImageHeightProperty =
        AvaloniaProperty.RegisterDirect<VImageButton, double>(
            nameof(VImageHeight),
            o => o.VImageHeight,
            (o, v) => o.VImageHeight = v);

    private double vImageWidth;

    private double vImageHeight;

    private IImage? vSourceImage;

    public IImage? VSourceImage
    {
        get => vSourceImage;
        set => SetAndRaise(VSourceImageProperty, ref vSourceImage, value);
    }

    public double VImageHeight
    {
        get => vImageHeight;
        set => SetAndRaise(VImageHeightProperty, ref vImageHeight, value);
    }

    public double VImageWidth
    {
        get => vImageWidth;
        set => SetAndRaise(VImageWidthProperty, ref vImageWidth, value);
    }

    public Thickness VImageMargin
    {
        get => GetValue(VImageMarginProperty);
        set => SetValue(VImageMarginProperty, value);
    }

    public HorizontalAlignment VImageHorizontalAlignment
    {
        get => GetValue(VImageHorizontalAlignmentProperty);
        set => SetValue(VImageHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VImageVerticalAlignment
    {
        get => GetValue(VImageVerticalAlignmentProperty);
        set => SetValue(VImageVerticalAlignmentProperty, value);
    }
}