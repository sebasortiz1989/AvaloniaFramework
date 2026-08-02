using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Cards;

public class SimpleInfoCard : TemplatedControl
{
    public static readonly DirectProperty<SimpleInfoCard, double> IconWidthProperty =
       AvaloniaProperty.RegisterDirect<SimpleInfoCard, double>(
       nameof(IconWidth),
       o => o.IconWidth,
       (o, v) => o.IconWidth = v);

    public static readonly DirectProperty<SimpleInfoCard, double> IconHeightProperty =
       AvaloniaProperty.RegisterDirect<SimpleInfoCard, double>(
       nameof(IconHeight),
       o => o.IconHeight,
       (o, v) => o.IconHeight = v);

    public static readonly DirectProperty<SimpleInfoCard, double> ImageWidthProperty =
      AvaloniaProperty.RegisterDirect<SimpleInfoCard, double>(
      nameof(ImageWidth),
      o => o.ImageWidth,
      (o, v) => o.ImageWidth = v);

    public static readonly DirectProperty<SimpleInfoCard, double> ImageHeightProperty =
       AvaloniaProperty.RegisterDirect<SimpleInfoCard, double>(
       nameof(ImageHeight),
       o => o.ImageHeight,
       (o, v) => o.ImageHeight = v);

    public static readonly DirectProperty<SimpleInfoCard, double> FirstTextFontSizeProperty =
       AvaloniaProperty.RegisterDirect<SimpleInfoCard, double>(
       nameof(FirstTextFontSize),
       o => o.FirstTextFontSize,
       (o, v) => o.FirstTextFontSize = v);

    public static readonly DirectProperty<SimpleInfoCard, double> SecondTextFontSizeProperty =
       AvaloniaProperty.RegisterDirect<SimpleInfoCard, double>(
       nameof(SecondTextFontSize),
       o => o.SecondTextFontSize,
       (o, v) => o.SecondTextFontSize = v);

    public static readonly DirectProperty<SimpleInfoCard, double> DescriptionTextFontSizeProperty =
       AvaloniaProperty.RegisterDirect<SimpleInfoCard, double>(
       nameof(DescriptionTextFontSize),
       o => o.DescriptionTextFontSize,
       (o, v) => o.DescriptionTextFontSize = v);

    public static readonly DirectProperty<SimpleInfoCard, string?> FirstTextProperty =
        AvaloniaProperty.RegisterDirect<SimpleInfoCard, string?>(
        nameof(FirstText),
        o => o.FirstText,
        (o, v) => o.FirstText = v);

    public static readonly DirectProperty<SimpleInfoCard, string?> SecondTextProperty =
       AvaloniaProperty.RegisterDirect<SimpleInfoCard, string?>(
       nameof(SecondText),
       o => o.SecondText,
       (o, v) => o.SecondText = v);

    public static readonly DirectProperty<SimpleInfoCard, string?> DescriptionTextProperty =
      AvaloniaProperty.RegisterDirect<SimpleInfoCard, string?>(
      nameof(DescriptionText),
      o => o.DescriptionText,
      (o, v) => o.DescriptionText = v);

    public static readonly DirectProperty<SimpleInfoCard, bool> IsIconVisibleProperty =
     AvaloniaProperty.RegisterDirect<SimpleInfoCard, bool>(
     nameof(IsIconVisible),
     o => o.IsIconVisible,
     (o, v) => o.IsIconVisible = v);

    public static readonly DirectProperty<SimpleInfoCard, bool> IsImageVisibleProperty =
     AvaloniaProperty.RegisterDirect<SimpleInfoCard, bool>(
     nameof(IsImageVisible),
     o => o.IsImageVisible,
     (o, v) => o.IsImageVisible = v);

    public static readonly StyledProperty<IBrush> IconForegroundProperty =
        AvaloniaProperty.Register<SimpleInfoCard, IBrush>(nameof(IconForeground), Brushes.White);

    public static readonly StyledProperty<IBrush> FirstTextForegroundProperty =
        AvaloniaProperty.Register<SimpleInfoCard, IBrush>(nameof(FirstTextForeground), Brushes.White);

    public static readonly StyledProperty<IBrush> SecondTextForegroundProperty =
        AvaloniaProperty.Register<SimpleInfoCard, IBrush>(nameof(SecondTextForeground), Brushes.White);

    public static readonly StyledProperty<IBrush> DescriptionTextForegroundProperty =
        AvaloniaProperty.Register<SimpleInfoCard, IBrush>(nameof(DescriptionTextForeground), Brushes.White);

    public static readonly StyledProperty<HorizontalAlignment> IconHorizontalAlignmentProperty =
        AvaloniaProperty.Register<SimpleInfoCard, HorizontalAlignment>(nameof(IconHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> ImageHorizontalAlignmentProperty =
        AvaloniaProperty.Register<SimpleInfoCard, HorizontalAlignment>(nameof(ImageHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> TextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<SimpleInfoCard, HorizontalAlignment>(nameof(TextHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> DescriptionTextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<SimpleInfoCard, HorizontalAlignment>(nameof(DescriptionTextHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> IconVerticalAlignmentProperty =
        AvaloniaProperty.Register<SimpleInfoCard, VerticalAlignment>(nameof(IconVerticalAlignment), VerticalAlignment.Top);

    public static readonly StyledProperty<VerticalAlignment> ImageVerticalAlignmentProperty =
       AvaloniaProperty.Register<SimpleInfoCard, VerticalAlignment>(nameof(ImageVerticalAlignment), VerticalAlignment.Top);

    public static readonly StyledProperty<VerticalAlignment> TextVerticalAlignmentProperty =
        AvaloniaProperty.Register<SimpleInfoCard, VerticalAlignment>(nameof(TextVerticalAlignment), VerticalAlignment.Bottom);

    public static readonly StyledProperty<VerticalAlignment> DescriptionTextVerticalAlignmentProperty =
        AvaloniaProperty.Register<SimpleInfoCard, VerticalAlignment>(nameof(DescriptionTextVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<FontWeight> FirstTextFontWeightProperty =
        AvaloniaProperty.Register<SimpleInfoCard, FontWeight>(nameof(FirstTextFontWeight), FontWeight.SemiBold);

    public static readonly StyledProperty<FontWeight> SecondTextFontWeightProperty =
        AvaloniaProperty.Register<SimpleInfoCard, FontWeight>(nameof(SecondTextFontWeight), FontWeight.SemiBold);

    public static readonly StyledProperty<FontWeight> DescriptionTextFontWeightProperty =
        AvaloniaProperty.Register<SimpleInfoCard, FontWeight>(nameof(DescriptionTextFontWeight), FontWeight.SemiBold);

    public static readonly StyledProperty<Thickness> TextMarginProperty =
        AvaloniaProperty.Register<SimpleInfoCard, Thickness>(nameof(TextMargin), new Thickness(0, 0, 0, 0));

    public static readonly StyledProperty<Thickness> DescriptionTextMarginProperty =
        AvaloniaProperty.Register<SimpleInfoCard, Thickness>(nameof(DescriptionTextMargin), new Thickness(0, 0, 0, 0));

    public static readonly StyledProperty<Geometry> CardIconProperty =
        AvaloniaProperty.Register<SimpleInfoCard, Geometry>(nameof(CardIcon));

    public static readonly StyledProperty<IImage> CardImageProperty =
        AvaloniaProperty.Register<SimpleInfoCard, IImage>(nameof(CardImage));

    private double iconWidth = 80;
    private double iconHeight = 96;
    private double imageWidth = 80;
    private double imageHeight = 96;
    private double firstTextFontSize = 36;
    private double secondTextFontSize = 36;
    private double descriptionTextFontSize = 24;
    private string? firstText = "Lorem";
    private string? secondText;
    private string? labelText;
    private bool isIconVisible = true;
    private bool isImageVisible;

    public double IconWidth
    {
        get => iconWidth;
        set => SetAndRaise(IconWidthProperty, ref iconWidth, value);
    }

    public double IconHeight
    {
        get => iconHeight;
        set => SetAndRaise(IconHeightProperty, ref iconHeight, value);
    }

    public double ImageWidth
    {
        get => imageWidth;
        set => SetAndRaise(ImageWidthProperty, ref imageWidth, value);
    }

    public double ImageHeight
    {
        get => imageHeight;
        set => SetAndRaise(ImageHeightProperty, ref imageHeight, value);
    }

    public double FirstTextFontSize
    {
        get => firstTextFontSize;
        set => SetAndRaise(FirstTextFontSizeProperty, ref firstTextFontSize, value);
    }

    public double SecondTextFontSize
    {
        get => secondTextFontSize;
        set => SetAndRaise(SecondTextFontSizeProperty, ref secondTextFontSize, value);
    }

    public double DescriptionTextFontSize
    {
        get => descriptionTextFontSize;
        set => SetAndRaise(DescriptionTextFontSizeProperty, ref descriptionTextFontSize, value);
    }

    public string? FirstText
    {
        get => firstText;
        set => SetAndRaise(FirstTextProperty, ref firstText, value);
    }

    public string? SecondText
    {
        get => secondText;
        set => SetAndRaise(SecondTextProperty, ref secondText, value);
    }

    public string? DescriptionText
    {
        get => labelText;
        set => SetAndRaise(DescriptionTextProperty, ref labelText, value);
    }

    public bool IsIconVisible
    {
        get => isIconVisible;
        set => SetAndRaise(IsIconVisibleProperty, ref isIconVisible, value);
    }

    public bool IsImageVisible
    {
        get => isImageVisible;
        set => SetAndRaise(IsImageVisibleProperty, ref isImageVisible, value);
    }

    public IBrush IconForeground
    {
        get => this.GetValue(IconForegroundProperty);
        set => SetValue(IconForegroundProperty, value);
    }

    public IBrush FirstTextForeground
    {
        get => this.GetValue(FirstTextForegroundProperty);
        set => SetValue(FirstTextForegroundProperty, value);
    }

    public IBrush SecondTextForeground
    {
        get => this.GetValue(SecondTextForegroundProperty);
        set => SetValue(SecondTextForegroundProperty, value);
    }

    public IBrush DescriptionTextForeground
    {
        get => this.GetValue(DescriptionTextForegroundProperty);
        set => SetValue(DescriptionTextForegroundProperty, value);
    }

    public HorizontalAlignment IconHorizontalAlignment
    {
        get => this.GetValue(IconHorizontalAlignmentProperty);
        set => SetValue(IconHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment ImageHorizontalAlignment
    {
        get => this.GetValue(ImageHorizontalAlignmentProperty);
        set => SetValue(ImageHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment TextHorizontalAlignment
    {
        get => this.GetValue(TextHorizontalAlignmentProperty);
        set => SetValue(TextHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment DescriptionTextHorizontalAlignment
    {
        get => this.GetValue(DescriptionTextHorizontalAlignmentProperty);
        set => SetValue(DescriptionTextHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment IconVerticalAlignment
    {
        get => this.GetValue(IconVerticalAlignmentProperty);
        set => SetValue(IconVerticalAlignmentProperty, value);
    }

    public VerticalAlignment ImageVerticalAlignment
    {
        get => this.GetValue(ImageVerticalAlignmentProperty);
        set => SetValue(ImageVerticalAlignmentProperty, value);
    }

    public VerticalAlignment TextVerticalAlignment
    {
        get => this.GetValue(TextVerticalAlignmentProperty);
        set => SetValue(TextVerticalAlignmentProperty, value);
    }

    public VerticalAlignment DescriptionTextVerticalAlignment
    {
        get => this.GetValue(DescriptionTextVerticalAlignmentProperty);
        set => SetValue(DescriptionTextVerticalAlignmentProperty, value);
    }

    public FontWeight FirstTextFontWeight
    {
        get => this.GetValue(FirstTextFontWeightProperty);
        set => SetValue(FirstTextFontWeightProperty, value);
    }

    public FontWeight SecondTextFontWeight
    {
        get => this.GetValue(SecondTextFontWeightProperty);
        set => SetValue(SecondTextFontWeightProperty, value);
    }

    public FontWeight DescriptionTextFontWeight
    {
        get => this.GetValue(DescriptionTextFontWeightProperty);
        set => SetValue(DescriptionTextFontWeightProperty, value);
    }

    public Thickness TextMargin
    {
        get => this.GetValue(TextMarginProperty);
        set => SetValue(TextMarginProperty, value);
    }

    public Thickness DescriptionTextMargin
    {
        get => this.GetValue(DescriptionTextMarginProperty);
        set => SetValue(DescriptionTextMarginProperty, value);
    }

    public Geometry CardIcon
    {
        get => this.GetValue(CardIconProperty);
        set => SetValue(CardIconProperty, value);
    }

    public IImage CardImage
    {
        get => this.GetValue(CardImageProperty);
        set => SetValue(CardImageProperty, value);
    }
}