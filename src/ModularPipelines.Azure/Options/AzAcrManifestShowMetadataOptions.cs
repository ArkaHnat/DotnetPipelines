using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("acr", "manifest", "show-metadata")]
public record AzAcrManifestShowMetadataOptions : AzOptions
{
    [CommandSwitch("--name")]
    public string? Name { get; set; }

    [CommandSwitch("--password")]
    public string? Password { get; set; }

    [CommandSwitch("--registry")]
    public string? Registry { get; set; }

    [CommandSwitch("--suffix")]
    public string? Suffix { get; set; }

    [CommandSwitch("--username")]
    public string? Username { get; set; }

    [PositionalArgument(Position = Position.AfterSwitches)]
    public string? MANIFESTID { get; set; }
}