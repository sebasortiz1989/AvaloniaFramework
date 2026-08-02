using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Cards;

public class TextBlockCard : ItemsControl
{
    public static readonly StyledProperty<string> TitleCardProperty =
        AvaloniaProperty.Register<TextBlockCard, string>(nameof(TitleCard));

    public static readonly StyledProperty<string> StringFormatProperty =
        AvaloniaProperty.Register<TextBlockCard, string>(nameof(StringFormat));

    public static readonly StyledProperty<string> ContentCardProperty =
        AvaloniaProperty.Register<TextBlockCard, string>(nameof(ContentCard));

    public static readonly StyledProperty<Thickness> BorderThicknessCardProperty =
        AvaloniaProperty.Register<TextBlockCard, Thickness>(nameof(BorderThicknessCard), Thickness.Parse("0"));

    public static readonly StyledProperty<IBrush> BorderColorCardProperty =
        AvaloniaProperty.Register<TextBlockCard, IBrush>(nameof(BorderColorCard), Brush.Parse("#0000"));

    public static readonly StyledProperty<double> SizeFontCardProperty =
        AvaloniaProperty.Register<TextBlockCard, double>(nameof(SizeFontCard));

    public static readonly StyledProperty<IBrush> ForegroundTitleCardProperty =
        AvaloniaProperty.Register<TextBlockCard, IBrush>(nameof(ForegroundTitleCard));

    public static readonly StyledProperty<IBrush> ForegroundContentCardProperty =
        AvaloniaProperty.Register<TextBlockCard, IBrush>(nameof(ForegroundContentCard));

    public static readonly StyledProperty<IBrush> BackgroundCardProperty =
        AvaloniaProperty.Register<TextBlockCard, IBrush>(nameof(BackgroundCard));

    public static readonly StyledProperty<FontWeight> TitleFontWeightProperty =
        AvaloniaProperty.Register<TextBlockCard, FontWeight>(nameof(TitleFontWeight), FontWeight.SemiBold);

    public static readonly StyledProperty<FontWeight> ContentFontWeightProperty =
        AvaloniaProperty.Register<TextBlockCard, FontWeight>(nameof(ContentFontWeight), FontWeight.Regular);

    public static new readonly StyledProperty<CornerRadius> CornerRadiusProperty =
        AvaloniaProperty.Register<TextBlockCard, CornerRadius>(nameof(CornerRadius));

    public static readonly StyledProperty<HorizontalAlignment> TitleHorizontalAlignmentProperty =
      AvaloniaProperty.Register<TextBlockCard, HorizontalAlignment>(nameof(TitleHorizontalAlignment), HorizontalAlignment.Left);

    public static readonly StyledProperty<VerticalAlignment> TitleVerticalAlignmentProperty =
      AvaloniaProperty.Register<TextBlockCard, VerticalAlignment>(nameof(TitleVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> ContentHorizontalAlignmentProperty =
      AvaloniaProperty.Register<TextBlockCard, HorizontalAlignment>(nameof(ContentHorizontalAlignment), HorizontalAlignment.Left);

    public static readonly StyledProperty<VerticalAlignment> ContentVerticalAlignmentProperty =
      AvaloniaProperty.Register<TextBlockCard, VerticalAlignment>(nameof(ContentVerticalAlignment), VerticalAlignment.Center);

    public string TitleCard
    {
        get => this.GetValue(TitleCardProperty);
        set => SetValue(TitleCardProperty, value);
    }

    public string StringFormat
    {
        get => this.GetValue(StringFormatProperty);
        set => SetValue(StringFormatProperty, value);
    }

    public string ContentCard
    {
        get => this.GetValue(ContentCardProperty);
        set => SetValue(ContentCardProperty, value);
    }

    public Thickness BorderThicknessCard
    {
        get => GetValue(BorderThicknessCardProperty);
        set => SetValue(BorderThicknessCardProperty, value);
    }

    public IBrush BorderColorCard
    {
        get => GetValue(BorderColorCardProperty);
        set => SetValue(BorderColorCardProperty, value);
    }

    public double SizeFontCard
    {
        get => GetValue(SizeFontCardProperty);
        set => SetValue(SizeFontCardProperty, value);
    }

    public IBrush ForegroundTitleCard
    {
        get => GetValue(ForegroundTitleCardProperty);
        set => SetValue(ForegroundTitleCardProperty, value);
    }

    public IBrush ForegroundContentCard
    {
        get => GetValue(ForegroundContentCardProperty);
        set => SetValue(ForegroundContentCardProperty, value);
    }

    public IBrush BackgroundCard
    {
        get => GetValue(BackgroundCardProperty);
        set => SetValue(BackgroundCardProperty, value);
    }

    public FontWeight TitleFontWeight
    {
        get => GetValue(TitleFontWeightProperty);
        set => SetValue(TitleFontWeightProperty, value);
    }

    public FontWeight ContentFontWeight
    {
        get => GetValue(ContentFontWeightProperty);
        set => SetValue(ContentFontWeightProperty, value);
    }

    public new CornerRadius CornerRadius
    {
        get => GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public HorizontalAlignment TitleHorizontalAlignment
    {
        get => GetValue(TitleHorizontalAlignmentProperty);
        set => SetValue(TitleHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment TitleVerticalAlignment
    {
        get => GetValue(TitleVerticalAlignmentProperty);
        set => SetValue(TitleVerticalAlignmentProperty, value);
    }

    public HorizontalAlignment ContentHorizontalAlignment
    {
        get => GetValue(ContentHorizontalAlignmentProperty);
        set => SetValue(ContentHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment ContentVerticalAlignment
    {
        get => GetValue(ContentVerticalAlignmentProperty);
        set => SetValue(ContentVerticalAlignmentProperty, value);
    }
}