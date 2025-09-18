using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options;

public record DotnetToolEntityFrameworkToolRunEFMigrationsOptions : DotnetToolEntityFrameworkToolRunEFOptions
{
    public DotnetToolEntityFrameworkToolRunEFMigrationsOptions() : base()
    {
        CommandParts.Add("migrations");
    }
}