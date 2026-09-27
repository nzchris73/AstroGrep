# 06-validate-tier-2-dependencies: Confirm UI application still builds on .NET Framework 4.8

Before upgrading the WinForms UI application, verify that AstroGrep.csproj still compiles on .NET Framework 4.8, consuming .NET 10 Tier 1 libraries. This is the final between-tier validation—any incompatible changes in business logic must be resolved here.

**Done when**: AstroGrep.csproj builds successfully on .NET Framework 4.8 against .NET 10 libraries from Tiers 0 and 1.

---

## Research & Assessment

### Project Structure
- **Project**: `WinformsGUI/AstroGrep.csproj`
- **Current Target**: net48 (single-target)
- **Type**: WinForms GUI application (Windows Forms with Designer forms)
- **Dependencies**:
  - `libAstroGrep` (Tier 1, now net10.0;net48)
  - `IFilterTextReader` (Tier 1, now net10.0;net48)
  - `AstroGrep.Common` (Tier 0, now net48;net10.0)
  - `AdminProcess` (Tier 0, now net48;net10.0)

### What This Validates
When Tier 1 and Tier 0 projects are built for net10.0, they produce separate binaries. For net48:
- Tier 1 produces `net48` DLLs that consume `net48` versions of Tier 0
- Tier 2 (this project) on net48 consumes those net48 DLLs

**The Gate**: Can AstroGrep.csproj still compile and reference the correct net48 binaries from the multi-targeted projects?

### Execution Plan
1. Clean and restore to ensure freshly compiled binaries from Task 05
2. Build WinformsGUI/AstroGrep.csproj on net48 explicitly
3. Verify no new errors or warnings introduced
4. Compare to Task 04 baseline (pre-Task-05 state)
5. If successful, confirm no blocking API incompatibilities

### Expected Outcome
✅ Build succeeds with ≤ 2 pre-existing warnings (MSB3884 rule set + one WPF/AvalonEdit warning)
