using ModularPipelines.Context;
using ModularPipelines.DotNet.Options;
using ModularPipelines.DotNet.Services.Tools.SonarScanner;
using ModularPipelines.Models;
using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Core.Tokens;

namespace ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;
[ExcludeFromCodeCoverage]
public class DotnetToolEntityFramework
{
	public DotnetToolEntityFramework(ICommand internalCommand)
	{
		_command = internalCommand;
	}

	private readonly ICommand _command;

	public async Task<CommandResult> Install(DotnetToolEntityFrameworkOptions options = default, CancellationToken token = default)
	{
		return await _command.ExecuteCommandLineTool(options ?? new DotnetToolEntityFrameworkOptions(IDotnetToolEntityFramework.PackageNameConst), token);
	}
	public DotnetToolEntityFrameworkDbContext DbContext()
    {
		return new DotnetToolEntityFrameworkDbContext(_command);
    }
	
}

[ExcludeFromCodeCoverage]
public class DotnetToolEntityFrameworkDbContext
{
	public DotnetToolEntityFrameworkDbContext(ICommand internalCommand)
	{
		_command = internalCommand;
	}

	private readonly ICommand _command;


	public async Task<CommandResult> List()
    {
		return await _command.ExecuteCommandLineTool(new DotNetToolEntityFrameworkDbContextListOptions());
    }
}
