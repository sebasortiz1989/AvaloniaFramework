using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AvaloniaFramework.Presentation;

/// <summary>
/// The state behind an inline period picker: a year stepper over a grid of the whole year and the
/// twelve months. Pair it with <c>Controls.Pickers.VPeriodPicker</c>.
/// </summary>
/// <remarks>
/// <para>
/// Expanded in ordinary layout rather than in a popup. A popup lays out in its own visual root and
/// so ignores any scale applied further down the tree, which makes it the wrong choice in an app
/// that scales its whole UI to a design canvas.
/// </para>
/// <para>
/// It exists because stepping alone makes a past month expensive: reaching December of last year
/// means walking back a month at a time, seven taps or more. Year row, then a month cell, is three.
/// </para>
/// <para>
/// One per screen, driven through <see cref="PeriodScope"/>, so several screens offering a period
/// cannot drift apart — which is what happens to stepping logic each time a screen writes its own.
/// </para>
/// <para>
/// INotifyPropertyChanged is hand-written rather than woven. Consumers are free to use
/// PropertyChanged.Fody on their own view models, but a library should not require a weaver of the
/// assembly that references it.
/// </para>
/// </remarks>
public sealed class PeriodPicker : INotifyPropertyChanged
{
    private readonly PeriodScope scope;
    private readonly Func<int, string> shortMonthName;

    private bool isOpen;
    private string yearLabel = string.Empty;

    /// <summary>Initializes a new instance of the <see cref="PeriodPicker"/> class.</summary>
    /// <param name="scope">The screen whose period this picks.</param>
    /// <param name="shortMonthName">
    /// Names a month in the three-or-so letters a cell has room for. Defaults to the current
    /// culture's abbreviated month names; pass an app's own when those read badly at cell width.
    /// </param>
    public PeriodPicker(PeriodScope scope, Func<int, string>? shortMonthName = null)
    {
        this.scope = scope;
        this.shortMonthName = shortMonthName ?? DefaultShortMonthName;

        ToggleCommand = new SynchronizedCommand(Toggle, SynchronizationBehavior.Discard, true);
        PreviousYearCommand = new SynchronizedCommand(() => StepYear(-1), SynchronizationBehavior.Discard, true);
        NextYearCommand = new SynchronizedCommand(() => StepYear(1), SynchronizationBehavior.Discard, true);
    }

    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>Gets a value indicating whether the picker is expanded.</summary>
    public bool IsOpen
    {
        get => isOpen;
        private set => Set(ref isOpen, value);
    }

    /// <summary>
    /// Gets the year the picker is showing, as text.
    /// </summary>
    /// <remarks>
    /// Assigned in <see cref="Refresh"/> rather than computed from the scope. A computed form
    /// depends on a property of another object, which no change notification here can see, so the
    /// label freezes on the year the picker opened with.
    /// </remarks>
    public string YearLabel
    {
        get => yearLabel;
        private set => Set(ref yearLabel, value);
    }

    /// <summary>Gets the whole-year cell plus the twelve months.</summary>
    public ObservableCollection<PeriodCell> Cells { get; } = [];

    /// <summary>Gets the command opening and closing the picker.</summary>
    public ICommand ToggleCommand { get; }

    /// <summary>Gets the command moving back one year without closing the picker.</summary>
    public ICommand PreviousYearCommand { get; }

    /// <summary>Gets the command moving forward one year without closing the picker.</summary>
    public ICommand NextYearCommand { get; }

    /// <summary>
    /// Rebuilds the cells, which is also what re-marks the selected one and relabels the
    /// whole-year cell after a year change.
    /// </summary>
    /// <remarks>
    /// Call it from the screen's SelectedMonth and SelectedYear hooks, so the highlight cannot go
    /// stale whatever moved the period — an arrow, this picker, or a filter.
    /// Thirteen short-lived objects, rebuilt rather than mutated, matching an ordinary row list.
    /// </remarks>
    public void Refresh()
    {
        foreach (var cell in Cells)
        {
            cell.Dispose();
        }

        Cells.Clear();

        YearLabel = scope.SelectedYear.ToString(CultureInfo.CurrentCulture);

        var current = scope.SelectedMonth?.Number ?? MonthOption.WholeYear;

        foreach (var option in scope.MonthOptions)
        {
            var number = option.Number;

            // CA2000: ownership passes to the PeriodCell, which disposes the command when this
            // list is rebuilt above.
#pragma warning disable CA2000
            var select = new SynchronizedCommand(() => Select(number), SynchronizationBehavior.Discard, true);
#pragma warning restore CA2000

            // Short names so twelve months fit four to a row. The whole-year cell reads as the year
            // itself rather than a phrase, which comes out clipped in a cell this width — and the
            // year beside "jan" says the same thing in the space available.
            var label = number == MonthOption.WholeYear
                ? scope.SelectedYear.ToString(CultureInfo.CurrentCulture)
                : shortMonthName(number);

            Cells.Add(new PeriodCell(label, number, number == current, select));
        }
    }

    /// <summary>
    /// Closes the picker without selecting anything.
    /// </summary>
    /// <remarks>
    /// For a screen whose presenter is reused across records: a picker left open on one record
    /// would still be open when the next record's screen appears — an expanded control the new
    /// visitor never asked for.
    /// </remarks>
    public void Close() => IsOpen = false;

    private static string DefaultShortMonthName(int month) =>
        CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[month - 1];

    private void Toggle()
    {
        IsOpen = !IsOpen;

        if (IsOpen)
        {
            Refresh();
        }
    }

    /// <summary>Moves a whole year, leaving the picker open so a month can follow.</summary>
    private void StepYear(int delta)
    {
        var year = scope.SelectedYear + delta;

        if (!scope.YearOptions.Contains(year))
        {
            scope.YearOptions.Add(year);
        }

        scope.SelectedYear = year;
    }

    private void Select(int number)
    {
        scope.SelectedMonth = scope.MonthOptions.FirstOrDefault(m => m.Number == number) ?? scope.SelectedMonth;
        IsOpen = false;
    }

    private void Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return;
        }

        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}