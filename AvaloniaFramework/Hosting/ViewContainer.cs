using AvaloniaFramework.DependencyInjection;

namespace AvaloniaFramework.Hosting;

/// <summary>
/// The ambient container that views resolve their view model from.
/// </summary>
/// <remarks>
/// A static hand-off is unusual for dependency injection, but a control's constructor is called by
/// the XAML loader with no arguments, so there is nowhere else for the container to come from.
/// <see cref="ApplicationPreview"/> sets it during application construction.
/// </remarks>
public static class ViewContainer
{
    /// <summary>The container in force, or null before the application has been constructed.</summary>
    public static Container? Current { get; set; }

    /// <summary>The container in force, throwing a diagnosable error if it was never set.</summary>
    public static Container Required =>
        Current ?? throw new InvalidOperationException(
            $"No container has been published. Derive your Application from {nameof(ApplicationPreview)}, " +
            $"or set {nameof(ViewContainer)}.{nameof(Current)} before constructing views.");
}
