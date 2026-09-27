# 02-adapt-base-form-for-dark-mode: Update the BaseForm class to support dark mode colors

Update the BaseForm class to support dark mode colors. Since all 11 dialogs inherit from BaseForm, changes cascade automatically.

## Research Findings

### Current BaseForm Implementation
- **File**: `WinformsGUI/Windows/Forms/BaseForm.cs` (172 lines)
- **Base Class**: `Form`
- **Current Theme System**: Uses `Core.Theme.ThemeProvider.Theme.Colors` for color values
- **Key Methods**:
  - `LoadTheme(Control ctrl)` — recursively applies theme colors to control and children
  - `ForceBackColor(Control ctrl, Color backColor)` — overrides control colors
  - `OnLoad()` override — calls `LoadTheme(this)` when form loads (skips Designer mode)

### Existing Color Mappings
- **ForeColor**: `Core.Theme.ThemeProvider.Theme.Colors.ForeColor`
- **BackColor (standard)**: `Core.Theme.ThemeProvider.Theme.Colors.Control`
- **BackColor (textbox/combobox)**: `Core.Theme.ThemeProvider.Theme.Colors.Window`
- **LinkColor**: `Core.Theme.ThemeProvider.Theme.Colors.LinkColor`
- **Special handling**: CheckBox/Label transparency on frmMain, ComboBox/Panel handling

### Target State
- Replace custom theme system with `SystemColors` for standard UI elements
- Add support for dark mode via `Application.IsDarkModeEnabled`
- Define custom color palette for branded elements requiring light/dark variants
- Update form background and text colors to respect dark mode
- Keep existing special-case handling where necessary (buttons, strips)

### Affected Derived Forms (11 total)
- frmAbout, frmAddEditExclusions, frmAddEditTextEditor, frmCheckForUpdate, frmCheckForUpdateTemp
- frmExclusions, frmLanguage, frmLogDisplay, frmMain, frmOptions, frmPlugins

### Key Dependencies
- No external packages required
- Multi-target: net10.0-windows (has SetColorMode) and net48 (doesn't, silently skips)
- Existing `ProcessColorChange` flag controls application of theme logic

## Scope
- Adapt BaseForm.cs to use `SystemColors` for standard UI elements
- Update OnLoad() to apply correct colors for current dark/light mode
- Keep `ProcessColorChange` flag for backward compatibility
- Update special control handling (Buttons, CheckBox, LinkLabel, etc.)

## Key Work
- [x] Replace hardcoded theme system with `SystemColors` equivalents
- [x] Add dark mode detection via `Application.IsDarkModeEnabled`
- [x] Define custom color palette for non-standard UI elements
- [x] Update BackColor/ForeColor assignments to use SystemColors
- [x] Verify all 11 derived forms inherit changes automatically
- [x] Test form rendering in Designer and at runtime

## Affected File
- `WinformsGUI/Windows/Forms/BaseForm.cs`

## Build & Test
- Build: `dotnet build AstroGrep.sln -c Debug`
- Validation: No build errors, no new warnings introduced
- Runtime: All 11 derived dialogs render with correct colors in light/dark mode

## Done when
- BaseForm compiles without errors
- All 11 derived forms inherit dark mode support automatically
- Designer opens frmMain without errors
- No new build warnings introduced
