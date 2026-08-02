using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AvaloniaFramework.Apresentacao.Converters;

public class VStringFormatConverter : IMultiValueConverter
{
    public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is { Count: < 2 })
        {
            return values[0]?.ToString() ?? string.Empty;
        }

        try
        {
            if (values?[1] != null && float.TryParse((string?)values[0], NumberStyles.Any, culture, out var number))
            {
                var convert = string.Format(culture, (values[1] as string)!, number);
                return convert;
            }

            return values?[0]?.ToString() ?? string.Empty;
        }
        catch (FormatException)
        {
            // Handle formatting errors gracefully (e.g., log, return a default)
            return "Format Error";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}