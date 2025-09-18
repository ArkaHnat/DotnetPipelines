using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options;

public record DotnetToolEntityFrameworkToolOptions : DotnetToolEntityFrameworkOptions
{
    public DotnetToolEntityFrameworkToolOptions() : base()
    {
        CommandParts = ["tool"];
    }
}
