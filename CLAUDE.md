# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A .NET 10 class library packaging an MVP/navigation framework for Avalonia 12 into a redistributable
NuGet package (`AvaloniaFramework`, currently v1.0.0). There is no runnable app here — no
`Program.cs` or entry point. It is consumed by other Avalonia applications as a component library;
`../DapperDemo` is the reference consumer.

`README.md` documents the public API and how to wire an app; keep it in sync when the surface changes.

## Commands

```bash
dotnet build AvaloniaFramework.slnx
```

Packing is explicit and configuration-independent — output always lands in `artifacts/` (gitignored):

```bash
dotnet pack AvaloniaFramework.slnx -c Release
```

`GeneratePackageOnBuild` is deliberately **off**: `../DapperDemo` consumes this repo as a git
submodule + `ProjectReference`, so pack-on-build would fire on every one of that app's builds too.
Nothing needs packing for that app to pick up a change here — a plain rebuild is enough. Packing is
only for publishing.

Note the consumer pins a **submodule commit**, so a change here does not reach other machines until
it is committed and pushed and DapperDemo's submodule pointer is advanced.

There are no test projects and no lint/format tooling. Do not invent test or lint commands. Verify
changes by building, and for behavioural changes by building `../DapperDemo`
(`dotnet build DapperDemo.sln`). A GUI app cannot be launched from a headless shell — Avalonia's
native platform fails to start a render timer — so runtime verification of container, lifecycle,
navigation, and command behaviour is best done from a small console harness referencing the package.

## Two packages

- `AvaloniaFramework` — the runtime library.
- `AvaloniaFramework.Development` — build-only: MSBuild props/targets, the shared `stylecop.json`,
  and the analyzer ruleset. No assembly; `IncludeBuildOutput=false`. Consumers get StyleCop and the
  .NET analyzers from one `PackageReference`. See `README.md` for the knobs.

Two things about that package are deliberate and must not be "tidied":

- `StyleCop.Analyzers` is a **real dependency in the `.csproj`**, not a `PackageReference` inside
  `Analyzer.CodeQuality.targets`. Restore does not see a `PackageReference` that only exists once
  the package is installed — declaring it there ships the settings file but runs no rules, which
  looks configured and is not. This was verified empirically, and is the bug the original
  `Verion.Development` had.
- The package is **not** `DevelopmentDependency`, because that implies `PrivateAssets="all"` on the
  consumer and blocks the transitive analyzer flow the design depends on.

When changing it, verify from a scratch consumer project that an SA rule actually fires — a build
that merely succeeds proves nothing here.

## Layout

```
AvaloniaFramework/
  Core/            Unit, await helpers (WithSync/NoSync/Forget), SynchronizationContext.SwitchTo/Run
  DependencyInjection/  Container, ContainerBuilder, ImmutableContainerBuilder,
                        ContainerRegistration, Factory<T>, Lifestyle
  Presentation/    NavigationController, SynchronizedCommand, PresentationExecutionContext
    UseCase/       PresentationModelBase<,>, PresenterBase<,,>, LifecycleStep<,>
  Controls/        PresenterUserControl<,,>, Buttons/, Inputs/
  Hosting/         ApplicationPreview, ShellWindow, ShellView, Navigation/, DependencyInjection/
  LayoutStyles.axaml   Merges every control theme; consumers include this in App.axaml
```

## Conventions

- **`Unit`, never `Void`.** The no-input/no-result type is `Unit`. `Void` collides with
  `System.Void` once a consumer puts `AvaloniaFramework` in a global using, which is the intended
  consumption pattern.
- **Interfaces are not `I`-prefixed.** `NavigationController`, `PresenterBase<,,>`,
  `LifecycleStep<,>`, and `PresentationModel<,>` are interfaces. This is deliberate and matches the
  lineage this framework was extracted from — do not "fix" it piecemeal.
- **Every new control theme must be added to `LayoutStyles.axaml`** as a `ResourceInclude`, or the
  control renders untemplated in consuming apps with no build error.
- **Control appearance is expressed as `V*` styled properties per visual state**
  (`VNormalBackground`, `VPressedForeground`, `VCheckedImageOne`, …) rather than baked into the
  template, so consumers declare a whole variant as one style class. Template children are named
  `PART_*` and switched via nested `^:pressed /template/ …` selectors binding back with
  `{Binding $parent[ns:Control].VSomething}` — `TemplateBinding` does not work inside a nested
  style's setter.
- Controls deriving from a stock Avalonia control must override `StyleKeyOverride`, or they inherit
  the base control's theme instead of their own.
- `PresentationModelBase` declares a plain `PropertyChanged` event so consumers can use
  PropertyChanged.Fody (`[AddINotifyPropertyChangedInterface]`) on derived view models.
- **The container is reflection-based** (`ConstructorInfo.Invoke`, `MakeGenericMethod` for
  `Factory<T>`), which is where the `IL2104` trim warnings in consuming mobile builds come from.
  This is a known, documented limitation (see `README.md`) — not something to silence with a
  `NoWarn`. Resolution failures under trimming surface at runtime, not build time.

## Avalonia docs connector

An Avalonia MCP connector is configured for this repo. Before writing or editing any `.axaml`, custom
control, style selector, or binding, call `get_avalonia_expert_rules` once per session, then
`search_avalonia_docs` for the specific topic. Prefer it over recalling Avalonia from memory — this
project is on Avalonia **12.1.1**, so verify anything version-sensitive rather than assuming 11.x
behaviour.

Limits worth knowing:

- `lookup_avalonia_api` has gaps (e.g. no entry for `InputPane`, which `PresenterUserControl` relies
  on). `search_avalonia_docs` is the more reliable of the two.
- `search_avalonia_docs` can return responses too large to read in one call; prefer narrow queries.
- Some API details are faster to confirm against the reference assemblies in
  `~/.nuget/packages/avalonia/12.1.1/ref/net10.0/` than through the docs. For example `GotFocus`
  carries `FocusChangedEventArgs`, not a `GotFocusEventArgs`.
- CSS-shorthand `translate()` accepts **px only** — no percentages. A layout needing "shift by half
  my own height" has no declarative form; restructure the template instead.
- The migration tools (`analyze_wpf_project`, `migrate_to_avalonia`, `migrate_to_xpf`,
  `lookup_wpf_to_avalonia_mapping`) are for WPF ports and are not relevant here.
