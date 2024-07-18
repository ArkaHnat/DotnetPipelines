using System.Diagnostics.CodeAnalysis;

namespace ModularPipelines.DotNet.Options;

[ExcludeFromCodeCoverage]
public record DotnetToolRestoreOptions : DotNetOptions
{
    public DotnetToolRestoreOptions()
    {
        CommandParts = ["tool", "restore"];
    }
}
