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
  control renders untemplated in consuming apps with no build error. This applies to *templated*
  controls only. `Controls/Overlays/` and `Controls/Pickers/` hold composed `UserControl`s that ship
  their own visual tree, have no `ControlTheme`, and so belong nowhere in that file.
- **Never look a resource key up by name inside a control.** `{DynamicResource SurfaceRaised}` in a
  library control is a silent bet that every consuming app chose that word; it fails by rendering
  wrong, not by failing to build. Take appearance as `V*` properties with plain defaults instead,
  and let the app map its tokens on in one style block. The same goes for user-facing wording —
  `VHint`, `VShareText` and the like are properties with no default, so a library never invents a
  sentence in a language the app does not speak.
- **No IL weaver is required of consumers' assemblies or used in this one.** `PeriodPicker`
  hand-writes `INotifyPropertyChanged`; consumers remain free to use PropertyChanged.Fody in their
  own projects.
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
