using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("cosmosdb", "private-endpoint-connection", "approve")]
public record AzCosmosdbPrivateEndpointConnectionApproveOptions : AzOptions
{
    [CommandSwitch("--account-name")]
    public int? AccountName { get; set; }

    [CommandSwitch("--description")]
    public string? Description { get; set; }

    [CommandSwitch("--id")]
    public string? Id { get; set; }

    [CommandSwitch("--ids")]
    public string? Ids { get; set; }

    [CommandSwitch("--name")]
    public string? Name { get; set; }

    [CommandSwitch("--resource-group")]
    public string? ResourceGroup { get; set; }

    [CommandSwitch("--subscription")]
    public new string? Subscription { get; set; }
}