# .NET 10 Upgrade Plan

## Overview

**Target**: Upgrade AstroGrep solution from .NET Framework 4.8 to .NET 10 (LTS)
**Scope**: 5 projects across 3 dependency tiers, ~60k LOC with significant Windows Forms/GDI+ migration work required

## Selected Strategy

**Bottom-Up (Dependency-First)** — Upgrade from leaf nodes to root applications, tier by tier.

**Rationale**: 5 projects with a clear 3-tier dependency graph. Bottom-Up allows foundation libraries to be validated before moving to the complex WinForms UI application, reducing risk and making it easier to isolate issues. The UI application has the highest complexity (15,647 API issues), so validating dependencies first is critical.

### Dependency Graph

```
Tier 2: [AstroGrep.csproj] (WinForms Application)
		↓
Tier 1: [libAstroGrep] [IFilterTextReader] [AstroGrep.Common]
		↓
Tier 0: [AdminProcess] [AstroGrep.Common] (Foundation Libraries)
```

**Tier 0 (Foundation)**: AdminProcess, AstroGrep.Common
- No internal dependencies
- **Completion criteria**: Projects build successfully, unit tests pass

**Tier 1 (Business Logic)**: libAstroGrep, IFilterTextReader
- Depends on Tier 0 projects
- **Completion criteria**: Projects build successfully, tests pass, Tier 2 still builds on .NET Framework 4.8

**Tier 2 (UI Application)**: AstroGrep.csproj (WinForms)
- Depends on Tier 1 and Tier 0 projects
- **Completion criteria**: Application builds successfully, all tests pass, full solution validation complete

---

## Tasks

### 01-prepare-environment: Verify .NET 10 SDK and tooling

Validate that .NET 10 SDK is installed and available locally. Verify global.json compatibility if present. This is a prerequisite for all subsequent builds.

**Done when**: .NET 10 SDK is confirmed installed (`dotnet --version` shows net10.0 compatibility), and global.json (if present) allows .NET 10 SDK resolution.

---

### 02-convert-to-sdk-style: Convert all projects to SDK-style project format

All 5 projects currently use legacy .NET Framework project format (.csproj with ToolsVersion). Convert them to modern SDK-style format to enable .NET 10 targeting. This conversion is prerequisite to framework upgrade and must complete successfully on the current .NET Framework 4.8 target before moving to TFM changes.

The conversion includes modernizing the project structure, removing redundant ItemGroups, transitioning from app.config to project file configuration, and preparing for NuGet package management updates.

**Done when**: All 5 projects (AdminProcess, AstroGrep.Common, IFilterTextReader, libAstroGrep, AstroGrep.csproj) build successfully in SDK-style format on .NET Framework 4.8, and project files follow modern conventions.

---

### 03-foundation-libraries: Upgrade Tier 0 (AdminProcess, AstroGrep.Common) to .NET 10

Upgrade framework target from net48 to net10.0 for both foundation libraries. Update all dependent packages to versions compatible with .NET 10. Address any breaking changes in APIs or package interfaces. These two projects have no internal dependencies and serve as the stable foundation for all other upgrades.

Assessment shows minimal API incompatibilities for these projects (2 issues each), making this tier a good validation checkpoint before proceeding to more complex layers.

**Done when**: Both projects target net10.0, build successfully, all package dependencies are resolved, and any code changes necessitated by breaking changes are complete.

---

### 04-validate-tier-1-dependencies: Confirm Tier 1 still builds on .NET Framework 4.8

Before upgrading Tier 1 projects, verify that libAstroGrep and IFilterTextReader still build successfully on .NET Framework 4.8, consuming the new .NET 10 Tier 0 libraries. This between-tier validation is the core safety mechanism of the bottom-up strategy—if this step fails, we stop before proceeding.

**Done when**: libAstroGrep and IFilterTextReader build without errors on .NET Framework 4.8, demonstrating that Tier 0's new package versions are compatible with the old framework.

---

### 05-business-logic-layer: Upgrade Tier 1 (libAstroGrep, IFilterTextReader) to .NET 10

Upgrade both business logic projects to .NET 10. Update framework targets and packages. Address API breaking changes identified in assessment (libAstroGrep: 8 issues, IFilterTextReader: 20 issues). These projects' APIs feed into the UI application, so validation here prevents cascading issues.

**Done when**: Both projects target net10.0, build successfully, all tests pass, and the UI application still builds on .NET Framework 4.8 (confirming no breaking API changes to its dependencies).

---

### 06-validate-tier-2-dependencies: Confirm UI application still builds on .NET Framework 4.8

Before upgrading the WinForms UI application, verify that AstroGrep.csproj still compiles on .NET Framework 4.8, consuming .NET 10 Tier 1 libraries. This is the final between-tier validation—any incompatible changes in business logic must be resolved here.

**Done when**: AstroGrep.csproj builds successfully on .NET Framework 4.8 against .NET 10 libraries from Tiers 0 and 1.

---

### 07-ui-application: Upgrade Tier 2 (AstroGrep WinForms) to .NET 10

Upgrade the WinForms UI application to .NET 10. This is the most complex step (15,647 API issues detected, primarily Windows Forms and GDI+ APIs with behavioral changes in .NET 10). Update framework target and all packages. Address breaking changes in Windows Forms, GDI+, System.Drawing, and related APIs.

Key migration areas:
- GDI+/System.Drawing APIs (882 issues) — may require abstraction or alternative rendering approaches
- Windows Forms control APIs (14,374 issues) — property changes, event handling updates, layout manager changes
- Windows Forms legacy controls (88 issues) — deprecated or unsupported controls
- WPF APIs if any (285 issues) — though assessment primarily shows WinForms focus

**Done when**: AstroGrep.csproj targets net10.0, builds successfully, all tests pass, and the application runs without runtime errors related to deprecated or breaking APIs.

---

### 08-final-validation: Full solution build, tests, and documentation

Run full solution build in Release configuration. Execute all unit tests and integration tests. Verify no deprecation warnings or obsolete API usage remains. Document any deferred recommendations or post-upgrade modernization opportunities (e.g., adopting newer async patterns, nullable reference types).

**Done when**: Solution builds successfully in Release mode with zero errors and zero warnings, all tests pass, and a completion report is generated documenting the upgrade outcome.

