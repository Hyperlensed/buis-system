# Buis System

A Godot plugin that enables dynamic component instanciation through reactive data binding and a component-based architecture.

## Install

To install the addon run this command with git installed in the root directory of your godot project.
```bash
git submodule add https://github.com/Hyperlensed/buis-system.git addons/Hyperlensed/buis-system
```

## Support Table

| Feature                                       | Supported | Planned |
|:----------------------------------------------|:---------:|:-------:|
| Value Binding                                 | ❌        | ✅      |
| List                                          | ❌        | ✅      |
| List Insertion Carret                         | ❌        | ✅      |
| List Insertion Strategy                       | ❌        | ✅      |
| List Extraction Strategy                      | ❌        | ✅      |
| Screen Space Positioning Based On World Space | ❌        | ✅      |

## Known Bugs & Problems

| Bugs & Problems                                                                                                                | Reasons                                                                       | Current Workaround                                                       | Adoption Status                                                                                                                              |
|:-------------------------------------------------------------------------------------------------------------------------------|:------------------------------------------------------------------------------|:-------------------------------------------------------------------------|:---------------------------------------------------------------------------------------------------------------------------------------------|
| `BuisList` nodes incorrectly default to the base `Node` icon variant, instead of the appropriate type-specific one (e.g., `Node2D`, `Control`). | Godot's architecture limits a script file to being associated with only a single editor icon. | The only method is to create a separate script file for each icon variant. | ❌ **Rejected.** This workaround is unsuitable as it creates unnecessary script overhead and boilerplate for a purely visual distinction. |

Copyright (c) 2025 Hyperlensed
