using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("monitor", "data-collection", "rule", "log-analytics", "add")]
public record AzMonitorDataCollectionRuleLogAnalyticsAddOptions(
[property: CommandSwitch("--name")] string Name,
[property: CommandSwitch("--resource-id")] string ResourceId
) : AzOptions
{
    [CommandSwitch("--ids")]
    public string? Ids { get; set; }

    [CommandSwitch("--resource-group")]
    public string? ResourceGroup { get; set; }

    [CommandSwitch("--rule-name")]
    public string? RuleName { get; set; }

    [CommandSwitch("--subscription")]
    public new string? Subscription { get; set; }
}