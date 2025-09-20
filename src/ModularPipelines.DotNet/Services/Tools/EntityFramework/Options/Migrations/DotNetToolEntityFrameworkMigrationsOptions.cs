using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;

/// <summary>
/// 
/// </summary>

[CommandPrecedingArguments("migrations")]
public record DotNetToolEntityFrameworkMigrationsOptions : DotnetToolEntityFrameworkToolRunEFOptions
{
	public DotNetToolEntityFrameworkMigrationsOptions() : base()
	{
	}

	/// <summary>
	///     Gets or sets the file to write the result to.
	/// </summary>
	[CommandSwitch("--output ")]
	public virtual string? Output { get; set; }
}