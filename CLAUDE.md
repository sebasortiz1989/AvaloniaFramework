# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A .NET 8 class library that packages a set of reusable Avalonia UI controls, converters, markup
extensions, and presentation-layer (MVP) abstractions into a redistributable NuGet package
(`AvaloniaFramework`, currently v1.0.0). There is no runnable app in this repo — no `App.axaml`,
`Program.cs`, or entry point. It is consumed by other Avalonia applications as a component library.

## Commands

```bash
# Build (from repo root or AvaloniaFramework/)
dotnet build AvaloniaFramework.slnx
# or
cd AvaloniaFramework && dotnet build

# Pack the NuGet package (also happens automatically on build, since
# GeneratePackageOnBuild=true in the csproj)
cd AvaloniaFramework && dotnet pack
```

There are no test projects and no lint/format tooling configured in this repo. Do not invent test
or lint commands — verify changes by building and, if UI behavior changed, by referencing the
`Design.PreviewWith` block in the relevant `.axaml` file (Avalonia's XAML previewer) or by
consuming the package from a sample app.

## Project layout and source-inclusion gotcha


## Conventions specific to this codebase

## Avalonia docs connector

An Avalonia MCP connector is configured for this repo. Before writing or editing any `.axaml`, custom control, style selector, or binding, call `get_avalonia_expert_rules` once per session, then `search_avalonia_docs` for the specific topic. Prefer it over recalling Avalonia from memory — this project is on Avalonia **12.1.1**, so verify anything version-sensitive rather than assuming 11.x behaviour.

Limits worth knowing:

- `lookup_avalonia_api` has gaps (e.g. no entry for `InputPane`, which `UserControlMobile` relies on). `search_avalonia_docs` is the more reliable of the two.
- It covers stock Avalonia only. `Verion.Apresentacao.Avalonia` types (`VTextBoxWithLabel`, `PresenterBase`, `NavigationController`, `SynchronizedCommand`) are absent — read existing code or the package itself for those.
- The migration tools (`analyze_wpf_project`, `migrate_to_avalonia`, `migrate_to_xpf`, `lookup_wpf_to_avalonia_mapping`) are for WPF ports and are not relevant here.
