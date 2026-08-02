using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Core;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using System;
using AvaloniaFramework.Apresentacao.Converters;

namespace AvaloniaFramework.Apresentacao.MarkupExtensions;

public sealed class RangeBindingExtension : MarkupExtension
{
    public RangeBindingExtension()
    {
    }

    public RangeBindingExtension(string path)
    {
        Path = path;
    }

    [ConstructorArgument("path")]
    public string Path { get; set; } = string.Empty;

    public RangeMode Mode { get; set; } = RangeMode.IsBetween;

    public double? Min { get; set; }

    public double? Max { get; set; }

    public double? ExcludeMin { get; set; }

    public double? ExcludeMax { get; set; }

    public override MultiBinding ProvideValue(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        if (string.IsNullOrEmpty(Path) && serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget pvt)
        {
            var targetObject = pvt.TargetObject as StyledElement;

            if (targetObject != null && targetObject.GetValue(StyledRange.ValueProperty) is double d && !double.IsNaN(d))
            {
                var bind = targetObject.GetObservable(StyledRange.ValueProperty).ToBinding();

                // Build binding directly to the attached property
                return new MultiBinding
                {
                    Bindings = new[]
                    {
                        bind,
                        new Binding { RelativeSource = new RelativeSource(RelativeSourceMode.Self), TargetNullValue = false, FallbackValue = false },
                    },
                    Converter = RangeConverter.Instance,
                    ConverterParameter = new RangeParams(Mode, Min, Max, ExcludeMin, ExcludeMax),
                    FallbackValue = false,
                    TargetNullValue = false,
                };
            }
        }

        if (string.IsNullOrEmpty(Path))
        {
            return new MultiBinding { FallbackValue = false };
        }

        // Automatically create the MultiBinding that injects the Control ($self)
        return new MultiBinding
        {
            Bindings = new[]
            {
                new Binding(Path) { TargetNullValue = false, FallbackValue = false },
                new Binding { RelativeSource = new RelativeSource(RelativeSourceMode.Self), TargetNullValue = false, FallbackValue = false },
            },
            Converter = RangeConverter.Instance,
            ConverterParameter = new RangeParams(Mode, Min, Max, ExcludeMin, ExcludeMax),
            FallbackValue = false,
            TargetNullValue = false,
        };
    }
}
