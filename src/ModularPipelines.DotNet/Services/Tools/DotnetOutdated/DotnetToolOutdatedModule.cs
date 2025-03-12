using ModularPipelines.Context;
using ModularPipelines.DotNet.Options;
using ModularPipelines.Models;
using System.Diagnostics.CodeAnalysis;

namespace ModularPipelines.DotNet.Services.Tools.DotnetOutdated;

[ExcludeFromCodeCoverage]
public class DotNetToolOutdated
{
	public DotNetToolOutdated(ICommand internalCommand)
	{
		_command = internalCommand;
	}

	private readonly ICommand _command;

	public async Task<CommandResult> InstallToolAsync(DotNetToolOutdatedInstallOptions options, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options, token);
	}

	public async Task<CommandResult> Run(DotnetToolOutdatedRunOptions options, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options, token);
	}

}