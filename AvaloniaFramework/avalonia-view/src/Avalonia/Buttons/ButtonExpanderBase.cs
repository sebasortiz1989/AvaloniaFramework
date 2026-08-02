using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using System.Windows.Input;

namespace AvaloniaFramework.Apresentacao.Buttons
{
    public class ButtonExpanderBase : ItemsControl
    {
        public static readonly StyledProperty<ICommand?> CommandProperty =
            AvaloniaProperty.Register<ButtonExpanderBase, ICommand?>(nameof(Command));

        public static readonly StyledProperty<IBrush> IconForegroundProperty =
            AvaloniaProperty.Register<ButtonExpanderBase, IBrush>(nameof(IconForeground));

        public static readonly StyledProperty<bool> IsExpandedProperty =
            AvaloniaProperty.Register<ButtonExpanderBase, bool>(nameof(IsExpanded), false);

        public static readonly StyledProperty<ButtonExpandDirection> ExpandDirectionProperty =
            AvaloniaProperty.Register<ButtonExpanderBase, ButtonExpandDirection>(nameof(ExpandDirection), ButtonExpandDirection.ToRight);

        public static readonly StyledProperty<HorizontalAlignment> HorizontalIconAlignmentProperty =
            AvaloniaProperty.Register<ButtonExpanderBase, HorizontalAlignment>(nameof(HorizontalIconAlignment), HorizontalAlignment.Stretch);

        public static readonly StyledProperty<VerticalAlignment> VerticalIconAlignmentProperty =
            AvaloniaProperty.Register<ButtonExpanderBase, VerticalAlignment>(nameof(VerticalIconAlignment), VerticalAlignment.Stretch);

        public static readonly StyledProperty<double> OpenLengthProperty =
            AvaloniaProperty.Register<ButtonExpanderBase, double>(nameof(OpenLength));

        public static readonly StyledProperty<object> IconProperty =
            AvaloniaProperty.Register<ButtonExpanderBase, object>(nameof(Icon));

        public static readonly StyledProperty<Thickness> IconMarginProperty =
            AvaloniaProperty.Register<ButtonExpanderBase, Thickness>(nameof(IconMargin), default(Thickness));

        public static readonly StyledProperty<IBrush> IconBackgroundProperty =
            AvaloniaProperty.Register<ButtonExpanderBase, IBrush>(nameof(IconBackground));

        public ButtonExpanderBase()
        {
        }

        public ICommand? Command
        {
            get => GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public bool IsExpanded
        {
            get => GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        public ButtonExpandDirection ExpandDirection
        {
            get => GetValue(ExpandDirectionProperty);
            set => SetValue(ExpandDirectionProperty, value);
        }

        public object Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public HorizontalAlignment HorizontalIconAlignment
        {
            get => GetValue(HorizontalIconAlignmentProperty);
            set => SetValue(HorizontalIconAlignmentProperty, value);
        }

        public VerticalAlignment VerticalIconAlignment
        {
            get => GetValue(VerticalIconAlignmentProperty);
            set => SetValue(VerticalIconAlignmentProperty, value);
        }

        public Thickness IconMargin
        {
            get => GetValue(IconMarginProperty);
            set => SetValue(IconMarginProperty, value);
        }

        public IBrush IconForeground
        {
            get => GetValue(IconForegroundProperty);
            set => SetValue(IconForegroundProperty, value);
        }

        public IBrush IconBackground
        {
            get => GetValue(IconBackgroundProperty);
            set => SetValue(IconBackgroundProperty, value);
        }

        public double OpenLength
        {
            get => GetValue(OpenLengthProperty);
            set => SetValue(OpenLengthProperty, value);
        }
    }
}