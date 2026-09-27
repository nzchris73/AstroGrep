# 07-ui-application: Upgrade Tier 2 (AstroGrep WinForms) to .NET 10

Upgrade the WinForms UI application to .NET 10. This is the most complex step (15,647 API issues detected, primarily Windows Forms and GDI+ APIs with behavioral changes in .NET 10). Update framework target and all packages. Address breaking changes in Windows Forms, GDI+, System.Drawing, and related APIs.

Key migration areas:
- GDI+/System.Drawing APIs (882 issues) — may require abstraction or alternative rendering approaches
- Windows Forms control APIs (14,374 issues) — property changes, event handling updates, layout manager changes
- Windows Forms legacy controls (88 issues) — deprecated or unsupported controls
- WPF APIs if any (285 issues) — though assessment primarily shows WinForms focus

**Done when**: AstroGrep.csproj targets net10.0, builds successfully, all tests pass, and the application runs without runtime errors related to deprecated or breaking APIs.
