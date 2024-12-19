using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Build.Attributes;
using ModularPipelines.Context;
using ModularPipelines.FileSystem;
using ModularPipelines.Git.Extensions;
using ModularPipelines.GitHub.Attributes;
using ModularPipelines.Modules;
using ModularPipelines.Options;
using File = ModularPipelines.FileSystem.File;

namespace ModularPipelines.Build.Modules;

//[SkipIfNoGitHubToken]
//[SkipIfNoStandardGitHubToken]
[DependsOn<DownloadCodeCoverageFromOtherOperatingSystemBuildsModule>]
[DependsOn<RunUnitTestsModule>]
[ResolveDependencies]
public class MergeCoverageModule : Module<File>
{
	/// <inheritdoc/>
	protected override async Task<File?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
	{
		var coverageFilesFromThisRun = context.Git().RootDirectory
			.GetFiles(x => x.Name.Contains("cobertura") && x.Extension is ".xml").GroupBy(a => a.Folder)
			.Select(group => group.OrderByDescending(file => file.CreationTime).First())
			.ToList();

		var coverageFilesFromOtherSystems = await GetModule<DownloadCodeCoverageFromOtherOperatingSystemBuildsModule>();

		List<File> coverageFiles = new();
		if (!coverageFilesFromOtherSystems.SkipDecision.ShouldSkip)
		{
			if (coverageFilesFromOtherSystems.Value?.Count is null or < 1)
			{
				context.Logger.LogInformation("No code coverage found from other operating systems");
				return null;
			}
			coverageFiles = coverageFilesFromThisRun
			   .Concat(coverageFilesFromOtherSystems.Value ?? new())
			   .Distinct()
			   .ToList();
		}
		else
		{
			coverageFiles.AddRange(coverageFilesFromThisRun);
		}

		var outputPath = context.Git().RootDirectory / "_buildOutput/cobertura.xml";

		await context.Command.ExecuteCommandLineTool(new CommandLineToolOptions("dotnet-coverage")
		{
			Arguments = new[] { "merge", "--remove-input-files", "--output-format", "cobertura", "--output", outputPath.Path }.Concat(coverageFiles.Select(x => x.Path)),
		}, cancellationToken);

		return outputPath.Path;
	}
}