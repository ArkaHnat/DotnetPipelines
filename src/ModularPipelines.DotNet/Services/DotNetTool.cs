using System.Diagnostics.CodeAnalysis;
using DotnetModularPipelines.DotNet.Options;
using DotnetModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Options;
using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;
using ModularPipelines.DotNet.Services.Tools.SonarScanner;
using ModularPipelines.Models;
using Newtonsoft.Json;

namespace ModularPipelines.DotNet.Services;

[ExcludeFromCodeCoverage]
public class DotNetTool
{
	public DotNetTool(ICommand internalCommand, DotNetToolSonarScanner dotNetToolSonarScanner, DotNetToolOutdated dotnetToolOutdated, DotnetToolEntityFramework dotnetToolEntityFramework)
	{
		_command = internalCommand;
		this.dotNetToolSonarScanner = dotNetToolSonarScanner;
		this.dotnetToolOutdated = dotnetToolOutdated;
		this.dotnetToolEntityFramework = dotnetToolEntityFramework;
	}

	private readonly ICommand _command;

	private DotNetToolSonarScanner dotNetToolSonarScanner;

	private DotNetToolOutdated dotnetToolOutdated;
	private DotnetToolEntityFramework dotnetToolEntityFramework;

	public virtual async Task<CommandResult> Install(DotNetToolInstallOptions options, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options, token);
	}

	public virtual async Task<DotnetToolListResult> List(DotNetToolListOptions? options = default, CancellationToken token = default)
	{
		var resultString = await _command.ExecuteCommandLineTool(options ?? new DotNetToolListOptions(), token);
		var result = JsonConvert.DeserializeObject<DotnetToolListResult>(resultString.StandardOutput, JsonHelpers.JsonSerialiazerSettings);

		return result;

	}

	public virtual async Task<CommandResult> Update(DotNetToolUpdateOptions options, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options ?? new DotNetToolUpdateOptions(string.Empty, string.Empty), token);
	}
	public virtual async Task<CommandResult> Restore(DotnetToolRestoreOptions? options = default, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options ?? new DotnetToolRestoreOptions(), token);
	}
	public virtual async Task<CommandResult> Custom(DotnetCustomToolOptions? options = default, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options ?? new DotnetCustomToolOptions(string.Empty), token);
	}

	public virtual async Task<CommandResult> Uninstall(DotNetToolUninstallOptions options, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options, token);
	}

	public virtual async Task<CommandResult> Search(DotNetToolSearchOptions options, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options, token);
	}

	public DotNetToolSonarScanner SonarCubeScanner => dotNetToolSonarScanner;

	public DotNetToolOutdated DotnetOutdated => dotnetToolOutdated;

	public DotnetToolEntityFramework EntityFramework => dotnetToolEntityFramework;

}

public class DotnetToolListResult
{
	public int version { get; set; }
	public Datum[] data { get; set; }
}

public class Datum
{
	public string manifest { get; set; }
	public string packageId { get; set; }
	public string version { get; set; }
	public string[] commands { get; set; }
}
