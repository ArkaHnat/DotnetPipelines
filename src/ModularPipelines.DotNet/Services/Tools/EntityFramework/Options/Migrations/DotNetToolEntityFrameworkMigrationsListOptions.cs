using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;
/// <summary>
/// Lists available migrations.
/// </summary>
/// 
[CommandPrecedingArguments("list")]
public record DotNetToolEntityFrameworkMigrationsListOptions : DotNetToolEntityFrameworkMigrationsOptions
{
	public DotNetToolEntityFrameworkMigrationsListOptions() : base()
	{
	}

	/// <summary>
	/// Gets or sets the connection string to the database. Defaults to the one specified in AddDbContext or OnConfiguring.
	/// </summary>
	[CommandSwitch("--connection")]
	public virtual string? Connection { get; set; }

	/// <summary>
	/// Gets or sets don't connect to the database.
	/// </summary>
	[BooleanCommandSwitch("--no-connect")]
	public virtual bool? NoConnect { get; set; }
}