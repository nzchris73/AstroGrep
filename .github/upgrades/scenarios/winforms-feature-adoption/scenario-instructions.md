# WinForms Feature Adoption - Execution Instructions

## Strategy
Three-phase feature adoption with application-wide scope: Dark Mode (foundation) → Async APIs (mid-tier) → MVVM (architecture). Recommended after dotnet-version-upgrade scenario completes.

## Preferences
- **Flow Mode**: Guided
- **Commit Strategy**: After Each Task
- **Pace**: Standard
- **Features Selected**: All three (Dark Mode + Async APIs + MVVM)
- **Scope**: Application-wide (all forms and controls)

## Technical Context
- **Project**: WinformsGUI/AstroGrep.csproj
- **Target Frameworks**: net10.0-windows;net48 (both support all features)
- **Base Infrastructure**: BaseForm (11 dialogs inherit)
- **Custom Controls**: ColorButton, PictureButton, FilterValueType
- **Total Forms Scanned**: 12 main forms + 3 controls
- **Dark Mode Setup**: Application.SetColorMode required in Program.cs
- **Async Infrastructure**: Already partially present (legacy Task patterns)
- **MVVM Infrastructure**: Requires CommunityToolkit.Mvvm NuGet package

## Feature-Specific Dependencies
- **Dark Mode**: Depends on nothing; affects all forms via BaseForm
- **Async APIs**: Independent per-form; benefits from clean dark mode first
- **MVVM**: Requires NuGet package; has no hard dependencies on Dark Mode or Async

## Task Execution Order (Strict)

### Phase 1: Dark Mode (Tasks 01-05)
1. Enable dark mode in Program.cs (application-wide setup)
2. Update BaseForm (cascades to 11 dialogs)
3. Update frmMain (main window)
4. Update remaining dialogs (10 forms)
5. Update custom controls (3 controls)

### Phase 2: Async APIs (Tasks 06-10)
6. Update network-bound forms (frmCheckForUpdate, frmCheckForUpdateTemp)
7. Update log display (frmLogDisplay)
8. Update main form operations (frmMain search/file ops)
9. Update plugin discovery (frmPlugins)
10. Additional async opportunities (as discovered)

### Phase 3: MVVM (Tasks 11-15)
11. Set up MVVM infrastructure (NuGet + folders)
12. Refactor frmOptions (settings form)
13. Refactor frmLogDisplay (log management)
14. Refactor frmExclusions + frmAddEditExclusions (rule management)
15. Refactor remaining MVVM candidates (frmPlugins, frmAddEditTextEditor)

## Decisions
- **Centralize dark mode in BaseForm**: Changes propagate to 11 derived dialogs automatically
- **Phase order respects dependencies**: Dark mode is foundational, async is isolated per-form, MVVM is optional/architectural
- **Commit after each task**: Smaller, reviewable commits per task
- **Designer compatibility mandatory**: No modern C# in InitializeComponent; MVVM logic in code-behind Setup() methods

## Custom Instructions
- For all async tasks: Use `Control.InvokeAsync()` instead of `Control.Invoke()`
- For all MVVM tasks: Inherit from `ObservableObject` (CommunityToolkit.Mvvm) when possible
- For dark mode tasks: Use `SystemColors` for standard UI elements, custom palette for branded colors
- Never suppress warnings; fix all build warnings as they appear
