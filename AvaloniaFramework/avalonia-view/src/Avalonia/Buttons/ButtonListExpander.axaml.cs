using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class ButtonListExpander : SplitView
{
    public static readonly DirectProperty<ButtonListExpander, double> VButtonIconHeightProperty =
        AvaloniaProperty.RegisterDirect<ButtonListExpander, double>(
            nameof(VButtonIconHeight),
            o => o.VButtonIconHeight,
            (o, v) => o.VButtonIconHeight = v);

    public static readonly DirectProperty<ButtonListExpander, double> VButtonIconWidthProperty =
        AvaloniaProperty.RegisterDirect<ButtonListExpander, double>(
            nameof(VButtonIconWidth),
            o => o.VButtonIconWidth,
            (o, v) => o.VButtonIconWidth = v);

    public static readonly DirectProperty<ButtonListExpander, double> VButtonWidthProperty =
        AvaloniaProperty.RegisterDirect<ButtonListExpander, double>(
            nameof(VButtonWidth),
            o => o.VButtonWidth,
            (o, v) => o.VButtonWidth = v);

    public static readonly DirectProperty<ButtonListExpander, double> VButtonHeightProperty =
        AvaloniaProperty.RegisterDirect<ButtonListExpander, double>(
            nameof(VButtonHeight),
            o => o.VButtonHeight,
            (o, v) => o.VButtonHeight = v);

    public static readonly DirectProperty<ButtonListExpander, double> VButtonIconRotateProperty =
        AvaloniaProperty.RegisterDirect<ButtonListExpander, double>(
            nameof(VButtonIconRotate),
            o => o.VButtonIconRotate,
            (o, v) => o.VButtonIconRotate = v);

    public static readonly StyledProperty<IBrush> VButtonDisabledBackgroundProperty =
        AvaloniaProperty.Register<ButtonListExpander, IBrush>(nameof(VButtonDisabledBackground));

    public static readonly StyledProperty<IBrush> VButtonPointeroverBackgroundProperty =
        AvaloniaProperty.Register<ButtonListExpander, IBrush>(nameof(VButtonPointeroverBackground));

    public static readonly StyledProperty<IBrush> VButtonPressedBackgroundProperty =
        AvaloniaProperty.Register<ButtonListExpander, IBrush>(nameof(VButtonPressedBackground));

    public static readonly StyledProperty<IBrush> VButtonNormalBackgroundProperty =
        AvaloniaProperty.Register<ButtonListExpander, IBrush>(nameof(VButtonNormalBackground));

    public static readonly StyledProperty<IBrush> VButtonIconForegroundProperty =
        AvaloniaProperty.Register<ButtonListExpander, IBrush>(nameof(VButtonIconForeground));

    public static readonly StyledProperty<IBrush> VButtonIconForegroundOpenedProperty =
        AvaloniaProperty.Register<ButtonListExpander, IBrush>(nameof(VButtonIconForegroundOpened));

    public static readonly StyledProperty<HorizontalAlignment> VButtonHorizontalAlignmentProperty =
        AvaloniaProperty.Register<ButtonListExpander, HorizontalAlignment>(nameof(VButtonHorizontalAlignment), HorizontalAlignment.Left);

    public static readonly StyledProperty<Thickness> VButtonMarginProperty =
        AvaloniaProperty.Register<ButtonListExpander, Thickness>(nameof(VButtonMargin));

    public static readonly StyledProperty<VerticalAlignment> VButtonVerticalAlignmentProperty =
        AvaloniaProperty.Register<ButtonListExpander, VerticalAlignment>(nameof(VButtonVerticalAlignment), VerticalAlignment.Center);

    public static readonly StyledProperty<Geometry> VButtonIconProperty =
        AvaloniaProperty.Register<ButtonListExpander, Geometry>(nameof(VButtonIcon));

    public static readonly StyledProperty<Geometry> VButtonIconOpenedProperty =
       AvaloniaProperty.Register<ButtonListExpander, Geometry>(nameof(VButtonIconOpened));

    private double vButtonHeight;
    private double vButtonWidth;
    private double vButtonIconHeight;
    private double vButtonIconWidth;
    private double vButtonIconRotate;
    private SplitView? mainSplitView;
    private Button? expandButton;
    private PathIcon? chevron;

    public double VButtonIconHeight
    {
        get => vButtonIconHeight;
        set => SetAndRaise(VButtonIconHeightProperty, ref vButtonIconHeight, value);
    }

    public double VButtonIconWidth
    {
        get => vButtonIconWidth;
        set => SetAndRaise(VButtonIconWidthProperty, ref vButtonIconWidth, value);
    }

    public double VButtonWidth
    {
        get => vButtonWidth;
        set => SetAndRaise(VButtonWidthProperty, ref vButtonWidth, value);
    }

    public double VButtonHeight
    {
        get => vButtonHeight;
        set => SetAndRaise(VButtonHeightProperty, ref vButtonHeight, value);
    }

    public double VButtonIconRotate
    {
        get => vButtonIconRotate;
        set => SetAndRaise(VButtonIconRotateProperty, ref vButtonIconRotate, value);
    }

    public IBrush VButtonDisabledBackground
    {
        get => GetValue(VButtonDisabledBackgroundProperty);
        set => SetValue(VButtonDisabledBackgroundProperty, value);
    }

    public IBrush VButtonPointeroverBackground
    {
        get => GetValue(VButtonPointeroverBackgroundProperty);
        set => SetValue(VButtonPointeroverBackgroundProperty, value);
    }

    public IBrush VButtonPressedBackground
    {
        get => GetValue(VButtonPressedBackgroundProperty);
        set => SetValue(VButtonPressedBackgroundProperty, value);
    }

    public IBrush VButtonNormalBackground
    {
        get => GetValue(VButtonNormalBackgroundProperty);
        set => SetValue(VButtonNormalBackgroundProperty, value);
    }

    public IBrush VButtonIconForeground
    {
        get => GetValue(VButtonIconForegroundProperty);
        set => SetValue(VButtonIconForegroundProperty, value);
    }

    public IBrush VButtonIconForegroundOpened
    {
        get => GetValue(VButtonIconForegroundOpenedProperty);
        set => SetValue(VButtonIconForegroundOpenedProperty, value);
    }

    public Thickness VButtonMargin
    {
        get => GetValue(VButtonMarginProperty);
        set => SetValue(VButtonMarginProperty, value);
    }

    public HorizontalAlignment VButtonHorizontalAlignment
    {
        get => GetValue(VButtonHorizontalAlignmentProperty);
        set => SetValue(VButtonHorizontalAlignmentProperty, value);
    }

    public VerticalAlignment VButtonVerticalAlignment
    {
        get => GetValue(VButtonVerticalAlignmentProperty);
        set => SetValue(VButtonVerticalAlignmentProperty, value);
    }

    public Geometry VButtonIcon
    {
        get => GetValue(VButtonIconProperty);
        set => SetValue(VButtonIconProperty, value);
    }

    public Geometry VButtonIconOpened
    {
        get => GetValue(VButtonIconOpenedProperty);
        set => SetValue(VButtonIconOpenedProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (e == null)
        {
            return;
        }

        mainSplitView = e.NameScope.Find<SplitView>("MainSplitView");
        chevron = e.NameScope.Find<PathIcon>("Chevron");
        expandButton = e.NameScope.Find<Button>("ExpandButton");

        if (expandButton != null)
        {
            expandButton.Click += TopButton_Click;
        }
    }

    private void TopButton_Click(object? sender, RoutedEventArgs e)
    {
        IsPaneOpen = !IsPaneOpen;

        if (IsPaneOpen!)
        {
            chevron!.Data = VButtonIconOpened;
        }
        else
        {
            chevron!.Data = VButtonIcon;
        }
    }
}