using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Media;
using System;

namespace AvaloniaFramework.Apresentacao.MarkupExtensions;

public class VIcon : AvaloniaObject
{
    public static readonly AttachedProperty<IconPosition?> PositionProperty =
        AvaloniaProperty.RegisterAttached<VIcon, Control, IconPosition?>("Position", defaultValue: null);

    public static readonly AttachedProperty<Geometry?> IconProperty =
        AvaloniaProperty.RegisterAttached<VIcon, Control, Geometry?>("Icon");

    public static readonly AttachedProperty<IImage?> ImageProperty =
        AvaloniaProperty.RegisterAttached<VIcon, Control, IImage?>("Image");

    public static readonly AttachedProperty<double> WidthProperty =
        AvaloniaProperty.RegisterAttached<VIcon, Control, double>("Width", 16.0);

    public static readonly AttachedProperty<double> HeightProperty =
        AvaloniaProperty.RegisterAttached<VIcon, Control, double>("Height", 16.0);

    public static readonly AttachedProperty<Thickness> MarginProperty =
        AvaloniaProperty.RegisterAttached<VIcon, Control, Thickness>("Margin", new Thickness(0, 0, 5, 0));

    public static readonly AttachedProperty<Thickness> PaddingProperty =
        AvaloniaProperty.RegisterAttached<VIcon, Control, Thickness>("Padding", new Thickness(0));

    public static readonly AttachedProperty<IBrush?> ForegroundProperty =
        AvaloniaProperty.RegisterAttached<VIcon, Control, IBrush?>("Foreground", null);

    public static readonly AttachedProperty<IBrush?> BackgroundProperty =
        AvaloniaProperty.RegisterAttached<VIcon, Control, IBrush?>("Background", Brushes.Transparent);

    public static IconPosition? GetPosition(Control element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(PositionProperty);
    }

    public static void SetPosition(Control element, IconPosition? value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(PositionProperty, value);
    }

    public static Geometry? GetIcon(Control element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(IconProperty);
    }

    public static void SetIcon(Control element, Geometry? value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(IconProperty, value);
    }

    public static IImage? GetImage(Control element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(ImageProperty);
    }

    public static void SetImage(Control element, IImage? value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(ImageProperty, value);
    }

    public static double GetWidth(Control element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(WidthProperty);
    }

    public static void SetWidth(Control element, double value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(WidthProperty, value);
    }

    public static double GetHeight(Control element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(HeightProperty);
    }

    public static void SetHeight(Control element, double value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(HeightProperty, value);
    }

    public static Thickness GetMargin(Control element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(MarginProperty);
    }

    public static void SetMargin(Control element, Thickness value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(MarginProperty, value);
    }

    public static Thickness GetPadding(Control element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(PaddingProperty);
    }

    public static void SetPadding(Control element, Thickness value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(PaddingProperty, value);
    }

    public static IBrush? GetForeground(Control element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(ForegroundProperty);
    }

    public static void SetForeground(Control element, IBrush? value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(ForegroundProperty, value);
    }

    public static IBrush? GetBackground(Control element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(BackgroundProperty);
    }

    public static void SetBackground(Control element, IBrush? value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(BackgroundProperty, value);
    }
}
