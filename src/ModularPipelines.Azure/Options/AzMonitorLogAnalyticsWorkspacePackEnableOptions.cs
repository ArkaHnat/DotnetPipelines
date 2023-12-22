using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("monitor", "log-analytics", "workspace", "pack", "enable")]
public record AzMonitorLogAnalyticsWorkspacePackEnableOptions(
[property: CommandSwitch("--intelligence-pack-name")] string IntelligencePackName,
[property: CommandSwitch("--resource-group")] string ResourceGroup,
[property: CommandSwitch("--workspace-name")] string WorkspaceName
) : AzOptions;