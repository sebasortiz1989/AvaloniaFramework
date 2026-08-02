namespace AvaloniaFramework.DependencyInjection;

/// <summary>How long a resolved instance lives.</summary>
public enum Lifestyle
{
    /// <summary>A new instance per resolution.</summary>
    Transient,

    /// <summary>One instance per container, created on first resolution.</summary>
    Singleton,
}
