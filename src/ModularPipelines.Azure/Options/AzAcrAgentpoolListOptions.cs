using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("acr", "agentpool", "list")]
public record AzAcrAgentpoolListOptions(
[property: CommandSwitch("--registry")] string Registry
) : AzOptions
{
    [CommandSwitch("--resource-group")]
    public string? ResourceGroup { get; set; }
}