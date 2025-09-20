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
		try
		{
			var sanitizedOutput = JsonStringSanitizer.SanitizeOutput(cmdResult.StandardOutput);
			var deserializedOutput = JsonConvert.DeserializeObject<List<DotnetEfDbContextListElement>>(sanitizedOutput);
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
		var sanitizedOutput = JsonStringSanitizer.SanitizeOutput(cmdResult.StandardOutput);
		var deserializedOutput = JsonConvert.DeserializeObject<DotnetEfDbContextInfoElement>(sanitizedOutput);
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

	public virtual async Task<CommandResult> Script(DotNetToolEntityFrameworkDbContextScriptOptions options)
	{
		return await _command.ExecuteCommandLineTool(options ?? new DotNetToolEntityFrameworkDbContextScriptOptions());
	}
}
