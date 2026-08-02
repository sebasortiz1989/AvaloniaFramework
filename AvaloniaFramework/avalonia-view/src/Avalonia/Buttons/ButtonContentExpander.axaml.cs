using Avalonia;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System;

namespace AvaloniaFramework.Apresentacao.Buttons;

public class ButtonContentExpander : ButtonExpanderBase
{
    public static readonly StyledProperty<Geometry> CbCheckedIconProperty =
        AvaloniaProperty.Register<CustomButton, Geometry>(nameof(CbCheckedIcon));

    public static readonly StyledProperty<Geometry> CbUncheckedIconProperty =
        AvaloniaProperty.Register<CustomButton, Geometry>(nameof(CbUncheckedIcon));

    public static readonly StyledProperty<IBrush> CbBackgroundCheckedProperty =
        AvaloniaProperty.Register<ButtonContentExpander, IBrush>(nameof(CbBackgroundChecked));

    public static readonly StyledProperty<IBrush> CbBackgroundUncheckedProperty =
        AvaloniaProperty.Register<ButtonContentExpander, IBrush>(nameof(CbBackgroundUnchecked));

    public static readonly StyledProperty<IBrush> CbBorderBrushCheckedProperty =
        AvaloniaProperty.Register<ButtonContentExpander, IBrush>(nameof(CbBorderBrushChecked));

    public static readonly StyledProperty<IBrush> CbBorderBrushUncheckedProperty =
        AvaloniaProperty.Register<ButtonContentExpander, IBrush>(nameof(CbBorderBrushUnchecked));

    public static readonly StyledProperty<IBrush> CbCheckedIconForegroundProperty =
        AvaloniaProperty.Register<ButtonContentExpander, IBrush>(nameof(CbCheckedIconForeground));

    public static readonly StyledProperty<IBrush> CbUncheckedIconForegroundProperty =
        AvaloniaProperty.Register<ButtonContentExpander, IBrush>(nameof(CbUncheckedIconForeground));

    public static readonly DirectProperty<ButtonContentExpander, bool> CbUnderlineEnabledProperty =
        AvaloniaProperty.RegisterDirect<ButtonContentExpander, bool>(
            nameof(CbUnderlineEnabled),
            o => o.CbUnderlineEnabled,
            (o, v) => o.CbUnderlineEnabled = v);

    private bool cbUnderlineEnabled;
    private ItemsPresenter? innerItemsPresenter;

    static ButtonContentExpander()
    {
        IsExpandedProperty.Changed.AddClassHandler<ButtonContentExpander>(OnIsExpandedPropiedadeMudada);
    }

    public event EventHandler? Expanded;

    public event EventHandler? Unexpanded;

    public IBrush CbUncheckedIconForeground
    {
        get => this.GetValue(CbUncheckedIconForegroundProperty);
        set => SetValue(CbUncheckedIconForegroundProperty, value);
    }

    public bool CbUnderlineEnabled
    {
        get => cbUnderlineEnabled;
        set => SetAndRaise(CbUnderlineEnabledProperty, ref cbUnderlineEnabled, value);
    }

    public Geometry CbCheckedIcon
    {
        get => this.GetValue(CbCheckedIconProperty);
        set => SetValue(CbCheckedIconProperty, value);
    }

    public Geometry CbUncheckedIcon
    {
        get => this.GetValue(CbUncheckedIconProperty);
        set => SetValue(CbUncheckedIconProperty, value);
    }

    public IBrush CbBackgroundChecked
    {
        get => this.GetValue(CbBackgroundCheckedProperty);
        set => SetValue(CbBackgroundCheckedProperty, value);
    }

    public IBrush CbBackgroundUnchecked
    {
        get => this.GetValue(CbBackgroundUncheckedProperty);
        set => SetValue(CbBackgroundUncheckedProperty, value);
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

    public IBrush CbCheckedIconForeground
    {
        get => this.GetValue(CbCheckedIconForegroundProperty);
        set => SetValue(CbCheckedIconForegroundProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (e == null)
        {
            return;
        }

        innerItemsPresenter = e.NameScope.Find("PART_ItemsPresenter") as ItemsPresenter;

        Close();
    }

    private static void OnIsExpandedPropiedadeMudada(ButtonContentExpander buttonContentExpander, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is true)
        {
            buttonContentExpander.Open();
            buttonContentExpander.Expanded?.Invoke(buttonContentExpander, EventArgs.Empty);
        }
        else
        {
            buttonContentExpander.Close();
            buttonContentExpander.Unexpanded?.Invoke(buttonContentExpander, EventArgs.Empty);
        }
    }

    private void Open()
    {
        if (ExpandDirection == ButtonExpandDirection.ToRight)
        {
            Width = OpenLength;
        }

        if (ExpandDirection == ButtonExpandDirection.ToTop)
        {
            innerItemsPresenter!.RenderTransform = new ScaleTransform(1, 1);
            Height = OpenLength;
        }
    }

    private void Close()
    {
        if (ExpandDirection == ButtonExpandDirection.ToRight)
        {
            Width = 110;
        }

        if (ExpandDirection == ButtonExpandDirection.ToTop)
        {
            innerItemsPresenter!.RenderTransform = new ScaleTransform(1, 0);
            Height = 110;
        }
    }
}