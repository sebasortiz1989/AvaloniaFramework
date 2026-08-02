using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;
using AvaloniaFramework.Apresentacao.Cards.Enums;

namespace AvaloniaFramework.Apresentacao.Converters
{
    public class BackgroundStatusConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            IBrush c = Brushes.Transparent;

            if (value is VCardInfoBackgroundState backgroundState)
            {
                switch (backgroundState)
                {
                    case VCardInfoBackgroundState.Desconectado:
                        c = Brush.Parse("#555D66");
                        break;
                    case VCardInfoBackgroundState.Conectado:
                        c = Brush.Parse("#75CE27");
                        break;
                    case VCardInfoBackgroundState.SemCombustivel:
                        c = Brush.Parse("#7A1519");
                        break;
                }
            }

            return c;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value!;
        }
    }
}