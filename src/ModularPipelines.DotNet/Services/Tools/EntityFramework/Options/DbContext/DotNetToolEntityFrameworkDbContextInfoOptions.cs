using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
/// <summary>
/// Gets information about a DbContext type.
/// </summary>
[CommandPrecedingArguments("Info")]
public record DotNetToolEntityFrameworkDbContextInfoOptions : DotnetToolEntityFrameworkToolRunEFDbContextOptions
{
	public DotNetToolEntityFrameworkDbContextInfoOptions() : base()
	{
	}
}