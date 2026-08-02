using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaFramework.Hosting;

/// <summary>
/// The desktop shell. Assign it to <c>desktop.MainWindow</c>; the
/// <see cref="Navigation.AvaloniaNavigationController"/> then drives its content.
/// </summary>
public partial class ShellWindow : Window
{
    public ShellWindow() => AvaloniaXamlLoader.Load(this);
}