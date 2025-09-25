using ModularPipelines.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace ModularPipelines.DotNet.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("tool")]
public record DotnetCustomToolOptions : DotNetOptions
{
    public DotnetCustomToolOptions(string toolName)
    {
        CommandParts = [toolName];
    }
}
