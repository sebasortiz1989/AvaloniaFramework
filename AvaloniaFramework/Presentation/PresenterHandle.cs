namespace AvaloniaFramework.Presentation;

/// <summary>
/// The non-generic face of a presenter. The navigation stack holds these, since entries with
/// different input and result types have to sit in one stack.
/// </summary>
public interface PresenterHandle
{
    /// <summary>
    /// Whether this presenter is bound to <paramref name="model"/>. Used by
    /// <see cref="NavigationController.PopAsync"/> so a pop only succeeds when the caller is the
    /// screen currently on top.
    /// </summary>
    bool Hosts(object model);

    /// <summary>
    /// Cancels the presenter's current run so an awaiting <c>PushAsync</c>/<c>RootAsync</c> does
    /// not hang when the screen is removed without a normal pop (root reset, dispose, pop-to-root).
    /// </summary>
    /// <returns>True when a run was canceled; false when nothing was running.</returns>
    bool AbandonRun();
}
