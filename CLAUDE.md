# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

For deep, task-specific background, see the skills in `.claude/skills/`:
`development-analyzer-package` (the StyleCop/`.Development` package wiring) and
`framework-conventions` (Unit/interfaces/control-theming/DI design rules). Also
see the personal `avalonia-docs-connector` skill (`~/.claude/skills/`), which
applies across all Avalonia projects. Read the relevant one before working in
that area rather than duplicating it here.

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
  .NET analyzers from one `PackageReference`. See `README.md` for the knobs, and the
  `development-analyzer-package` skill before changing its wiring.

## Layout

```
AvaloniaFramework/
  Core/            Unit, await helpers (WithSync/NoSync/Forget), SynchronizationContext.SwitchTo/Run
  DependencyInjection/  Container, ContainerBuilder, ImmutableContainerBuilder,
                        ContainerRegistration, Factory<T>, Lifestyle
  Presentation/    NavigationController, SynchronizedCommand, PresentationExecutionContext,
                   PeriodPicker + PeriodScope/PeriodCell/MonthOption (hand-written INPC — see below)
    UseCase/       PresentationModelBase<,>, PresenterBase<,,>, LifecycleStep<,>
  Controls/        PresenterUserControl<,,>, Buttons/, Inputs/, Overlays/, Pickers/
  Imaging/         ImageLoader (attached), PhotoCache, PhotoDownscaler, ExifOrientation
  Hosting/         ApplicationPreview, ShellWindow, ShellView, ScreenOverlay, Navigation/,
                   DependencyInjection/
  LayoutStyles.axaml   Merges every control theme; consumers include this in App.axaml
```

## Conventions

See the `framework-conventions` skill for the full detail (Unit vs Void, interface naming, control
theming, the reflection-based container). Summary: don't normalize these to generic .NET/Avalonia
defaults — they're deliberate API choices consumers rely on.

Two additions from the 2026-08-21 move of DapperDemo's shared components into this library:

- **A control here never looks a resource key up by name.** No `{DynamicResource SurfaceRaised}`,
  no `{StaticResource IconClose}` — that is a silent bet on the consuming app having chosen that
  word. Appearance is `V*` styled properties with plain-but-legible defaults, and the app maps its
  tokens onto them in one style block. Same rule for user-facing wording (`VHint`, `VShareText`):
  a property with no default, never an English or Portuguese literal baked in.
- **No weaver.** `Presentation.PeriodPicker` implements `INotifyPropertyChanged` by hand rather
  than with `[AddINotifyPropertyChangedInterface]`. Consumers are welcome to use PropertyChanged.Fody
  on their own view models; a library must not require it of the assembly that references it.

`Controls/Overlays/` and `Controls/Pickers/` hold **composed** controls — whole pieces of screen,
`UserControl` with a fixed arrangement, not primitives with states worth re-templating. They have no
`ControlTheme` and so nothing to add to `LayoutStyles.axaml`; that rule is about templated controls.
Reach for a `ControlTheme` when the arrangement itself is the thing a consumer would want to replace.

## Avalonia docs connector

See the `avalonia-docs-connector` skill — call `get_avalonia_expert_rules` once per session before
touching any `.axaml`, custom control, style selector, or binding.
