using System.Diagnostics.CodeAnalysis;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Json;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
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
    public virtual async Task<CommandResult> Add(DotNetToolEntityFrameworkDbContextListOptions options)
    {
        var cmdResult = await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextListOptions());
        return cmdResult;
    }

    /// <summary>
    /// Creates an executable to update the database.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual async Task<CommandResult> Bundle(DotNetToolEntityFrameworkDbContextInfoOptions options)
    {
        var cmdResult = await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextInfoOptions());
        return cmdResult;
    }

    /// <summary>
    /// Checks if any changes have been made to the model since the last migration.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual async Task<CommandResult> HasPendingModelChanges(DotNetToolEntityFrameworkDbContextOptimizeOptions options)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextOptimizeOptions());
    }

    /// <summary>
    /// Lists available migrations.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual async Task<CommandResult> List(DotNetToolEntityFrameworkDbContextListOptions options)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextListOptions());
    }

    /// <summary>
    /// Removes the last migration, rolling back the code changes that were done for the latest migration.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual async Task<CommandResult> Remove(DotNetToolEntityFrameworkDbContextListOptions options)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextListOptions());
    }

    /// <summary>
    /// Generates a SQL script from migrations.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public virtual async Task<CommandResult> Script(DotNetToolEntityFrameworkDbContextListOptions options)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextListOptions());
    }
}
