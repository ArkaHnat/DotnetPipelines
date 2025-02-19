using ModularPipelines.Context;
using ModularPipelines.DotNet.Options;
using ModularPipelines.Models;
using System.Diagnostics.CodeAnalysis;

namespace ModularPipelines.DotNet.Services.Tools;
[ExcludeFromCodeCoverage]
public class DotnetToolStryker
{
	public DotnetToolStryker(ICommand internalCommand)
	{
		_command = internalCommand;
	}

	private readonly ICommand _command;

	public async Task<CommandResult> Install(DotnetToolStrykerOptions options = default, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options ?? new DotnetToolStrykerOptions(IDotnetToolStryker.PackageNameConst), token);
	}
}
