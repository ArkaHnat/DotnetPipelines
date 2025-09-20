using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options;

[CommandPrecedingArguments("run")]
public record DotnetToolEntityFrameworkToolRunOptions : DotnetToolEntityFrameworkToolOptions
{
    public DotnetToolEntityFrameworkToolRunOptions() : base()
    {
    }
}
