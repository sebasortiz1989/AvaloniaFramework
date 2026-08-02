using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Inputs.TextFields;

public class TextField : TemplatedControl
{
    public static readonly DirectProperty<TextField, string> TextProperty =
        AvaloniaProperty.RegisterDirect<TextField, string>(
            nameof(Text),
            o => o.Text,
            (o, v) => o.Text = v);

    public static readonly DirectProperty<TextField, Geometry?> SvgIconLeftProperty =
        AvaloniaProperty.RegisterDirect<TextField, Geometry?>(
            nameof(SvgIconLeft),
            o => o.SvgIconLeft,
            (o, v) => o.SvgIconLeft = v);

    public static readonly DirectProperty<TextField, Geometry?> SvgIconRightProperty =
        AvaloniaProperty.RegisterDirect<TextField, Geometry?>(
            nameof(SvgIconRight),
            o => o.SvgIconRight,
            (o, v) => o.SvgIconRight = v);

    public static readonly DirectProperty<TextField, string> SvgStringLeftProperty =
        AvaloniaProperty.RegisterDirect<TextField, string>(
            nameof(SvgStringLeft),
            o => o.SvgStringLeft,
            (o, v) => o.SvgStringLeft = v);

    public static readonly DirectProperty<TextField, string> SvgStringRightProperty =
        AvaloniaProperty.RegisterDirect<TextField, string>(
            nameof(SvgStringRight),
            o => o.SvgStringRight,
            (o, v) => o.SvgStringRight = v);

    private string text = "0";

    private Geometry? svgIconLeft;

    private Geometry? svgIconRight;

    private string svgStringLeft = string.Empty;

    private string svgStringRight = string.Empty;

    static TextField()
    {
        SvgStringLeftProperty.Changed.AddClassHandler<TextField>((sender, e) => SvgStringLeftPropertyChanged(sender, e));
        SvgStringRightProperty.Changed.AddClassHandler<TextField>((sender, e) => SvgStringRightPropertyChanged(sender, e));
    }

    public string Text
    {
        get => text;
        set => SetAndRaise(TextProperty, ref text, value);
    }

    public Geometry? SvgIconLeft
    {
        get => svgIconLeft;
        set => SetAndRaise(SvgIconLeftProperty, ref svgIconLeft, value);
    }

    public Geometry? SvgIconRight
    {
        get => svgIconRight;
        set => SetAndRaise(SvgIconRightProperty, ref svgIconRight, value);
    }

    public string SvgStringLeft
    {
        get => svgStringLeft;
        set => SetAndRaise(SvgStringLeftProperty, ref svgStringLeft, value);
    }

    public string SvgStringRight
    {
        get => svgStringRight;
        set => SetAndRaise(SvgStringRightProperty, ref svgStringRight, value);
    }

    private static void SvgStringLeftPropertyChanged(TextField textField, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is string newStringSvg)
        {
            textField.SvgIconLeft = PathGeometry.Parse(newStringSvg);
        }
    }

    private static void SvgStringRightPropertyChanged(TextField textField, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is string newStringSvg)
        {
            textField.SvgIconRight = PathGeometry.Parse(newStringSvg);
        }
    }
}

// private static void OnTipoPropertyChanged(TextField sender, AvaloniaPropertyChangedEventArgs e)
// {
//     Classes.Clear();
//     var newTipoString = ((TipoDeTextBox)e.NewValue).ToString();
//     Classes.Set(newTipoString, true);
// }

// public enum TipoDeTextBox
// {
//     Nenhum,
//     Byte,
//     Decimal,
// }