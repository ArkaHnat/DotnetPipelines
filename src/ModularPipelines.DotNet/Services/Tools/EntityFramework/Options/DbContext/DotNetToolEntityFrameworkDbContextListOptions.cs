using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
/// <summary>
/// Lists available DbContext types.
/// </summary>
public record DotNetToolEntityFrameworkDbContextListOptions : DotnetToolEntityFrameworkToolRunEFDbContextOptions
{
    public DotNetToolEntityFrameworkDbContextListOptions()
    {
        CommandParts.Add("list");
    }
}