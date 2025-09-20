using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Database;
using ModularPipelines.Context;
using ModularPipelines.Models;
using System.Diagnostics.CodeAnalysis;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;

[ExcludeFromCodeCoverage]
public class DotnetToolEntityFrameworkDatabase
{
    public DotnetToolEntityFrameworkDatabase(ICommand internalCommand)
    {
        _command = internalCommand;
    }

    private readonly ICommand _command;

    public virtual async Task<CommandResult> Drop(DotNetToolEntityFrameworkDatabaseDropOptions options)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDatabaseDropOptions());
    }

    public virtual async Task<CommandResult> Update(DotNetToolEntityFrameworkDatabaseUpdateOptions options)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDatabaseUpdateOptions());
    }
}
