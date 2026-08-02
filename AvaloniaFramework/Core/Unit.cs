namespace AvaloniaFramework;

/// <summary>
/// A unit type: the value a generic signature carries when it has nothing to carry.
/// Used by presenters that take no input and produce no result.
/// </summary>
public readonly struct Unit : IEquatable<Unit>
{
    /// <summary>The single inhabitant of the type.</summary>
    public static readonly Unit Value = default;

    public static bool operator ==(Unit left, Unit right) => left.Equals(right);

    public static bool operator !=(Unit left, Unit right) => !left.Equals(right);

    public bool Equals(Unit other) => true;

    public override bool Equals(object? obj) => obj is Unit;

    public override int GetHashCode() => 0;

    public override string ToString() => "()";
}
