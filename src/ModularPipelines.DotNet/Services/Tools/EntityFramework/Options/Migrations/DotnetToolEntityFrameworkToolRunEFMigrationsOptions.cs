using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;

[CommandPrecedingArguments("migrations")]
public record DotnetToolEntityFrameworkToolRunEFMigrationsOptions : DotnetToolEntityFrameworkToolRunEFOptions
{
    public DotnetToolEntityFrameworkToolRunEFMigrationsOptions() : base()
    {
    }
}