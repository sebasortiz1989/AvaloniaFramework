using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace AvaloniaFramework.Apresentacao.Converters;

public class DoubleToStringConverter : IValueConverter
{
    public static readonly DoubleToStringConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value != null && value is double result)
        {
            return $"{result:0.00}";
        }

        return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string)
        {
            return $"{value:0.00}";
        }

        return string.Empty;
    }
}