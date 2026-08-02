using Avalonia;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class VTextButton : VButtonBase
{
    public static readonly StyledProperty<Thickness> VTextMarginProperty =
        AvaloniaProperty.Register<VTextButton, Thickness>(nameof(VTextMargin));

    public static readonly StyledProperty<HorizontalAlignment> VTextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<VTextButton, HorizontalAlignment>(nameof(VTextHorizontalAlignment));

    public static readonly StyledProperty<VerticalAlignment> VTextVerticalAlignmentProperty =
        AvaloniaProperty.Register<VTextButton, VerticalAlignment>(nameof(VTextVerticalAlignment));

    public static readonly DirectProperty<VTextButton, string?> VTextProperty =
        AvaloniaProperty.RegisterDirect<VTextButton, string?>(
            nameof(VText),
            o => o.VText,
            (o, v) => o.VText = v);

    public static readonly DirectProperty<VTextButton, FontWeight> VFontWeightProperty =
        AvaloniaProperty.RegisterDirect<VTextButton, FontWeight>(
            nameof(VFontWeight),
            o => o.VFontWeight,
            (o, v) => o.VFontWeight = v);

    public static readonly DirectProperty<VTextButton, double> VFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VTextButton, double>(
            nameof(VFontSize),
            o => o.VFontSize,
            (o, v) => o.VFontSize = v);

    private double vFontSize = 22;

    private FontWeight vFontWeight = FontWeight.Bold;

    private string? vText = string.Empty;

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
}