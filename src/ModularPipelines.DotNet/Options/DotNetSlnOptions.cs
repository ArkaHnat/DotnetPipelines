using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Options;
using System.Diagnostics.CodeAnalysis;


namespace DotnetModularPipelines.DotNet.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("sln")]
public record DotNetSlnOptions : DotNetOptions
{
}
