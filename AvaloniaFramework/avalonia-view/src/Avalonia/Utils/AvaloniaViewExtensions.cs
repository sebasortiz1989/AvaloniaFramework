using Avalonia.Media;
using System;

namespace AvaloniaFramework.Apresentacao.Utils;

public static class AvaloniaViewExtensions
{
    public static Color HexToColor(string hex)
    {
        // Remove the '#' character if present
        if (hex != null)
        {
            hex = hex.TrimStart('#');

            // Parse the hex string
            byte a = 255; // Default to fully opaque
            byte r, g, b;

            if (hex.Length == 6)
            {
                r = Convert.ToByte(hex.Substring(0, 2), 16);
                g = Convert.ToByte(hex.Substring(2, 2), 16);
                b = Convert.ToByte(hex.Substring(4, 2), 16);
            }
            else if (hex.Length == 8)
            {
                a = Convert.ToByte(hex.Substring(0, 2), 16);
                r = Convert.ToByte(hex.Substring(2, 2), 16);
                g = Convert.ToByte(hex.Substring(4, 2), 16);
                b = Convert.ToByte(hex.Substring(6, 2), 16);
            }
            else
            {
                throw new ArgumentException("Invalid hex color format. Must be 6 or 8 characters long.");
            }

            return Color.FromArgb(a, r, g, b);
        }

        return Colors.Black;
    }
}