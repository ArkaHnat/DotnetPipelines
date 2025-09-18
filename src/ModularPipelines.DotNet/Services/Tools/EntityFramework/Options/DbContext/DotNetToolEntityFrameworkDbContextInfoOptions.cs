using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
/// <summary>
/// Gets information about a DbContext type.
/// </summary>
public record DotNetToolEntityFrameworkDbContextInfoOptions : DotnetToolEntityFrameworkToolRunEFDbContextOptions
{
    public DotNetToolEntityFrameworkDbContextInfoOptions() : base()
    {
        CommandParts.Add("Info");
    }
}