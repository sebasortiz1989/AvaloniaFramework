using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using System.Collections.ObjectModel;

namespace AvaloniaFramework.Apresentacao.Inputs;

public class VComboBox : ComboBox
{
    public static readonly StyledProperty<bool> VIsDescriptionVisibleProperty =
        AvaloniaProperty.Register<VComboBox, bool>(nameof(VIsDescriptionVisible), true);

    public static readonly StyledProperty<double> VBorderThicknessNormalProperty =
        AvaloniaProperty.Register<VComboBox, double>(nameof(VBorderThicknessNormal), 1);

    public static readonly StyledProperty<double> VBorderThicknessFocusedProperty =
        AvaloniaProperty.Register<VComboBox, double>(nameof(VBorderThicknessFocused), 2);

    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<VComboBox, double>(nameof(IconSize), 12);

    public static readonly StyledProperty<string?> VDescriptionTextProperty =
        AvaloniaProperty.Register<VComboBox, string?>(nameof(VDescriptionText), "Tamanho do Motor");

    public static readonly StyledProperty<IBrush> VBorderBrushProperty =
    AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VBorderBrush), Brush.Parse("#43B6DB"));

    public static readonly StyledProperty<IBrush> VForegroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VForeground), Brushes.White);

    public static readonly StyledProperty<IBrush> PlaceholderBaseForegroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(PlaceholderBaseForeground), Brushes.White);

    public static readonly StyledProperty<IBrush> PointeroverBackgroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(PointeroverBackground), Brush.Parse("#4D5DE4FF"));

    public static readonly StyledProperty<IBrush> PointeroverForegroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(PointeroverForeground), Brushes.White);

    public static readonly StyledProperty<IBrush> VSelectedItemBackgroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VSelectedItemBackground), Brushes.Blue);

    public static readonly StyledProperty<IBrush> VSelectedItemForegroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VSelectedItemForeground), Brushes.White);

    public static readonly StyledProperty<IBrush> VDescriptionBackgroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VDescriptionBackground), Brush.Parse("#040404"));

    public static readonly StyledProperty<IBrush> VDescriptionForegroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VDescriptionForeground), Brush.Parse("#43B6DB"));

    public static readonly StyledProperty<IBrush> VPrincipalBorderBackgroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VPrincipalBorderBackground), Brush.Parse("#43B6DB"));

    public static readonly StyledProperty<IBrush> VDropdownOpenBorderBackgroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VDropdownOpenBorderBackground), Brushes.Transparent);

    public static readonly StyledProperty<IBrush> VListBorderBackgroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VListBorderBackground), Brush.Parse("#000000"));

    public static readonly StyledProperty<IBrush> VBorderBrushNormalProperty =
    AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VBorderBrushNormal), Brushes.Gray);

    public static readonly StyledProperty<IBrush> VBorderBrushFocusedProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VBorderBrushFocused), Brush.Parse("#9DFDFF"));

    public static readonly StyledProperty<IBrush> VBackgroundNormalProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VBackgroundNormal), Brushes.White);

    public static readonly StyledProperty<IBrush> VForegroundNormalProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VForegroundNormal), Brush.Parse("#6B7280"));

    public static readonly StyledProperty<IBrush> VPopupBorderBrushProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VPopupBorderBrush), Brush.Parse("#D1D5DB"));

    public static readonly StyledProperty<IBrush> VPopupItemBackgroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VPopupItemBackground), Brushes.White);

    public static readonly StyledProperty<IBrush> VPopupItemForegroundProperty =
        AvaloniaProperty.Register<VComboBox, IBrush>(nameof(VPopupItemForeground), Brush.Parse("#111827"));

    public static readonly StyledProperty<Thickness> NormalBorderThicknessProperty =
        AvaloniaProperty.Register<VComboBox, Thickness>(nameof(NormalBorderThickness), Thickness.Parse("2"));

    public static readonly StyledProperty<Thickness> OpenedBorderThicknessProperty =
        AvaloniaProperty.Register<VComboBox, Thickness>(nameof(OpenedBorderThickness), Thickness.Parse("2"));

    public static readonly StyledProperty<Thickness> VDescriptionPaddingProperty =
        AvaloniaProperty.Register<VComboBox, Thickness>(nameof(VDescriptionPadding), Thickness.Parse("4,0,4,0"));

    public static readonly StyledProperty<FontWeight> VContentFontWeightProperty =
        AvaloniaProperty.Register<VComboBox, FontWeight>(nameof(VContentFontWeight), FontWeight.Normal);

    public static readonly StyledProperty<FontWeight> VDescriptionFontWeightProperty =
        AvaloniaProperty.Register<VComboBox, FontWeight>(nameof(VDescriptionFontWeight), FontWeight.Bold);

    public static readonly DirectProperty<VComboBox, double> VHeightProperty =
        AvaloniaProperty.RegisterDirect<VComboBox, double>(
            nameof(VHeight), o => o.VHeight, (o, v) => o.VHeight = v);

    public static readonly DirectProperty<VComboBox, double> VWidthProperty =
        AvaloniaProperty.RegisterDirect<VComboBox, double>(
            nameof(VWidth), o => o.VWidth, (o, v) => o.VWidth = v);

    public static readonly DirectProperty<VComboBox, double> VContentFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VComboBox, double>(
            nameof(VContentFontSize), o => o.VContentFontSize, (o, v) => o.VContentFontSize = v);

    public static readonly DirectProperty<VComboBox, double> VDescriptionFontSizeProperty =
        AvaloniaProperty.RegisterDirect<VComboBox, double>(
            nameof(VDescriptionFontSize), o => o.VDescriptionFontSize, (o, v) => o.VDescriptionFontSize = v);

    public static readonly DirectProperty<VComboBox, double> VDescriptionCanvasLeftProperty =
        AvaloniaProperty.RegisterDirect<VComboBox, double>(
            nameof(VDescriptionCanvasLeft), o => o.VDescriptionCanvasLeft, (o, v) => o.VDescriptionCanvasLeft = v);

    public static readonly DirectProperty<VComboBox, IBrush> VDropDownGlyphForegroundProperty =
        AvaloniaProperty.RegisterDirect<VComboBox, IBrush>(
            nameof(VDropDownGlyphForeground), o => o.VDropDownGlyphForeground, (o, v) => o.VDropDownGlyphForeground = v);

    public static readonly DirectProperty<VComboBox, IBrush> VDropDownGlyphForegroundFocusedProperty =
        AvaloniaProperty.RegisterDirect<VComboBox, IBrush>(
            nameof(VDropDownGlyphForegroundFocused), o => o.VDropDownGlyphForegroundFocused, (o, v) => o.VDropDownGlyphForegroundFocused = v);

    public static readonly DirectProperty<VComboBox, IBrush> VPopupSelectedBackgroundProperty =
        AvaloniaProperty.RegisterDirect<VComboBox, IBrush>(
            nameof(VPopupSelectedBackground), o => o.VPopupSelectedBackground, (o, v) => o.VPopupSelectedBackground = v);

    private double vHeight = 40;
    private double vWidth = 250;
    private double vContentFontSize = 22;
    private double vDescriptionFontSize = 20;
    private double vDescriptionCanvasLeft = 12.28;
    private Panel? contentHolder;
    private Panel? generalHolder;
    private TextBlock? descriptionText;
    private Border? mainHolder;
    private Border? popupBorder = new();
    private IBrush vDropDownGlyphForeground = Brush.Parse("#43B6DB");
    private IBrush vDropDownGlyphForegroundFocused = Brush.Parse("#9DFDFF");
    private IBrush vPopupSelectedBackground = Brush.Parse("#269DFDFF");

    public IBrush VBorderBrush
    {
        get => GetValue(VBorderBrushProperty);
        set => SetValue(VBorderBrushProperty, value);
    }

    public IBrush PointeroverBackground
    {
        get => GetValue(PointeroverBackgroundProperty);
        set => SetValue(PointeroverBackgroundProperty, value);
    }

    public IBrush PointeroverForeground
    {
        get => GetValue(PointeroverForegroundProperty);
        set => SetValue(PointeroverForegroundProperty, value);
    }

    public IBrush VForeground
    {
        get => GetValue(VForegroundProperty);
        set => SetValue(VForegroundProperty, value);
    }

    public IBrush PlaceholderBaseForeground
    {
        get => GetValue(PlaceholderBaseForegroundProperty);
        set => SetValue(PlaceholderBaseForegroundProperty, value);
    }

    public IBrush VSelectedItemBackground
    {
        get => GetValue(VSelectedItemBackgroundProperty);
        set => SetValue(VSelectedItemBackgroundProperty, value);
    }

    public IBrush VSelectedItemForeground
    {
        get => GetValue(VSelectedItemForegroundProperty);
        set => SetValue(VSelectedItemForegroundProperty, value);
    }

    public FontWeight VContentFontWeight
    {
        get => GetValue(VContentFontWeightProperty);
        set => SetValue(VContentFontWeightProperty, value);
    }

    public FontWeight VDescriptionFontWeight
    {
        get => GetValue(VDescriptionFontWeightProperty);
        set => SetValue(VDescriptionFontWeightProperty, value);
    }

    public IBrush VDescriptionBackground
    {
        get => GetValue(VDescriptionBackgroundProperty);
        set => SetValue(VDescriptionBackgroundProperty, value);
    }

    public Thickness NormalBorderThickness
    {
        get => GetValue(NormalBorderThicknessProperty);
        set => SetValue(NormalBorderThicknessProperty, value);
    }

    public Thickness OpenedBorderThickness
    {
        get => GetValue(OpenedBorderThicknessProperty);
        set => SetValue(OpenedBorderThicknessProperty, value);
    }

    public Thickness VDescriptionPadding
    {
        get => GetValue(VDescriptionPaddingProperty);
        set => SetValue(VDescriptionPaddingProperty, value);
    }

    public IBrush VPrincipalBorderBackground
    {
        get => GetValue(VPrincipalBorderBackgroundProperty);
        set => SetValue(VPrincipalBorderBackgroundProperty, value);
    }

    public IBrush VDropdownOpenBorderBackground
    {
        get => GetValue(VDropdownOpenBorderBackgroundProperty);
        set => SetValue(VDropdownOpenBorderBackgroundProperty, value);
    }

    public IBrush VListBorderBackground
    {
        get => GetValue(VListBorderBackgroundProperty);
        set => SetValue(VListBorderBackgroundProperty, value);
    }

    public bool VIsDescriptionVisible
    {
        get => GetValue(VIsDescriptionVisibleProperty);
        set => SetValue(VIsDescriptionVisibleProperty, value);
    }

    public string? VDescriptionText
    {
        get => GetValue(VDescriptionTextProperty);
        set => SetValue(VDescriptionTextProperty, value);
    }

    public IBrush VDescriptionForeground
    {
        get => GetValue(VDescriptionForegroundProperty);
        set => SetValue(VDescriptionForegroundProperty, value);
    }

    public double VHeight
    {
        get => vHeight;
        set => SetAndRaise(VHeightProperty, ref vHeight, value);
    }

    public double VWidth
    {
        get => vWidth;
        set => SetAndRaise(VWidthProperty, ref vWidth, value);
    }

    public double VContentFontSize
    {
        get => vContentFontSize;
        set => SetAndRaise(VContentFontSizeProperty, ref vContentFontSize, value);
    }

    public double VDescriptionFontSize
    {
        get => vDescriptionFontSize;
        set => SetAndRaise(VDescriptionFontSizeProperty, ref vDescriptionFontSize, value);
    }

    public double VDescriptionCanvasLeft
    {
        get => vDescriptionCanvasLeft;
        set => SetAndRaise(VDescriptionCanvasLeftProperty, ref vDescriptionCanvasLeft, value);
    }

    public double IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    public IBrush VBorderBrushNormal
    {
        get => GetValue(VBorderBrushNormalProperty);
        set => SetValue(VBorderBrushNormalProperty, value);
    }

    public IBrush VBorderBrushFocused
    {
        get => GetValue(VBorderBrushFocusedProperty);
        set => SetValue(VBorderBrushFocusedProperty, value);
    }

    public IBrush VBackgroundNormal
    {
        get => GetValue(VBackgroundNormalProperty);
        set => SetValue(VBackgroundNormalProperty, value);
    }

    public IBrush VForegroundNormal
    {
        get => GetValue(VForegroundNormalProperty);
        set => SetValue(VForegroundNormalProperty, value);
    }

    public IBrush VPopupBorderBrush
    {
        get => GetValue(VPopupBorderBrushProperty);
        set => SetValue(VPopupBorderBrushProperty, value);
    }

    public IBrush VPopupSelectedBackground
    {
        get => vPopupSelectedBackground;
        set => SetAndRaise(VPopupSelectedBackgroundProperty, ref vPopupSelectedBackground, value);
    }

    public IBrush VPopupItemBackground
    {
        get => GetValue(VPopupItemBackgroundProperty);
        set => SetValue(VPopupItemBackgroundProperty, value);
    }

    public IBrush VPopupItemForeground
    {
        get => GetValue(VPopupItemForegroundProperty);
        set => SetValue(VPopupItemForegroundProperty, value);
    }

    public double VBorderThicknessNormal
    {
        get => GetValue(VBorderThicknessNormalProperty);
        set => SetValue(VBorderThicknessNormalProperty, value);
    }

    public double VBorderThicknessFocused
    {
        get => GetValue(VBorderThicknessFocusedProperty);
        set => SetValue(VBorderThicknessFocusedProperty, value);
    }

    public IBrush VDropDownGlyphForeground
    {
        get => vDropDownGlyphForeground;
        set => SetAndRaise(VDropDownGlyphForegroundProperty, ref vDropDownGlyphForeground, value);
    }

    public IBrush VDropDownGlyphForegroundFocused
    {
        get => vDropDownGlyphForegroundFocused;
        set => SetAndRaise(VDropDownGlyphForegroundFocusedProperty, ref vDropDownGlyphForegroundFocused, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (e == null)
        {
            return;
        }

        contentHolder = e.NameScope.Find<Panel>("ContentHolder");
        generalHolder = e.NameScope.Find<Panel>("GeneralHolder");
        popupBorder = e.NameScope.Find<Border>("PopupBorder");
        descriptionText = e.NameScope.Find<TextBlock>("DescriptionTextBlock")!;
        mainHolder = e.NameScope.Find<Border>("Background");

        if (popupBorder != null)
        {
            popupBorder.Tapped += PopupBorder_TappedEvent;
        }
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        if (contentHolder != null && generalHolder != null && (descriptionText != null))
        {
            Canvas.SetTop(descriptionText, -(descriptionText!.Bounds.Height / 2));
            Canvas.SetLeft(descriptionText, VDescriptionCanvasLeft);
        }

        if (popupBorder != null && mainHolder != null)
        {
            popupBorder.Width = mainHolder.DesiredSize.Width;
        }
    }

    private void PopupBorder_TappedEvent(object? sender, TappedEventArgs e)
    {
        IsDropDownOpen = false;
    }
}