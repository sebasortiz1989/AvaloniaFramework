using Avalonia;
using AvaloniaFramework.DependencyInjection;

namespace AvaloniaFramework.Hosting;

/// <summary>
/// Base <see cref="Application"/> for apps using this framework. It holds the container and
/// publishes it to <see cref="ViewContainer"/> so views can resolve their own view model during
/// construction — which is what allows a view to be instantiated by the XAML previewer.
/// </summary>
/// <example>
/// <code>
/// public partial class App : ApplicationPreview
/// {
///     public App(Container container) : base(container) { }
///
///     // Parameterless constructor: used by the designer.
///     public App() : base(new AppContainerBuilder().Build()) { }
/// }
/// </code>
/// </example>
public abstract class ApplicationPreview : Application
{
    protected ApplicationPreview(Container container)
    {
        ArgumentNullException.ThrowIfNull(container);

        Container = container;
        ViewContainer.Current = container;
    }

    /// <summary>The application's dependency container.</summary>
    protected Container Container { get; }
}
