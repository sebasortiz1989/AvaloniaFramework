using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AvaloniaFramework.Apresentacao.Converters;

public class IndicatorProgressColorConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        IBrush c = Brushes.White;
        if (values == null)
        {
            return null;
        }

        if (values.Count < 4)
        {
            return AvaloniaProperty.UnsetValue;
        }

        if (values[0] is double theoreticalDose &&
            values[1] is double midTaxLimit &&
            values[2] is double criticalTaxLimit &&
            values[3] is double actualValue &&
            values[4] is IBrush normalColor &&
            values[5] is IBrush midColor &&
            values[6] is IBrush criticalColor)
        {
            double difference = Math.Abs(theoreticalDose - actualValue);
            double midValue = theoreticalDose * (midTaxLimit / 100);
            double critValue = theoreticalDose * (criticalTaxLimit / 100);

            if (difference > midValue && difference < critValue)
            {
                c = midColor;
            }
            else if (difference >= critValue)
            {
                c = criticalColor;
            }
            else
            {
                c = normalColor;
            }
        }

        return c;
    }
}