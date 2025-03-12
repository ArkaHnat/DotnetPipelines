using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Options;
using ModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using ModularPipelines.DotNet.Services.Tools.SonarScanner;
using ModularPipelines.Models;

namespace ModularPipelines.DotNet.Services;

[ExcludeFromCodeCoverage]
public class DotNetTool
{
    public DotNetTool(ICommand internalCommand, DotNetToolSonarScanner dotNetToolSonarScanner, DotNetToolOutdated dotnetToolOutdated)
    {
        _command = internalCommand;
		this.dotNetToolSonarScanner = dotNetToolSonarScanner;
        this.dotnetToolOutdated = dotnetToolOutdated;
	}

    private readonly ICommand _command;

    private DotNetToolSonarScanner dotNetToolSonarScanner;

	private DotNetToolOutdated dotnetToolOutdated;
	public async Task<CommandResult> Install(DotNetToolInstallOptions options, CancellationToken token = default)
    {
        return await _command.ExecuteCommandLineTool(options, token);
    }

    public async Task<CommandResult> List(DotNetToolListOptions? options = default, CancellationToken token = default)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotNetToolListOptions(), token);
    }

    public async Task<CommandResult> Restore(DotnetToolRestoreOptions? options = default, CancellationToken token = default)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotnetToolRestoreOptions(), token);
    }

    public async Task<CommandResult> Custom(DotnetCustomToolOptions? options = default, CancellationToken token = default)
    {
        return await _command.ExecuteCommandLineTool(options ?? new DotnetCustomToolOptions(string.Empty), token);
    }

    public async Task<CommandResult> Update(DotNetToolUpdateOptions options, CancellationToken token = default)
    {
        return await _command.ExecuteCommandLineTool(options, token);
    }

    public async Task<CommandResult> Uninstall(DotNetToolUninstallOptions options, CancellationToken token = default)
    {
        return await _command.ExecuteCommandLineTool(options, token);
    }

    public async Task<CommandResult> Search(DotNetToolSearchOptions options, CancellationToken token = default)
    {
        return await _command.ExecuteCommandLineTool(options, token);
    }

	public DotNetToolSonarScanner SonarCubeScanner => dotNetToolSonarScanner;

	public DotNetToolOutdated DotnetOutdated => dotnetToolOutdated;

}
