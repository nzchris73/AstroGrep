# 15-refactor-text-editor-config-to-mvvm: Refactor frmAddEditTextEditor to use MVVM for editor configuration settings

Refactor frmAddEditTextEditor to use MVVM for editor configuration settings.

**Scope**: frmAddEditTextEditor.cs, new frmAddEditTextEditorViewModel.cs
**Key Work**:
- Extract editor configuration logic to ViewModel
- Bind form fields to ViewModel properties
- Implement validation rules in ViewModel
- Convert button actions to Commands

**Affected Files**: `WinformsGUI/Windows/Forms/frmAddEditTextEditor.cs`, new ViewModel

**Done when**: Editor configuration is managed by ViewModel, validation works, and data binding is complete
