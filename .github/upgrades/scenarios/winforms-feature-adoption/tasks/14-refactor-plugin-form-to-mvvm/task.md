# 14-refactor-plugin-form-to-mvvm: Refactor frmPlugins to use MVVM for plugin list management and commands

Refactor frmPlugins to use MVVM for plugin list management and commands.

**Scope**: frmPlugins.cs, new frmPluginsViewModel.cs
**Key Work**:
- Create ViewModel to manage plugin collection
- Bind plugin list to observable collection
- Implement Commands for load, unload, and refresh actions
- Move plugin discovery logic to ViewModel

**Affected Files**: `WinformsGUI/Windows/Forms/frmPlugins.cs`, new `WinformsGUI/ViewModels/frmPluginsViewModel.cs`

**Done when**: Plugin list is managed by ViewModel, plugin actions work through Commands
