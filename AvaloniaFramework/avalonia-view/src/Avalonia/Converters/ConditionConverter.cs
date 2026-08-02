using Avalonia.Data.Converters;
using System;
using System.Globalization;
using AvaloniaFramework.Apresentacao.MarkupExtensions;

namespace AvaloniaFramework.Apresentacao.Converters;

public sealed class ConditionConverter : IValueConverter
{
    // Instância Singleton estática (Internal e Stateless).
    // Necessária pois o 'RangeBinding' cria o Binding via código C# e não consegue
    // resolver facilmente referências via {StaticResource}.
    // Isso também evita alocações desnecessárias de memória (Garbage Collection)
    // se o binding for usado milhares de vezes.
    internal static readonly ConditionConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        object? val = value;

        if (parameter is not Tuple<ConditionMode, object?> args)
            return false;

        ConditionMode mode = args.Item1;
        object? target = args.Item2;

        return mode switch
        {
            ConditionMode.IsNull => val == null,
            ConditionMode.IsNotNull => val != null,

            ConditionMode.IsTrue => val is true || (bool.TryParse(val?.ToString(), out bool b1) && b1),
            ConditionMode.IsFalse => val is false || (bool.TryParse(val?.ToString(), out bool b2) && !b2),

            ConditionMode.Equals => IsEqual(val, target),
            ConditionMode.NotEquals => !IsEqual(val, target),

            _ => false,
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    private static bool IsEqual(object? a, object? b)
    {
        if (a == null && b == null) return true;
        if (a == null || b == null) return false;

        return string.Equals(a.ToString(), b.ToString(), StringComparison.OrdinalIgnoreCase);
    }
}