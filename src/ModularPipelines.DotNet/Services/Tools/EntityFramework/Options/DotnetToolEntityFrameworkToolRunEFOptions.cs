using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options;

[CommandPrecedingArguments("dotnet-ef")]
public record DotnetToolEntityFrameworkToolRunEFOptions : DotnetToolEntityFrameworkToolRunOptions
{
    public DotnetToolEntityFrameworkToolRunEFOptions() : base()
    {
    }
}
