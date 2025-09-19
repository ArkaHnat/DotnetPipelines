using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;
/// <summary>
/// Removes the last migration, rolling back the code changes that were done for the latest migration.
/// </summary>
public record DotNetToolEntityFrameworkMigrationsRemoveOptions : DotNetToolEntityFrameworkMigrationsOptions
{
    public DotNetToolEntityFrameworkMigrationsRemoveOptions() : base()
    {
        CommandParts = ["remove"];
    }

    /// <summary>
    /// Gets or sets revert the latest migration, rolling back both code and database changes that were done for the latest migration. Continues to roll back only the code changes if an error occurs while connecting to the database.
    /// </summary>
    [BooleanCommandSwitch("--force")]
    public virtual bool? NoConnect { get; set; }
}