using DotnetModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using DotnetModularPipelines.Git.Options;
using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

[DependsOn<GitCommitDotnetToolUpdates>]
[ResolveDependencies]
public class DotNetToolOutdatedModule : Module<CommandResult>
{
	public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

	/// <inheritdoc/>
	protected override async Task<CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
	{
		_ = Directory.GetCurrentDirectory();

		var options = new DotnetToolOutdatedRunOptions(context.Git().RootDirectory / "ModularPipelines.Merged.sln")
		{
			UpdateOnlySinglePackage = true,
			OutputFilename = context.Git().RootDirectory / "_buildOutput/dotnet-outdated-output.json",
			OutputFormat = DotnetToolUpdateOutputFormat.Json,
			Upgrade = true,

		};
		var result = await context.DotNet().Tool.DotnetOutdated.Run(options);

		var status = await context.Git().Commands.Add(new GitAddOptions()
		{
			FilePattern = context.Git().RootDirectory / "Directory.Packages.Props",
		});



		var commit = await context.Git().Commands.Commit(new Git.Options.GitCommitOptions()
		{
			Message = $"chore(deps): Updated dotnet tool [{updatedTool.Value.Value.packageId}] to version [{updatedTool.Value.Value.version}]",
		});
		return result;
	}

	protected override async Task<SkipDecision> ShouldSkip(IPipelineContext context)
	{
		var result = await GetModule<DotnetToolUpdateModule>();
		if (string.IsNullOrWhiteSpace(result.Value.Value.toVersion))
		{
			return SkipDecision.DoNotSkip;
		}
		else
		{
			return SkipDecision.Skip("DotnetToolUpdateModule already updated files");
		}
	}

	/// <inheritdoc/>
	protected override async Task OnAfterExecute(IPipelineContext context)
	{
		_ = await this;
		context.Logger.LogInformation("Restored dotnet tools.");
	}
}

public class DotnetOutdatedToolOutput
{
	public Project[] Projects { get; set; }
}

public class Project
{
	public string Name { get; set; }
	public string FilePath { get; set; }
	public Targetframework[] TargetFrameworks { get; set; }
}

public class Targetframework
{
	public string Name { get; set; }
	public Dependency[] Dependencies { get; set; }
}

public class Dependency
{
	public string Name { get; set; }
	public string ResolvedVersion { get; set; }
	public string LatestVersion { get; set; }
	public string UpgradeSeverity { get; set; }
}
