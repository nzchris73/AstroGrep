# .NET Framework 4.8 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Convert AstroGrep.csproj to SDK-style project format
2. Convert AdminProcess.csproj to SDK-style project format
3. Convert IFilterTextReader.csproj to SDK-style project format
4. Update NuGet package versions for SDK-style projects
5. Verify all projects build successfully

## Settings

This section contains settings and data used by execution steps.

### Project upgrade details

#### AstroGrep.csproj modifications

Project properties changes:
  - Convert from traditional .NET Framework project format to SDK-style project format
  - Ensure Target framework remains `net48`

Feature upgrades:
  - Convert traditional .csproj to modern SDK-style format for better maintainability

#### AdminProcess.csproj modifications

Project properties changes:
  - Convert from traditional .NET Framework project format to SDK-style project format
  - Ensure Target framework remains `net48`

Feature upgrades:
  - Convert traditional .csproj to modern SDK-style format for better maintainability

#### IFilterTextReader.csproj modifications

Project properties changes:
  - Convert from traditional .NET Framework project format to SDK-style project format
  - Ensure Target framework remains `net48`

Feature upgrades:
  - Convert traditional .csproj to modern SDK-style format for better maintainability

#### AstroGrep.Common.csproj modifications

NuGet packages changes:
  - System.Configuration.ConfigurationManager should be updated from `9.0.6` to `10.0.12`
  - System.ServiceModel.Primitives should be updated from `8.1.2` (replace with CoreWCF packages)
  - System.ServiceModel.Http should be updated from `8.1.2` (replace with CoreWCF packages)
  - System.ServiceModel.Duplex should be updated from `4.10.3` (replace with CoreWCF packages)
  - System.ServiceModel.NetTcp should be updated from `8.1.2` (replace with CoreWCF packages)
  - System.ServiceModel.Security should be updated from `4.10.3` (replace with CoreWCF packages)

#### libAstroGrep.csproj modifications

NuGet packages changes:
  - System.Configuration.ConfigurationManager should be updated from `9.0.6` to `10.0.12`
  - System.ServiceModel.Primitives should be updated from `8.1.2` (replace with CoreWCF packages)
  - System.ServiceModel.Http should be updated from `8.1.2` (replace with CoreWCF packages)
  - System.ServiceModel.Duplex should be updated from `4.10.3` (replace with CoreWCF packages)
  - System.ServiceModel.NetTcp should be updated from `8.1.2` (replace with CoreWCF packages)
  - System.ServiceModel.Security should be updated from `4.10.3` (replace with CoreWCF packages)
