using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;

[CommandPrecedingArguments("dbContext")]
public record DotnetToolEntityFrameworkToolRunEFDbContextOptions : DotnetToolEntityFrameworkToolRunEFOptions
{
	public DotnetToolEntityFrameworkToolRunEFDbContextOptions() : base()
	{
	}
}
