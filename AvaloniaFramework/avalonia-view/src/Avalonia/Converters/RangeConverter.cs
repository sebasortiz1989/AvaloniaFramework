using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using AvaloniaFramework.Apresentacao.MarkupExtensions;

namespace AvaloniaFramework.Apresentacao.Converters;

public sealed class RangeConverter : IMultiValueConverter
{
    // Instância Singleton estática (Internal e Stateless).
    // Necessária pois o 'RangeBinding' cria o Binding via código C# e não consegue
    // resolver facilmente referências via {StaticResource}.
    // Isso também evita alocações desnecessárias de memória (Garbage Collection)
    // se o binding for usado milhares de vezes.
    internal static readonly RangeConverter Instance = new();

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values == null || values.Count < 2 || values[1] is not StyledElement control)
            return false;

        double val = double.NaN;
        if (!double.TryParse(values[0]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out val))
        {
            val = control.GetValue(StyledRange.ValueProperty);
        }

        var args = parameter as RangeParams;
        var mode = args?.Mode ?? RangeMode.IsBetween;

        double min = args?.Min ?? control.GetValue(StyledRange.MinProperty);
        double max = args?.Max ?? control.GetValue(StyledRange.MaxProperty);
        double? exMin = args?.ExcludeMin;
        double? exMax = args?.ExcludeMax;

        // 4. Logic Evaluation
        return mode switch
        {
            RangeMode.IsBetween => CheckIsBetween(val, min, max, exMin, exMax),
            RangeMode.NotIn => CheckNotIn(val, min, max),
            RangeMode.LessThan => (double.IsNaN(min) || val < min) && (!exMin.HasValue || val <= exMin.Value),
            RangeMode.GreaterThan => (double.IsNaN(max) || val > max) && (!exMax.HasValue || val >= exMax.Value),
            _ => false,
        };
    }

    private static bool CheckIsBetween(double val, double min, double max, double? exMin, double? exMax)
    {
        bool insideOuter = (double.IsNaN(min) || val >= min) &&
                           (double.IsNaN(max) || val <= max);

        if (!insideOuter) return false;

        if (exMin.HasValue && exMax.HasValue)
        {
            bool insideHole = val >= exMin.Value && val <= exMax.Value;
            return !insideHole;
        }

        if (exMin.HasValue && val <= exMin.Value) return false;
        if (exMax.HasValue && val >= exMax.Value) return false;

        return true;
    }

    private static bool CheckNotIn(double val, double min, double max)
    {
        bool tooLow = !double.IsNaN(min) && val < min;
        bool tooHigh = !double.IsNaN(max) && val > max;
        return tooLow || tooHigh;
    }
}
