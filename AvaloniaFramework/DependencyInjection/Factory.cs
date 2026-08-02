namespace AvaloniaFramework.DependencyInjection;

/// <summary>
/// A deferred resolution of <typeparamref name="TService"/>. Take one as a constructor parameter
/// when you need a fresh instance later rather than at construction time — a screen that pushes
/// another screen every time a button is tapped, for instance.
/// </summary>
/// <remarks>
/// Never registered explicitly: the container synthesises a <see cref="Factory{TService}"/> for
/// any service type it can resolve.
/// </remarks>
public sealed class Factory<TService>(Func<TService> create)
    where TService : class
{
    /// <summary>Resolves a new <typeparamref name="TService"/> from the container.</summary>
    public TService Create() => create();
}
