namespace AvaloniaFramework.DependencyInjection;

/// <summary>Thrown when a service is asked for that nothing registered.</summary>
public sealed class ServiceNotRegisteredException(Type serviceType)
    : Exception($"No registration for '{serviceType}'. Register it in a container builder.")
{
    public Type ServiceType { get; } = serviceType;
}

/// <summary>
/// Thrown when a registration exists but an instance could not be produced — most often because
/// one of its constructor parameters is itself unregistered.
/// </summary>
public sealed class ActivationException(Type serviceType, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public Type ServiceType { get; } = serviceType;
}
