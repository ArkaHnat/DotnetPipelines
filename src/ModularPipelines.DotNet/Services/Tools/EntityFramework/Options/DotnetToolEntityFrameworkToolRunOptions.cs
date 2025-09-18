using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options;

public record DotnetToolEntityFrameworkToolRunOptions : DotnetToolEntityFrameworkToolOptions
{
    public DotnetToolEntityFrameworkToolRunOptions() : base()
    {
        CommandParts.Add("run");
    }
}
