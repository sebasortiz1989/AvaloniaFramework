using Avalonia.Data;
using Avalonia.Markup.Xaml;
using System;
using AvaloniaFramework.Apresentacao.Converters;

namespace AvaloniaFramework.Apresentacao.MarkupExtensions;

public record RangeParams(RangeMode Mode, double? Min, double? Max, double? ExcludeMin, double? ExcludeMax);
