using System.Diagnostics.CodeAnalysis;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Json;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
using ModularPipelines.Context;
using ModularPipelines.Models;
using Newtonsoft.Json;

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
        var deserializedOutput = JsonConvert.DeserializeObject<List<DotnetEfDbContextListElement>>(cmdResult.StandardOutput);
        return deserializedOutput;
    }

    public async Task<DotnetEfDbContextInfoElement> Info(DotNetToolEntityFrameworkDbContextInfoOptions options)
    {
        var cmdResult = await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextInfoOptions());
        var deserializedOutput = JsonConvert.DeserializeObject<DotnetEfDbContextInfoElement>(cmdResult.StandardOutput);
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
}
