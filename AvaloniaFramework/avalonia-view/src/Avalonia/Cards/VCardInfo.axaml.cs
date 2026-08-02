using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaFramework.Apresentacao.Cards.Enums;

namespace AvaloniaFramework.Apresentacao.Cards;

public class VCardInfo : TemplatedControl
{
    public static readonly DirectProperty<VCardInfo, double> VCardWidthProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardWidth),
            o => o.VCardWidth,
            (o, v) => o.VCardWidth = v);

    public static readonly DirectProperty<VCardInfo, double> VCardHeightProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardHeight),
            o => o.VCardHeight,
            (o, v) => o.VCardHeight = v);

    public static readonly DirectProperty<VCardInfo, double> VCardBackgroundOpacityProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardBackgroundOpacity),
            o => o.VCardBackgroundOpacity,
            (o, v) => o.VCardBackgroundOpacity = v);

    public static readonly DirectProperty<VCardInfo, double> VCardContentIconWidthProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardContentIconWidth),
            o => o.VCardContentIconWidth,
            (o, v) => o.VCardContentIconWidth = v);

    public static readonly DirectProperty<VCardInfo, double> VCardContentIconHeightProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardContentIconHeight),
            o => o.VCardContentIconHeight,
            (o, v) => o.VCardContentIconHeight = v);

    public static readonly DirectProperty<VCardInfo, double> VCardContentImageWidthProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardContentImageWidth),
            o => o.VCardContentImageWidth,
            (o, v) => o.VCardContentImageWidth = v);

    public static readonly DirectProperty<VCardInfo, double> VCardContentImageHeightProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardContentImageHeight),
            o => o.VCardContentImageHeight,
            (o, v) => o.VCardContentImageHeight = v);

    public static readonly DirectProperty<VCardInfo, double> VCardContentTextFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardContentTextFontSize),
            o => o.VCardContentTextFontSize,
            (o, v) => o.VCardContentTextFontSize = v);

    public static readonly DirectProperty<VCardInfo, double> VCardContentTextOpacityProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardContentTextOpacity),
            o => o.VCardContentTextOpacity,
            (o, v) => o.VCardContentTextOpacity = v);

    public static readonly DirectProperty<VCardInfo, double> VCardStatusWidthProperty =
       AvaloniaProperty.RegisterDirect<VCardInfo, double>(
           nameof(VCardStatusWidth),
           o => o.VCardStatusWidth,
           (o, v) => o.VCardStatusWidth = v);

    public static readonly DirectProperty<VCardInfo, double> VCardStatusEllipseWidthProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardStatusEllipseWidth),
            o => o.VCardStatusEllipseWidth,
            (o, v) => o.VCardStatusEllipseWidth = v);

    public static readonly DirectProperty<VCardInfo, double> VCardStatusEllipseHeightProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardStatusEllipseHeight),
            o => o.VCardStatusEllipseHeight,
            (o, v) => o.VCardStatusEllipseHeight = v);

    public static readonly DirectProperty<VCardInfo, double> VCardStatusTextFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, double>(
            nameof(VCardStatusTextFontSize),
            o => o.VCardStatusTextFontSize,
            (o, v) => o.VCardStatusTextFontSize = v);

    public static readonly DirectProperty<VCardInfo, string> VCardContentTextProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, string>(
            nameof(VCardContentText),
            o => o.VCardContentText,
            (o, v) => o.VCardContentText = v);

    public static readonly DirectProperty<VCardInfo, string> VCardStatusTextProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, string>(
            nameof(VCardStatusText),
            o => o.VCardStatusText,
            (o, v) => o.VCardStatusText = v);

    public static readonly DirectProperty<VCardInfo, bool> VIsCardContentIconVisibleProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, bool>(
            nameof(VIsCardContentIconVisible),
            o => o.VIsCardContentIconVisible,
            (o, v) => o.VIsCardContentIconVisible = v);

    public static readonly DirectProperty<VCardInfo, bool> VIsCardContentImageVisibleProperty =
        AvaloniaProperty.RegisterDirect<VCardInfo, bool>(
            nameof(VIsCardContentImageVisible),
            o => o.VIsCardContentImageVisible,
            (o, v) => o.VIsCardContentImageVisible = v);

    public static readonly StyledProperty<IBrush> VCardBackgroundProperty =
        AvaloniaProperty.Register<VCardInfo, IBrush>(nameof(VCardBackground), Brush.Parse("#555D66"));

    public static readonly StyledProperty<IBrush> VCardBorderBrushProperty =
        AvaloniaProperty.Register<VCardInfo, IBrush>(nameof(VCardBorderBrush), Brush.Parse("#444444"));

    public static readonly StyledProperty<IBrush> VCardContentIconForegroundProperty =
        AvaloniaProperty.Register<VCardInfo, IBrush>(nameof(VCardContentIconForeground), Brush.Parse("#BCBCBC"));

    public static readonly StyledProperty<IBrush> VCardContentTextForegroundProperty =
        AvaloniaProperty.Register<VCardInfo, IBrush>(nameof(VCardContentTextForeground), Brushes.White);

    public static readonly StyledProperty<IBrush> VCardStatusEllipseBackgroundProperty =
        AvaloniaProperty.Register<VCardInfo, IBrush>(nameof(VCardStatusEllipseBackground), Brush.Parse("#E20613"));

    public static readonly StyledProperty<IBrush> VCardStatusTextForegroundProperty =
        AvaloniaProperty.Register<VCardInfo, IBrush>(nameof(VCardStatusTextForeground), Brushes.White);

    public static readonly StyledProperty<HorizontalAlignment> VCardContentIconHorizontalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, HorizontalAlignment>(nameof(VCardContentIconHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VCardContentImageHorizontalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, HorizontalAlignment>(nameof(VCardContentImageHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VCardContentTextHorizontalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, HorizontalAlignment>(nameof(VCardContentTextHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VCardStatusHorizontalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, HorizontalAlignment>(nameof(VCardStatusHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VCardStatusEllipseHorizontalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, HorizontalAlignment>(nameof(VCardStatusEllipseHorizontalAlignment));

    public static readonly StyledProperty<HorizontalAlignment> VCardStatusTextHorizontalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, HorizontalAlignment>(nameof(VCardStatusTextHorizontalAlignment));

    public static readonly StyledProperty<HorizontalAlignment> VCardDeniedIconHorizontalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, HorizontalAlignment>(nameof(VCardDeniedIconHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VCardAlertIconHorizontalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, HorizontalAlignment>(nameof(VCardAlertIconHorizontalAlignment), HorizontalAlignment.Right);

    public static readonly StyledProperty<VerticalAlignment> VCardContentIconVerticalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, VerticalAlignment>(nameof(VCardContentIconVerticalAlignment), VerticalAlignment.Top);

    public static readonly StyledProperty<VerticalAlignment> VCardContentImageVerticalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, VerticalAlignment>(nameof(VCardContentImageVerticalAlignment), VerticalAlignment.Top);

    public static readonly StyledProperty<VerticalAlignment> VCardContentTextVerticalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, VerticalAlignment>(nameof(VCardContentTextVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VCardStatusVerticalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, VerticalAlignment>(nameof(VCardStatusVerticalAlignment), VerticalAlignment.Bottom);

    public static readonly StyledProperty<VerticalAlignment> VCardStatusEllipseVerticalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, VerticalAlignment>(nameof(VCardStatusEllipseVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VCardStatusTextVerticalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, VerticalAlignment>(nameof(VCardStatusTextVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VCardDeniedIconVerticalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, VerticalAlignment>(nameof(VCardDeniedIconVerticalAlignment), VerticalAlignment.Top);

    public static readonly StyledProperty<VerticalAlignment> VCardAlertIconVerticalAlignmentProperty =
       AvaloniaProperty.Register<VCardInfo, VerticalAlignment>(nameof(VCardAlertIconVerticalAlignment), VerticalAlignment.Top);

    public static readonly StyledProperty<Thickness> VCardBorderThicknessProperty =
        AvaloniaProperty.Register<VCardInfo, Thickness>(nameof(VCardBorderThickness), Thickness.Parse("6"));

    public static readonly StyledProperty<Thickness> VCardPaddingProperty =
        AvaloniaProperty.Register<VCardInfo, Thickness>(nameof(VCardPadding), Thickness.Parse("20, 45, 20, 37.5"));

    public static readonly StyledProperty<Thickness> VCardContentIconMarginProperty =
        AvaloniaProperty.Register<VCardInfo, Thickness>(nameof(VCardContentIconMargin));

    public static readonly StyledProperty<Thickness> VCardContentImageMarginProperty =
        AvaloniaProperty.Register<VCardInfo, Thickness>(nameof(VCardContentImageMargin));

    public static readonly StyledProperty<Thickness> VCardContentMarginProperty =
        AvaloniaProperty.Register<VCardInfo, Thickness>(nameof(VCardContentMargin));

    public static readonly StyledProperty<Thickness> VCardContentTextMarginProperty =
        AvaloniaProperty.Register<VCardInfo, Thickness>(nameof(VCardContentTextMargin));

    public static readonly StyledProperty<Thickness> VCardStatusMarginProperty =
        AvaloniaProperty.Register<VCardInfo, Thickness>(nameof(VCardStatusMargin));

    public static readonly StyledProperty<Thickness> VCardStatusEllipseMarginProperty =
        AvaloniaProperty.Register<VCardInfo, Thickness>(nameof(VCardStatusEllipseMargin));

    public static readonly StyledProperty<Thickness> VCardStatusTextMarginProperty =
        AvaloniaProperty.Register<VCardInfo, Thickness>(nameof(VCardStatusTextMargin));

    public static readonly StyledProperty<Thickness> VCardDeniedIconMarginProperty =
        AvaloniaProperty.Register<VCardInfo, Thickness>(nameof(VCardDeniedIconMargin), Thickness.Parse("0, -16, 0, 0"));

    public static readonly StyledProperty<Thickness> VCardAlertIconMarginProperty =
        AvaloniaProperty.Register<VCardInfo, Thickness>(nameof(VCardAlertIconMargin), Thickness.Parse("0, 51.5, 9.5, 0"));

    public static readonly StyledProperty<FontWeight> VCardContentTextFontWeightProperty =
       AvaloniaProperty.Register<VCardInfo, FontWeight>(nameof(VCardContentTextFontWeight), FontWeight.SemiBold);

    public static readonly StyledProperty<FontWeight> VCardStatusTextFontWeightProperty =
       AvaloniaProperty.Register<VCardInfo, FontWeight>(nameof(VCardStatusTextFontWeight), FontWeight.Regular);

    public static readonly StyledProperty<CornerRadius> VCardCornerRadiusProperty =
        AvaloniaProperty.Register<VCardInfo, CornerRadius>(nameof(VCardCornerRadius), CornerRadius.Parse("17"));

    public static readonly StyledProperty<VCardInfoBackgroundState> VCardBackgroundEnumProperty =
        AvaloniaProperty.Register<VCardInfo, VCardInfoBackgroundState>(nameof(VCardBackgroundEnum));

    public static readonly StyledProperty<Geometry> VCardContentIconProperty =
        AvaloniaProperty.Register<VCardInfo, Geometry>(nameof(VCardContentIcon));

    public static readonly StyledProperty<IImage> VCardContentImageProperty =
        AvaloniaProperty.Register<VCardInfo, IImage>(nameof(VCardContentImage));

    private double vCardWidth = 218;
    private double vCardHeight = 276;
    private double vCardBackgroundOpacity = 0.5;
    private double vCardContentIconWidth;
    private double vCardContentIconHeight;
    private double vCardContentImageWidth;
    private double vCardContentImageHeight;
    private double vCardContentTextFontSize = 28;
    private double vCardContentTextOpacity = 1;
    private double vCardStatusWidth = 177;
    private double vCardStatusTextFontSize = 24;
    private double vCardStatusEllipseWidth = 13;
    private double vCardStatusEllipseHeight = 13;
    private string vCardContentText = string.Empty;
    private string vCardStatusText = string.Empty;
    private bool vIsCardContentIconVisible;
    private bool vIsCardContentImageVisible;
    private Border? cardBackground;
    private PathIcon? contentIcon;
    private Image? contentImage;
    private PathIcon? deniedIcon;
    private PathIcon? alertIcon;
    private TextBlock? contentText;
    private TextBlock? statusText;

    public double VCardWidth
    {
        get => vCardWidth;
        set => SetAndRaise(VCardWidthProperty, ref vCardWidth, value);
    }

    public double VCardHeight
    {
        get => vCardHeight;
        set => SetAndRaise(VCardHeightProperty, ref vCardHeight, value);
    }

    public double VCardBackgroundOpacity
    {
        get => vCardBackgroundOpacity;
        set => SetAndRaise(VCardBackgroundOpacityProperty, ref vCardBackgroundOpacity, value);
    }

    public double VCardContentIconWidth
    {
        get => vCardContentIconWidth;
        set => SetAndRaise(VCardContentIconWidthProperty, ref vCardContentIconWidth, value);
    }

    public double VCardContentIconHeight
    {
        get => vCardContentIconHeight;
        set => SetAndRaise(VCardContentIconHeightProperty, ref vCardContentIconHeight, value);
    }

    public double VCardContentImageWidth
    {
        get => vCardContentImageWidth;
        set => SetAndRaise(VCardContentImageWidthProperty, ref vCardContentImageWidth, value);
    }

    public double VCardContentImageHeight
    {
        get => vCardContentImageHeight;
        set => SetAndRaise(VCardContentImageHeightProperty, ref vCardContentImageHeight, value);
    }

    public double VCardContentTextFontSize
    {
        get => vCardContentTextFontSize;
        set => SetAndRaise(VCardContentTextFontSizeProperty, ref vCardContentTextFontSize, value);
    }

    public double VCardContentTextOpacity
    {
        get => vCardContentTextOpacity;
        set => SetAndRaise(VCardContentTextOpacityProperty, ref vCardContentTextOpacity, value);
    }

    public double VCardStatusTextFontSize
    {
        get => vCardStatusTextFontSize;
        set => SetAndRaise(VCardStatusTextFontSizeProperty, ref vCardStatusTextFontSize, value);
    }

    public double VCardStatusWidth
    {
        get => vCardStatusWidth;
        set => SetAndRaise(VCardStatusWidthProperty, ref vCardStatusWidth, value);
    }

    public double VCardStatusEllipseWidth
    {
        get => vCardStatusEllipseWidth;
        set => SetAndRaise(VCardStatusEllipseWidthProperty, ref vCardStatusEllipseWidth, value);
    }

    public double VCardStatusEllipseHeight
    {
        get => vCardStatusEllipseHeight;
        set => SetAndRaise(VCardStatusEllipseHeightProperty, ref vCardStatusEllipseHeight, value);
    }

    public string VCardContentText
    {
        get => vCardContentText;
        set => SetAndRaise(VCardContentTextProperty, ref vCardContentText, value);
    }

    public string VCardStatusText
    {
        get => vCardStatusText;
        set => SetAndRaise(VCardStatusTextProperty, ref vCardStatusText, value);
    }

    public bool VIsCardContentIconVisible
    {
        get => vIsCardContentIconVisible;
        set => SetAndRaise(VIsCardContentIconVisibleProperty, ref vIsCardContentIconVisible, value);
    }

    public bool VIsCardContentImageVisible
    {
        get => vIsCardContentImageVisible;
        set => SetAndRaise(VIsCardContentImageVisibleProperty, ref vIsCardContentImageVisible, value);
    }

    public IBrush VCardBackground
    {
        get => this.GetValue(VCardBackgroundProperty);
        set => SetValue(VCardBackgroundProperty, value);
    }

    public IBrush VCardBorderBrush
    {
        get => this.GetValue(VCardBorderBrushProperty);
        set => SetValue(VCardBorderBrushProperty, value);
    }

    public IBrush VCardContentIconForeground
    {
        get => this.GetValue(VCardContentIconForegroundProperty);
        set => SetValue(VCardContentIconForegroundProperty, value);
    }

    public IBrush VCardContentTextForeground
    {
        get => this.GetValue(VCardContentTextForegroundProperty);
        set => SetValue(VCardContentTextForegroundProperty, value);
    }

    public IBrush VCardStatusEllipseBackground
    {
        get => this.GetValue(VCardStatusEllipseBackgroundProperty);
        set => SetValue(VCardStatusEllipseBackgroundProperty, value);
    }

    public IBrush VCardStatusTextForeground
    {
        get => this.GetValue(VCardStatusTextForegroundProperty);
        set => SetValue(VCardStatusTextForegroundProperty, value);
    }

    public HorizontalAlignment VCardContentIconHorizontalAlignment
    {
        get => this.GetValue(VCardContentIconHorizontalAlignmentProperty);
        set => SetValue(VCardContentIconHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VCardContentImageHorizontalAlignment
    {
        get => this.GetValue(VCardContentImageHorizontalAlignmentProperty);
        set => SetValue(VCardContentImageHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VCardContentTextHorizontalAlignment
    {
        get => this.GetValue(VCardContentTextHorizontalAlignmentProperty);
        set => SetValue(VCardContentTextHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VCardStatusHorizontalAlignment
    {
        get => this.GetValue(VCardStatusHorizontalAlignmentProperty);
        set => SetValue(VCardStatusHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VCardStatusEllipseHorizontalAlignment
    {
        get => this.GetValue(VCardStatusEllipseHorizontalAlignmentProperty);
        set => SetValue(VCardStatusEllipseHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VCardStatusTextHorizontalAlignment
    {
        get => this.GetValue(VCardStatusTextHorizontalAlignmentProperty);
        set => SetValue(VCardStatusTextHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VCardDeniedIconHorizontalAlignment
    {
        get => this.GetValue(VCardDeniedIconHorizontalAlignmentProperty);
        set => SetValue(VCardDeniedIconHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VCardAlertIconHorizontalAlignment
    {
        get => this.GetValue(VCardAlertIconHorizontalAlignmentProperty);
        set => SetValue(VCardAlertIconHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VCardContentIconVerticalAlignment
    {
        get => this.GetValue(VCardContentIconVerticalAlignmentProperty);
        set => SetValue(VCardContentIconVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VCardContentImageVerticalAlignment
    {
        get => this.GetValue(VCardContentImageVerticalAlignmentProperty);
        set => SetValue(VCardContentImageVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VCardContentTextVerticalAlignment
    {
        get => this.GetValue(VCardContentTextVerticalAlignmentProperty);
        set => SetValue(VCardContentTextVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VCardStatusVerticalAlignment
    {
        get => this.GetValue(VCardStatusVerticalAlignmentProperty);
        set => SetValue(VCardStatusVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VCardStatusEllipseVerticalAlignment
    {
        get => this.GetValue(VCardStatusEllipseVerticalAlignmentProperty);
        set => SetValue(VCardStatusEllipseVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VCardStatusTextVerticalAlignment
    {
        get => this.GetValue(VCardStatusTextVerticalAlignmentProperty);
        set => SetValue(VCardStatusTextVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VCardDeniedIconVerticalAlignment
    {
        get => this.GetValue(VCardDeniedIconVerticalAlignmentProperty);
        set => SetValue(VCardDeniedIconVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VCardAlertIconVerticalAlignment
    {
        get => this.GetValue(VCardAlertIconVerticalAlignmentProperty);
        set => SetValue(VCardAlertIconVerticalAlignmentProperty, value);
    }

    public Thickness VCardBorderThickness
    {
        get => this.GetValue(VCardBorderThicknessProperty);
        set => SetValue(VCardBorderThicknessProperty, value);
    }

    public Thickness VCardPadding
    {
        get => this.GetValue(VCardPaddingProperty);
        set => SetValue(VCardPaddingProperty, value);
    }

    public Thickness VCardContentMargin
    {
        get => this.GetValue(VCardContentMarginProperty);
        set => SetValue(VCardContentMarginProperty, value);
    }

    public Thickness VCardContentIconMargin
    {
        get => this.GetValue(VCardContentIconMarginProperty);
        set => SetValue(VCardContentIconMarginProperty, value);
    }

    public Thickness VCardContentImageMargin
    {
        get => this.GetValue(VCardContentImageMarginProperty);
        set => SetValue(VCardContentImageMarginProperty, value);
    }

    public Thickness VCardContentTextMargin
    {
        get => this.GetValue(VCardContentTextMarginProperty);
        set => SetValue(VCardContentTextMarginProperty, value);
    }

    public Thickness VCardStatusMargin
    {
        get => this.GetValue(VCardStatusMarginProperty);
        set => SetValue(VCardStatusMarginProperty, value);
    }

    public Thickness VCardStatusEllipseMargin
    {
        get => this.GetValue(VCardStatusEllipseMarginProperty);
        set => SetValue(VCardStatusEllipseMarginProperty, value);
    }

    public Thickness VCardStatusTextMargin
    {
        get => this.GetValue(VCardStatusTextMarginProperty);
        set => SetValue(VCardStatusTextMarginProperty, value);
    }

    public Thickness VCardDeniedIconMargin
    {
        get => this.GetValue(VCardDeniedIconMarginProperty);
        set => SetValue(VCardDeniedIconMarginProperty, value);
    }

    public Thickness VCardAlertIconMargin
    {
        get => this.GetValue(VCardAlertIconMarginProperty);
        set => SetValue(VCardAlertIconMarginProperty, value);
    }

    public FontWeight VCardContentTextFontWeight
    {
        get => this.GetValue(VCardContentTextFontWeightProperty);
        set => SetValue(VCardContentTextFontWeightProperty, value);
    }

    public FontWeight VCardStatusTextFontWeight
    {
        get => this.GetValue(VCardStatusTextFontWeightProperty);
        set => SetValue(VCardStatusTextFontWeightProperty, value);
    }

    public CornerRadius VCardCornerRadius
    {
        get => this.GetValue(VCardCornerRadiusProperty);
        set => SetValue(VCardCornerRadiusProperty, value);
    }

    public VCardInfoBackgroundState VCardBackgroundEnum
    {
        get => this.GetValue(VCardBackgroundEnumProperty);
        set => SetValue(VCardBackgroundEnumProperty, value);
    }

    public Geometry VCardContentIcon
    {
        get => this.GetValue(VCardContentIconProperty);
        set => SetValue(VCardContentIconProperty, value);
    }

    public IImage VCardContentImage
    {
        get => this.GetValue(VCardContentImageProperty);
        set => SetValue(VCardContentImageProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (e == null)
        {
            return;
        }

        cardBackground = e.NameScope.Find<Border>("cardBackground");
        contentIcon = e.NameScope.Find<PathIcon>("contentIcon");
        contentImage = e.NameScope.Find<Image>("contentImage");
        deniedIcon = e.NameScope.Find<PathIcon>("deniedIcon");
        alertIcon = e.NameScope.Find<PathIcon>("alertIcon");
        contentText = e.NameScope.Find<TextBlock>("contentText");
        statusText = e.NameScope.Find<TextBlock>("statusText");

        if (cardBackground != null)
            cardBackground.PropertyChanged += CardBackgroundColorChanged;
    }

    private void CardBackgroundColorChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        switch (VCardBackgroundEnum)
        {
            case VCardInfoBackgroundState.Desconectado:
                VCardBackgroundOpacity = 0.4;
                VCardContentIconForeground = Brush.Parse("#BCBCBC");
                VCardContentTextForeground = Brush.Parse("#BCBCBC");
                VCardContentTextOpacity = 0.4;
                VCardStatusWidth = 218;
                if (contentImage != null)
                    contentImage.Opacity = 40;
                if (contentIcon != null)
                    contentIcon.Opacity = 0.4;
                if (deniedIcon != null)
                    deniedIcon.IsVisible = true;
                break;

            case VCardInfoBackgroundState.Conectado:
                VCardBackgroundOpacity = 0.5;
                VCardContentIconForeground = Brushes.White;
                VCardStatusTextFontWeight = FontWeight.SemiBold;
                break;

            case VCardInfoBackgroundState.SemCombustivel:
                VCardBackgroundOpacity = 0.5;
                VCardPadding = Thickness.Parse("20, 31.5, 20, 21.5");
                VCardContentIconForeground = Brushes.White;
                if (alertIcon != null)
                    alertIcon.IsVisible = true;
                break;
        }
    }
}