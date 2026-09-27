# 01-prepare-environment: Verify .NET 10 SDK and tooling

Validate that .NET 10 SDK is installed and available locally. Verify global.json compatibility if present. This is a prerequisite for all subsequent builds.

**Done when**: .NET 10 SDK is confirmed installed (`dotnet --version` shows net10.0 compatibility), and global.json (if present) allows .NET 10 SDK resolution.
