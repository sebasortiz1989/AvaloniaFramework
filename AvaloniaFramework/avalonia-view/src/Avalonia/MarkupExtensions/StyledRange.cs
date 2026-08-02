using Avalonia;
using Avalonia.Data;
using System;

namespace AvaloniaFramework.Apresentacao.MarkupExtensions;

public sealed class StyledRange : AvaloniaObject
{
    public static readonly AttachedProperty<double> MinProperty =
        AvaloniaProperty.RegisterAttached<StyledRange, StyledElement, double>("Min", double.NaN);

    public static readonly AttachedProperty<double> MaxProperty =
        AvaloniaProperty.RegisterAttached<StyledRange, StyledElement, double>("Max", double.NaN);

    public static readonly AttachedProperty<double> ValueProperty =
    AvaloniaProperty.RegisterAttached<StyledRange, StyledElement, double>("Value", double.NaN, defaultBindingMode: BindingMode.TwoWay);

    public static double GetMin(StyledElement element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(MinProperty);
    }

    public static void SetMin(StyledElement element, double value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(MinProperty, value);
    }

    public static double GetMax(StyledElement element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(MaxProperty);
    }

    public static void SetMax(StyledElement element, double value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(MaxProperty, value);
    }

    public static double GetValue(StyledElement element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(ValueProperty);
    }

    public static void SetValue(StyledElement element, double value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(ValueProperty, value);
    }
}
