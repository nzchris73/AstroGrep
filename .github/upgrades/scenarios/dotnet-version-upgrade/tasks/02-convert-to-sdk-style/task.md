# 02-convert-to-sdk-style: Convert all projects to SDK-style project format

All 5 projects currently use legacy .NET Framework project format (.csproj with ToolsVersion). Convert them to modern SDK-style format to enable .NET 10 targeting. This conversion is prerequisite to framework upgrade and must complete successfully on the current .NET Framework 4.8 target before moving to TFM changes.

The conversion includes modernizing the project structure, removing redundant ItemGroups, transitioning from app.config to project file configuration, and preparing for NuGet package management updates.

**Done when**: All 5 projects (AdminProcess, AstroGrep.Common, IFilterTextReader, libAstroGrep, AstroGrep.csproj) build successfully in SDK-style format on .NET Framework 4.8, and project files follow modern conventions.
