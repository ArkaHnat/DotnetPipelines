using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Database;
/// <summary>
/// Deletes the database.
/// </summary>
[CommandPrecedingArguments("drop")]
public record DotNetToolEntityFrameworkDatabaseDropOptions : DotnetToolEntityFrameworkToolRunEFDatabaseOptions
{
	public DotNetToolEntityFrameworkDatabaseDropOptions() : base()
	{
	}

	/// <summary>
	///  Gets or sets overwrite existing files.
	/// </summary>
	[BooleanCommandSwitch("--force")]
	public virtual string? Force { get; set; }

	/// <summary>
	///  Gets or sets show which database would be dropped, but don't drop it.
	/// </summary>
	[BooleanCommandSwitch("--dry-run")]
	public virtual string? DryRun { get; set; }
}