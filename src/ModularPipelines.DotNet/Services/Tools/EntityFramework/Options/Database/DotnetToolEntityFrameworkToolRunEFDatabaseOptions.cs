using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Database;

[CommandPrecedingArguments("database")]
public record DotnetToolEntityFrameworkToolRunEFDatabaseOptions : DotnetToolEntityFrameworkToolRunEFOptions
{
	public DotnetToolEntityFrameworkToolRunEFDatabaseOptions() : base()
	{
	}
}
