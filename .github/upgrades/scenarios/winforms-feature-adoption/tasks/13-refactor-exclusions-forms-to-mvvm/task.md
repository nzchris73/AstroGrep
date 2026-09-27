# 13-refactor-exclusions-forms-to-mvvm: Refactor frmExclusions and frmAddEditExclusions to use MVVM for rule list management and editing

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
