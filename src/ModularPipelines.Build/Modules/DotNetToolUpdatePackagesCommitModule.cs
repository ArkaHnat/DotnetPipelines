using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;
using DotnetModularPipelines.Git.Options;
using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Build.Model;
using ModularPipelines.Context;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;
using Newtonsoft.Json;

namespace ModularPipelines.Build.Modules;

[DependsOn<DotNetToolUpdatePackagesModule>]
[ResolveDependencies]
public class DotNetToolUpdatePackagesCommitModule : Module<CommandResult>
{
	private (string Name, string fromVersion, string toVersion) upgradedProjectInfo = default;

	public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

	/// <inheritdoc/>
	protected override async Task<CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
	{
		_ = Directory.GetCurrentDirectory();
		_ = await context.Git().Commands.Add(new GitAddOptions()
		{
			FilePattern = context.Git().RootDirectory / "Directory.Packages.props",
		});
		var commit = await context.Git().Commands.Commit(new Git.Options.GitCommitOptions()
		{
			Message = $"chore(deps): Updated dotnet package [{upgradedProjectInfo.Name}] from version [{upgradedProjectInfo.fromVersion}] to version [{upgradedProjectInfo.toVersion}]",
		});
		return commit;
	}

	protected override async Task<SkipDecision> ShouldSkip(IPipelineContext context)
	{
		var dotnetOutdatedOutputJson = new FileSystem.File(context.Git().RootDirectory / "_buildOutput/dotnet-outdated-output.json");

		if (dotnetOutdatedOutputJson.Exists)
		{
			var dotnetOutdatedOutputJsonContent = await dotnetOutdatedOutputJson.ReadAsync();
			var deserializedFileContent = JsonConvert.DeserializeObject<DotnetOutdatedToolOutput>(dotnetOutdatedOutputJsonContent, JsonHelpers.JsonSerialiazerSettings);
			var updatedProjects = deserializedFileContent.Projects
				.SelectMany(a => a.TargetFrameworks)
				.SelectMany(a => a.Dependencies)
				.Where(a => a.Upgraded);
			var leftToUpdate = deserializedFileContent.Projects
				.SelectMany(a => a.TargetFrameworks)
				.SelectMany(a => a.Dependencies)
				.Where(a => !a.Upgraded);
			if (leftToUpdate.Any())
			{
				context.Logger.LogInformation(@$"There are [{leftToUpdate.Count()}] to update");
			}

			var numberOfChanges = updatedProjects.Select(a => a.Name).Distinct().Count();
			switch (numberOfChanges)
			{
				case > 1:
					return SkipDecision.Skip("Upgraded more than one project. Not commiting.");
				case < 1:
					return SkipDecision.Skip("Nothing. Upgraded Not commiting.");
				case 1:
					upgradedProjectInfo.Name = updatedProjects.FirstOrDefault()!.Name;
					upgradedProjectInfo.fromVersion = updatedProjects.FirstOrDefault()!.ResolvedVersion;
					upgradedProjectInfo.toVersion = updatedProjects.FirstOrDefault()!.LatestVersion;
					return SkipDecision.DoNotSkip;
			}
		}

		return SkipDecision.Skip("No output from dotnet outdated tool");
	}

	/// <inheritdoc/>
	protected override async Task OnAfterExecute(IPipelineContext context)
	{
		_ = await this;
		context.Logger.LogInformation("Restored dotnet tools.");
	}
}
