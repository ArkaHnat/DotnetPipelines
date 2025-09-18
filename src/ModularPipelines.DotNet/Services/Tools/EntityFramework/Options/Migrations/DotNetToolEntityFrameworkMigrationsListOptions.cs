using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;
/// <summary>
/// Lists available migrations.
/// </summary>
public record DotNetToolEntityFrameworkMigrationsListOptions : DotNetToolEntityFrameworkMigrationsOptions
{
    public DotNetToolEntityFrameworkMigrationsListOptions() : base()
    {
        CommandParts = ["list"];
    }

	/// <summary>
	/// The connection string to the database. Defaults to the one specified in AddDbContext or OnConfiguring.
	/// </summary>
	[CommandSwitch("--connection")]
    public virtual string? connection { get; set; }


	/// <summary>
	/// Don't connect to the database.
	/// </summary>
	[BooleanCommandSwitch("--no-connect")]
    public virtual bool? NoConnect { get; set; }
}