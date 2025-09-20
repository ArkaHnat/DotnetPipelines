using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options;

[CommandPrecedingArguments("tool")]
public record DotnetToolEntityFrameworkToolOptions : DotnetToolEntityFrameworkOptions
{
    public DotnetToolEntityFrameworkToolOptions() : base()
    {
    }
}
