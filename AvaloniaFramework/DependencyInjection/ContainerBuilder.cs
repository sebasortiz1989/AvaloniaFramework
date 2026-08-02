namespace AvaloniaFramework.DependencyInjection;

/// <summary>
/// Declares what a layer contributes to the container. Each layer of an app owns one builder that
/// yields the builders of the layers below it plus its own registrations, so a platform head only
/// has to construct its own builder to get the whole graph.
/// </summary>
public abstract class ContainerBuilder
{
    /// <summary>Every registration this builder contributes, including those of nested builders.</summary>
    public abstract IEnumerable<ContainerRegistration> Registrations { get; }

    /// <summary>Materialises the registrations into a container ready to resolve from.</summary>
    public Container Build() => new(Registrations);

    /// <summary>Registers <typeparamref name="TImplementation"/> as itself, one instance per container.</summary>
    protected static ContainerRegistration CreateSingleton<TImplementation>()
        where TImplementation : class =>
        ContainerRegistration.Create(typeof(TImplementation), typeof(TImplementation), Lifestyle.Singleton);

    /// <summary>Registers an already-constructed instance.</summary>
    protected static ContainerRegistration CreateSingleton<TImplementation>(TImplementation instance)
        where TImplementation : class
    {
        ArgumentNullException.ThrowIfNull(instance);
        return ContainerRegistration.Create(
            instance.GetType(),
            typeof(TImplementation),
            Lifestyle.Singleton,
            _ => instance);
    }

    /// <summary>Registers a singleton built by hand rather than by constructor injection.</summary>
    protected static ContainerRegistration CreateSingleton<TImplementation>(Func<Container, TImplementation> factory)
        where TImplementation : class
    {
        ArgumentNullException.ThrowIfNull(factory);
        return ContainerRegistration.Create(
            typeof(TImplementation),
            typeof(TImplementation),
            Lifestyle.Singleton,
            container => factory(container));
    }

    /// <summary>Registers <typeparamref name="TImplementation"/> under <typeparamref name="TService"/>.</summary>
    protected static ContainerRegistration CreateSingleton<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService =>
        ContainerRegistration.Create(typeof(TImplementation), typeof(TService), Lifestyle.Singleton);

    /// <summary>Registers <typeparamref name="TImplementation"/> as itself, a new instance per resolution.</summary>
    protected static ContainerRegistration CreateTransient<TImplementation>()
        where TImplementation : class =>
        ContainerRegistration.Create(typeof(TImplementation), typeof(TImplementation), Lifestyle.Transient);

    /// <summary>Registers a transient built by hand rather than by constructor injection.</summary>
    protected static ContainerRegistration CreateTransient<TImplementation>(Func<Container, TImplementation> factory)
        where TImplementation : class
    {
        ArgumentNullException.ThrowIfNull(factory);
        return ContainerRegistration.Create(
            typeof(TImplementation),
            typeof(TImplementation),
            Lifestyle.Transient,
            container => factory(container));
    }

    /// <summary>Registers <typeparamref name="TImplementation"/> under <typeparamref name="TService"/>.</summary>
    protected static ContainerRegistration CreateTransient<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService =>
        ContainerRegistration.Create(typeof(TImplementation), typeof(TService), Lifestyle.Transient);
}
