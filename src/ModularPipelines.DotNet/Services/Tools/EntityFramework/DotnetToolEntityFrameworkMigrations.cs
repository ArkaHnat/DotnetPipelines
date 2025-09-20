using System.Diagnostics.CodeAnalysis;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Json;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;
using ModularPipelines.Context;
using ModularPipelines.Models;
using Newtonsoft.Json;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;

[ExcludeFromCodeCoverage]
public class DotnetToolEntityFrameworkMigrations
{
    public DotnetToolEntityFrameworkMigrations(ICommand internalCommand)
    {
        _command = internalCommand;
    }

    private readonly ICommand _command;

    /// <summary>
    /// Adds a new migration.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual async Task<CommandResult> Add(DotNetToolEntityFrameworkMigrationsAddOptions options)
    {
        var cmdResult = await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkMigrationsAddOptions());
        return cmdResult;
    }

    /// <summary>
    /// Removes the last migration, rolling back the code changes that were done for the latest migration.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual async Task<CommandResult> Remove(DotNetToolEntityFrameworkMigrationsRemoveOptions options)
    {
        var cmdResult = await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkMigrationsRemoveOptions());
        return cmdResult;
    }

    /// <summary>
    /// Lists available migrations.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual async Task<List<DotnetEfMigrationsListElement>> List(DotNetToolEntityFrameworkMigrationsListOptions options)
    {
        var cmdResult = await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkMigrationsListOptions());
        try
		{
			var sanitizedOutput = JsonStringSanitizer.SanitizeOutput(cmdResult.StandardOutput);
			var deserializedOutput = JsonConvert.DeserializeObject<List<DotnetEfMigrationsListElement>>(sanitizedOutput);
            return deserializedOutput;
        }
        catch (Exception ex)
        {
            ex.Data["CmdStandardOutput"] = cmdResult.StandardOutput;
            ex.Data["CmdStandardError"] = cmdResult.StandardError;
            ex.Data["CommandInput"] = cmdResult.CommandInput;
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual async Task<CommandResult> Bundle(DotNetToolEntityFrameworkMigrationsBundleOptions options)
    {
        var cmdResult = await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkMigrationsBundleOptions());
        return cmdResult;
    }

    /// <summary>
    /// Checks if any changes have been made to the model since the last migration.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual async Task<CommandResult> HasPendingMigrations(DotNetToolEntityFrameworkMigrationsHasPendingMigrationsOptions options)
    {
        var cmdResult = await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkMigrationsHasPendingMigrationsOptions());
        return cmdResult;
    }
}
