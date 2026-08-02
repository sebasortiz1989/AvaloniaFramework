namespace AvaloniaFramework.DependencyInjection;

/// <summary>
/// One entry in a container: the concrete type to build, how to build it, how long it lives, and
/// the set of service types it answers to.
/// </summary>
public sealed class ContainerRegistration
{
    private readonly IReadOnlyList<Type> serviceTypes;

    private ContainerRegistration(
        Type implementationType,
        IReadOnlyList<Type> serviceTypes,
        Lifestyle lifestyle,
        Func<Container, object>? factory)
    {
        ImplementationType = implementationType;
        this.serviceTypes = serviceTypes;
        Lifestyle = lifestyle;
        Factory = factory;
    }

    /// <summary>The type that gets constructed.</summary>
    public Type ImplementationType { get; }

    /// <summary>The lifetime of instances produced by this registration.</summary>
    public Lifestyle Lifestyle { get; }

    /// <summary>Explicit construction delegate, or null to use constructor injection.</summary>
    public Func<Container, object>? Factory { get; }

    /// <summary>Every type this registration can be resolved as.</summary>
    public IEnumerable<Type> ServiceTypes => serviceTypes;

    internal static ContainerRegistration Create(
        Type implementationType,
        Type serviceType,
        Lifestyle lifestyle,
        Func<Container, object>? factory = null) =>
        new(implementationType, [serviceType], lifestyle, factory);

    /// <summary>
    /// Also exposes the implementation under every interface it implements and every base class
    /// it derives from (excluding <see cref="object"/>). This is what lets a view registered as
    /// its concrete type be resolved as <c>PresenterBase&lt;TModel, TInput, TResult&gt;</c>.
    /// </summary>
    public ContainerRegistration WithAbstractions()
    {
        var expanded = new List<Type>(serviceTypes);

        foreach (var contract in Abstractions(ImplementationType))
        {
            if (!expanded.Contains(contract))
                expanded.Add(contract);
        }

        return new ContainerRegistration(ImplementationType, expanded, Lifestyle, Factory);
    }

    /// <summary>Adds a single extra service type to answer to.</summary>
    public ContainerRegistration As<TService>()
        where TService : class
    {
        if (serviceTypes.Contains(typeof(TService)))
            return this;

        return new ContainerRegistration(
            ImplementationType,
            [.. serviceTypes, typeof(TService)],
            Lifestyle,
            Factory);
    }

    private static IEnumerable<Type> Abstractions(Type type)
    {
        foreach (var contract in type.GetInterfaces())
            yield return contract;

        for (var baseType = type.BaseType; baseType is not null && baseType != typeof(object); baseType = baseType.BaseType)
            yield return baseType;
    }
}
