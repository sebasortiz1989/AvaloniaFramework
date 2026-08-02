using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace AvaloniaFramework.Apresentacao.Converters;

public class ComboBoxConverterNullparaMargin : IValueConverter
{
    public static readonly ComboBoxConverterNullparaMargin Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return new Thickness(8, 54, 8, 8);
        }

        return new Thickness(8, 74, 8, 8);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}