# 02-adapt-base-form-for-dark-mode: Update the BaseForm class to support dark mode colors and remove hardcoded color values

Update the BaseForm class to support dark mode colors and remove hardcoded color values. Since all 11 dialogs inherit from BaseForm, this change cascades to the entire UI.

**Scope**: BaseForm.cs, implementation of color switching logic
**Key Work**:
- Replace hardcoded colors with `SystemColors` equivalents
- Add theme-aware property accessors for common UI elements
- Update any custom painting code to respect dark mode
- Test in Designer to ensure property display works

**Affected Files**: `WinformsGUI/Windows/Forms/BaseForm.cs`

**Done when**: BaseForm compiles, all derived forms inherit dark mode support, and Designer opens without errors
