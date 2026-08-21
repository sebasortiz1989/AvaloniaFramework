namespace AvaloniaFramework.Presentation;

/// <summary>
/// One entry of a period picker: the month number a query needs, and the name the user reads.
/// </summary>
/// <remarks>
/// The label is supplied rather than derived, so an app decides its own wording and language for
/// both the months and the whole-year entry.
/// </remarks>
/// <param name="number">Month number, 1 to 12, or <see cref="WholeYear"/>.</param>
/// <param name="label">The month's name, already capitalised.</param>
public sealed class MonthOption(int number, string label)
{
    /// <summary>
    /// The month number standing for "the whole year rather than one month".
    /// </summary>
    /// <remarks>
    /// Zero because no real month has it, so a query can compare month numbers without a separate
    /// flag travelling beside them.
    /// </remarks>
    public const int WholeYear = 0;

    /// <summary>Gets the month number, or <see cref="WholeYear"/>.</summary>
    public int Number { get; } = number;

    /// <summary>Gets the name shown for this entry.</summary>
    public string Label { get; } = label;
}