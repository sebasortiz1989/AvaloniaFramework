---
name: framework-conventions
description: AvaloniaFramework's own API design conventions — Unit vs Void, non-I-prefixed interfaces, control theming (V* styled properties, LayoutStyles.axaml, StyleKeyOverride), the reflection-based container and its trim warnings. Use when adding or changing public types, controls, or DI wiring in this library.
---

# AvaloniaFramework conventions

These are this library's own deliberate API design choices — consumers rely on them, so don't
"normalize" them to generic .NET/Avalonia defaults without checking `README.md` for the reasoning.

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
