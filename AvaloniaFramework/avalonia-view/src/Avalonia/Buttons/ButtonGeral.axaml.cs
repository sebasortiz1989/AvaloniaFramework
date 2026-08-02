using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class ButtonGeral : Button
{
    public static readonly StyledProperty<IBrush> TextForegroundProperty =
        AvaloniaProperty.Register<ButtonGeral, IBrush>(nameof(TextForeground), Brush.Parse("#FFFFFF"));

    public static readonly DirectProperty<ButtonGeral, string> TextProperty =
        AvaloniaProperty.RegisterDirect<ButtonGeral, string>(
            nameof(Text),
            o => o.Text,
            (o, v) => o.Text = v);

    public static readonly DirectProperty<ButtonGeral, double> TextSizeProperty =
        AvaloniaProperty.RegisterDirect<ButtonGeral, double>(
            nameof(TextSize),
            o => o.TextSize,
            (o, v) => o.TextSize = v);

    public static readonly DirectProperty<ButtonGeral, ButtonGeralContent> ButtonGeralContentProperty =
        AvaloniaProperty.RegisterDirect<ButtonGeral, ButtonGeralContent>(
            nameof(ButtonGeralContent),
            o => o.ButtonGeralContent,
            (o, v) => o.ButtonGeralContent = v);

    public static readonly DirectProperty<ButtonGeral, IImage?> SourceImageButtonProperty =
        AvaloniaProperty.RegisterDirect<ButtonGeral, IImage?>(
            nameof(SourceImageButton),
            o => o.SourceImageButton,
            (o, v) => o.SourceImageButton = v);

    public static readonly DirectProperty<ButtonGeral, double> ImageWidthProperty =
        AvaloniaProperty.RegisterDirect<ButtonGeral, double>(
            nameof(ImageWidth),
            o => o.ImageWidth,
            (o, v) => o.ImageWidth = v);

    public static readonly DirectProperty<ButtonGeral, double> ImageHeightProperty =
        AvaloniaProperty.RegisterDirect<ButtonGeral, double>(
            nameof(ImageHeight),
            o => o.ImageHeight,
            (o, v) => o.ImageHeight = v);

    public static readonly DirectProperty<ButtonGeral, Thickness> ImageMarginProperty =
        AvaloniaProperty.RegisterDirect<ButtonGeral, Thickness>(
            nameof(ImageMargin),
            o => o.ImageMargin,
            (o, v) => o.ImageMargin = v);

    private string text = string.Empty;
    private double textSize;
    private ButtonGeralContent buttonGeralContent;
    private double imageWidth;
    private double imageHeight;
    private Thickness imageMargin;
    private IImage? sourceImageButton;

    public ButtonGeral()
    {
        Text = "New Button";
        TextSize = 11;
        Foreground = new SolidColorBrush(new Color(byte.MaxValue, 255, 255, 255));
        Background = new SolidColorBrush(new Color(byte.MaxValue, 217, 217, 217));
        Margin = new Thickness(0);
        BorderBrush = new SolidColorBrush(new Color(byte.MaxValue, 0, 0, 0));
        CornerRadius = new CornerRadius(8);
    }

    public IImage? SourceImageButton
    {
        get => sourceImageButton;
        set => SetAndRaise(SourceImageButtonProperty, ref sourceImageButton, value);
    }

    public IBrush TextForeground
    {
        get => this.GetValue(TextForegroundProperty);
        set => SetValue(TextForegroundProperty, value);
    }

    public string Text
    {
        get => text;
        set => SetAndRaise(TextProperty, ref text, value);
    }

    public double TextSize
    {
        get => textSize;
        set => SetAndRaise(TextSizeProperty, ref textSize, value);
    }

    public ButtonGeralContent ButtonGeralContent
    {
        get => buttonGeralContent;
        set => SetAndRaise(ButtonGeralContentProperty, ref buttonGeralContent, value);
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

    public Thickness ImageMargin
    {
        get => imageMargin;
        set => SetAndRaise(ImageMarginProperty, ref imageMargin, value);
    }
}