using System.Collections.Specialized;
using System.ComponentModel;

namespace AvaloniaFramework.Presentation.View;

// To be kept outside <see cref="ObservableRangeCollection{T}"/>, since otherwise, a new instance will be created
// for each generic type used.
internal static class EventArgsCache
{
    internal static readonly PropertyChangedEventArgs CountPropertyChanged = new PropertyChangedEventArgs("Count");
    internal static readonly PropertyChangedEventArgs IndexerPropertyChanged = new PropertyChangedEventArgs("Item[]");
    internal static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
}