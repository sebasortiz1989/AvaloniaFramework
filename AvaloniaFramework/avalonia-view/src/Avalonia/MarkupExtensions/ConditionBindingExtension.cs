using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using System;
using AvaloniaFramework.Apresentacao.Converters;

namespace AvaloniaFramework.Apresentacao.MarkupExtensions;

public sealed class ConditionBindingExtension : MarkupExtension
{
    public ConditionBindingExtension()
    {
    }

    public ConditionBindingExtension(string path)
    {
        Path = path;
    }

    [ConstructorArgument("path")]
    public string Path { get; set; } = string.Empty;

    public ConditionMode Mode { get; set; } = ConditionMode.IsTrue;

    public object? Target { get; set; }

    public override Binding ProvideValue(IServiceProvider serviceProvider)
    {
        var path = Path;
        var mode = Mode;

        // Loop to handle single (!), or multiple (!!!) negations
        while (!string.IsNullOrEmpty(path) && path.StartsWith('!'))
        {
            path = path.Substring(1).Trim();

            // 2. Flip the mode logic
            mode = mode switch
            {
                ConditionMode.IsTrue => ConditionMode.IsFalse,
                ConditionMode.IsFalse => ConditionMode.IsTrue,

                ConditionMode.IsNull => ConditionMode.IsNotNull,
                ConditionMode.IsNotNull => ConditionMode.IsNull,

                ConditionMode.Equals => ConditionMode.NotEquals,
                ConditionMode.NotEquals => ConditionMode.Equals,

                _ => mode, // Fallback for undefined modes
            };
        }

        return new Binding(path)
        {
            Converter = ConditionConverter.Instance,
            ConverterParameter = new Tuple<ConditionMode, object?>(mode, Target),
        };
    }
}
