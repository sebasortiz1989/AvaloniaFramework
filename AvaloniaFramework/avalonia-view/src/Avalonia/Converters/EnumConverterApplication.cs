using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace AvaloniaFramework.Apresentacao.Converters;

public class EnumConverterApplication : IValueConverter
{
    public static readonly EnumConverterApplication Instance = new();

    private readonly Dictionary<object, string> cache = new();
    private readonly Dictionary<string, object> reverse = new();
    private Type? typeEnum;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
            return string.Empty;

        if (typeEnum == null)
            typeEnum = value.GetType();

        if (cache.TryGetValue(value, out var valueCache))
            return valueCache;

        string? traducao = null;
        if (Application.Current!.TryFindResource(typeEnum.Name + value!, out var valueApp))
            traducao = valueApp!.ToString()!;

        if (traducao == null)
        {
            if (Application.Current!.TryFindResource(value.ToString()!, out valueApp))
                traducao = valueApp!.ToString()!;
        }

        if (traducao == null)
        {
            MemberInfo member = typeEnum.GetMembers().SingleOrDefault(m => m.MemberType == MemberTypes.Field && m.Name == value.ToString()!)!;
            DisplayAttribute? attribute = null;
            if (member != null)
                attribute = (DisplayAttribute?)member.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

            if (attribute is { ResourceType: not null })
                traducao = new ResourceManager(attribute.ResourceType).GetString(attribute.Name!, culture);
            else
                traducao = attribute?.Name ?? member?.Name;
        }

        traducao ??= value.ToString() ?? string.Empty;
        cache.Add(value, traducao);
        reverse.Add(traducao, value);
        return traducao;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string enumDesc)
        {
            if (reverse.TryGetValue(enumDesc, out var valueCache))
                return valueCache;

            var members = typeEnum!.GetMembers().Where(m => m.MemberType == MemberTypes.Field);
            foreach (var member in members)
            {
                string? traducao = null;
                if (Application.Current!.TryFindResource(typeEnum.Name + member.Name, out var valueApp))
                    traducao = valueApp!.ToString()!;

                if (traducao == null)
                {
                    if (Application.Current!.TryFindResource(member.Name, out valueApp))
                        traducao = valueApp!.ToString()!;
                }

                if (traducao == null)
                {
                    var attribute = (DisplayAttribute?)member.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
                    if (attribute is { ResourceType: not null })
                        traducao = new ResourceManager(attribute.ResourceType).GetString(attribute.Name!, culture);
                    else
                        traducao = member.Name;
                }

                if (traducao != null && traducao == enumDesc)
                {
                    if (Enum.TryParse(typeEnum!, (string)member.Name, out var val))
                        return val;
                }
            }
        }

        if (value is not null)
            return value;

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }
}