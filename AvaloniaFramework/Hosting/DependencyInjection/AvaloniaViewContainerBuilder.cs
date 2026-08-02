using Avalonia.Threading;
using AvaloniaFramework.DependencyInjection;
using AvaloniaFramework.Hosting.Navigation;
using AvaloniaFramework.Presentation;

namespace AvaloniaFramework.Hosting.DependencyInjection;

/// <summary>
/// The framework's own registrations: the UI <see cref="SynchronizationContext"/> and the
/// navigation controller. Yield this first from your view layer's builder so everything above it
/// can take a <see cref="NavigationController"/> or a <see cref="SynchronizationContext"/>.
/// </summary>
public sealed class AvaloniaViewContainerBuilder : ImmutableContainerBuilder
{
    public AvaloniaViewContainerBuilder()
        : base(GetRegistrations())
    {
    }

    private static IEnumerable<ContainerRegistration> GetRegistrations()
    {
        // Installed here rather than in Program.cs so that any thread building the container ends
        // up with the same context the navigation controller and view models will marshal to.
        if (SynchronizationContext.Current is not AvaloniaSynchronizationContext)
            SynchronizationContext.SetSynchronizationContext(new AvaloniaSynchronizationContext(DispatcherPriority.Render));

        yield return CreateSingleton(SynchronizationContext.Current!);

        yield return CreateSingleton<NavigationController, AvaloniaNavigationController>();
    }
}
