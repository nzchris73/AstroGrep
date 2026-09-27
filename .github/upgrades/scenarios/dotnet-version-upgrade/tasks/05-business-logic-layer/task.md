# 05-business-logic-layer: Upgrade Tier 1 (libAstroGrep, IFilterTextReader) to .NET 10

Upgrade both business logic projects to .NET 10. Update framework targets and packages. Address API breaking changes identified in assessment. These projects' APIs feed into the UI application, so validation here prevents cascading issues.

**Done when**: Both projects target net10.0, build successfully, all tests pass, and the UI application still builds on .NET Framework 4.8 (confirming no breaking API changes to its dependencies).

---

## Research & Assessment

### libAstroGrep Project

**Current State:**
- Target Framework: `net48`
- Project Format: SDK-style (converted in Task 02) ✅
- Files: 97 total
- Dependencies: NLog 5.0.2 (✅ Compatible with .NET 10)

**Assessment Findings:**
- SDK-style conversion: Already done ✅
- TFM change required: `net48` → `net10.0`
- Source incompatible (Potential): 6 issues - pre-release/deprecated APIs requiring updates
- Binary incompatible (Mandatory): 2 issues - Remoting/Serialization (deprecated in .NET Core)
- **Total issues to address**: 8 API breaking changes

**Technologies Affected:**
- Remoting (2 issues)
- Deprecated Serialization patterns

---

### IFilterTextReader Project

**Current State:**
- Target Framework: `net48`
- Project Format: SDK-style (converted in Task 02) ✅
- Files: 24 total
- Dependencies: None

**Assessment Findings:**
- SDK-style conversion: Already done ✅
- TFM change required: `net48` → `net10.0`
- Source incompatible (Potential): 20 issues - APIs requiring updates for .NET 10 compatibility
- **Total issues to address**: 20 API breaking changes

**Technologies Affected:**
- Likely IFilter COM interface or P/Invoke updates needed

---

## Execution Plan

### Phase 1: Project File Updates (Configuration)
- [ ] Update libAstroGrep to multi-target `net48;net10.0`
- [ ] Update IFilterTextReader to multi-target `net48;net10.0`
- [ ] Run `dotnet restore` to validate package resolution

### Phase 2: Build & Issue Discovery
- [ ] Build libAstroGrep on `net10.0` target to identify specific compiler errors
- [ ] Build IFilterTextReader on `net10.0` target to identify specific compiler errors
- [ ] Document errors and breaking changes

### Phase 3: Code Fixes
- [ ] Address Remoting issues in libAstroGrep
- [ ] Address Serialization issues in libAstroGrep
- [ ] Fix source compatibility issues in both projects
- [ ] Apply conditional compilation (`#if NET10_0_OR_GREATER`) where needed

### Phase 4: Integration Validation
- [ ] Verify both projects build on `net48` target (no regressions)
- [ ] Verify both projects build on `net10.0` target
- [ ] Confirm Tier 2 (AstroGrep WinForms) still builds on `net48`
- [ ] Full solution build on mixed frameworks

---

## Done When Checklist

- [x] Task assessed and findings documented
- [ ] Both projects multi-target `net48;net10.0`
- [ ] Both projects build successfully on `net10.0` target
- [ ] Both projects build successfully on `net48` target
- [ ] Tier 2 (WinForms) still builds on `net48` (API compatibility verified)
- [ ] Full solution builds without errors
- [ ] progress-details.md completed with before/after summary
