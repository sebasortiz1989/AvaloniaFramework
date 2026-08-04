using Xunit;

namespace AvaloniaFramework.Tests;

/// <summary>
/// The no-input/no-result type. Named Unit rather than Void on purpose: Void collides with
/// System.Void once a consuming app puts AvaloniaFramework in a global using, which is the
/// intended way to consume it.
/// </summary>
public class UnitTests
{
    [Fact]
    public void EveryUnitIsEqualToEveryOther()
    {
        Assert.Equal(default, Unit.Value);
        Assert.True(Unit.Value == default);
        Assert.False(Unit.Value != default);
        Assert.True(Unit.Value.Equals(default));
    }

    [Fact]
    public void AUnitIsNotEqualToSomethingElse()
    {
        Assert.False(Unit.Value.Equals("not a unit"));
        Assert.False(Unit.Value.Equals(null));
    }

    /// <summary>A constant hash keeps it usable as a dictionary key without surprises.</summary>
    [Fact]
    public void EveryUnitHashesTheSame()
    {
        Assert.Equal(Unit.Value.GetHashCode(), default(Unit).GetHashCode());
    }

    [Fact]
    public void ItPrintsAsEmptyParentheses()
    {
        Assert.Equal("()", Unit.Value.ToString());
    }
}