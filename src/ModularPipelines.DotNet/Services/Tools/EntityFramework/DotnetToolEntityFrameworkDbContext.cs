using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Json;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;
using ModularPipelines.Context;
using ModularPipelines.Models;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;

[ExcludeFromCodeCoverage]
public class DotnetToolEntityFrameworkDbContext
{
    public DotnetToolEntityFrameworkDbContext(ICommand internalCommand)
    {
        _command = internalCommand;
    }

    private readonly ICommand _command;

    public async Task<List<DotnetEfDbContextListElement>> List(DotNetToolEntityFrameworkDbContextListOptions options)
    {
        var cmdResult = await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextListOptions());
        try
        {
            var sanitizedOutput = JsonHelpers.SanitizeString(cmdResult.StandardOutput);
            var deserializedOutput = JsonConvert.DeserializeObject<List<DotnetEfDbContextListElement>>(sanitizedOutput, JsonHelpers.JsonSerialiazerSettings);
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

    public async Task<DotnetEfDbContextInfoElement> Info(DotNetToolEntityFrameworkDbContextInfoOptions options)
    {
        var cmdResult = await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextInfoOptions());
        var sanitizedOutput = JsonHelpers.SanitizeString(cmdResult.StandardOutput);
        var deserializedOutput = JsonConvert.DeserializeObject<DotnetEfDbContextInfoElement>(sanitizedOutput, JsonHelpers.JsonSerialiazerSettings);
        return deserializedOutput;
    }

    public virtual async Task<CommandResult> Optimize(DotNetToolEntityFrameworkDbContextOptimizeOptions options)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextOptimizeOptions());
    }

    public virtual async Task<CommandResult> Scaffold(DotNetToolEntityFrameworkDbContextListOptions options)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextListOptions());
    }

    public virtual async Task<CommandResult> Script(DotNetToolEntityFrameworkMigrationsScriptOptions options)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkMigrationsScriptOptions());
    }
}
