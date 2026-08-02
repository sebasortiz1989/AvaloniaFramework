using Avalonia;
using Avalonia.Data.Converters;

namespace AvaloniaFramework.Apresentacao.Buttons;

public static class ConvertersButtons
{
    public static FuncValueConverter<Thickness?, Thickness?> ThicknessToThicknessConverter { get; } = new(num => num != null ? new Thickness(0, 0, 0, num.Value.Bottom) : default);
}