using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace AvaloniaFramework;

/// <summary>
/// The framework's control themes. Include it once in <c>App.axaml</c>, after the base theme:
/// <code>
/// &lt;Application.Styles&gt;
///     &lt;FluentTheme /&gt;
///     &lt;framework:LayoutStyles /&gt;
/// &lt;/Application.Styles&gt;
/// </code>
/// Without it the framework's controls render untemplated.
/// </summary>
public class LayoutStyles : Styles
{
    public LayoutStyles(IServiceProvider? serviceProvider = null) =>
        AvaloniaXamlLoader.Load(serviceProvider, this);
}
