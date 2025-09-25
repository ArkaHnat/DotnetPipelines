

using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Options;
using System.Diagnostics.CodeAnalysis;

namespace DotnetModularPipelines.DotNet.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("tool")]
public record DotnetToolOptions : DotNetOptions
{
}

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("list")]
public record DotNetToolListOptions : DotnetToolOptions
{
    public DotNetToolListOptions()
    {
        CommandParts = ["[<PACKAGE_ID>]"];
    }

    public DotNetToolListOptions(
        string packageId
    )
    {
        CommandParts = ["[<PACKAGE_ID>]"];

        PackageId = packageId;
    }

    [BooleanCommandSwitch("--global")]
    public virtual bool? Global { get; set; }

    [BooleanCommandSwitch("--tool-path")]
    public virtual bool? ToolPath { get; set; }

    [BooleanCommandSwitch("--local")]
    public virtual bool? Local { get; set; }

    [PositionalArgument(PlaceholderName = "[<PACKAGE_ID>]")]
    public string? PackageId { get; set; }

    [CommandSwitch("--format")]
    public virtual DotnetToolListFormat? Format { get; set; }

    public enum DotnetToolListFormat
    {
        Json,
        Table,
    }
}
