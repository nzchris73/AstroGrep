# 04-adapt-dialogs-for-dark-mode: Update all remaining dialog forms (frmOptions, frmAbout, frmLogDisplay, frmPlugins, etc.) to support dark mode colors

Update all remaining dialog forms (frmOptions, frmAbout, frmLogDisplay, frmPlugins, etc.) to support dark mode colors.

**Scope**: All dialog forms in `WinformsGUI/Windows/Forms/`
**Key Work**:
- Systematically review each form's color settings and hardcoded values
- Replace with SystemColors or dynamic color calculations
- Test each dialog in both light and dark modes
- Ensure labels, buttons, text fields, and other controls remain readable

**Affected Files**: `frmOptions.cs`, `frmAbout.cs`, `frmLogDisplay.cs`, `frmCheckForUpdate.cs`, `frmCommandLine.cs`, `frmPlugins.cs`, `frmExclusions.cs`, `frmAddEditTextEditor.cs`, `frmAddEditFileEncoding.cs`, `frmAddEditExclusions.cs`, `frmCheckForUpdateTemp.cs`, `UnhandledExceptionDialog.cs`

**Done when**: All dialogs display correctly in both light and dark modes with no unreadable text or visual artifacts
