using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("iot", "edge", "deployment", "list")]
public record AzIotEdgeDeploymentListOptions : AzOptions
{
    [CommandSwitch("--auth-type")]
    public string? AuthType { get; set; }

    [CommandSwitch("--hub-name")]
    public string? HubName { get; set; }

    [CommandSwitch("--login")]
    public string? Login { get; set; }

    [CommandSwitch("--resource-group")]
    public string? ResourceGroup { get; set; }

    [CommandSwitch("--top")]
    public string? Top { get; set; }
}