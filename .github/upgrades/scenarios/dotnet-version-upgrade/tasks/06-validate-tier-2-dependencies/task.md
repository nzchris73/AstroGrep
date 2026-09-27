# 06-validate-tier-2-dependencies: Confirm UI application still builds on .NET Framework 4.8

Before upgrading the WinForms UI application, verify that AstroGrep.csproj still compiles on .NET Framework 4.8, consuming .NET 10 Tier 1 libraries. This is the final between-tier validation—any incompatible changes in business logic must be resolved here.

**Done when**: AstroGrep.csproj builds successfully on .NET Framework 4.8 against .NET 10 libraries from Tiers 0 and 1.
