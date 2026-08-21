using System.Collections.ObjectModel;

namespace AvaloniaFramework.Presentation;

/// <summary>
/// A screen that scopes what it shows to a month or a whole year.
/// </summary>
/// <remarks>
/// Implement it on the screen's view model and hand the screen to a <see cref="PeriodPicker"/>.
/// Every screen that offers a period then shares one picker instead of carrying its own copy of the
/// stepping and labelling — which is what drifts apart when each screen writes it again.
/// Not I-prefixed, matching this library's convention.
/// </remarks>
public interface PeriodScope
{
    /// <summary>Gets the whole-year entry plus the twelve months.</summary>
    ObservableCollection<MonthOption> MonthOptions { get; }

    /// <summary>Gets the years worth offering, most recent first.</summary>
    ObservableCollection<int> YearOptions { get; }

    /// <summary>Gets or sets the month shown, or the whole-year entry.</summary>
    MonthOption? SelectedMonth { get; set; }

    /// <summary>Gets or sets the year shown.</summary>
    int SelectedYear { get; set; }
}