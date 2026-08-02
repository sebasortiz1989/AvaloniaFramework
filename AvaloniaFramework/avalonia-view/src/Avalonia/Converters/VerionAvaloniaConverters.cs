using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace AvaloniaFramework.Apresentacao.Converters;

public static class VerionAvaloniaConverters
{
    public static FuncValueConverter<bool?, bool?> InvertBooleanConverter { get; } = new(boolean =>
    {
        if (boolean != null)
        {
            return !boolean;
        }

        return false;
    });

    public static Color HexToArgb(string hexColor)
    {
        // Remove the # character if present
#pragma warning disable CA1307
        if (hexColor != null)
        {
            hexColor = hexColor.Replace("#", string.Empty);
#pragma warning restore CA1307

            // Parse the hexadecimal string to an integer
            int colorValue = int.Parse(hexColor, NumberStyles.HexNumber, CultureInfo.CurrentCulture);

            // Extract ARGB components
            byte a, r, g, b;

            if (hexColor.Length == 8)
            {
                // 8-digit hex code (with alpha)
                a = (byte)((colorValue >> 24) & 0xFF);
                r = (byte)((colorValue >> 16) & 0xFF);
                g = (byte)((colorValue >> 8) & 0xFF);
                b = (byte)(colorValue & 0xFF);
            }
            else if (hexColor.Length == 6)
            {
                // 6-digit hex code (without alpha)
                a = 255; // Fully opaque
                r = (byte)((colorValue >> 16) & 0xFF);
                g = (byte)((colorValue >> 8) & 0xFF);
                b = (byte)(colorValue & 0xFF);
            }
            else
            {
                throw new ArgumentException("Invalid hex color format. Use 6 or 8 digits.");
            }

            return Color.FromArgb(a, r, g, b);
        }

        return Colors.Black;
    }
}