# 07-ui-application: Upgrade Tier 2 (AstroGrep WinForms/WPF UI) to .NET 10

Upgrade the WinForms UI application (with embedded WPF/AvalonEdit) to .NET 10. This is the highest-risk upgrade due to platform-specific APIs, WPF framework requirements, designer support, and complex plugin architecture.

**Done when**: `WinformsGUI/AstroGrep.csproj` multi-targets `net10.0-windows;net48`, builds successfully, all tests pass, all platform-specific analyzer warnings are properly handled, and the application is ready for Tier 2 production deployment.

---

## Research: WinForms/WPF Hybrid Compatibility Analysis

### Project Structure
- **Project**: `WinformsGUI/AstroGrep.csproj`
- **Current Target**: `net48` (will upgrade to `net10.0-windows;net48`)
- **Type**: Complex WinForms UI with embedded WPF/AvalonEdit editor and plugin architecture
- **Build Configuration**: `UseWindowsForms=true`, `UseWPF=true`, `ImportWindowsDesktopTargets=true`, `GenerateResourceUsePreserializedResources=true`, `OutputType=WinExe`

### Key Dependencies (6 NuGet Packages)
| Package | Version | Target | Risk | Notes |
|---------|---------|--------|------|-------|
| AvalonEdit | 6.1.3.50 | net45 | LOW | WPF text editor; pre-existing CS0618 warning in CustomLineNumberMargin.cs (line 186) |
| DocumentFormat.OpenXml | 2.17.1 | net46 | LOW | Word plugin; stable, well-tested |
| ExcelDataReader | 3.6.0 | net45 | LOW–LOW-MEDIUM | Excel plugin; dynamic load, graceful degradation |
| ExcelNumberFormat | 1.1.0 | net20 | LOW | Excel plugin dependency; stable |
| NLog | 5.0.2 | net46 | LOW | Already validated in Tier 0 ✅ |
| TagLibSharp | 2.2.0 | net45 | LOW | Media plugin; dynamic load, stable API |

### Internal Dependencies
- **AstroGrep.Common** (Tier 0, now `net48;net10.0`) ✅  
- **libAstroGrep** (Tier 1, now `net48;net10.0`) ✅  
- **IFilterTextReader** (Tier 1, now `net48;net10.0`) ✅

### Assessment Findings vs. Reality

**Reported Issues**: 15,647 total  
- `Project.0001`: SDK-style conversion (✅ already done)
- `Project.0002`: Target framework change (this task)
- `Api.0002`: System.Drawing.Bitmap (mostly false positives from auto-generated Resources.Designer.cs)

**Reality Check**:
- ~14,500+ of the 15,647 issues are **generated resource accessor noise** (Bitmap properties in designer-generated files; not real migration blockers)
- Actual blockers are **minimal**: platform-specific analyzer warnings (CA1416, SYSLIB0006) on intentional Windows APIs

### Expected Compatible Areas (No Code Changes Needed)

✅ **Registry Access** (`Microsoft.Win32.Registry`, `RegistryKey`)  
- Widely tested on .NET 5+ / .NET Core 3.1+  
- Fully supported on net10.0-windows  

✅ **Windows Shell APIs (P/Invoke)**  
- File dialogs (IFileDialog, IFileOpenDialog)  
- Shell links (IShellLink)  
- Taskbar progress (ITaskbarList3)  
- Icon retrieval (SHGetFileInfo)  
- UAC detection  
- **Status**: Stable, platform-specific; suppressions needed for CA1416 warnings  

✅ **Win32 Structures**  
- RECT, HDITEM, SHFILEINFO, FORMATRANGE, COMBOBOXINFO, etc.  
- **Status**: All compatible; no changes needed  

✅ **System.Drawing.Bitmap / GDI+**  
- Available on net10.0-windows (Windows desktop framework requirement)  
- Designer-generated resource accessors don't need changes  
- **Status**: Resources.Designer.cs is auto-generated; no edits required  

✅ **WPF Framework Integration**  
- AvalonEdit embedded as WPF control  
- Theme system with WPF brushes/colors  
- **Requirement**: Target must be `net10.0-windows` (not bare `net10.0`) to include WPF  
- **Status**: Pre-requisite met when TargetFramework changes  

✅ **WinForms Designer Support**  
- Visual Studio designer works on net10.0-windows (same support level as net48)  
- Post-build resource cleanup still applies  
- **Status**: Fully supported  

### Expected Compatibility Issues (Requires Action)

⚠️ **Thread.Abort() (if used)**  
- Deprecated in .NET 5, removed in .NET 10  
- **Likelihood**: Check `Grep.cs` line 301 and similar search/cancellation logic  
- **Action**: Replace with `CancellationToken` or conditional compilation  

⚠️ **Platform-Specific Analyzer Warnings**  
- CA1416: "...is only supported on 'windows'" for P/Invoke and shell APIs  
- SYSLIB0006: "Thread.Abort is not supported" (if found)  
- **Action**: Add `<NoWarn>CA1416;SYSLIB0006</NoWarn>` to PropertyGroup or suppress at call sites  

⚠️ **Plugin Dynamic Loading Under net10.0**  
- Plugins load via reflection from disk  
- ExcelDataReader and TagLibSharp have net45 targets (compatible with net10.0 on Windows)  
- **Risk**: LOW; plugins fail gracefully if assembly load fails  
- **Action**: Test plugin system after build; monitor for assembly bind errors  

### Execution Strategy

1. **Change TargetFramework** → `net10.0-windows;net48` (multi-target for compatibility)
2. **Add Analyzer Suppressions** → `<NoWarn>CA1416;SYSLIB0006</NoWarn>` in PropertyGroup
3. **Check Thread.Abort() Usage** → Search Grep.cs and search-related files
4. **Clean & Restore** → Full package resolution for net10.0-windows
5. **Build & Validate** → Verify both net48 and net10.0-windows build successfully
6. **Test Plugin Loading** → Run application briefly to confirm plugin discovery works
7. **Full Solution Validation** → Confirm Tier 0, Tier 1, and Tier 2 integrate correctly

### No Code Changes Anticipated
- Registry access (works as-is)  
- Win32 P/Invoke (stable, suppression-only)  
- System.Drawing (compatible on Windows desktop TFM)  
- Designer-generated resources (auto-generated, no edits)  
- WPF/AvalonEdit embedding (framework change handles it)  

### Expected Outcome

✅ Multi-target `net10.0-windows;net48`  
✅ Clean builds on both net48 and net10.0-windows  
✅ Platform warnings suppressed intentionally  
✅ Plugin system functional  
✅ Full solution integrates end-to-end  
✅ Ready for production on both framework versions
