using System;
using System.Collections.Generic;

namespace AvaloniaFramework.Hosting;

/// <summary>
/// Tracks whether anything is covering the screen, so the navigation bar can get out of its way.
/// </summary>
/// <remarks>
/// <para>
/// A shell that hosts its tabs in one control and puts the navigation bar beside them, added after,
/// has a bar that paints over everything a tab draws — including that tab's own dialogs and
/// full-screen images. Nothing a screen renders can get above it: <c>ZIndex</c> orders siblings
/// within one panel, and the bar is in a different parent entirely.
/// </para>
/// <para>
/// Moving the overlays into Avalonia's <c>OverlayLayer</c> would put them above the bar, but that
/// layer sits on the TopLevel and so outside any scaling applied further down — the trap that rules
/// popups out of a design-canvas layout. Hiding the bar is what is left, and it is usually what the
/// app wants anyway: a question that covers the bar cannot be walked away from by tapping a tab.
/// </para>
/// <para>
/// Wiring is two halves. A component that covers the screen calls
/// <see cref="Set"/> with itself and its open state; the shell subscribes to
/// <see cref="CoveredChanged"/> and binds its bar's visibility to <see cref="IsCovered"/>.
/// </para>
/// <para>
/// Sources are held by identity rather than counted. A control that reports "open" twice must not
/// leave the count stuck above zero and the bar hidden for the rest of the session.
/// </para>
/// </remarks>
public sealed class ScreenOverlay
{
    private readonly HashSet<object> covering = [];

    private ScreenOverlay()
    {
    }

    /// <summary>Raised when the screen becomes covered, or stops being covered.</summary>
    public event EventHandler? CoveredChanged;

    /// <summary>Gets the one instance. The navigation bar is a singleton too.</summary>
    public static ScreenOverlay Current { get; } = new();

    /// <summary>Gets a value indicating whether something is covering the screen.</summary>
    public bool IsCovered => covering.Count > 0;

    /// <summary>
    /// Records whether one control is covering the screen.
    /// </summary>
    /// <param name="source">The control, used as an identity. Safe to call repeatedly.</param>
    /// <param name="covered">Whether it is covering the screen now.</param>
    public void Set(object source, bool covered)
    {
        var was = IsCovered;

        if (covered)
        {
            covering.Add(source);
        }
        else
        {
            covering.Remove(source);
        }

        if (IsCovered != was)
        {
            CoveredChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}