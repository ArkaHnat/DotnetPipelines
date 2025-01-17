using ModularPipelines.Context;
using ModularPipelines.DotNet.Options;
using ModularPipelines.Models;
using System.Diagnostics.CodeAnalysis;

namespace ModularPipelines.DotNet.Services.Tools;

[ExcludeFromCodeCoverage]
public class DotNetToolSonarScanner
{
	public DotNetToolSonarScanner(ICommand internalCommand)
	{
		_command = internalCommand;
	}

	private readonly ICommand _command;

	public async Task<CommandResult> InstallToolAsync(DotNetToolSonarScannerInstallOptions options, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options, token);
	}

	public async Task<CommandResult> BeginScanAsync(DotNetToolSonarScannerBeginOptions options, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options, token);
	}

	public async Task<CommandResult> EndScanAsync(DotNetToolSonarScannerEndOptions options, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options, token);
	}
}