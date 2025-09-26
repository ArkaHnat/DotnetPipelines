using DotnetModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;
using DotnetModularPipelines.Git.Options;
using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;
using Newtonsoft.Json;

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
		var dotnetOutdatedOutputJson = new ModularPipelines.FileSystem.File(context.Git().RootDirectory / "_buildOutput/dotnet-outdated-output.json");
		if (dotnetOutdatedOutputJson.Exists)
		{
			dotnetOutdatedOutputJson.Delete();
		}

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
			FilePattern = context.Git().RootDirectory / "Directory.Packages.props",
		});

		if (dotnetOutdatedOutputJson.Exists)
		{
			var dotnetOutdatedOutputJsonContent = await dotnetOutdatedOutputJson.ReadAsync(cancellationToken);
			var deserializedFileContent = JsonConvert.DeserializeObject<DotnetOutdatedToolOutput>(dotnetOutdatedOutputJsonContent, JsonHelpers.JsonSerialiazerSettings);
			var upgradedProjects = deserializedFileContent.Projects
			.SelectMany(a => a.TargetFrameworks)
			.SelectMany(a => a.Dependencies)
			.Where(a => a.Upgraded);
			var numberOfChanges = upgradedProjects.Select(a => a.Name).Distinct().Count();
			switch (numberOfChanges)
			{
				case > 1:
					context.Logger.LogInformation("Upgraded more than one project. Not commiting.");
					break;
				case < 1:

					context.Logger.LogInformation("Nothing. Upgraded Not commiting.");
					break;
				case 1:
					var commit = await context.Git().Commands.Commit(new Git.Options.GitCommitOptions()
					{
						Message = $"chore(deps): Updated dotnet package [{upgradedProjects.FirstOrDefault().Name}] from version [{upgradedProjects.FirstOrDefault().ResolvedVersion}] to version [{upgradedProjects.FirstOrDefault().LatestVersion}]",
					});
					break;
			}
		}

		return result;
	}

	protected override async Task<SkipDecision> ShouldSkip(IPipelineContext context)
	{
		var result = await GetModule<DotnetToolUpdateModule>();
		return string.IsNullOrWhiteSpace(result.Value.Value.toVersion)
			? SkipDecision.DoNotSkip
			: SkipDecision.Skip("DotnetToolUpdateModule already updated files");
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
	public required Project[] Projects { get; set; }
}

public class Project
{
	public required string Name { get; set; }

	public required string FilePath { get; set; }

	public required Targetframework[] TargetFrameworks { get; set; }
}

public class Targetframework
{
	public required string Name { get; set; }

	public required Dependency[] Dependencies { get; set; }
}

public class Dependency
{
	public required string Name { get; set; }

	public required string ResolvedVersion { get; set; }

	public required string LatestVersion { get; set; }

	public required string UpgradeSeverity { get; set; }

	public bool Upgraded { get; set; }
}
