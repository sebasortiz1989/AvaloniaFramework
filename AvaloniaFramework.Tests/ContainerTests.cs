using Xunit;

namespace AvaloniaFramework.Tests;

/// <summary>
/// The reflection-based container: lifestyles, constructor injection, the abstractions a
/// registration answers to, and the synthesised <see cref="Factory{TService}"/>.
/// </summary>
public class ContainerTests
{
    /// <summary>A builder that takes its registrations verbatim, so each test declares its own graph.</summary>
    private sealed class TestBuilder(params ContainerRegistration[] registrations) : ContainerBuilder
    {
        public override IEnumerable<ContainerRegistration> Registrations { get; } = registrations;

        public static ContainerRegistration Singleton<T>()
            where T : class => CreateSingleton<T>();

        public static ContainerRegistration SingletonInstance<T>(T instance)
            where T : class => CreateSingleton(instance);

        public static ContainerRegistration SingletonFrom<T>(Func<Container, T> factory)
            where T : class => CreateSingleton(factory);

        public static ContainerRegistration Transient<T>()
            where T : class => CreateTransient<T>();

        public static ContainerRegistration TransientAs<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService => CreateTransient<TService, TImplementation>();
    }

    private interface Greeter
    {
        string Greet();
    }

    private class Leaf
    {
        public Guid Id { get; } = Guid.NewGuid();
    }

    private sealed class PoliteGreeter : Leaf, Greeter
    {
        public string Greet() => "hello";
    }

    private sealed class NeedsLeaf(Leaf leaf)
    {
        public Leaf Leaf { get; } = leaf;
    }

    private sealed class NeedsFactory(Factory<Leaf> factory)
    {
        public Factory<Leaf> Factory { get; } = factory;
    }

    private sealed class NeedsMissing(Greeter greeter)
    {
        public Greeter Greeter { get; } = greeter;
    }

    [Fact]
    public void ASingletonIsTheSameInstanceEveryTime()
    {
        var container = new TestBuilder(TestBuilder.Singleton<Leaf>()).Build();

        Assert.Same(container.Resolve<Leaf>(), container.Resolve<Leaf>());
    }

    [Fact]
    public void ATransientIsANewInstanceEveryTime()
    {
        var container = new TestBuilder(TestBuilder.Transient<Leaf>()).Build();

        Assert.NotSame(container.Resolve<Leaf>(), container.Resolve<Leaf>());
    }

    [Fact]
    public void ConstructorParametersAreResolvedFromTheContainer()
    {
        var container = new TestBuilder(
            TestBuilder.Singleton<Leaf>(),
            TestBuilder.Transient<NeedsLeaf>()).Build();

        var resolved = container.Resolve<NeedsLeaf>();

        Assert.Same(container.Resolve<Leaf>(), resolved.Leaf);
    }

    [Fact]
    public void AnAlreadyBuiltInstanceIsHandedBackAsIs()
    {
        var instance = new Leaf();
        var container = new TestBuilder(TestBuilder.SingletonInstance(instance)).Build();

        Assert.Same(instance, container.Resolve<Leaf>());
    }

    [Fact]
    public void AFactoryDelegateIsUsedInsteadOfConstructorInjection()
    {
        var built = new Leaf();
        var container = new TestBuilder(TestBuilder.SingletonFrom(_ => built)).Build();

        Assert.Same(built, container.Resolve<Leaf>());
    }

    /// <summary>A hand-built registration can still reach the rest of the graph.</summary>
    [Fact]
    public void AFactoryDelegateCanResolveFromTheContainerItIsGiven()
    {
        var container = new TestBuilder(
            TestBuilder.Singleton<Leaf>(),
            TestBuilder.SingletonFrom(c => new NeedsLeaf(c.Resolve<Leaf>()))).Build();

        Assert.Same(container.Resolve<Leaf>(), container.Resolve<NeedsLeaf>().Leaf);
    }

    [Fact]
    public void AnImplementationCanBeRegisteredUnderAService()
    {
        var container = new TestBuilder(TestBuilder.TransientAs<Greeter, PoliteGreeter>()).Build();

        Assert.Equal("hello", container.Resolve<Greeter>().Greet());
    }

    /// <summary>
    /// WithAbstractions is what lets a view registered as its concrete type be resolved as the
    /// presenter base it derives from.
    /// </summary>
    [Fact]
    public void WithAbstractionsExposesInterfacesAndBaseTypes()
    {
        var container = new TestBuilder(TestBuilder.Singleton<PoliteGreeter>().WithAbstractions()).Build();

        Assert.IsType<PoliteGreeter>(container.Resolve<PoliteGreeter>());
        Assert.IsType<PoliteGreeter>(container.Resolve<Greeter>());
        Assert.IsType<PoliteGreeter>(container.Resolve<Leaf>());
    }

    /// <summary>The base walk stops before object, or everything would answer to it.</summary>
    [Fact]
    public void WithAbstractionsDoesNotRegisterObject()
    {
        var container = new TestBuilder(TestBuilder.Singleton<PoliteGreeter>().WithAbstractions()).Build();

        Assert.False(container.IsRegistered(typeof(object)));
    }

    /// <summary>A singleton exposed under several services is still one instance.</summary>
    [Fact]
    public void WithAbstractionsKeepsASingletonSingle()
    {
        var container = new TestBuilder(TestBuilder.Singleton<PoliteGreeter>().WithAbstractions()).Build();

        Assert.Same(container.Resolve<PoliteGreeter>(), (object)container.Resolve<Greeter>());
        Assert.Same(container.Resolve<PoliteGreeter>(), container.Resolve<Leaf>());
    }

    [Fact]
    public void AsAddsASingleExtraServiceType()
    {
        var container = new TestBuilder(TestBuilder.Singleton<PoliteGreeter>().As<Greeter>()).Build();

        Assert.Equal("hello", container.Resolve<Greeter>().Greet());
        Assert.False(container.IsRegistered(typeof(Leaf)));
    }

    /// <summary>
    /// Later registrations win, which is how a platform head replaces a default the shared layer
    /// registered — the mechanism the desktop and mobile heads rely on.
    /// </summary>
    [Fact]
    public void TheLastRegistrationForAServiceWins()
    {
        var first = new Leaf();
        var second = new Leaf();
        var container = new TestBuilder(
            TestBuilder.SingletonInstance(first),
            TestBuilder.SingletonInstance(second)).Build();

        Assert.Same(second, container.Resolve<Leaf>());
    }

    [Fact]
    public void ResolvingSomethingUnregisteredThrows()
    {
        var container = new TestBuilder().Build();

        Assert.Throws<ServiceNotRegisteredException>(() => container.Resolve<Leaf>());
    }

    /// <summary>A missing constructor dependency has to surface, not resolve to null.</summary>
    [Fact]
    public void AMissingDependencyOfARegisteredTypeThrows()
    {
        var container = new TestBuilder(TestBuilder.Transient<NeedsMissing>()).Build();

        Assert.ThrowsAny<Exception>(() => container.Resolve<NeedsMissing>());
    }

    [Fact]
    public void TryResolveReportsFailureInsteadOfThrowing()
    {
        var container = new TestBuilder(TestBuilder.Singleton<Leaf>()).Build();

        Assert.True(container.TryResolve<Leaf>(out var found));
        Assert.NotNull(found);
        Assert.False(container.TryResolve<NeedsLeaf>(out var missing));
        Assert.Null(missing);
    }

    [Fact]
    public void IsRegisteredAnswersForRegisteredAndUnregisteredTypes()
    {
        var container = new TestBuilder(TestBuilder.Singleton<Leaf>()).Build();

        Assert.True(container.IsRegistered(typeof(Leaf)));
        Assert.False(container.IsRegistered(typeof(NeedsLeaf)));
    }

    /// <summary>The container synthesises a factory for anything it can resolve; none is registered.</summary>
    [Fact]
    public void AFactoryIsSynthesisedForAnyResolvableService()
    {
        var container = new TestBuilder(TestBuilder.Transient<Leaf>()).Build();

        var factory = container.Resolve<Factory<Leaf>>();

        Assert.NotNull(factory);
        Assert.NotSame(factory.Create(), factory.Create());
    }

    [Fact]
    public void ASynthesisedFactoryCanBeInjected()
    {
        var container = new TestBuilder(
            TestBuilder.Transient<Leaf>(),
            TestBuilder.Transient<NeedsFactory>()).Build();

        var resolved = container.Resolve<NeedsFactory>();

        Assert.NotNull(resolved.Factory.Create());
    }

    /// <summary>A factory of a singleton still hands back the one instance.</summary>
    [Fact]
    public void AFactoryOfASingletonReturnsTheSameInstance()
    {
        var container = new TestBuilder(TestBuilder.Singleton<Leaf>()).Build();

        var factory = container.Resolve<Factory<Leaf>>();

        Assert.Same(factory.Create(), factory.Create());
        Assert.Same(container.Resolve<Leaf>(), factory.Create());
    }

    /// <summary>
    /// A factory of something the container cannot build has to fail rather than hand back a
    /// factory that throws later, at the point of use.
    /// </summary>
    /// <remarks>
    /// Note that IsRegistered still answers true here: it recognises the Factory&lt;&gt; shape
    /// without checking that the service inside it can be resolved. Resolve is the honest answer.
    /// </remarks>
    [Fact]
    public void ResolvingAFactoryOfAnUnregisteredServiceThrows()
    {
        var container = new TestBuilder().Build();

        Assert.Throws<ServiceNotRegisteredException>(() => container.Resolve<Factory<Leaf>>());
    }

    [Fact]
    public void ResolveAllReturnsEveryRegistrationForAService()
    {
        var container = new TestBuilder(
            TestBuilder.SingletonInstance(new Leaf()),
            TestBuilder.SingletonInstance(new Leaf())).Build();

        Assert.Equal(2, container.ResolveAll<Leaf>().Count());
    }

    [Fact]
    public void ResolveAllIsEmptyForAnUnregisteredService()
    {
        var container = new TestBuilder().Build();

        Assert.Empty(container.ResolveAll<Leaf>());
    }

    /// <summary>
    /// Composing builders is how a platform head picks up every layer below it by constructing
    /// only its own.
    /// </summary>
    [Fact]
    public void NestedBuildersContributeTheirRegistrations()
    {
        var inner = new TestBuilder(TestBuilder.Singleton<Leaf>());
        var outer = new ImmutableContainerBuilder([inner, new TestBuilder(TestBuilder.Transient<NeedsLeaf>())]);

        var container = outer.Build();

        Assert.NotNull(container.Resolve<NeedsLeaf>().Leaf);
    }
}