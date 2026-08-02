using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AvaloniaFramework.Apresentacao.Converters;

public class EnumConverter : IValueConverter
{
    public static readonly EnumConverter Instance = new();

    public static FuncValueConverter<object, List<object>> EnumToList { get; } = new(typeSelected =>
    {
        List<object> items = new();
        if (typeSelected != null)
        {
            var type = typeSelected.GetType();
            if (type.IsEnum)
            {
                foreach (object item in Enum.GetValues(type))
                    items.Add(item);
            }
        }

        return items;
    });

    public static FuncValueConverter<Type, List<object>> EnumToListWithType { get; } = new(type =>
    {
        List<object> items = new();
        if (type is { IsEnum: true })
        {
            foreach (object item in Enum.GetValues(type))
                items.Add(item.ToString()!);
        }

        return items;
    });

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string enumDesc)
        {
            if (Enum.TryParse(targetType, enumDesc, out var val))
            {
                return val;
            }
        }

        if (value is not null)
            return value;

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }
}