using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options;

public record DotnetToolEntityFrameworkToolRunEFDbContextOptions : DotnetToolEntityFrameworkToolRunEFOptions
{
    public DotnetToolEntityFrameworkToolRunEFDbContextOptions() : base()
    {
        CommandParts.Add("dbContext");
    }
}
