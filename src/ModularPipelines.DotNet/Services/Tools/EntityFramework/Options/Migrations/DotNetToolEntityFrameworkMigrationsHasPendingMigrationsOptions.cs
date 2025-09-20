using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;
/// <summary>
/// Checks if any changes have been made to the model since the last migration.
/// </summary>
/// 

[CommandPrecedingArguments("has-pending-model-changes")]
public record DotNetToolEntityFrameworkMigrationsHasPendingMigrationsOptions : DotNetToolEntityFrameworkMigrationsOptions
{
    public DotNetToolEntityFrameworkMigrationsHasPendingMigrationsOptions() : base()
    {
    }
}