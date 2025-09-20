using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
/// <summary>
/// Lists available DbContext types.
/// </summary>
[CommandPrecedingArguments("list")]
public record DotNetToolEntityFrameworkDbContextListOptions : DotnetToolEntityFrameworkToolRunEFDbContextOptions
{
	public DotNetToolEntityFrameworkDbContextListOptions()
	{
	}
}