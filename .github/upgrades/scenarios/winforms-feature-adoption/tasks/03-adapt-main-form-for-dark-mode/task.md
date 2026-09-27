# 03-adapt-main-form-for-dark-mode: Update frmMain (the primary application window) for dark mode compliance

Update frmMain (the primary application window) for dark mode compliance, including the menu bar, toolbars, tree views, and any custom-drawn elements.

**Scope**: frmMain.cs, frmMain.Designer.cs
**Key Work**:
- Review and replace hardcoded colors in Initialize and paint operations
- Update control foreground/background colors to use SystemColors or theme-aware palette
- Ensure tree view, list views, and custom controls display correctly in both modes
- Manual test: verify menu bar, buttons, text fields are readable in light and dark modes

**Affected Files**: `WinformsGUI/Windows/Forms/frmMain.cs`

**Done when**: frmMain renders correctly in both light and dark modes, all text is readable, and controls are visually distinct
