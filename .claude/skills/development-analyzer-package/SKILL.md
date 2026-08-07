---
name: development-analyzer-package
description: Why AvaloniaFramework.Development wires StyleCop.Analyzers as a real csproj dependency instead of inside targets, and why it is not DevelopmentDependency. Use when changing the AvaloniaFramework.Development package, its props/targets, stylecop.json, or analyzer wiring.
---

# The `AvaloniaFramework.Development` analyzer package

`AvaloniaFramework.Development` is build-only: MSBuild props/targets, the shared
`stylecop.json`, and the analyzer ruleset. No assembly; `IncludeBuildOutput=false`.
Consumers get StyleCop and the .NET analyzers from one `PackageReference`. See
`README.md` for the knobs.

Two things about that package are deliberate and must not be "tidied":

- `StyleCop.Analyzers` is a **real dependency in the `.csproj`**, not a
  `PackageReference` inside `Analyzer.CodeQuality.targets`. Restore does not see
  a `PackageReference` that only exists once the package is installed —
  declaring it there ships the settings file but runs no rules, which looks
  configured and is not. This was verified empirically, and is the bug the
  original `Verion.Development` had.
- The package is **not** `DevelopmentDependency`, because that implies
  `PrivateAssets="all"` on the consumer and blocks the transitive analyzer flow
  the design depends on.

When changing it, verify from a scratch consumer project that an SA rule
actually fires — a build that merely succeeds proves nothing here.
