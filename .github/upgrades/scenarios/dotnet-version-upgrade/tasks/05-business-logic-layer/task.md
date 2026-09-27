# 05-business-logic-layer: Upgrade Tier 1 (libAstroGrep, IFilterTextReader) to .NET 10

Upgrade both business logic projects to .NET 10. Update framework targets and packages. Address API breaking changes identified in assessment (libAstroGrep: 8 issues, IFilterTextReader: 20 issues). These projects' APIs feed into the UI application, so validation here prevents cascading issues.

**Done when**: Both projects target net10.0, build successfully, all tests pass, and the UI application still builds on .NET Framework 4.8 (confirming no breaking API changes to its dependencies).
