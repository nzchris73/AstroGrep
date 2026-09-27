# 06-modernize-update-check-async: Update frmCheckForUpdate and frmCheckForUpdateTemp to use modern async APIs for network operations

Update frmCheckForUpdate and frmCheckForUpdateTemp to use modern async APIs for network operations instead of blocking calls or legacy Task.Run patterns.

**Scope**: frmCheckForUpdate.cs, frmCheckForUpdateTemp.cs
**Key Work**:
- Replace synchronous waits and Task.Result calls with async/await
- Convert `Control.Invoke()` calls to `await Control.InvokeAsync()`
- Wrap async event handlers in try/catch blocks
- Ensure cancellation tokens are properly passed
- Remove blocking calls from the UI thread

**Affected Files**: `WinformsGUI/Windows/Forms/frmCheckForUpdate.cs`, `frmCheckForUpdateTemp.cs`

**Done when**: Update checks complete asynchronously, UI remains responsive, and exception handling works correctly
