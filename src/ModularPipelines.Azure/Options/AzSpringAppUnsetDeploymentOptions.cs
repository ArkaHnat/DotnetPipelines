using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("spring", "app", "unset-deployment")]
public record AzSpringAppUnsetDeploymentOptions(
[property: CommandSwitch("--name")] string Name,
[property: CommandSwitch("--resource-group")] string ResourceGroup,
[property: CommandSwitch("--service")] string Service
) : AzOptions
{
    [BooleanCommandSwitch("--no-wait")]
    public bool? NoWait { get; set; }
}