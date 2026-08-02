using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AvaloniaFramework.Presentation.View;

[DebuggerStepThrough]
internal static class Throws
{
    [DoesNotReturn]
    public static object Argument([Localizable(true)] string message, params object[] args)
    {
        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, message, args));
    }

    [DoesNotReturn]
    public static T Argument<T>([Localizable(true)] string message, params object[] args)
    {
        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, message, args));
    }
}