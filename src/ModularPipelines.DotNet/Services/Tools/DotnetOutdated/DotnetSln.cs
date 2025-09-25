using DotnetModularPipelines.DotNet.Options;
using ModularPipelines.Context;
using ModularPipelines.Models;
using System.Diagnostics.CodeAnalysis;

namespace DotnetModularPipelines.DotNet.Services.Tools.DotnetOutdated;

[ExcludeFromCodeCoverage]
public class DotnetSln
{
    public DotnetSln(ICommand internalCommand)
    {
        _command = internalCommand;
    }

    private readonly ICommand _command;

    public virtual async Task<CommandResult> List(DotNetSlnListOptions? options = default, CancellationToken token = default)
    {
        var result = await _command.ExecuteCommandLineTool(options ?? new DotNetSlnListOptions(), token);
        return result;
    }
}