using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AvaloniaFramework.Apresentacao.Converters
{
    public class TankColorConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            IBrush c = Brushes.White;
            if (values == null)
            {
                return null;
            }

            if (values.Count < 3)
            {
                return AvaloniaProperty.UnsetValue;
            }

            if (values[0] is IBrush componentColor &&
                values[1] is IBrush filledColor &&
                values[2] is double actualValue)
            {
                if (actualValue > 0)
                {
                    c = filledColor;
                }
                else
                {
                    c = componentColor;
                }
            }

            return c;
        }
    }
}