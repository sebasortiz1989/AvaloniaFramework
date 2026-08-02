using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaFramework.Hosting;

/// <summary>
/// The single-view shell, for mobile lifetimes. Assign it to
/// <c>singleViewPlatform.MainView</c>.
/// </summary>
public partial class ShellView : UserControl
{
    public ShellView() => AvaloniaXamlLoader.Load(this);
}