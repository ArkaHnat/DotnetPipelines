using System.Diagnostics.CodeAnalysis;

namespace ModularPipelines.DotNet.Options;

[ExcludeFromCodeCoverage]
public record DotnetCustomToolOptions : DotNetOptions
{
    public DotnetCustomToolOptions(string toolName)
    {
        CommandParts = ["tool", toolName];
    }
}
