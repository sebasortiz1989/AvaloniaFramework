---
name: avalonia-docs-connector
description: How to use the Avalonia MCP docs connector correctly for this project (Avalonia 12.1.1). Use before writing or editing any .axaml, custom control, style selector, or binding.
---

# Using the Avalonia docs connector

An Avalonia MCP connector is configured for this repo. Before writing or
editing any `.axaml`, custom control, style selector, or binding, call
`get_avalonia_expert_rules` once per session, then `search_avalonia_docs` for
the specific topic. Prefer it over recalling Avalonia from memory — this
project is on Avalonia **12.1.1**, so verify anything version-sensitive rather
than assuming 11.x behaviour.

Limits worth knowing:

- `lookup_avalonia_api` has gaps (e.g. no entry for `InputPane`, which
  `PresenterUserControl` relies on). `search_avalonia_docs` is the more
  reliable of the two.
- `search_avalonia_docs` can return responses too large to read in one call;
  prefer narrow queries.
- Some API details are faster to confirm against the reference assemblies in
  `~/.nuget/packages/avalonia/12.1.1/ref/net10.0/` than through the docs. For
  example `GotFocus` carries `FocusChangedEventArgs`, not a `GotFocusEventArgs`.
- CSS-shorthand `translate()` accepts **px only** — no percentages. A layout
  needing "shift by half my own height" has no declarative form; restructure
  the template instead.
- The migration tools (`analyze_wpf_project`, `migrate_to_avalonia`,
  `migrate_to_xpf`, `lookup_wpf_to_avalonia_mapping`) are for WPF ports and are
  not relevant here.
