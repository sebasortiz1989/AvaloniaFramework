using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace AvaloniaFramework.Apresentacao.Inputs;

public class EnumComboBox : TemplatedControl
{
    public static readonly DirectProperty<EnumComboBox, bool> HabilitadoProperty =
        AvaloniaProperty.RegisterDirect<EnumComboBox, bool>(
            nameof(Habilitado),
            o => o.Habilitado,
            (o, v) => o.Habilitado = v);

    public static readonly DirectProperty<EnumComboBox, object?> SelectedItemProperty =
        AvaloniaProperty.RegisterDirect<EnumComboBox, object?>(
            nameof(SelectedItem),
            o => o.SelectedItem,
            (o, v) => o.SelectedItem = v,
            enableDataValidation: true,
            defaultBindingMode: BindingMode.TwoWay);

    private object? selectedIteml;
    private bool habilitado = true;

    public object? SelectedItem
    {
        get => selectedIteml ??= new();
        set
        {
            if (value != null)
                SetAndRaise(SelectedItemProperty, ref selectedIteml, value);
        }
    }

    public bool Habilitado
    {
        get => habilitado;
        set => SetAndRaise(HabilitadoProperty, ref habilitado, value);
    }
}