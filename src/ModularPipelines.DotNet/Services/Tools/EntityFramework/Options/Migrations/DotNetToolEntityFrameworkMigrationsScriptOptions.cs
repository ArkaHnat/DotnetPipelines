using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;
/// <summary>
/// Generates a SQL script from migrations.
/// </summary>
public record DotNetToolEntityFrameworkMigrationsScriptOptions : DotNetToolEntityFrameworkMigrationsOptions
{
    public DotNetToolEntityFrameworkMigrationsScriptOptions() : base()
    {
        CommandParts = ["script", "<FROM>", "<TO>"];
    }

    /// <summary>
    ///     Gets or sets the starting migration. Migrations may be identified by name or by ID. The number 0 is a special case that means before the first migration. Defaults to 0.
    /// </summary>
    [PositionalArgument(PlaceholderName = "<FROM>")]
    public string? FromMigration { get; set; }

    /// <summary>
    ///     Gets or sets the ending migration. Defaults to the last migration.
    /// </summary>
    [PositionalArgument(PlaceholderName = "<TO>")]
    public string? ToMigration { get; set; }

    /// <summary>
    /// Gets or sets the file to write the script to.
    /// </summary>
    [CommandSwitch("--output")]
    public virtual string Output { get; set; }

    /// <summary>
    /// Gets or sets revert the latest migration, rolling back both code and database changes that were done for the latest migration. Continues to roll back only the code changes if an error occurs while connecting to the database.
    /// </summary>
    [BooleanCommandSwitch("--Generate a script that can be used on a database at any migration.")]
    public virtual bool? Idempotent { get; set; }

    /// <summary>
    ///     Gets or sets don't generate SQL transaction statements.
    /// </summary>
    [BooleanCommandSwitch("--no-transactions")]
    public virtual bool? NoTransactions { get; set; }
}