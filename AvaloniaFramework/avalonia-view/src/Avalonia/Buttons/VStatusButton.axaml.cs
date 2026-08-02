using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaFramework.Apresentacao.Buttons;

public enum IconType
{
    Vehicle,
    Scissors,
}

public enum TemplateType
{
    Icon,
    Text,
    Image,
}

public class VStatusButton : Button
{
    public static readonly Geometry DefaultVehicleIcon =
        Geometry.Parse("M9 6C9 2.71875 11.625 0 15 0H24.9375C27.375 0 29.625 1.5 30.4688 3.84375L34.9688 15H45V11.9062C45 9.5625 45.4688 7.21875 46.5 5.15625L46.7812 4.6875C47.5312 3.1875 49.3125 2.625 50.8125 3.375C52.3125 4.125 52.875 5.90625 52.125 7.40625L51.9375 7.875C51.2812 9.09375" +
                       " 51 10.5 51 11.9062V15H56.25C58.3125 15 60 16.6875 60 18.75V23.0625C60 24.5625 59.1562 26.0625 57.8438 26.9062L53.8125 29.4375C52.4062 28.875 51 28.5 49.5 28.5C45.75 28.5 42.4688 30.2812 40.4062 33H33C33 34.6875 31.5938 36 30 36H29.1562C29.0625 36.4688 28.875 36.9375" +
                       " 28.5938 37.4062L29.1562 37.875C30.375 39.0938 30.375 40.9688 29.1562 42.1875L27.0938 44.25C25.875 45.4688 24 45.4688 22.7812 44.25L22.3125 43.6875C21.8438 43.9688 21.375 44.1562 21 44.25V45C21 46.6875 19.5938 48 18 48H15C13.3125 48 12 46.6875 12 45V44.25C11.5312 44.1562" +
                       " 11.0625 43.9688 10.5938 43.6875L10.125 44.25C8.90625 45.4688 7.03125 45.4688 5.8125 44.25L3.75 42.1875C2.53125 40.9688 2.53125 39.0938 3.75 37.875L4.3125 37.4062C4.03125 36.9375 3.84375 36.4688 3.75 36H3C1.3125 36 0 34.6875 0 33V30C0 28.4062 1.3125 27 3 27H3.75C3.84375" +
                       " 26.625 4.03125 26.1562 4.3125 25.6875L3.75 25.2188C2.53125 24 2.53125 22.125 3.75 20.9062L5.8125 18.8438C6.65625 18 7.875 17.7188 9 18.0938V6ZM15 6V15H28.5L24.9375 6H15ZM16.5 24C12.2812 24 9 27.375 9 31.5C9 35.7188 12.2812 39 16.5 39C20.625 39 24 35.7188 24 31.5C24" +
                       " 27.375 20.625 24 16.5 24ZM41.25 39.75C41.25 37.0312 42.5625 34.5 44.7188 33C46.0312 32.0625 47.7188 31.5 49.5 31.5C49.6875 31.5 49.9688 31.5938 50.1562 31.5938C54.375 31.9688 57.75 35.5312 57.75 39.75C57.75 44.3438 54 48 49.5 48C44.9062 48 41.25 44.3438 41.25 39.75ZM49.5" +
                       " 42C50.7188 42 51.75 41.0625 51.75 39.75C51.75 38.5312 50.7188 37.5 49.5 37.5C48.1875 37.5 47.25 38.5312 47.25 39.75C47.25 41.0625 48.1875 42 49.5 42Z");

    public static readonly Geometry DefaultScissorsIcon =
        Geometry.Parse("M27.1719 21.7969L40.6094 35.1562C41.0781 35.7031 41.0781 36.4844 40.6094 36.9531C38.1875 39.375 34.2031 39.375 31.7812 36.9531L21.8594 27.1094L27.1719 21.7969ZM40.6094 4.92188L17.6406 27.8125C19.0469 31.0156 18.4219" +
                      " 34.8438 15.8438 37.5C14.125 39.2188 11.7812 40 9.67188 40C7.48438 40 5.21875 39.2188 3.42188 37.5C0.0625 34.0625 0.0625 28.5156 3.42188 25.0781C5.14062 23.3594 7.40625 22.5 9.67188 22.5C10.4531 22.5 11.2344 22.6562 12.0156" +
                      " 22.8906L14.8281 20L11.9375 17.1875C11.1562 17.4219 10.375 17.5 9.67188 17.5C7.48438 17.5 5.21875 16.7188 3.5 15C0.140625 11.5625 0.140625 6.01562 3.5 2.57812C5.21875 0.859375 7.32812 0 9.67188 0C11.9375 0 14.2031 0.859375" +
                      " 15.9219 2.57812C18.5 5.23438 19.125 9.0625 17.7188 12.2656L20.1406 14.7656L31.7812 3.125C34.2031 0.703125 38.1875 0.703125 40.6094 3.125C41.0781 3.59375 41.0781 4.375 40.6094 4.92188ZM9.75 12.5C11.7812 12.5 13.5 10.8594" +
                      " 13.5 8.75C13.5 6.71875 11.7812 5 9.75 5C7.64062 5 6 6.71875 6 8.75C6 10.8594 7.64062 12.5 9.75 12.5ZM9.75 35C11.7812 35 13.5 33.3594 13.5 31.25C13.5 29.2188 11.7812 27.5 9.75 27.5C7.64062 27.5 6 29.2188 6 31.25C6 33.3594" +
                      " 7.64062 35 9.75 35Z");

    public static readonly StyledProperty<TemplateType> VTemplateTypeProperty =
        AvaloniaProperty.Register<VStatusButton, TemplateType>(nameof(VTemplateType));

    public static readonly StyledProperty<IconType> DefaultIconTypeProperty =
        AvaloniaProperty.Register<VStatusButton, IconType>(nameof(DefaultIconType));

    public static readonly StyledProperty<bool> VIsCheckedProperty =
        AvaloniaProperty.Register<VStatusButton, bool>(nameof(VIsChecked), defaultValue: false);

    public static readonly StyledProperty<string> VTextProperty =
        AvaloniaProperty.Register<VStatusButton, string>(nameof(VText), defaultValue: "x");

    public static readonly StyledProperty<IBrush> VCheckedBackground1Property =
        AvaloniaProperty.Register<VStatusButton, IBrush>(nameof(VCheckedBackground1), defaultValue: Brush.Parse("#76FD9E"));

    public static readonly StyledProperty<IBrush> VCheckedBackground2Property =
        AvaloniaProperty.Register<VStatusButton, IBrush>(nameof(VCheckedBackground2), defaultValue: Brush.Parse("#323533"));

    public static readonly StyledProperty<IBrush> VCheckedIconForegroundProperty =
        AvaloniaProperty.Register<VStatusButton, IBrush>(nameof(VCheckedIconForeground), defaultValue: Brush.Parse("#76FD9E"));

    public static readonly StyledProperty<IBrush> VCheckedTextForegroundProperty =
        AvaloniaProperty.Register<VStatusButton, IBrush>(nameof(VCheckedTextForeground), defaultValue: Brush.Parse("#76FD9E"));

    public static readonly StyledProperty<Geometry> VCheckedIconProperty =
        AvaloniaProperty.Register<VStatusButton, Geometry>(nameof(VCheckedIcon), defaultValue: DefaultVehicleIcon);

    public static readonly StyledProperty<IBrush> VUncheckedBackground1Property =
        AvaloniaProperty.Register<VStatusButton, IBrush>(nameof(VUncheckedBackground1), defaultValue: Brush.Parse("#E20613"));

    public static readonly StyledProperty<IBrush> VUncheckedBackground2Property =
        AvaloniaProperty.Register<VStatusButton, IBrush>(nameof(VUncheckedBackground2), defaultValue: Brush.Parse("#040404"));

    public static readonly StyledProperty<IBrush> VUncheckedIconForegroundProperty =
        AvaloniaProperty.Register<VStatusButton, IBrush>(nameof(VUncheckedIconForeground), defaultValue: Brush.Parse("#E20613"));

    public static readonly StyledProperty<IBrush> VUncheckedTextForegroundProperty =
        AvaloniaProperty.Register<VStatusButton, IBrush>(nameof(VUncheckedTextForeground), defaultValue: Brush.Parse("#E20613"));

    public static readonly StyledProperty<Geometry> VUncheckedIconProperty =
        AvaloniaProperty.Register<VStatusButton, Geometry>(nameof(VUncheckedIcon), defaultValue: DefaultVehicleIcon);

    public static readonly StyledProperty<double> VIconWidthProperty =
        AvaloniaProperty.Register<VStatusButton, double>(nameof(VIconWidth), defaultValue: 60);

    public static readonly StyledProperty<double> VIconHeightProperty =
        AvaloniaProperty.Register<VStatusButton, double>(nameof(VIconHeight), defaultValue: 48);

    public static readonly StyledProperty<double> VImageWidthProperty =
        AvaloniaProperty.Register<VStatusButton, double>(nameof(VImageWidth), defaultValue: 30);

    public static readonly StyledProperty<double> VImageHeightProperty =
        AvaloniaProperty.Register<VStatusButton, double>(nameof(VImageHeight), defaultValue: 30);

    public static readonly StyledProperty<double> VTextFontSizeProperty =
        AvaloniaProperty.Register<VStatusButton, double>(nameof(VTextFontSize), defaultValue: 32);

    public static readonly StyledProperty<IImage> VCheckedSourceImageProperty =
        AvaloniaProperty.Register<VStatusButton, IImage>(nameof(VCheckedSourceImage));

    public static readonly StyledProperty<IImage> VUncheckedSourceImageProperty =
        AvaloniaProperty.Register<VStatusButton, IImage>(nameof(VUncheckedSourceImage));

    public static readonly StyledProperty<Thickness> VBorderThicknessProperty =
        AvaloniaProperty.Register<VStatusButton, Thickness>(nameof(VBorderThickness), new Thickness(2));

    static VStatusButton()
    {
        DefaultIconTypeProperty.Changed.AddClassHandler<VStatusButton>((s, e) => s.OnDefaultIconTypePropiedadeMudada(e));
    }

    public TemplateType VTemplateType
    {
        get => GetValue(VTemplateTypeProperty);
        set => SetValue(VTemplateTypeProperty, value);
    }

    public bool VIsChecked
    {
        get => GetValue(VIsCheckedProperty);
        set => SetValue(VIsCheckedProperty, value);
    }

    public string VText
    {
        get => GetValue(VTextProperty);
        set => SetValue(VTextProperty, value);
    }

    public IconType DefaultIconType
    {
        get => GetValue(DefaultIconTypeProperty);
        set => SetValue(DefaultIconTypeProperty, value);
    }

    public IBrush VCheckedBackground1
    {
        get => GetValue(VCheckedBackground1Property);
        set => SetValue(VCheckedBackground1Property, value);
    }

    public IBrush VCheckedBackground2
    {
        get => GetValue(VCheckedBackground2Property);
        set => SetValue(VCheckedBackground2Property, value);
    }

    public IBrush VCheckedIconForeground
    {
        get => GetValue(VCheckedIconForegroundProperty);
        set => SetValue(VCheckedIconForegroundProperty, value);
    }

    public IBrush VCheckedTextForeground
    {
        get => GetValue(VCheckedTextForegroundProperty);
        set => SetValue(VCheckedTextForegroundProperty, value);
    }

    public IBrush VUncheckedTextForeground
    {
        get => GetValue(VUncheckedTextForegroundProperty);
        set => SetValue(VUncheckedTextForegroundProperty, value);
    }

    public Geometry VCheckedIcon
    {
        get => GetValue(VCheckedIconProperty);
        set => SetValue(VCheckedIconProperty, value);
    }

    public IBrush VUncheckedBackground1
    {
        get => GetValue(VUncheckedBackground1Property);
        set => SetValue(VUncheckedBackground1Property, value);
    }

    public IBrush VUncheckedBackground2
    {
        get => GetValue(VUncheckedBackground2Property);
        set => SetValue(VUncheckedBackground2Property, value);
    }

    public IBrush VUncheckedIconForeground
    {
        get => GetValue(VUncheckedIconForegroundProperty);
        set => SetValue(VUncheckedIconForegroundProperty, value);
    }

    public Geometry VUncheckedIcon
    {
        get => GetValue(VUncheckedIconProperty);
        set => SetValue(VUncheckedIconProperty, value);
    }

    public double VIconWidth
    {
        get => GetValue(VIconWidthProperty);
        set => SetValue(VIconWidthProperty, value);
    }

    public double VIconHeight
    {
        get => GetValue(VIconHeightProperty);
        set => SetValue(VIconHeightProperty, value);
    }

    public double VImageWidth
    {
        get => GetValue(VImageWidthProperty);
        set => SetValue(VImageWidthProperty, value);
    }

    public double VImageHeight
    {
        get => GetValue(VImageHeightProperty);
        set => SetValue(VImageHeightProperty, value);
    }

    public double VTextFontSize
    {
        get => GetValue(VTextFontSizeProperty);
        set => SetValue(VTextFontSizeProperty, value);
    }

    public IImage VCheckedSourceImage
    {
        get => GetValue(VCheckedSourceImageProperty);
        set => SetValue(VCheckedSourceImageProperty, value);
    }

    public IImage VUncheckedSourceImage
    {
        get => GetValue(VUncheckedSourceImageProperty);
        set => SetValue(VUncheckedSourceImageProperty, value);
    }

    public Thickness VBorderThickness
    {
        get => GetValue(VBorderThicknessProperty);
        set => SetValue(VBorderThicknessProperty, value);
    }

    private void OnDefaultIconTypePropiedadeMudada(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is IconType iconType)
        {
            switch (iconType)
            {
                case IconType.Vehicle:
                    SetValue(VCheckedIconProperty, DefaultVehicleIcon);
                    SetValue(VUncheckedIconProperty, DefaultVehicleIcon);
                    break;
                case IconType.Scissors:
                    SetValue(VCheckedIconProperty, DefaultScissorsIcon);
                    SetValue(VUncheckedIconProperty, DefaultScissorsIcon);
                    break;
            }
        }
    }
}