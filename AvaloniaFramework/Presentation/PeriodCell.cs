using System.Windows.Input;

namespace AvaloniaFramework.Presentation;

/// <summary>
/// One tappable cell of a <see cref="PeriodPicker"/>: a month, or the whole year.
/// </summary>
/// <remarks>
/// Carries its own command so the month grid needs no <c>$parent</c> binding back to the screen's
/// view model.
/// </remarks>
/// <param name="label">What the cell reads, e.g. "Ago" or the year itself.</param>
/// <param name="number">The month number the query needs, or <see cref="MonthOption.WholeYear"/>.</param>
/// <param name="isSelected">Whether this is the period currently shown.</param>
/// <param name="selectCommand">Selects this period and closes the picker.</param>
public sealed class PeriodCell(string label, int number, bool isSelected, ICommand selectCommand) : IDisposable
{
    /// <summary>Gets the text the cell reads.</summary>
    public string Label { get; } = label;

    /// <summary>Gets the month number this cell selects.</summary>
    public int Number { get; } = number;

    /// <summary>Gets a value indicating whether this cell is the period on screen.</summary>
    public bool IsSelected { get; } = isSelected;

    /// <summary>Gets the command selecting this period.</summary>
    public ICommand SelectCommand { get; } = selectCommand;

    /// <summary>Disposes the cell's command.</summary>
    public void Dispose() => (SelectCommand as IDisposable)?.Dispose();
}