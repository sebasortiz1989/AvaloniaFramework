using Avalonia;
using Avalonia.Controls;
using System;

namespace AvaloniaFramework.Apresentacao.Inputs;

public class ButtonStatistic : Button
{
    public static readonly StyledProperty<int> RepeatCountProperty =
        AvaloniaProperty.Register<ButtonStatistic, int>(nameof(RepeatCount), defaultValue: 1);

    public int RepeatCount
    {
        get => GetValue(RepeatCountProperty);
        set => SetValue(RepeatCountProperty, value);
    }

    protected override Type StyleKeyOverride => typeof(Button);
}