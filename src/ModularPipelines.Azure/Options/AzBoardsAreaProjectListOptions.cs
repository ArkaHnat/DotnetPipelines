using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("boards", "area", "project", "list")]
public record AzBoardsAreaProjectListOptions : AzOptions
{
    [CommandSwitch("--depth")]
    public string? Depth { get; set; }

    [BooleanCommandSwitch("--detect")]
    public bool? Detect { get; set; }

    [CommandSwitch("--org")]
    public string? Org { get; set; }

    [CommandSwitch("--path")]
    public string? Path { get; set; }

    [CommandSwitch("--project")]
    public string? Project { get; set; }
}