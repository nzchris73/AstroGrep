# 10-setup-mvvm-infrastructure: Add MVVM infrastructure to the project, including NuGet dependencies and base ViewModel class

Add MVVM infrastructure to the project, including NuGet dependencies and base ViewModel class.

**Scope**: Project-level setup
**Key Work**:
- Add `CommunityToolkit.Mvvm` NuGet package (or configure plain INotifyPropertyChanged if preferred)
- Create ViewModels folder structure in `WinformsGUI/`
- Add base ViewModel class if not using toolkit
- Verify project builds

**Affected Files**: `WinformsGUI/AstroGrep.csproj`, new folder `WinformsGUI/ViewModels/`

**Done when**: MVVM infrastructure is in place, project builds, and base classes are available for form refactoring
