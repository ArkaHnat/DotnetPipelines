using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Google.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("network-services", "gateways", "import")]
public record GcloudNetworkServicesGatewaysImportOptions(
[property: PositionalArgument] string Gateway,
[property: PositionalArgument] string Location
) : GcloudOptions
{
    [BooleanCommandSwitch("--async")]
    public bool? Async { get; set; }

    [CommandSwitch("--source")]
    public string? Source { get; set; }
}