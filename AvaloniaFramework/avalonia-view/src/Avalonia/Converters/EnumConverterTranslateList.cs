using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace AvaloniaFramework.Apresentacao.Converters;

public class EnumConverterTranslateList : IValueConverter
{
    public static readonly EnumConverterTranslateList Instance = new();

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
                    string? traducao;
                    MemberInfo member = type.GetMembers().SingleOrDefault(m => m.MemberType == MemberTypes.Field && m.Name == item.ToString()!)!;
                    var attribute = (DisplayAttribute?)member.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
                    if (attribute is { ResourceType: not null })
                    {
                        traducao = new ResourceManager(attribute.ResourceType).GetString(attribute.Name!, culture);
                    }
                    else
                    {
                        var attributeDescription = (DescriptionAttribute?)member.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                        if (attributeDescription != null)
                            traducao = attributeDescription.Description;
                        else
                            traducao = attribute?.Name ?? member.Name;
                    }

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