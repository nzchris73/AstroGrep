# 11-refactor-options-form-to-mvvm: Refactor frmOptions to use MVVM with a ViewModel for data binding and validation

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
