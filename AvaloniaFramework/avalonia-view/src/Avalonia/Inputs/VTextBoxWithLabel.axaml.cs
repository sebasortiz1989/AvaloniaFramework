using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Text.RegularExpressions;
using AvaloniaFramework.Apresentacao.Buttons;

namespace AvaloniaFramework.Apresentacao.Inputs;

public class VTextBoxWithLabel : TemplatedControl
{
    public static readonly StyledProperty<double> VHeightProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, double>(nameof(VHeight), 60);

    public static readonly StyledProperty<double> VWidthProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, double>(nameof(VWidth), double.NaN);

    public static readonly StyledProperty<double> VContentFontSizeProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, double>(nameof(VContentFontSize), 24);

    public static readonly StyledProperty<double> VDescriptionFontSizeProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, double>(nameof(VDescriptionFontSize), 20);

    public static readonly StyledProperty<bool> VAllowDecimalsProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, bool>(nameof(VAllowDecimals));

    public static readonly StyledProperty<bool> VIsDescriptionVisibleProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, bool>(nameof(VIsDescriptionVisible), false);

    public static readonly StyledProperty<string> VTextProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, string>(nameof(VText), string.Empty, false, BindingMode.TwoWay);

    public static readonly StyledProperty<string?> VTextWatermarkProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, string?>(nameof(VTextWatermark), "Insira o Valor");

    public static readonly StyledProperty<string?> VDescriptionTextProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, string?>(nameof(VDescriptionText), "Lorem Ipsum Dolor");

    public static readonly StyledProperty<IBrush> VBackgroundProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush>(nameof(VBackground), Brushes.Transparent);

    public static readonly StyledProperty<IBrush> VTextForegroundProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush>(nameof(VTextForeground), Brush.Parse("#FFFFFF"), true, BindingMode.OneTime);

    public static readonly StyledProperty<IBrush> VBorderBrushProperty =
       AvaloniaProperty.Register<VTextBoxWithLabel, IBrush>(nameof(VBorderBrush), Brush.Parse("#43B6DB"), true, BindingMode.OneTime);

    public static readonly StyledProperty<IBrush> VCaretBrushProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush>(nameof(VCaretBrush), Brush.Parse("#FFFFFF"));

    public static readonly StyledProperty<IBrush> VDescriptionBackgroundProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush>(nameof(VDescriptionBackground), Brush.Parse("#040404"));

    public static readonly StyledProperty<IBrush> VDescriptionForegroundProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush>(nameof(VDescriptionForeground), Brush.Parse("#43B6DB"));

    public static readonly StyledProperty<IBrush> VCaretBrushFocusProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush>(nameof(VCaretBrushFocus), Brush.Parse("#9DFDFF"));

    public static readonly StyledProperty<IBrush> VBackgroundFocusProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush>(nameof(VBackgroundFocus), Brushes.Transparent);

    public static readonly StyledProperty<IBrush> VTextForegroundFocusProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush>(nameof(VTextForegroundFocus), Brush.Parse("#9DFDFF"));

    public static readonly StyledProperty<IBrush> VBorderBrushFocusProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush>(nameof(VBorderBrushFocus), Brush.Parse("#9DFDFF"));

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
    AvaloniaProperty.Register<VTextBoxWithLabel, Thickness>(nameof(VBorderThickness), new Thickness(2));

    public static readonly StyledProperty<Thickness> VTextBoxPaddingProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, Thickness>(nameof(VTextBoxPadding), Thickness.Parse("14,0,0,0"));

    public static readonly StyledProperty<Thickness> VBorderThicknessFocusProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, Thickness>(nameof(VBorderThicknessFocus), new Thickness(2));

    public static readonly StyledProperty<Thickness> VDescriptionPaddingProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, Thickness>(nameof(VDescriptionPadding), Thickness.Parse("4,0,4,0"));

    public static readonly StyledProperty<FontWeight> VContentFontWeightProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, FontWeight>(nameof(VContentFontWeight), FontWeight.Normal);

    public static readonly StyledProperty<FontWeight> VDescriptionFontWeightProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, FontWeight>(nameof(VDescriptionFontWeight), FontWeight.Bold);

    public static readonly StyledProperty<CornerRadius> VCornerRadiusProperty =
        AvaloniaProperty.Register<VButtonBase, CornerRadius>(nameof(VCornerRadius), new CornerRadius(8));

    public static readonly StyledProperty<TextAlignment> VTextBoxTextAlignmentProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, TextAlignment>(nameof(VTextBoxTextAlignment), TextAlignment.Left);

    public static readonly StyledProperty<int> VTextFontSizeProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, int>(nameof(VTextFontSize), 16, true, BindingMode.OneTime);

    public static readonly StyledProperty<char> VPasswordCharProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, char>(nameof(VPasswordChar), char.MinValue, true, BindingMode.OneTime);

    public static readonly DirectProperty<VTextBoxWithLabel, double> VDescriptionLeftMarginProperty =
        AvaloniaProperty.RegisterDirect<VTextBoxWithLabel, double>(
            nameof(VDescriptionLeftMargin),
            o => o.VDescriptionLeftMargin,
            (o, v) => o.VDescriptionLeftMargin = v);

    public static readonly DirectProperty<VTextBoxWithLabel, double> VDescriptionCanvasLeftProperty =
        AvaloniaProperty.RegisterDirect<VTextBoxWithLabel, double>(
            nameof(VDescriptionCanvasLeft),
            o => o.VDescriptionCanvasLeft,
            (o, v) => o.VDescriptionCanvasLeft = v);

    public static readonly DirectProperty<VTextBoxWithLabel, bool> VOnlyNumbersProperty =
        AvaloniaProperty.RegisterDirect<VTextBoxWithLabel, bool>(
            "VOnlyNumbers",
            o => o.VOnlyNumbers,
            (o, v) => o.VOnlyNumbers = v);

    private double vDescriptionCanvasLeft = 12.28;
    private double vDescriptionLeftMargin = 16;
    private bool vOnlyNumbers;
    private bool beginWriting;
    private TextBlock? descriptionLabel;
    private TextBox? internalTextBox;
    private Panel? contentHolder;

    public double VHeight
    {
        get => GetValue(VHeightProperty);
        set => SetValue(VHeightProperty, value);
    }

    public double VWidth
    {
        get => GetValue(VWidthProperty);
        set => SetValue(VWidthProperty, value);
    }

    public double VDescriptionFontSize
    {
        get => GetValue(VDescriptionFontSizeProperty);
        set => SetValue(VDescriptionFontSizeProperty, value);
    }

    public double VDescriptionCanvasLeft
    {
        get => vDescriptionCanvasLeft;
        set => SetAndRaise(VDescriptionCanvasLeftProperty, ref vDescriptionCanvasLeft, value);
    }

    public double VContentFontSize
    {
        get => GetValue(VContentFontSizeProperty);
        set => SetValue(VContentFontSizeProperty, value);
    }

    public double VDescriptionLeftMargin
    {
        get => vDescriptionLeftMargin;
        set => SetAndRaise(VDescriptionLeftMarginProperty, ref vDescriptionLeftMargin, value);
    }

    public bool VIsDescriptionVisible
    {
        get => GetValue(VIsDescriptionVisibleProperty);
        set => SetValue(VIsDescriptionVisibleProperty, value);
    }

    public bool VAllowDecimals
    {
        get => GetValue(VAllowDecimalsProperty);
        set => SetValue(VAllowDecimalsProperty, value);
    }

    public bool VOnlyNumbers
    {
        get => vOnlyNumbers;
        set => SetAndRaise(VOnlyNumbersProperty, ref vOnlyNumbers, value);
    }

    public string VText
    {
        get => GetValue(VTextProperty);
        set => SetValue(VTextProperty, value);
    }

    public string? VTextWatermark
    {
        get => GetValue(VTextWatermarkProperty);
        set => SetValue(VTextWatermarkProperty, value);
    }

    public string? VDescriptionText
    {
        get => GetValue(VDescriptionTextProperty);
        set => SetValue(VDescriptionTextProperty, value);
    }

    public int VTextFontSize
    {
        get => GetValue(VTextFontSizeProperty);
        set => SetValue(VTextFontSizeProperty, value);
    }

    public char VPasswordChar
    {
        get => GetValue(VPasswordCharProperty);
        set => SetValue(VPasswordCharProperty, value);
    }

    public IBrush VBackground
    {
        get => GetValue(VBackgroundProperty);
        set => SetValue(VBackgroundProperty, value);
    }

    public IBrush VTextForeground
    {
        get => GetValue(VTextForegroundProperty);
        set => SetValue(VTextForegroundProperty, value);
    }

    public IBrush VBorderBrush
    {
        get => GetValue(VBorderBrushProperty);
        set => SetValue(VBorderBrushProperty, value);
    }

    public IBrush VCaretBrush
    {
        get => GetValue(VCaretBrushProperty);
        set => SetValue(VCaretBrushProperty, value);
    }

    public IBrush VDescriptionBackground
    {
        get => GetValue(VDescriptionBackgroundProperty);
        set => SetValue(VDescriptionBackgroundProperty, value);
    }

    public IBrush VDescriptionForeground
    {
        get => GetValue(VDescriptionForegroundProperty);
        set => SetValue(VDescriptionForegroundProperty, value);
    }

    public IBrush VBackgroundFocus
    {
        get => GetValue(VBackgroundFocusProperty);
        set => SetValue(VBackgroundFocusProperty, value);
    }

    public IBrush VTextForegroundFocus
    {
        get => GetValue(VTextForegroundFocusProperty);
        set => SetValue(VTextForegroundFocusProperty, value);
    }

    public IBrush VBorderBrushFocus
    {
        get => GetValue(VBorderBrushFocusProperty);
        set => SetValue(VBorderBrushFocusProperty, value);
    }

    public IBrush VCaretBrushFocus
    {
        get => GetValue(VCaretBrushFocusProperty);
        set => SetValue(VCaretBrushFocusProperty, value);
    }

    public Thickness VBorderThickness
    {
        get => GetValue(VBorderThicknessProperty);
        set => SetValue(VBorderThicknessProperty, value);
    }

    public Thickness VTextBoxPadding
    {
        get => GetValue(VTextBoxPaddingProperty);
        set => SetValue(VTextBoxPaddingProperty, value);
    }

    public Thickness VBorderThicknessFocus
    {
        get => GetValue(VBorderThicknessFocusProperty);
        set => SetValue(VBorderThicknessFocusProperty, value);
    }

    public Thickness VDescriptionPadding
    {
        get => GetValue(VDescriptionPaddingProperty);
        set => SetValue(VDescriptionPaddingProperty, value);
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

    public CornerRadius VCornerRadius
    {
        get => GetValue(VCornerRadiusProperty);
        set => SetValue(VCornerRadiusProperty, value);
    }

    public TextAlignment VTextBoxTextAlignment
    {
        get => GetValue(VTextBoxTextAlignmentProperty);
        set => SetValue(VTextBoxTextAlignmentProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (e == null)
        {
            return;
        }

        descriptionLabel = e.NameScope.Find<TextBlock>("DescriptionTextBlock")!;
        contentHolder = e.NameScope.Find<Panel>("ContentHolder");

        if (VOnlyNumbers)
        {
            internalTextBox = e.NameScope.Find<TextBox>("InternalTextBox");
            internalTextBox?.AddHandler(TextInputEvent, TextInputHandler, RoutingStrategies.Tunnel);
        }
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        if (contentHolder != null && descriptionLabel != null)
        {
            Canvas.SetTop(descriptionLabel, -(descriptionLabel.Bounds.Height / 2));
            Canvas.SetLeft(descriptionLabel, VDescriptionCanvasLeft);
        }
    }

    protected override void OnGotFocus(Avalonia.Input.GotFocusEventArgs e)
    {
        base.OnGotFocus(e);

        if (VOnlyNumbers)
        {
            beginWriting = true;
        }

        if (internalTextBox is { Text: not null })
        {
            internalTextBox.CaretIndex = internalTextBox.Text.Length + 1;
        }
    }

    private void TextInputHandler(object? sender, TextInputEventArgs e)
    {
        if (!VOnlyNumbers || e.Text == null)
            return;

        if (beginWriting)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                var pattern = VAllowDecimals ? @"^-?(?:0(?:\.\d*)?|[1-9]\d*(?:\.\d*)?)$" : @"^-?[0-9]*$";
                var readOnlySpan = e.Text;
                if (string.IsNullOrEmpty(e.Text) || Regex.IsMatch(readOnlySpan, pattern))
                {
                    VText = readOnlySpan;
                    internalTextBox!.CaretIndex = internalTextBox.Text!.Length + 1;
                }

                e.Handled = true;
                beginWriting = false;
            }
        }
        else
        {
            var pattern = VAllowDecimals ? @"^-?(?:0(?:\.\d*)?|[1-9]\d*(?:\.\d*)?)$" : @"^-?[0-9]*$";
            if (VText == "0")
            {
                VText = e.Text;

                if (internalTextBox is { Text: not null })
                {
                    internalTextBox.CaretIndex = internalTextBox.Text.Length;
                }

                e.Handled = true;
            }
            else
            {
                string readOnlySpan = VText + e.Text;
                if (e.Text != null && !Regex.IsMatch(readOnlySpan, pattern))
                {
                    e.Handled = true;
                }
            }
        }

        // if (e.Text != null && !char.IsDigit(e.Text[0]))
        // {
        //     e.Handled = true;
        // }
    }
}