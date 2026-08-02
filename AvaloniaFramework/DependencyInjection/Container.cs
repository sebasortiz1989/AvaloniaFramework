using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace AvaloniaFramework.DependencyInjection;

/// <summary>
/// The resolved, immutable result of a <see cref="ContainerBuilder"/>. Built once at startup and
/// then only read from, so it is safe to resolve from any thread.
/// </summary>
public sealed class Container
{
    private readonly Dictionary<Type, List<ContainerRegistration>> registrationsByService;
    private readonly ConcurrentDictionary<ContainerRegistration, object> singletons = new();

    internal Container(IEnumerable<ContainerRegistration> registrations)
    {
        registrationsByService = [];

        foreach (var registration in registrations)
        {
            foreach (var serviceType in registration.ServiceTypes)
            {
                if (!registrationsByService.TryGetValue(serviceType, out var list))
                    registrationsByService[serviceType] = list = [];

                list.Add(registration);
            }
        }
    }

    /// <summary>Resolves <typeparamref name="TService"/>, throwing when nothing provides it.</summary>
    public TService Resolve<TService>()
        where TService : class => (TService)Resolve(typeof(TService));

    /// <summary>Resolves <paramref name="serviceType"/>, throwing when nothing provides it.</summary>
    public object Resolve(Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);

        return TryResolve(serviceType, out var value)
            ? value
            : throw new ServiceNotRegisteredException(serviceType);
    }

    /// <summary>Resolves <typeparamref name="TService"/>, reporting failure instead of throwing.</summary>
    public bool TryResolve<TService>([NotNullWhen(true)] out TService? value)
        where TService : class
    {
        if (TryResolve(typeof(TService), out var resolved))
        {
            value = (TService)resolved;
            return true;
        }

        value = null;
        return false;
    }

    /// <summary>Every registration that answers to <typeparamref name="TService"/>.</summary>
    public IEnumerable<TService> ResolveAll<TService>()
        where TService : class
    {
        if (!registrationsByService.TryGetValue(typeof(TService), out var list))
            yield break;

        foreach (var registration in list)
            yield return (TService)Instantiate(registration);
    }

    /// <summary>True when the container can produce <paramref name="serviceType"/>.</summary>
    public bool IsRegistered(Type serviceType) =>
        registrationsByService.ContainsKey(serviceType) || IsFactoryType(serviceType);

    private static bool IsFactoryType(Type serviceType) =>
        serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(Factory<>);

    private bool TryResolve(Type serviceType, [NotNullWhen(true)] out object? value)
    {
        // Last registration wins, so a later builder in the chain can replace an earlier default.
        if (registrationsByService.TryGetValue(serviceType, out var list))
        {
            value = Instantiate(list[^1]);
            return true;
        }

        if (IsFactoryType(serviceType))
            return TryCreateFactory(serviceType, out value);

        value = null;
        return false;
    }

    private static readonly MethodInfo CreateFactoryCoreMethod =
        typeof(Container).GetMethod(nameof(CreateFactoryCore), BindingFlags.Instance | BindingFlags.NonPublic)!;

    private bool TryCreateFactory(Type factoryType, [NotNullWhen(true)] out object? value)
    {
        var serviceType = factoryType.GetGenericArguments()[0];
        if (serviceType.IsValueType || !IsRegistered(serviceType))
        {
            value = null;
            return false;
        }

        value = CreateFactoryCoreMethod.MakeGenericMethod(serviceType).Invoke(this, null)!;
        return true;
    }

    private Factory<TService> CreateFactoryCore<TService>()
        where TService : class => new(Resolve<TService>);

    private object Instantiate(ContainerRegistration registration)
    {
        if (registration.Lifestyle == Lifestyle.Singleton)
            return singletons.GetOrAdd(registration, Build);

        return Build(registration);
    }

    private object Build(ContainerRegistration registration)
    {
        if (registration.Factory is not null)
            return registration.Factory(this);

        var constructor = SelectConstructor(registration.ImplementationType);
        var parameters = constructor.GetParameters();
        var arguments = new object[parameters.Length];

        for (var i = 0; i < parameters.Length; i++)
        {
            if (!TryResolve(parameters[i].ParameterType, out var argument))
            {
                throw new ActivationException(
                    registration.ImplementationType,
                    $"Cannot construct '{registration.ImplementationType}': its constructor parameter " +
                    $"'{parameters[i].Name}' of type '{parameters[i].ParameterType}' is not registered.");
            }

            arguments[i] = argument;
        }

        try
        {
            return constructor.Invoke(arguments);
        }
        catch (TargetInvocationException exception) when (exception.InnerException is not null)
        {
            throw new ActivationException(
                registration.ImplementationType,
                $"The constructor of '{registration.ImplementationType}' threw.",
                exception.InnerException);
        }
    }

    private ConstructorInfo SelectConstructor(Type implementationType)
    {
        var constructors = implementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

        if (constructors.Length == 0)
        {
            throw new ActivationException(
                implementationType,
                $"'{implementationType}' has no public constructor.");
        }

        // Greediest satisfiable constructor, so an optional dependency simply drops out of the
        // selection instead of failing the whole resolution.
        var best = constructors
            .OrderByDescending(candidate => candidate.GetParameters().Length)
            .FirstOrDefault(candidate => candidate.GetParameters()
                .All(parameter => IsRegistered(parameter.ParameterType)));

        return best ?? constructors.OrderByDescending(c => c.GetParameters().Length).First();
    }
}
