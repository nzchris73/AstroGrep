# WinForms Feature Adoption Plan

## Overview

**Target**: Modernize the AstroGrep WinForms UI application with .NET 10 features
**Scope**: 12 main forms, 3 custom controls, and 1 base infrastructure class
**Strategy**: Three-phase adoption (Dark Mode → Async APIs → MVVM) with application-wide scope

---

## Tasks

### Phase 1: Dark Mode Support

#### 01-enable-dark-mode-application-wide

Enable dark mode support at the application level so all forms automatically respect the OS theme preference.

**Scope**: Program.cs
**Key Work**: 
- Add `Application.SetColorMode(SystemColorMode.System)` before `Application.Run(form)`
- Verify it's called after any theme/UI initialization
- Test on both light and dark OS themes
- Validate all forms respond to theme changes

**Affected Files**: `WinformsGUI/Program.cs`

**Done when**: Application starts, respects OS dark mode setting, and all forms display with appropriate colors for the active theme

---

#### 02-adapt-base-form-for-dark-mode

Update the BaseForm class to support dark mode colors and remove hardcoded color values. Since all 11 dialogs inherit from BaseForm, this change cascades to the entire UI.

**Scope**: BaseForm.cs, implementation of color switching logic
**Key Work**:
- Replace hardcoded colors with `SystemColors` equivalents
- Add theme-aware property accessors for common UI elements
- Update any custom painting code to respect dark mode
- Test in Designer to ensure property display works

**Affected Files**: `WinformsGUI/Windows/Forms/BaseForm.cs`

**Done when**: BaseForm compiles, all derived forms inherit dark mode support, and Designer opens without errors

---

#### 03-adapt-main-form-for-dark-mode

Update frmMain (the primary application window) for dark mode compliance, including the menu bar, toolbars, tree views, and any custom-drawn elements.

**Scope**: frmMain.cs, frmMain.Designer.cs
**Key Work**:
- Review and replace hardcoded colors in Initialize and paint operations
- Update control foreground/background colors to use SystemColors or theme-aware palette
- Ensure tree view, list views, and custom controls display correctly in both modes
- Manual test: verify menu bar, buttons, text fields are readable in light and dark modes

**Affected Files**: `WinformsGUI/Windows/Forms/frmMain.cs`

**Done when**: frmMain renders correctly in both light and dark modes, all text is readable, and controls are visually distinct

---

#### 04-adapt-dialogs-for-dark-mode

Update all remaining dialog forms (frmOptions, frmAbout, frmLogDisplay, frmPlugins, etc.) to support dark mode colors.

**Scope**: All dialog forms in `WinformsGUI/Windows/Forms/`
**Key Work**:
- Systematically review each form's color settings and hardcoded values
- Replace with SystemColors or dynamic color calculations
- Test each dialog in both light and dark modes
- Ensure labels, buttons, text fields, and other controls remain readable

**Affected Files**: `frmOptions.cs`, `frmAbout.cs`, `frmLogDisplay.cs`, `frmCheckForUpdate.cs`, `frmCommandLine.cs`, `frmPlugins.cs`, `frmExclusions.cs`, `frmAddEditTextEditor.cs`, `frmAddEditFileEncoding.cs`, `frmAddEditExclusions.cs`, `frmCheckForUpdateTemp.cs`, `UnhandledExceptionDialog.cs`

**Done when**: All dialogs display correctly in both light and dark modes with no unreadable text or visual artifacts

---

#### 05-adapt-custom-controls-for-dark-mode

Update ColorButton and PictureButton custom controls to support dark mode.

**Scope**: ColorButton.cs, PictureButton.cs, FilterValueType.cs
**Key Work**:
- Ensure button backgrounds, borders, and hover states adapt to dark mode
- Update the color palette in ColorButton for light/dark variants
- Test color picker dialog appearance in both modes

**Affected Files**: `WinformsGUI/Windows/Controls/ColorButton.cs`, `PictureButton.cs`, `FilterValueType.cs`

**Done when**: Custom controls adapt their appearance to the active theme and are usable in both modes

---

### Phase 2: Async API Modernization

#### 06-modernize-update-check-async

Update frmCheckForUpdate and frmCheckForUpdateTemp to use modern async APIs for network operations instead of blocking calls or legacy Task.Run patterns.

**Scope**: frmCheckForUpdate.cs, frmCheckForUpdateTemp.cs
**Key Work**:
- Replace synchronous waits and Task.Result calls with async/await
- Convert `Control.Invoke()` calls to `await Control.InvokeAsync()`
- Wrap async event handlers in try/catch blocks
- Ensure cancellation tokens are properly passed
- Remove blocking calls from the UI thread

**Affected Files**: `WinformsGUI/Windows/Forms/frmCheckForUpdate.cs`, `frmCheckForUpdateTemp.cs`

**Done when**: Update checks complete asynchronously, UI remains responsive, and exception handling works correctly

---

#### 07-modernize-log-display-async

Update frmLogDisplay to use async APIs for loading large log datasets without blocking the UI thread.

**Scope**: frmLogDisplay.cs
**Key Work**:
- Convert log loading from synchronous to async operation
- Replace legacy invoke patterns with `InvokeAsync()`
- Ensure large log files don't freeze the UI
- Add proper error handling for async operations

**Affected Files**: `WinformsGUI/Windows/Forms/frmLogDisplay.cs`

**Done when**: Logs load asynchronously, UI stays responsive during large data operations

---

#### 08-modernize-main-form-async

Update frmMain search and file operation calls to use modern async patterns.

**Scope**: frmMain.cs
**Key Work**:
- Replace Task.Run with async operations
- Convert UI updates to InvokeAsync
- Pass cancellation tokens for long-running operations
- Ensure search operations can be cancelled

**Affected Files**: `WinformsGUI/Windows/Forms/frmMain.cs`

**Done when**: Main form operations execute asynchronously with proper UI updates and cancellation support

---

#### 09-modernize-plugin-discovery-async

Update frmPlugins to load plugins asynchronously without blocking the UI.

**Scope**: frmPlugins.cs
**Key Work**:
- Convert plugin discovery from synchronous enumeration to async operation
- Use InvokeAsync for UI updates
- Show loading indicator or progress feedback

**Affected Files**: `WinformsGUI/Windows/Forms/frmPlugins.cs`

**Done when**: Plugin loading is non-blocking, with proper progress feedback and error handling

---

### Phase 3: MVVM Pattern Adoption

#### 10-setup-mvvm-infrastructure

Add MVVM infrastructure to the project, including NuGet dependencies and base ViewModel class.

**Scope**: Project-level setup
**Key Work**:
- Add `CommunityToolkit.Mvvm` NuGet package (or configure plain INotifyPropertyChanged if preferred)
- Create ViewModels folder structure in `WinformsGUI/`
- Add base ViewModel class if not using toolkit
- Verify project builds

**Affected Files**: `WinformsGUI/AstroGrep.csproj`, new folder `WinformsGUI/ViewModels/`

**Done when**: MVVM infrastructure is in place, project builds, and base classes are available for form refactoring

---

#### 11-refactor-options-form-to-mvvm

Refactor frmOptions to use MVVM with a ViewModel for data binding and validation.

**Scope**: frmOptions.cs, new frmOptionsViewModel.cs
**Key Work**:
- Extract business logic from code-behind to ViewModel
- Implement INotifyPropertyChanged (or use ObservableObject)
- Convert event handlers to Commands
- Wire control bindings to ViewModel properties
- Ensure Designer still opens the form

**Affected Files**: `WinformsGUI/Windows/Forms/frmOptions.cs`, new `WinformsGUI/ViewModels/frmOptionsViewModel.cs`

**Done when**: Settings are loaded/saved through the ViewModel, UI responds to property changes, and Designer works correctly

---

#### 12-refactor-log-display-to-mvvm

Refactor frmLogDisplay to use MVVM for managing the log list data and display state.

**Scope**: frmLogDisplay.cs, new frmLogDisplayViewModel.cs
**Key Work**:
- Create ViewModel to manage log collection and filtering
- Bind list view to ViewModel's ObservableCollection
- Implement Commands for clear, export, and filter actions
- Move log loading logic to ViewModel
- Separate UI state from business logic

**Affected Files**: `WinformsGUI/Windows/Forms/frmLogDisplay.cs`, new `WinformsGUI/ViewModels/frmLogDisplayViewModel.cs`

**Done when**: Log display is fully data-bound to ViewModel, filtering/clearing work through Commands, and logic is testable

---

#### 13-refactor-exclusions-forms-to-mvvm

Refactor frmExclusions and frmAddEditExclusions to use MVVM for rule list management and editing.

**Scope**: frmExclusions.cs, frmAddEditExclusions.cs, and corresponding ViewModels
**Key Work**:
- Create ViewModels for rule list and rule editor
- Implement INotifyPropertyChanged for rule properties
- Convert rule add/edit/delete to Commands
- Bind rule list and editor controls to ViewModels
- Ensure Designer compatibility

**Affected Files**: `WinformsGUI/Windows/Forms/frmExclusions.cs`, `frmAddEditExclusions.cs`, new ViewModels

**Done when**: Rule management is fully bound to ViewModels, CRUD operations work smoothly, and Designer opens forms

---

#### 14-refactor-plugin-form-to-mvvm

Refactor frmPlugins to use MVVM for plugin list management and commands.

**Scope**: frmPlugins.cs, new frmPluginsViewModel.cs
**Key Work**:
- Create ViewModel to manage plugin collection
- Bind plugin list to observable collection
- Implement Commands for load, unload, and refresh actions
- Move plugin discovery logic to ViewModel

**Affected Files**: `WinformsGUI/Windows/Forms/frmPlugins.cs`, new `WinformsGUI/ViewModels/frmPluginsViewModel.cs`

**Done when**: Plugin list is managed by ViewModel, plugin actions work through Commands

---

#### 15-refactor-text-editor-config-to-mvvm

Refactor frmAddEditTextEditor to use MVVM for editor configuration settings.

**Scope**: frmAddEditTextEditor.cs, new frmAddEditTextEditorViewModel.cs
**Key Work**:
- Extract editor configuration logic to ViewModel
- Bind form fields to ViewModel properties
- Implement validation rules in ViewModel
- Convert button actions to Commands

**Affected Files**: `WinformsGUI/Windows/Forms/frmAddEditTextEditor.cs`, new ViewModel

**Done when**: Editor configuration is managed by ViewModel, validation works, and data binding is complete

---

## Success Criteria

- ✅ All selected features (Dark Mode, Async APIs, MVVM) successfully adopted
- ✅ Application builds without errors
- ✅ WinForms Designer can open and edit all modified forms
- ✅ Dark mode visual verification: forms display correctly in both light and dark OS themes
- ✅ Async operations: UI remains responsive during network calls, file operations, and data loading
- ✅ MVVM validation: ViewModels have no UI dependencies, property change notifications work correctly
- ✅ All existing tests pass
- ✅ No new compiler warnings introduced
