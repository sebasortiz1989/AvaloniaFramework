using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Cards;

public class InfoCard : ContentControl
{
    public static readonly StyledProperty<int> CardWidthProperty = AvaloniaProperty.Register<InfoCard, int>(nameof(CardWidth), 161);
    public static readonly StyledProperty<string?> CardTitleProperty = AvaloniaProperty.Register<InfoCard, string?>(nameof(CardTitle), "Test");
    public static readonly StyledProperty<int> CardTextTitleSizeProperty = AvaloniaProperty.Register<InfoCard, int>(nameof(CardTextTitleSize), 20);
    public static readonly StyledProperty<IBrush> CardBackgroundColorProperty = AvaloniaProperty.Register<InfoCard, IBrush>(nameof(CardBackgroundColor), Brush.Parse("#43B6DB"));
    public static readonly StyledProperty<IBrush> TextColorProperty = AvaloniaProperty.Register<InfoCard, IBrush>(nameof(TextColor), Brush.Parse("#323232"));

    public int CardWidth
    {
        get => GetValue(CardWidthProperty);
        set => SetValue(CardWidthProperty, value);
    }

    public string? CardTitle
    {
        get => GetValue(CardTitleProperty);
        set => SetValue(CardTitleProperty, value);
    }

    public int CardTextTitleSize
    {
        get => GetValue(CardTextTitleSizeProperty);
        set => SetValue(CardTextTitleSizeProperty, value);
    }

    public IBrush CardBackgroundColor
    {
        get => GetValue(CardBackgroundColorProperty);
        set => SetValue(CardBackgroundColorProperty, value);
    }

    public IBrush TextColor
    {
        get => GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
}