using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using AvaloniaFramework.Apresentacao.Utils;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class GroupButton : RadioButton
{
    public static readonly StyledProperty<IBrush> VDisabledIconForegroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(
            nameof(VDisabledIconForeground), Brushes.DarkGray);

    public static readonly StyledProperty<IBrush> VDisabledTextForegroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(
            nameof(VDisabledTextForeground), Brushes.DarkGray);

    public static readonly StyledProperty<IBrush> VDisabledBorderBrushProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(
            nameof(VDisabledBorderBrush), Brushes.Gray);

    public static readonly StyledProperty<IBrush> VDisabledBackgroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(
            nameof(VDisabledBackground), Brushes.Gray);

    public static readonly StyledProperty<IBrush> VPointeroverTextForegroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(
            nameof(VPointeroverTextForeground), Brushes.Black);

    public static readonly StyledProperty<IBrush> VCheckedIconForegroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(
            nameof(VCheckedIconForeground), Brushes.Black);

    public static readonly StyledProperty<IBrush> VCheckedTextForegroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(
            nameof(VCheckedTextForeground), Brushes.Black);

    public static readonly StyledProperty<IBrush> VPointeroverBackgroundColorProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(nameof(VPointeroverBackgroundColor), AccentBrush!);

    public static readonly StyledProperty<IBrush> VCheckedBackgroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(nameof(VCheckedBackground), AccentBrush!);

    public static readonly StyledProperty<IBrush> VPointeroverBorderBrushProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(nameof(VPointeroverBorderBrush), AccentBrush!);

    public static readonly StyledProperty<IBrush> VIconNormalForegroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(nameof(VIconNormalForeground), AccentBrush!);

    public static readonly StyledProperty<IBrush> VCheckedBorderBrushProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(nameof(VCheckedBorderBrush), Brushes.Transparent);

    public static readonly StyledProperty<IBrush> VPointeroverIconForegroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(nameof(VPointeroverIconForeground), Brushes.Black);

    public static readonly StyledProperty<Thickness> VTextMarginProperty =
        AvaloniaProperty.Register<GroupButton, Thickness>(nameof(VTextMargin));

    public static readonly StyledProperty<HorizontalAlignment> VTextHorizontalAlignmentProperty =
        AvaloniaProperty.Register<GroupButton, HorizontalAlignment>(nameof(VTextHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VTextVerticalAlignmentProperty =
        AvaloniaProperty.Register<GroupButton, VerticalAlignment>(nameof(VTextVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<Thickness> VIconOneMarginProperty =
        AvaloniaProperty.Register<GroupButton, Thickness>(nameof(VIconOneMargin));

    public static readonly StyledProperty<Thickness> VIconTwoMarginProperty =
        AvaloniaProperty.Register<GroupButton, Thickness>(nameof(VIconTwoMargin));

    public static readonly StyledProperty<HorizontalAlignment> VIconOneHorizontalAlignmentProperty =
        AvaloniaProperty.Register<GroupButton, HorizontalAlignment>(nameof(VIconOneHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VIconTwoHorizontalAlignmentProperty =
        AvaloniaProperty.Register<GroupButton, HorizontalAlignment>(nameof(VIconTwoHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconOneVerticalAlignmentProperty =
        AvaloniaProperty.Register<GroupButton, VerticalAlignment>(nameof(VIconOneVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VIconTwoVerticalAlignmentProperty =
        AvaloniaProperty.Register<GroupButton, VerticalAlignment>(nameof(VIconTwoVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<Geometry?> VNormalIconTwoProperty =
        AvaloniaProperty.Register<GroupButton, Geometry?>(nameof(VNormalIconTwo));

    public static readonly StyledProperty<Geometry?> VNormalIconOneProperty =
        AvaloniaProperty.Register<GroupButton, Geometry?>(nameof(VNormalIconOne));

    public static readonly StyledProperty<Thickness> VPaddingProperty =
        AvaloniaProperty.Register<GroupButton, Thickness>(nameof(VPadding));

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<GroupButton, Thickness>(nameof(VBorderThickness));

    public static readonly StyledProperty<IBrush> VNormalBorderBrushProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(nameof(VNormalBorderBrush));

    public static readonly StyledProperty<CornerRadius> VCornerRadiusProperty =
        AvaloniaProperty.Register<GroupButton, CornerRadius>(nameof(VCornerRadius), CornerRadius.Parse("10"));

    public static readonly StyledProperty<IBrush> VTextForegroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(nameof(VTextForeground), AccentBrush!);

    public static readonly StyledProperty<IBrush> VNormalBackgroundProperty =
        AvaloniaProperty.Register<GroupButton, IBrush>(nameof(VNormalBackground), Brushes.Black);

    public static readonly StyledProperty<Thickness> VImageTwoMarginProperty =
        AvaloniaProperty.Register<GroupButton, Thickness>(nameof(VImageTwoMargin));

    public static readonly StyledProperty<Thickness> VImageOneMarginProperty =
        AvaloniaProperty.Register<GroupButton, Thickness>(nameof(VImageOneMargin));

    public static readonly StyledProperty<HorizontalAlignment> VImageTwoHorizontalAlignmentProperty =
        AvaloniaProperty.Register<GroupButton, HorizontalAlignment>(nameof(VImageTwoHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<HorizontalAlignment> VImageOneHorizontalAlignmentProperty =
        AvaloniaProperty.Register<GroupButton, HorizontalAlignment>(nameof(VImageOneHorizontalAlignment), HorizontalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VImageTwoVerticalAlignmentProperty =
        AvaloniaProperty.Register<GroupButton, VerticalAlignment>(nameof(VImageTwoVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<VerticalAlignment> VImageOneVerticalAlignmentProperty =
        AvaloniaProperty.Register<GroupButton, VerticalAlignment>(nameof(VImageOneVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<IImage?> VNormalImageOneProperty =
        AvaloniaProperty.Register<GroupButton, IImage?>(nameof(VNormalImageOne));

    public static readonly StyledProperty<IImage?> VNormalImageTwoProperty =
        AvaloniaProperty.Register<GroupButton, IImage?>(nameof(VNormalImageTwo));

    public static readonly StyledProperty<IImage?> VCheckedImageOneProperty =
        AvaloniaProperty.Register<GroupButton, IImage?>(nameof(VCheckedImageOne));

    public static readonly StyledProperty<IImage?> VCheckedImageTwoProperty =
        AvaloniaProperty.Register<GroupButton, IImage?>(nameof(VCheckedImageTwo));

    public static readonly StyledProperty<double> VImageOneWidthProperty =
        AvaloniaProperty.Register<GroupButton, double>(nameof(VImageOneWidth), 35);

    public static readonly StyledProperty<double> VImageTwoWidthProperty =
        AvaloniaProperty.Register<GroupButton, double>(nameof(VImageTwoWidth), 35);

    public static readonly StyledProperty<double> VImageOneHeightProperty =
        AvaloniaProperty.Register<GroupButton, double>(nameof(VImageOneHeight), 35);

    public static readonly StyledProperty<double> VImageTwoHeightProperty =
        AvaloniaProperty.Register<GroupButton, double>(nameof(VImageTwoHeight), 35);

    public static readonly StyledProperty<double> VIconTwoHeightProperty =
        AvaloniaProperty.Register<GroupButton, double>(nameof(VIconTwoHeight), 35);

    public static readonly StyledProperty<FontWeight> VFontWeightProperty =
        AvaloniaProperty.Register<GroupButton, FontWeight>(nameof(VFontWeight), FontWeight.Normal);

    public static readonly StyledProperty<double> VFontSizeProperty =
        AvaloniaProperty.Register<GroupButton, double>(nameof(VFontSize), 32);

    public static readonly StyledProperty<double> VIconTwoWidthProperty =
        AvaloniaProperty.Register<GroupButton, double>(nameof(VIconTwoWidth), 35);

    public static readonly StyledProperty<double> VIconOneWidthProperty =
        AvaloniaProperty.Register<GroupButton, double>(nameof(VIconOneWidth), 35);

    public static readonly StyledProperty<double> VIconOneHeightProperty =
        AvaloniaProperty.Register<GroupButton, double>(nameof(VIconOneHeight), 35);

    public static readonly DirectProperty<GroupButton, string?> VTextProperty =
        AvaloniaProperty.RegisterDirect<GroupButton, string?>(
            nameof(VText),
            o => o.VText,
            (o, v) => o.VText = v);

    public static readonly StyledProperty<ITransform?> VRenderTransformProperty =
        AvaloniaProperty.Register<GroupButton, ITransform?>(nameof(VRenderTransform), new ScaleTransform(0.98d, 0.98d));

    private static readonly IBrush AccentBrush =
        new ImmutableSolidColorBrush(AvaloniaViewExtensions.HexToColor("#43B6DB"));

    private string? vText = string.Empty;

    public IBrush VDisabledIconForeground
    {
        get => GetValue(VDisabledIconForegroundProperty);
        set => SetValue(VDisabledIconForegroundProperty, value);
    }

    public IBrush VDisabledTextForeground
    {
        get => GetValue(VDisabledTextForegroundProperty);
        set => SetValue(VDisabledTextForegroundProperty, value);
    }

    public IBrush VDisabledBorderBrush
    {
        get => GetValue(VDisabledBorderBrushProperty);
        set => SetValue(VDisabledBorderBrushProperty, value);
    }

    public IBrush VDisabledBackground
    {
        get => GetValue(VDisabledBackgroundProperty);
        set => SetValue(VDisabledBackgroundProperty, value);
    }

    public IBrush VPointeroverTextForeground
    {
        get => GetValue(VPointeroverTextForegroundProperty);
        set => SetValue(VPointeroverTextForegroundProperty, value);
    }

    public IBrush VCheckedIconForeground
    {
        get => GetValue(VCheckedIconForegroundProperty);
        set => SetValue(VCheckedIconForegroundProperty, value);
    }

    public IBrush VCheckedTextForeground
    {
        get => GetValue(VCheckedTextForegroundProperty);
        set => SetValue(VCheckedTextForegroundProperty, value);
    }

    public IBrush VPointeroverIconForeground
    {
        get => GetValue(VPointeroverIconForegroundProperty);
        set => SetValue(VPointeroverIconForegroundProperty, value);
    }

    public IBrush VPointeroverBorderBrush
    {
        get => GetValue(VPointeroverBorderBrushProperty);
        set => SetValue(VPointeroverBorderBrushProperty, value);
    }

    public IBrush VCheckedBorderBrush
    {
        get => GetValue(VCheckedBorderBrushProperty);
        set => SetValue(VCheckedBorderBrushProperty, value);
    }

    public IBrush VPointeroverBackgroundColor
    {
        get => GetValue(VPointeroverBackgroundColorProperty);
        set => SetValue(VPointeroverBackgroundColorProperty, value);
    }

    public IBrush VCheckedBackground
    {
        get => GetValue(VCheckedBackgroundProperty);
        set => SetValue(VCheckedBackgroundProperty, value);
    }

    public IImage? VNormalImageOne
    {
        get => GetValue(VNormalImageOneProperty);
        set => SetValue(VNormalImageOneProperty, value);
    }

    public IImage? VNormalImageTwo
    {
        get => GetValue(VNormalImageTwoProperty);
        set => SetValue(VNormalImageTwoProperty, value);
    }

    public IImage? VCheckedImageOne
    {
        get => GetValue(VCheckedImageOneProperty);
        set => SetValue(VCheckedImageOneProperty, value);
    }

    public IImage? VCheckedImageTwo
    {
        get => GetValue(VCheckedImageTwoProperty);
        set => SetValue(VCheckedImageTwoProperty, value);
    }

    public double VImageTwoHeight
    {
        get => GetValue(VImageTwoHeightProperty);
        set => SetValue(VImageTwoHeightProperty, value);
    }

    public double VImageOneHeight
    {
        get => GetValue(VImageOneHeightProperty);
        set => SetValue(VImageOneHeightProperty, value);
    }

    public double VImageTwoWidth
    {
        get => GetValue(VImageTwoWidthProperty);
        set => SetValue(VImageTwoWidthProperty, value);
    }

    public double VImageOneWidth
    {
        get => GetValue(VImageOneWidthProperty);
        set => SetValue(VImageOneWidthProperty, value);
    }

    public Thickness VImageTwoMargin
    {
        get => GetValue(VImageTwoMarginProperty);
        set => SetValue(VImageTwoMarginProperty, value);
    }

    public Thickness VImageOneMargin
    {
        get => GetValue(VImageOneMarginProperty);
        set => SetValue(VImageOneMarginProperty, value);
    }

    public HorizontalAlignment VImageTwoHorizontalAlignment
    {
        get => GetValue(VImageTwoHorizontalAlignmentProperty);
        set => SetValue(VImageTwoHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VImageOneHorizontalAlignment
    {
        get => GetValue(VImageOneHorizontalAlignmentProperty);
        set => SetValue(VImageOneHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VImageTwoVerticalAlignment
    {
        get => GetValue(VImageTwoVerticalAlignmentProperty);
        set => SetValue(VImageTwoVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VImageOneVerticalAlignment
    {
        get => GetValue(VImageOneVerticalAlignmentProperty);
        set => SetValue(VImageOneVerticalAlignmentProperty, value);
    }

    public double VIconTwoHeight
    {
        get => GetValue(VIconTwoHeightProperty);
        set => SetValue(VIconTwoHeightProperty, value);
    }

    public IBrush VNormalBorderBrush
    {
        get => GetValue(VNormalBorderBrushProperty);
        set => SetValue(VNormalBorderBrushProperty, value);
    }

    public IBrush VNormalBackground
    {
        get => GetValue(VNormalBackgroundProperty);
        set => SetValue(VNormalBackgroundProperty, value);
    }

    public Thickness VPadding
    {
        get => GetValue(VPaddingProperty);
        set => SetValue(VPaddingProperty, value);
    }

    public Thickness VBorderThickness
    {
        get => GetValue(VBorderThicknessProperty);
        set => SetValue(VBorderThicknessProperty, value);
    }

    public CornerRadius VCornerRadius
    {
        get => GetValue(VCornerRadiusProperty);
        set => SetValue(VCornerRadiusProperty, value);
    }

    public IBrush VTextForeground
    {
        get => GetValue(VTextForegroundProperty);
        set => SetValue(VTextForegroundProperty, value);
    }

    public HorizontalAlignment VTextHorizontalAlignment
    {
        get => GetValue(VTextHorizontalAlignmentProperty);
        set => SetValue(VTextHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VTextVerticalAlignment
    {
        get => GetValue(VTextVerticalAlignmentProperty);
        set => SetValue(VTextVerticalAlignmentProperty, value);
    }

    public Thickness VTextMargin
    {
        get => GetValue(VTextMarginProperty);
        set => SetValue(VTextMarginProperty, value);
    }

    public string? VText
    {
        get => vText;
        set => SetAndRaise(VTextProperty, ref vText, value);
    }

    public FontWeight VFontWeight
    {
        get => GetValue(VFontWeightProperty);
        set => SetValue(VFontWeightProperty, value);
    }

    public double VFontSize
    {
        get => GetValue(VFontSizeProperty);
        set => SetValue(VFontSizeProperty, value);
    }

    public IBrush VIconNormalForeground
    {
        get => GetValue(VIconNormalForegroundProperty);
        set => SetValue(VIconNormalForegroundProperty, value);
    }

    public double VIconOneHeight
    {
        get => GetValue(VIconOneHeightProperty);
        set => SetValue(VIconOneHeightProperty, value);
    }

    public double VIconOneWidth
    {
        get => GetValue(VIconOneWidthProperty);
        set => SetValue(VIconOneWidthProperty, value);
    }

    public double VIconTwoWidth
    {
        get => GetValue(VIconTwoWidthProperty);
        set => SetValue(VIconTwoWidthProperty, value);
    }

    public Thickness VIconOneMargin
    {
        get => GetValue(VIconOneMarginProperty);
        set => SetValue(VIconOneMarginProperty, value);
    }

    public Thickness VIconTwoMargin
    {
        get => GetValue(VIconTwoMarginProperty);
        set => SetValue(VIconTwoMarginProperty, value);
    }

    public HorizontalAlignment VIconOneHorizontalAlignment
    {
        get => GetValue(VIconOneHorizontalAlignmentProperty);
        set => SetValue(VIconOneHorizontalAlignmentProperty, value);
    }

    public HorizontalAlignment VIconTwoHorizontalAlignment
    {
        get => GetValue(VIconTwoHorizontalAlignmentProperty);
        set => SetValue(VIconTwoHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VIconOneVerticalAlignment
    {
        get => GetValue(VIconOneVerticalAlignmentProperty);
        set => SetValue(VIconOneVerticalAlignmentProperty, value);
    }

    public VerticalAlignment VIconTwoVerticalAlignment
    {
        get => GetValue(VIconTwoVerticalAlignmentProperty);
        set => SetValue(VIconTwoVerticalAlignmentProperty, value);
    }

    public Geometry? VNormalIconOne
    {
        get => GetValue(VNormalIconOneProperty);
        set => SetValue(VNormalIconOneProperty, value);
    }

    public Geometry? VNormalIconTwo
    {
        get => GetValue(VNormalIconTwoProperty);
        set => SetValue(VNormalIconTwoProperty, value);
    }

    public ITransform? VRenderTransform
    {
        get => GetValue(VRenderTransformProperty);
        set => SetValue(VRenderTransformProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (e == null)
        {
            return;
        }

        if (VNormalIconOne == null)
        {
            var icon = e.NameScope.Find<PathIcon>("PART_IconOne");
            if (icon != null)
            {
                icon.IsEnabled = false;
            }
        }

        if (VNormalIconTwo == null)
        {
            var icon = e.NameScope.Find<PathIcon>("PART_IconTwo");
            if (icon != null)
            {
                icon.IsEnabled = false;
            }
        }

        if (VNormalImageOne == null)
        {
            var image = e.NameScope.Find<Image>("PART_ImageOne");
            if (image != null)
            {
                image.IsEnabled = false;
            }
        }

        if (string.IsNullOrEmpty(VText))
        {
            var t = e.NameScope.Find<TextBlock>("PART_MainText");
            if (t != null)
            {
                t.IsEnabled = false;
            }
        }
    }
}