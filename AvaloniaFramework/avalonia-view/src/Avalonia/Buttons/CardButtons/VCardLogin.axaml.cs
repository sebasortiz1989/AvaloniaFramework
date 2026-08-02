using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons.CardButtons;

public class VCardLogin : Button
{
    public static readonly StyledProperty<Geometry> CardIconProperty =
        AvaloniaProperty.Register<VCardLogin, Geometry>(nameof(CardIcon));

    public static readonly DirectProperty<VCardLogin, string> ButtonTextProperty =
        AvaloniaProperty.RegisterDirect<VCardLogin, string>(
            nameof(ButtonText),
            o => o.ButtonText,
            (o, v) => o.ButtonText = v);

    public static readonly DirectProperty<VCardLogin, double> CardIconHeightProperty =
        AvaloniaProperty.RegisterDirect<VCardLogin, double>(
            nameof(CardIconHeight),
            o => o.CardIconHeight,
            (o, v) => o.CardIconHeight = v);

    public static readonly DirectProperty<VCardLogin, double> CardIconWidthProperty =
        AvaloniaProperty.RegisterDirect<VCardLogin, double>(
            nameof(CardIconWidth),
            o => o.CardIconWidth,
            (o, v) => o.CardIconWidth = v);

    public static readonly DirectProperty<VCardLogin, IBrush> BtForegroundProperty =
        AvaloniaProperty.RegisterDirect<VCardLogin, IBrush>(
            nameof(BtForeground),
            o => o.BtForeground,
            (o, v) => o.BtForeground = v);

    public static readonly DirectProperty<VCardLogin, IBrush> CardIconForegroundProperty =
    AvaloniaProperty.RegisterDirect<VCardLogin, IBrush>(
        nameof(CardIconForeground),
        o => o.CardIconForeground,
        (o, v) => o.CardIconForeground = v);

    public static readonly DirectProperty<VCardLogin, Thickness> BorderThicknessSquareProperty =
        AvaloniaProperty.RegisterDirect<VCardLogin, Thickness>(
            nameof(BorderThicknessSquare),
            o => o.BorderThicknessSquare,
            (o, v) => o.BorderThicknessSquare = v);

    private double cardIconWidth = 58;
    private double cardIconHeight = 58;
    private string buttonText = string.Empty;
    private IBrush btForeground = new SolidColorBrush(new Color(255, 255, 255, 255));
    private IBrush cardIconForeground = new SolidColorBrush(new Color(255, 255, 255, 255));
    private Thickness buttonThicknessSquare;

    static VCardLogin()
    {
        BorderThicknessProperty.Changed.AddClassHandler<VCardLogin>((sender, e) => BorderThicknessPropertyChanged(sender));
    }

    public Geometry CardIcon
    {
        get => GetValue(CardIconProperty);
        set => SetValue(CardIconProperty, value);
    }

    public double CardIconWidth
    {
        get => cardIconWidth;
        set => SetAndRaise(CardIconWidthProperty, ref cardIconWidth, value);
    }

    public double CardIconHeight
    {
        get => cardIconHeight;
        set => SetAndRaise(CardIconHeightProperty, ref cardIconHeight, value);
    }

    public IBrush BtForeground
    {
        get => btForeground;
        set => SetAndRaise(BtForegroundProperty, ref btForeground, value);
    }

    public IBrush CardIconForeground
    {
        get => cardIconForeground;
        set => SetAndRaise(CardIconForegroundProperty, ref cardIconForeground, value);
    }

    public string ButtonText
    {
        get => buttonText;
        set => SetAndRaise(ButtonTextProperty, ref buttonText, value);
    }

    public Thickness BorderThicknessSquare
    {
        get => buttonThicknessSquare;
        set => SetAndRaise(BorderThicknessSquareProperty, ref buttonThicknessSquare, value);
    }

    private static void BorderThicknessPropertyChanged(VCardLogin sender)
    {
        sender.BorderThicknessSquare = new Thickness(0, sender.BorderThickness.Top, 0, 0);
    }
}