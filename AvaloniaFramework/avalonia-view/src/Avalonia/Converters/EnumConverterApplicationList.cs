using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace AvaloniaFramework.Apresentacao.Converters;

public class EnumConverterApplicationList : IValueConverter
{
    public static readonly EnumConverterApplicationList Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        List<object> items = new();
        if (value != null)
        {
            var type = value.GetType();
            if (type != null && type.IsEnum)
            {
                foreach (object item in Enum.GetValues(type))
                {
                    string? traducao = null;

                    if (Application.Current!.TryFindResource(type.Name + item!, out var valueApp))
                        traducao = valueApp!.ToString()!;

                    if (traducao == null)
                    {
                        if (Application.Current!.TryFindResource(item.ToString()!, out valueApp))
                            traducao = valueApp!.ToString()!;
                    }

                    if (traducao == null)
                    {
                        MemberInfo member = type.GetMembers().SingleOrDefault(m => m.MemberType == MemberTypes.Field && m.Name == item.ToString()!)!;
                        var attribute = (DisplayAttribute?)member.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
                        if (attribute is { ResourceType: not null })
                            traducao = new ResourceManager(attribute.ResourceType).GetString(attribute.Name!, culture);
                        else
                            traducao = attribute?.Name ?? member.Name;
                    }

                    traducao ??= value.ToString() ?? string.Empty;
                    items.Add(traducao ?? item);
                }
            }
        }

        return items;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}