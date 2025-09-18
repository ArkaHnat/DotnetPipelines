namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;
/// <summary>
/// Checks if any changes have been made to the model since the last migration.
/// </summary>
public record DotNetToolEntityFrameworkMigrationsHasPendingMigrationsOptions : DotNetToolEntityFrameworkMigrationsOptions
{
	public DotNetToolEntityFrameworkMigrationsHasPendingMigrationsOptions() : base()
	{
		CommandParts = ["has-pending-model-changes"];
	}
}