using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options;

public record DotnetToolEntityFrameworkToolRunEFOptions : DotnetToolEntityFrameworkToolRunOptions
{
    public DotnetToolEntityFrameworkToolRunEFOptions() : base()
    {
        CommandParts.Add(IDotnetToolEntityFrameworkConsts.PackageNameConst);
    }
}
