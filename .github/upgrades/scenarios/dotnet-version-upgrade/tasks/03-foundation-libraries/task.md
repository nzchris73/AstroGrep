# 03-foundation-libraries: Upgrade Tier 0 (AdminProcess, AstroGrep.Common) to .NET 10

## Objective

Upgrade both foundation library projects (AdminProcess and AstroGrep.Common) from .NET Framework 4.8 (`net48`) to .NET 10 (`net10.0`). These are Tier 0 projects with no internal dependencies on other projects in the solution, making them ideal for early validation of the upgrade process.

## Scope

### Projects Affected
1. **AdminProcess** (Console application)
   - Path: `AdminProcess/AdminProcess.csproj`
   - Current TFM: `net48`
   - Target TFM: `net10.0`
   - SDK-style: ✅ Converted (Task 02)
   - Status: 2 issues (SDK-style already done; TFM needs changing)

2. **AstroGrep.Common** (Class library)
   - Path: `AstroGrep.Common/AstroGrep.Common.csproj`
   - Current TFM: `net48`
   - Target TFM: `net10.0`
   - SDK-style: ✅ Converted (Task 02)
   - Dependencies: 
     - NLog 5.0.2 ✅ (compatible with .NET 10)
   - Status: 2 issues (SDK-style already done; TFM needs changing)

### Assessment Findings
- **AdminProcess**: 2 mandatory issues
  - Project.0001: SDK-style conversion (✅ already completed)
  - Project.0002: TFM change (🔴 pending)
  - No API issues detected

- **AstroGrep.Common**: 2 mandatory issues
  - Project.0001: SDK-style conversion (✅ already completed)
  - Project.0002: TFM change (🔴 pending)
  - No API issues detected
  - Single NLog 5.0.2 dependency (compatible with .NET 10)

## Execution Plan

### Step 1: Change Target Framework
Replace `<TargetFramework>net48</TargetFramework>` with `<TargetFramework>net10.0</TargetFramework>` in each project file

### Step 2: Restore and Build Validation
- Build each project individually
- Build full solution to validate no cross-project issues
- Fix all warnings

### Step 3: Validate Success

**Done when**:
- [ ] `AdminProcess/AdminProcess.csproj` TargetFramework changed to `net10.0`
- [ ] `AstroGrep.Common/AstroGrep.Common.csproj` TargetFramework changed to `net10.0`
- [ ] Both projects build successfully with `dotnet build` (no errors)
- [ ] All NuGet packages resolve for `.NET 10` target
- [ ] No build warnings in either project
- [ ] Dependent projects (libAstroGrep, IFilterTextReader, AstroGrep.csproj) still build on `net48` against new `net10.0` libraries
- [ ] Assemblies produced in correct `bin/Debug/net10.0/` output path
