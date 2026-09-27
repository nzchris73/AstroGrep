# 04-validate-tier-1-dependencies: Confirm Tier 1 still builds on .NET Framework 4.8

Before upgrading Tier 1 projects, verify that libAstroGrep and IFilterTextReader still build successfully on .NET Framework 4.8, consuming the new .NET 10 Tier 0 libraries. This between-tier validation is the core safety mechanism of the bottom-up strategy—if this step fails, we stop before proceeding.

**Done when**: libAstroGrep and IFilterTextReader build without errors on .NET Framework 4.8, demonstrating that Tier 0's new package versions are compatible with the old framework.
