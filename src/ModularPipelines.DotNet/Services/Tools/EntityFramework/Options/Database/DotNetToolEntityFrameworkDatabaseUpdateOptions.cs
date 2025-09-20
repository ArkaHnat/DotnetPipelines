using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Database;
/// <summary>
/// Updates the database to the last migration or to a specified migration.
/// </summary>
[CommandPrecedingArguments("update")]
public record DotNetToolEntityFrameworkDatabaseUpdateOptions : DotnetToolEntityFrameworkToolRunEFDatabaseOptions
{
	public DotNetToolEntityFrameworkDatabaseUpdateOptions() : base()
	{
		CommandParts = ["<MIGRATION>"];
	}

	/// <summary>
	/// Gets or sets the target migration. Migrations may be identified by name or by ID. The number 0 is a special case that means before the first migration and causes all migrations to be reverted. If no migration is specified, the command defaults to the last migration.
	/// </summary>
	[PositionalArgument(PlaceholderName = "<MIGRATION>")]
	public string? Migration { get; set; }
}