# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0 (.NET 10, LTS)

## Strategy
**Selected**: Bottom-Up (Dependency-First)
**Rationale**: 5 projects with 3-tier dependency hierarchy. Bottom-Up allows foundation libraries to be validated before the complex WinForms UI (15,647 API issues), isolating and fixing issues tier-by-tier.

### Execution Constraints
- Strict tier ordering: Tier N must complete and validate before Tier N+1
- SDK-style conversion prerequisite: All projects must convert to SDK-style format on net48 BEFORE any TFM changes
- Between-tier validation: After each tier upgrade, confirm higher tiers still build on old framework
- Final validation: Full solution Release build + complete test suite + zero warnings

## Source Control
- **Source Branch**: master
- **Working Branch**: upgrade-dotnet-10
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)
