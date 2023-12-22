using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Node.Models;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("token", "list")]
public record NpmTokenListOptions : NpmOptions
{
    [BooleanCommandSwitch("--read-only")]
    public bool? ReadOnly { get; set; }

    [CommandSwitch("--cidr")]
    public string? Cidr { get; set; }

    [CommandSwitch("--registry")]
    public Uri? Registry { get; set; }

    [CommandSwitch("--otp")]
    public string? Otp { get; set; }
}