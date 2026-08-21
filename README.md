# AvaloniaFramework

A small MVP/navigation framework for [Avalonia](https://avaloniaui.net) 12, packaged as the
`AvaloniaFramework` NuGet package. It provides four things:

- a **dependency-injection container** with layered builders and deferred `Factory<T>` resolution
- a **presenter lifecycle** where showing a screen is an awaitable operation that returns a result
- a **navigation controller** that drives the app shell from a stack of presenters
- a few **styled controls** whose per-state appearance is set through style classes

## Install

```bash
dotnet add package AvaloniaFramework
```

## Wiring an app

Each layer owns a container builder that yields the builders below it plus its own registrations.

```csharp
public sealed class ViewContainerBuilder : ImmutableContainerBuilder
{
    public ViewContainerBuilder() : base(GetBuilders()) { }

    private static IEnumerable<ContainerBuilder> GetBuilders()
    {
        yield return new AvaloniaViewContainerBuilder();   // SynchronizationContext + NavigationController
        yield return new ViewmodelContainerBuilder();
        yield return new ImmutableContainerBuilder(GetRegistrations());
    }

    private static IEnumerable<ContainerRegistration> GetRegistrations()
    {
        // .WithAbstractions() also registers the view under PresenterBase<LoginViewModel, Unit, Unit>.
        yield return CreateTransient<LoginView>().WithAbstractions();
    }
}
```

Derive your `Application` from `ApplicationPreview` and include `LayoutStyles`:

```xml
<Application xmlns:framework="using:AvaloniaFramework" ...>
    <Application.Styles>
        <FluentTheme />
        <framework:LayoutStyles />
    </Application.Styles>
</Application>
```

```csharp
public partial class App : ApplicationPreview
{
    public App(Container container) : base(container) { }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new ShellWindow();
        else if (ApplicationLifetime is ISingleViewApplicationLifetime single)
            single.MainView = new ShellView();

        base.OnFrameworkInitializationCompleted();

        var navigation = Container.Resolve<NavigationController>();
        var login = Container.Resolve<PresenterBase<LoginViewModel, Unit, Unit>>();
        _ = navigation.PushAsync(login);
    }
}
```

## Screens

A view model derives from `PresentationModelBase<TInput, TResult>`; its run does not complete until
something finishes it, which is what makes navigation awaitable.

```csharp
public class LoginViewModel : PresentationModelBase<Unit, Unit>
{
    private readonly NavigationController navigation;
    private readonly Factory<PresenterBase<MainViewModel, Unit, Unit>> mainFactory;

    public LoginViewModel(
        NavigationController navigation,
        Factory<PresenterBase<MainViewModel, Unit, Unit>> mainFactory)
    {
        this.navigation = navigation;
        this.mainFactory = mainFactory;
        LoginCommand = new SynchronizedCommand(LoginAsync, SynchronizationBehavior.Discard, true);
    }

    public ICommand LoginCommand { get; }

    protected override Task OnRunStarting(Unit input) => Task.CompletedTask;

    // Completes only once MainViewModel is popped.
    private Task LoginAsync() => navigation.PushAsync(mainFactory.Create());
}
```

The matching view derives from `PresenterUserControl<,,>`, which resolves the view model from the
container and sets it as `DataContext`:

```csharp
public partial class LoginView : PresenterUserControl<LoginViewModel, Unit, Unit>
{
    public LoginView() => InitializeComponent();
}
```

## What's in the box

| Namespace | Contents |
|---|---|
| `AvaloniaFramework` | `Unit` (the no-input/no-result type), `LayoutStyles` |
| `AvaloniaFramework.Threading` | `WithSync()` / `NoSync()` / `Forget()`, `SynchronizationContext.SwitchTo()` and `Run()` |
| `AvaloniaFramework.DependencyInjection` | `Container`, `ContainerBuilder`, `ImmutableContainerBuilder`, `ContainerRegistration`, `Factory<T>` |
| `AvaloniaFramework.Presentation` | `NavigationController`, `SynchronizedCommand`, `SynchronizationBehavior`, `PresentationExecutionContext`, `PeriodPicker`, `PeriodScope`, `PeriodCell`, `MonthOption` |
| `AvaloniaFramework.Presentation.UseCase` | `PresentationModelBase<,>`, `PresenterBase<,,>`, `LifecycleStep<,>` |
| `AvaloniaFramework.Controls` | `PresenterUserControl<,,>`, `VButton`, `GroupButton`, `VTextBoxWithLabel`, `VPhotoViewer`, `VReportPreview`, `VPeriodPicker` |
| `AvaloniaFramework.Imaging` | `ImageLoader`, `PhotoCache`, `PhotoDownscaler`, `ExifOrientation` |
| `AvaloniaFramework.Hosting` | `ApplicationPreview`, `ShellWindow`, `ShellView`, `ScreenOverlay`, `AvaloniaNavigationController`, `AvaloniaViewContainerBuilder` |

`Unit` deliberately avoids the name `Void`, which collides with `System.Void` under a global using.

## Controls

`VButton`, `GroupButton`, and `VTextBoxWithLabel` expose their normal/pressed/checked/focused
appearance as separate `V*` styled properties, so a design system declares a whole variant as one
style class:

```xml
<Style Selector="buttons|VButton.BtnPrimary">
    <Setter Property="VNormalForeground" Value="{StaticResource ColorAccent}" />
    <Setter Property="VPressedBackground" Value="{StaticResource ColorAccentTint12}" />
    <Setter Property="VCornerRadius" Value="7" />
</Style>
```

```xml
<buttons:VButton Classes="BtnPrimary" Command="{Binding LoginCommand}" VText="Entrar" />
```

### Composed controls

`VPhotoViewer`, `VReportPreview` (`Controls.Overlays`) and `VPeriodPicker` (`Controls.Pickers`) are
whole pieces of screen rather than primitives, so they are `UserControl`s with a fixed arrangement —
but they take their appearance the same way, through `V*` properties. They never look a resource key
up by name: a control that reaches for `{DynamicResource SurfaceRaised}` is betting that every app it
lands in picked that word, and the bet fails silently. Every default is plain but legible, so the
control can be dropped in and judged before an app commits to styling it.

```xml
<Style Selector="overlays|VPhotoViewer">
    <Setter Property="VScrim" Value="{DynamicResource Scrim}" />
    <Setter Property="VSurface" Value="{DynamicResource SurfaceRaised}" />
    <Setter Property="VInk" Value="{DynamicResource InkPrimary}" />
</Style>
```

User-facing wording is a property too (`VHint`, `VShareText`, `VSaveText`), with no default — a
library has no business inventing a sentence in a language the app does not use. An unset caption
renders an empty button, which is loud enough to catch on the first run.

The two overlays cover the whole screen, so they report their open state to `ScreenOverlay`. A shell
that paints a navigation bar over its content subscribes to `ScreenOverlay.CoveredChanged` and hides
the bar while anything is covering; without that the bar sits on top of the overlay, because `ZIndex`
only orders siblings and the bar is in a different parent.

`VPeriodPicker` is driven by a `Presentation.PeriodPicker`, which the screen's view model owns and
constructs over itself as a `PeriodScope`. That interface is four members the screen almost certainly
already has — the month options, the year options, and the selected month and year.

### Imaging

`ImageLoader` is an attached property that fills an `Image` from a file without decoding on the UI
thread — the thing a value converter cannot do, because a converter has to return the bitmap inside
the layout pass:

```xml
<Image imaging:ImageLoader.Path="{Binding PhotoPath}" imaging:ImageLoader.DecodeWidth="192" />
```

`PhotoCache` is the bounded LRU behind it, keyed by path *and* decode width, so a row scrolled back
into view refills in the same frame. `PhotoDownscaler.ReduceAsync` caps a picked photo's longest edge
and re-encodes it — run it in the file picker, not at the storage layer, and every caller gets it for
free. It bakes EXIF rotation into the pixels and drops the tag, because a re-encode that kept the tag
would turn the photo twice; a photo already small and already upright passes through byte for byte.

## AvaloniaFramework.Development

A second, build-only package in this repo. It carries no assembly — just MSBuild props/targets, a
shared `stylecop.json`, and an analyzer ruleset, so every project can share one code-quality
configuration instead of copying settings around:

```xml
<PackageReference Include="AvaloniaFramework.Development" Version="1.0.0" />
```

That single reference turns on StyleCop plus the .NET analyzers (`AnalysisMode=AllEnabledByDefault`,
`EnforceCodeStyleInBuild`) and applies the shared settings. Warnings fail the build **in Release
only**, so day-to-day Debug builds stay workable.

The shared `stylecop.json` sets: `using` directives outside the namespace, `System.*` **not** sorted
first (plain alphabetical), XML docs required on public interfaces but not on `internal` members.

Knobs:

| Property | Effect |
|---|---|
| `<EnsureCodeQuality>false</EnsureCodeQuality>` | Opts a project out entirely. Test projects (`IsTestProject`) are excluded automatically. |
| `<EnsureCodeQualitySettings>false</EnsureCodeQualitySettings>` | Keeps the analyzers but drops the packaged `stylecop.json`, for a project supplying its own. |

Delete any local `stylecop.json` when adopting this package — two `AdditionalFiles` of that name is
ambiguous.

> **Note on the `PackageReference`-in-`.targets` pattern.** `StyleCop.Analyzers` is declared as a
> real dependency of this package, not added from `Analyzer.CodeQuality.targets`. A
> `PackageReference` that only appears inside a package's own imported targets is invisible to
> restore — the analyzer never installs and no StyleCop rule ever runs, while the settings file
> still ships and makes it look configured. For the same reason this package is deliberately **not**
> marked `DevelopmentDependency`: that implies `PrivateAssets="all"` on the consumer, which blocks
> the transitive analyzer flow this relies on.

## Trimming and NativeAOT

The container resolves by reflection (`ConstructorInfo.Invoke`, and `MakeGenericMethod` to
synthesise `Factory<T>`). That is fine for JIT and for the default mobile builds, but it produces
`IL2104` trim warnings and is **not safe under full trimming or NativeAOT** — a registered type
whose constructor is trimmed away will fail at resolution time, at runtime rather than at build
time. If you enable aggressive trimming, root your registered types (e.g. via a
`TrimmerRootDescriptor`) or replace the reflection path with source-generated factories.

## Build

```bash
dotnet build AvaloniaFramework.slnx
dotnet pack AvaloniaFramework.slnx -c Release   # -> artifacts/AvaloniaFramework.<version>.nupkg
```

Packing is an explicit step rather than a side effect of building, and always writes to
`artifacts/` regardless of configuration. There are no tests in this repo.

## Consuming it

Either add the published package, or — while the framework and the app are developed together —
vendor it as a git submodule and reference the project directly, which removes the pack/version/
restore cycle entirely:

```bash
git submodule add https://github.com/sebasortiz1989/AvaloniaFramework.git external/AvaloniaFramework
dotnet sln YourApp.sln add external/AvaloniaFramework/AvaloniaFramework/AvaloniaFramework.csproj
```

Then `<ProjectReference Include="…/AvaloniaFramework/AvaloniaFramework.csproj" />`. Consumers must
clone with `--recursive` (or run `git submodule update --init`), and the project must be in the
solution or restore fails with `NU1105`.
