using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;
using ModularPipelines.Models;

namespace ModularPipelines.Google.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("bigtable", "instances", "tables", "add-iam-policy-binding")]
public record GcloudBigtableInstancesTablesAddIamPolicyBindingOptions(
[property: PositionalArgument] string Table,
[property: PositionalArgument] string Instance,
[property: CommandSwitch("--member")] string Member,
[property: CommandSwitch("--role")] string Role
) : GcloudOptions
{
    [CommandSwitch("--condition")]
    public IEnumerable<KeyValue>? Condition { get; set; }

    [BooleanCommandSwitch("expression")]
    public bool? Expression { get; set; }

    [BooleanCommandSwitch("title")]
    public bool? Title { get; set; }

    [BooleanCommandSwitch("description")]
    public bool? Description { get; set; }

    [CommandSwitch("--condition-from-file")]
    public string? ConditionFromFile { get; set; }
}