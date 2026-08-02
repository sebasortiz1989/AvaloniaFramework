using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using System;
using System.Windows.Input;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class CustomButton : TemplatedControl
{
    public static readonly DirectProperty<CustomButton, ButtonContent> ButtonContentProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, ButtonContent>(
            nameof(ButtonContent),
            o => o.ButtonContent,
            (o, v) => o.ButtonContent = v);

    public static readonly DirectProperty<CustomButton, ButtonEffects> ButtonEffectProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, ButtonEffects>(
            nameof(ButtonEffect),
            o => o.ButtonEffect,
            (o, v) => o.ButtonEffect = v);

    public static readonly DirectProperty<CustomButton, string?> ButtonTextProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, string?>(
            nameof(ButtonText),
            o => o.ButtonText,
            (o, v) => o.ButtonText = v);

    public static readonly DirectProperty<CustomButton, VerticalAlignment> TextVerticalAlignmentProperty =
    AvaloniaProperty.RegisterDirect<CustomButton, VerticalAlignment>(
        nameof(TextVerticalAlignment),
        o => o.TextVerticalAlignment,
        (o, v) => o.TextVerticalAlignment = v);

    public static readonly DirectProperty<CustomButton, HorizontalAlignment> TextHorizontalAlignmentProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, HorizontalAlignment>(
            nameof(TextHorizontalAlignment),
            o => o.TextHorizontalAlignment,
            (o, v) => o.TextHorizontalAlignment = v);

    public static readonly DirectProperty<CustomButton, double> TextSizeProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, double>(
            nameof(TextSize),
            o => o.TextSize,
            (o, v) => o.TextSize = v);

    public static readonly DirectProperty<CustomButton, double> IconWidthProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, double>(
            nameof(IconWidth),
            o => o.IconWidth,
            (o, v) => o.IconWidth = v);

    public static readonly DirectProperty<CustomButton, double> IconHeightProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, double>(
            nameof(IconHeight),
            o => o.IconHeight,
            (o, v) => o.IconHeight = v);

    public static readonly DirectProperty<CustomButton, VerticalAlignment> IconVerticalAlignmentProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, VerticalAlignment>(
            nameof(IconVerticalAlignment),
            o => o.IconVerticalAlignment,
            (o, v) => o.IconVerticalAlignment = v);

    public static readonly DirectProperty<CustomButton, HorizontalAlignment> IconHorizontalAlignmentProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, HorizontalAlignment>(
            nameof(IconHorizontalAlignment),
            o => o.IconHorizontalAlignment,
            (o, v) => o.IconHorizontalAlignment = v);

    public static readonly DirectProperty<CustomButton, VerticalAlignment> ToggleVerticalAlignmentProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, VerticalAlignment>(
            nameof(ToggleVerticalAlignment),
            o => o.ToggleVerticalAlignment,
            (o, v) => o.ToggleVerticalAlignment = v);

    public static readonly DirectProperty<CustomButton, HorizontalAlignment> ToggleHorizontalAlignmentProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, HorizontalAlignment>(
            nameof(ToggleHorizontalAlignment),
            o => o.ToggleHorizontalAlignment,
            (o, v) => o.ToggleHorizontalAlignment = v);

    public static readonly DirectProperty<CustomButton, Thickness> ToggleBorderThicknessProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, Thickness>(
            nameof(ToggleBorderThickness),
            o => o.ToggleBorderThickness,
            (o, v) => o.ToggleBorderThickness = v);

    public static readonly DirectProperty<CustomButton, FontFamily> TextFontFamilyProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, FontFamily>(
            nameof(TextFontFamily),
            o => o.TextFontFamily,
            (o, v) => o.TextFontFamily = v);

    public static readonly DirectProperty<CustomButton, ICommand?> CommandProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, ICommand?>(
            nameof(Command),
            o => o.Command,
            (o, v) => o.Command = v);

    public static readonly StyledProperty<CornerRadius> ToggleButtonCornerRadiusProperty =
        AvaloniaProperty.Register<CustomButton, CornerRadius>(nameof(ToggleButtonCornerRadius), CornerRadius.Parse("8"));

    public static readonly StyledProperty<Geometry> TopIconProperty =
        AvaloniaProperty.Register<CustomButton, Geometry>(nameof(TopIcon));

    public static readonly StyledProperty<Geometry> CheckedIconProperty =
        AvaloniaProperty.Register<CustomButton, Geometry>(nameof(CheckedIcon));

    public static readonly StyledProperty<Geometry> UncheckedIconProperty =
        AvaloniaProperty.Register<CustomButton, Geometry>(nameof(UncheckedIcon));

    public static readonly StyledProperty<Geometry> LeftIconProperty =
        AvaloniaProperty.Register<CustomButton, Geometry>(nameof(LeftIcon));

    public static readonly DirectProperty<CustomButton, string> SvgStringLeftProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, string>(
            nameof(SvgStringLeft),
            o => o.SvgStringLeft,
            (o, v) => o.SvgStringLeft = v);

    public static readonly StyledProperty<Geometry> RightIconProperty =
        AvaloniaProperty.Register<CustomButton, Geometry>(nameof(RightIcon));

    public static readonly DirectProperty<CustomButton, string> SvgStringRightProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, string>(
            nameof(SvgStringRight),
            o => o.SvgStringRight,
            (o, v) => o.SvgStringRight = v);

    public static readonly DirectProperty<CustomButton, bool> IsCheckedProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, bool>(
            nameof(IsChecked),
            o => o.IsChecked,
            (o, v) => o.IsChecked = v);

    public static readonly DirectProperty<CustomButton, IImage?> SourceImageProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, IImage?>(
            nameof(SourceImage),
            o => o.SourceImage,
            (o, v) => o.SourceImage = v);

    public static readonly DirectProperty<CustomButton, double> ImageWidthProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, double>(
            nameof(ImageWidth),
            o => o.ImageWidth,
            (o, v) => o.ImageWidth = v);

    public static readonly DirectProperty<CustomButton, double> ImageHeightProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, double>(
            nameof(ImageHeight),
            o => o.ImageHeight,
            (o, v) => o.ImageHeight = v);

    public static readonly DirectProperty<CustomButton, Thickness> ImageMarginProperty =
        AvaloniaProperty.RegisterDirect<CustomButton, Thickness>(
            nameof(ImageMargin),
            o => o.ImageMargin,
            (o, v) => o.ImageMargin = v);

    public static readonly StyledProperty<IBrush> TextForegroundProperty =
        AvaloniaProperty.Register<CustomButton, IBrush>(nameof(TextForeground), Brush.Parse("#FFFFFF"));

    public static readonly StyledProperty<IBrush> IconForegroundCheckedProperty =
        AvaloniaProperty.Register<CustomButton, IBrush>(nameof(IconForegroundChecked), Brush.Parse("#000000"));

    public static readonly StyledProperty<IBrush> IconForegroundUncheckedProperty =
        AvaloniaProperty.Register<CustomButton, IBrush>(nameof(IconForegroundUnchecked), Brush.Parse("#FFFFFF"));

    public static readonly StyledProperty<IBrush> CheckedBackgroundProperty =
        AvaloniaProperty.Register<CustomButton, IBrush>(nameof(CheckedBackground), Brush.Parse("#43B6DB"));

    public static readonly StyledProperty<IBrush> UncheckedBackgroundProperty =
        AvaloniaProperty.Register<CustomButton, IBrush>(nameof(UncheckedBackground), Brush.Parse("#003F7D"));

    public static readonly StyledProperty<IBrush> UnderlineColorProperty =
       AvaloniaProperty.Register<CustomButton, IBrush>(nameof(UnderlineColor), Brush.Parse("#43B6DB"));

    public static readonly StyledProperty<Thickness> TextMarginProperty =
       AvaloniaProperty.Register<CustomButton, Thickness>(nameof(TextMargin));

    public static readonly StyledProperty<Thickness> VerionIconMarginProperty =
       AvaloniaProperty.Register<CustomButton, Thickness>(nameof(VerionIconMargin), Thickness.Parse("15"));

    public static readonly StyledProperty<Thickness> TogglePaddingProperty =
       AvaloniaProperty.Register<CustomButton, Thickness>(nameof(TogglePadding));

    public static readonly StyledProperty<IBrush> CbBorderBrushCheckedProperty =
        AvaloniaProperty.Register<CustomButton, IBrush>(nameof(CbBorderBrushChecked), Brush.Parse("#0000"));

    public static readonly StyledProperty<IBrush> CbBorderBrushUncheckedProperty =
    AvaloniaProperty.Register<CustomButton, IBrush>(nameof(CbBorderBrushUnchecked), Brush.Parse("#0000"));

    public static readonly StyledProperty<Color> TopDropshadowColorProperty =
        AvaloniaProperty.Register<CustomButton, Color>(nameof(TopDropshadowColor), Color.Parse("#43b6db66"));

    public static readonly StyledProperty<Color> BottomDropshadowColorProperty =
        AvaloniaProperty.Register<CustomButton, Color>(nameof(BottomDropshadowColor), Color.Parse("#096785"));

    private ButtonContent buttonContent = ButtonContent.Icon;
    private ButtonEffects buttonEffect = ButtonEffects.None;
    private string? buttonText = "Mussum Ipsum";
    private VerticalAlignment textVerticalAlignment = VerticalAlignment.Center;
    private HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Center;
    private double textSize = 18;
    private double iconWidth = 32;
    private double iconHeight = 32;
    private VerticalAlignment iconVerticalAlignment = VerticalAlignment.Center;
    private HorizontalAlignment iconHorizontalAlignment = HorizontalAlignment.Center;
    private VerticalAlignment toggleVerticalAlignment = VerticalAlignment.Center;
    private HorizontalAlignment toggleHorizontalAlignment = HorizontalAlignment.Center;
    private Thickness toggleBorderThickness = Thickness.Parse("2");
    private string svgStringLeft = string.Empty;
    private string svgStringRight = string.Empty;
    private FontFamily textFontFamily = FontFamily.Default;
    private bool isChecked;
    private ICommand? command;
    private IImage? sourceImage;
    private double imageWidth = 32;
    private double imageHeight = 32;
    private Thickness imageMargin;

    static CustomButton()
    {
        SvgStringLeftProperty.Changed.AddClassHandler<CustomButton>((sender, e) => SvgStringLeftPropertyChanged(sender, e));
        SvgStringRightProperty.Changed.AddClassHandler<CustomButton>((sender, e) => SvgStringRightPropertyChanged(sender, e));
    }

    public event EventHandler<RoutedEventArgs>? IsCheckedChanged;

    public IImage? SourceImage
    {
        get => sourceImage;
        set => SetAndRaise(SourceImageProperty, ref sourceImage, value);
    }

    public ButtonContent ButtonContent
    {
        get => buttonContent;
        set => SetAndRaise(ButtonContentProperty, ref buttonContent, value);
    }

    public ButtonEffects ButtonEffect
    {
        get => buttonEffect;
        set => SetAndRaise(ButtonEffectProperty, ref buttonEffect, value);
    }

    public string? ButtonText
    {
        get => buttonText;
        set => SetAndRaise(ButtonTextProperty, ref buttonText, value);
    }

    public VerticalAlignment TextVerticalAlignment
    {
        get => textVerticalAlignment;
        set => SetAndRaise(TextVerticalAlignmentProperty, ref textVerticalAlignment, value);
    }

    public HorizontalAlignment TextHorizontalAlignment
    {
        get => textHorizontalAlignment;
        set => SetAndRaise(TextHorizontalAlignmentProperty, ref textHorizontalAlignment, value);
    }

    public double TextSize
    {
        get => textSize;
        set => SetAndRaise(TextSizeProperty, ref textSize, value);
    }

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

    public VerticalAlignment IconVerticalAlignment
    {
        get => iconVerticalAlignment;
        set => SetAndRaise(IconVerticalAlignmentProperty, ref iconVerticalAlignment, value);
    }

    public HorizontalAlignment IconHorizontalAlignment
    {
        get => iconHorizontalAlignment;
        set => SetAndRaise(IconHorizontalAlignmentProperty, ref iconHorizontalAlignment, value);
    }

    public CornerRadius ToggleButtonCornerRadius
    {
        get => this.GetValue(ToggleButtonCornerRadiusProperty);
        set => SetValue(ToggleButtonCornerRadiusProperty, value);
    }

    public Geometry TopIcon
    {
        get => this.GetValue(TopIconProperty);
        set => SetValue(TopIconProperty, value);
    }

    public Geometry CheckedIcon
    {
        get => this.GetValue(CheckedIconProperty);
        set => SetValue(CheckedIconProperty, value);
    }

    public Geometry UncheckedIcon
    {
        get => this.GetValue(UncheckedIconProperty);
        set => SetValue(UncheckedIconProperty, value);
    }

    public Geometry LeftIcon
    {
        get => this.GetValue(LeftIconProperty);
        set => SetValue(LeftIconProperty, value);
    }

    public string SvgStringLeft
    {
        get => svgStringLeft;
        set => SetAndRaise(SvgStringLeftProperty, ref svgStringLeft, value);
    }

    public Geometry RightIcon
    {
        get => this.GetValue(RightIconProperty);
        set => SetValue(RightIconProperty, value);
    }

    public string SvgStringRight
    {
        get => svgStringRight;
        set => SetAndRaise(SvgStringRightProperty, ref svgStringRight, value);
    }

    public IBrush TextForeground
    {
        get => this.GetValue(TextForegroundProperty);
        set => SetValue(TextForegroundProperty, value);
    }

    public IBrush IconForegroundChecked
    {
        get => this.GetValue(IconForegroundCheckedProperty);
        set => SetValue(IconForegroundCheckedProperty, value);
    }

    public IBrush IconForegroundUnchecked
    {
        get => this.GetValue(IconForegroundUncheckedProperty);
        set => SetValue(IconForegroundUncheckedProperty, value);
    }

    public IBrush CheckedBackground
    {
        get => this.GetValue(CheckedBackgroundProperty);
        set => SetValue(CheckedBackgroundProperty, value);
    }

    public IBrush UncheckedBackground
    {
        get => this.GetValue(UncheckedBackgroundProperty);
        set => SetValue(UncheckedBackgroundProperty, value);
    }

    public IBrush UnderlineColor
    {
        get => this.GetValue(UnderlineColorProperty);
        set => SetValue(UnderlineColorProperty, value);
    }

    public Color TopDropshadowColor
    {
        get => this.GetValue(TopDropshadowColorProperty);
        set => SetValue(TopDropshadowColorProperty, value);
    }

    public Color BottomDropshadowColor
    {
        get => this.GetValue(BottomDropshadowColorProperty);
        set => SetValue(BottomDropshadowColorProperty, value);
    }

    public Thickness TextMargin
    {
        get => this.GetValue(TextMarginProperty);
        set => SetValue(TextMarginProperty, value);
    }

    public Thickness VerionIconMargin
    {
        get => this.GetValue(VerionIconMarginProperty);
        set => SetValue(VerionIconMarginProperty, value);
    }

    public VerticalAlignment ToggleVerticalAlignment
    {
        get => toggleVerticalAlignment;
        set => SetAndRaise(ToggleVerticalAlignmentProperty, ref toggleVerticalAlignment, value);
    }

    public HorizontalAlignment ToggleHorizontalAlignment
    {
        get => toggleHorizontalAlignment;
        set => SetAndRaise(ToggleHorizontalAlignmentProperty, ref toggleHorizontalAlignment, value);
    }

    public Thickness TogglePadding
    {
        get => this.GetValue(TogglePaddingProperty);
        set => SetValue(TogglePaddingProperty, value);
    }

    public IBrush CbBorderBrushChecked
    {
        get => this.GetValue(CbBorderBrushCheckedProperty);
        set => SetValue(CbBorderBrushCheckedProperty, value);
    }

    public IBrush CbBorderBrushUnchecked
    {
        get => this.GetValue(CbBorderBrushUncheckedProperty);
        set => SetValue(CbBorderBrushUncheckedProperty, value);
    }

    public Thickness ToggleBorderThickness
    {
        get => toggleBorderThickness;
        set => SetAndRaise(ToggleBorderThicknessProperty, ref toggleBorderThickness, value);
    }

    public FontFamily TextFontFamily
    {
        get => textFontFamily;
        set => SetAndRaise(TextFontFamilyProperty, ref textFontFamily, value);
    }

    public ICommand? Command
    {
        get => command;
        set => SetAndRaise(CommandProperty, ref command, value);
    }

    public bool IsChecked
    {
        get => isChecked;
        set
        {
            SetAndRaise(IsCheckedProperty, ref isChecked, value);
            IsCheckedChanged?.Invoke(this, new RoutedEventArgs());
        }
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

    private static void SvgStringLeftPropertyChanged(CustomButton sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is string newStringSvg)
        {
            sender.LeftIcon = PathGeometry.Parse(newStringSvg);
        }
    }

    private static void SvgStringRightPropertyChanged(CustomButton sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is string newStringSvg)
        {
            sender.RightIcon = PathGeometry.Parse(newStringSvg);
        }
    }
}