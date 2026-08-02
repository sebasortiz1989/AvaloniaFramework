using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Media;

namespace AvaloniaFramework.Controls.Inputs;

/// <summary>
/// A single-line text field with its label sitting on the border, and separate brushes for the
/// resting and focused states. <see cref="VText"/> binds two-way by default, so
/// <c>VText="{Binding Email}"</c> is all a form field needs.
/// </summary>
public class VTextBoxWithLabel : TemplatedControl
{
    /// <summary>The entered text. Binds two-way by default.</summary>
    public static readonly StyledProperty<string?> VTextProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, string?>(
            nameof(VText),
            string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

    /// <summary>The label drawn over the top border.</summary>
    public static readonly StyledProperty<string?> VDescriptionTextProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, string?>(nameof(VDescriptionText));

    /// <summary>Placeholder shown while the field is empty.</summary>
    public static readonly StyledProperty<string?> VTextWatermarkProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, string?>(nameof(VTextWatermark));

    /// <summary>Set to mask input, as for a password field.</summary>
    public static readonly StyledProperty<char> VPasswordCharProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, char>(nameof(VPasswordChar), char.MinValue);

    public static readonly StyledProperty<double> VWidthProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, double>(nameof(VWidth), double.NaN);

    public static readonly StyledProperty<double> VHeightProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, double>(nameof(VHeight), 60d);

    public static readonly StyledProperty<double> VTextFontSizeProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, double>(nameof(VTextFontSize), 16d);

    public static readonly StyledProperty<double> VContentFontSizeProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, double>(nameof(VContentFontSize), 24d);

    public static readonly StyledProperty<double> VDescriptionFontSizeProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, double>(nameof(VDescriptionFontSize), 20d);

    public static readonly StyledProperty<FontWeight> VDescriptionFontWeightProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, FontWeight>(nameof(VDescriptionFontWeight), FontWeight.Normal);

    public static readonly StyledProperty<IBrush?> VBackgroundProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush?>(nameof(VBackground), Brushes.Transparent);

    public static readonly StyledProperty<IBrush?> VTextForegroundProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush?>(nameof(VTextForeground), Brushes.Black);

    public static readonly StyledProperty<IBrush?> VTextForegroundFocusProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush?>(nameof(VTextForegroundFocus), Brushes.Black);

    public static readonly StyledProperty<IBrush?> VBorderBrushProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush?>(nameof(VBorderBrush), Brushes.Gray);

    public static readonly StyledProperty<IBrush?> VBorderBrushFocusProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush?>(nameof(VBorderBrushFocus), Brushes.Black);

    public static readonly StyledProperty<IBrush?> VCaretBrushProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush?>(nameof(VCaretBrush), Brushes.Black);

    public static readonly StyledProperty<IBrush?> VCaretBrushFocusProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush?>(nameof(VCaretBrushFocus), Brushes.Black);

    public static readonly StyledProperty<IBrush?> VDescriptionForegroundProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush?>(nameof(VDescriptionForeground), Brushes.Gray);

    /// <summary>
    /// Painted behind the label so it masks the border it sits on. Set it to the surface colour
    /// the field is placed on.
    /// </summary>
    public static readonly StyledProperty<IBrush?> VDescriptionBackgroundProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, IBrush?>(nameof(VDescriptionBackground), Brushes.White);

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, Thickness>(nameof(VBorderThickness), new Thickness(2));

    public static readonly StyledProperty<CornerRadius> VCornerRadiusProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, CornerRadius>(nameof(VCornerRadius), new CornerRadius(8));

    public static readonly StyledProperty<Thickness> VTextBoxPaddingProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, Thickness>(nameof(VTextBoxPadding), new Thickness(14, 0, 0, 0));

    public static readonly StyledProperty<Thickness> VDescriptionPaddingProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, Thickness>(nameof(VDescriptionPadding), new Thickness(4, 0, 4, 0));

    public static readonly StyledProperty<TextAlignment> VTextBoxTextAlignmentProperty =
        AvaloniaProperty.Register<VTextBoxWithLabel, TextAlignment>(nameof(VTextBoxTextAlignment), TextAlignment.Left);

    /// <inheritdoc cref="VTextProperty" />
    public string? VText
    {
        get => GetValue(VTextProperty);
        set => SetValue(VTextProperty, value);
    }

    /// <inheritdoc cref="VDescriptionTextProperty" />
    public string? VDescriptionText
    {
        get => GetValue(VDescriptionTextProperty);
        set => SetValue(VDescriptionTextProperty, value);
    }

    /// <inheritdoc cref="VTextWatermarkProperty" />
    public string? VTextWatermark
    {
        get => GetValue(VTextWatermarkProperty);
        set => SetValue(VTextWatermarkProperty, value);
    }

    /// <inheritdoc cref="VPasswordCharProperty" />
    public char VPasswordChar
    {
        get => GetValue(VPasswordCharProperty);
        set => SetValue(VPasswordCharProperty, value);
    }

    public double VWidth
    {
        get => GetValue(VWidthProperty);
        set => SetValue(VWidthProperty, value);
    }

    public double VHeight
    {
        get => GetValue(VHeightProperty);
        set => SetValue(VHeightProperty, value);
    }

    public double VTextFontSize
    {
        get => GetValue(VTextFontSizeProperty);
        set => SetValue(VTextFontSizeProperty, value);
    }

    public double VContentFontSize
    {
        get => GetValue(VContentFontSizeProperty);
        set => SetValue(VContentFontSizeProperty, value);
    }

    public double VDescriptionFontSize
    {
        get => GetValue(VDescriptionFontSizeProperty);
        set => SetValue(VDescriptionFontSizeProperty, value);
    }

    public FontWeight VDescriptionFontWeight
    {
        get => GetValue(VDescriptionFontWeightProperty);
        set => SetValue(VDescriptionFontWeightProperty, value);
    }

    public IBrush? VBackground
    {
        get => GetValue(VBackgroundProperty);
        set => SetValue(VBackgroundProperty, value);
    }

    public IBrush? VTextForeground
    {
        get => GetValue(VTextForegroundProperty);
        set => SetValue(VTextForegroundProperty, value);
    }

    public IBrush? VTextForegroundFocus
    {
        get => GetValue(VTextForegroundFocusProperty);
        set => SetValue(VTextForegroundFocusProperty, value);
    }

    public IBrush? VBorderBrush
    {
        get => GetValue(VBorderBrushProperty);
        set => SetValue(VBorderBrushProperty, value);
    }

    public IBrush? VBorderBrushFocus
    {
        get => GetValue(VBorderBrushFocusProperty);
        set => SetValue(VBorderBrushFocusProperty, value);
    }

    public IBrush? VCaretBrush
    {
        get => GetValue(VCaretBrushProperty);
        set => SetValue(VCaretBrushProperty, value);
    }

    public IBrush? VCaretBrushFocus
    {
        get => GetValue(VCaretBrushFocusProperty);
        set => SetValue(VCaretBrushFocusProperty, value);
    }

    public IBrush? VDescriptionForeground
    {
        get => GetValue(VDescriptionForegroundProperty);
        set => SetValue(VDescriptionForegroundProperty, value);
    }

    /// <inheritdoc cref="VDescriptionBackgroundProperty" />
    public IBrush? VDescriptionBackground
    {
        get => GetValue(VDescriptionBackgroundProperty);
        set => SetValue(VDescriptionBackgroundProperty, value);
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

    public Thickness VTextBoxPadding
    {
        get => GetValue(VTextBoxPaddingProperty);
        set => SetValue(VTextBoxPaddingProperty, value);
    }

    public Thickness VDescriptionPadding
    {
        get => GetValue(VDescriptionPaddingProperty);
        set => SetValue(VDescriptionPaddingProperty, value);
    }

    public TextAlignment VTextBoxTextAlignment
    {
        get => GetValue(VTextBoxTextAlignmentProperty);
        set => SetValue(VTextBoxTextAlignmentProperty, value);
    }

}
