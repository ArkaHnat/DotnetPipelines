using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Google.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("compute", "url-maps", "describe")]
public record GcloudComputeUrlMapsDescribeOptions(
[property: PositionalArgument] string UrlMap
) : GcloudOptions
{
    [BooleanCommandSwitch("--global")]
    public bool? Global { get; set; }

    [CommandSwitch("--region")]
    public string? Region { get; set; }
}