# 01-enable-dark-mode-application-wide: Enable dark mode support at the application level

Enable dark mode support at the application level so all forms automatically respect the OS theme preference.

## Research Findings

**Current State**:
- Program.cs location: `WinformsGUI/Windows/Program.cs`
- Main() method starts at line 82
- Current sequence:
  1. `Application.EnableVisualStyles()` (line 87)
  2. `Application.SetCompatibleTextRenderingDefault(false)` (line 88)
  3. Exception handlers setup (lines 90-94)
  4. Command line processing (line 96)
  5. Language/localization loading (lines 103-104)
  6. Form creation and `Application.Run()` (lines 106-118)

**Correct Insertion Point**:
- Add `Application.SetColorMode(SystemColorMode.System)` after `Application.SetCompatibleTextRenderingDefault(false)` (line 88)
- This ensures dark mode is configured before any form is created
- Placement: between EnableVisualStyles/SetCompatibleTextRenderingDefault and exception handlers

**Project Configuration**:
- Target frameworks: net10.0-windows;net48 - both support dark mode (.NET 9+ feature available in net10.0)
- Multi-target note: SetColorMode requires .NET 9+ conditional compilation on net48

## Scope
- **File**: WinformsGUI/Windows/Program.cs
- **Lines affected**: ~89 (insertion point)
- **Projects**: WinformsGUI only

## Key Work
- Add `Application.SetColorMode(SystemColorMode.System)` before exception handlers
- Handle multi-target scenario (net10.0-windows vs net48) if needed
- Test on both light and dark OS themes
- Validate all forms respond to theme changes

**Affected Files**: `WinformsGUI/Windows/Program.cs`

**Done when**: 
- Application starts successfully without errors
- Respects OS dark mode setting (manual test switching OS theme)
- All forms display with appropriate colors for the active theme
