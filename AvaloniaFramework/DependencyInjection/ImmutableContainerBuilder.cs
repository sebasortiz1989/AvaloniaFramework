namespace AvaloniaFramework.DependencyInjection;

/// <summary>
/// A builder fixed at construction time. Derive from it and pass either your own registrations or
/// the builders of the layers you sit on top of:
/// <code>
/// public sealed class ViewContainerBuilder : ImmutableContainerBuilder
/// {
///     public ViewContainerBuilder()
///         : base(GetBuilders())
///     {
///     }
///
///     private static IEnumerable&lt;ContainerBuilder&gt; GetBuilders()
///     {
///         yield return new AvaloniaViewContainerBuilder();
///         yield return new ImmutableContainerBuilder(GetRegistrations());
///     }
/// }
/// </code>
/// Registrations are ordered: a later builder replaces an earlier one for the same service type.
/// </summary>
public class ImmutableContainerBuilder : ContainerBuilder
{
    private readonly IReadOnlyList<ContainerRegistration> registrations;

    /// <summary>Builds from a flat set of registrations.</summary>
    public ImmutableContainerBuilder(IEnumerable<ContainerRegistration> registrations)
    {
        ArgumentNullException.ThrowIfNull(registrations);
        this.registrations = [.. registrations];
    }

    /// <summary>Builds from nested builders, concatenating their registrations in order.</summary>
    public ImmutableContainerBuilder(IEnumerable<ContainerBuilder> builders)
    {
        ArgumentNullException.ThrowIfNull(builders);
        this.registrations = [.. builders.SelectMany(builder => builder.Registrations)];
    }

    /// <summary>Builds from nested builders.</summary>
    public ImmutableContainerBuilder(params ContainerBuilder[] builders)
        : this((IEnumerable<ContainerBuilder>)builders)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<ContainerRegistration> Registrations => registrations;
}
