using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Modules;
using ModularPipelines.Options;
using File = ModularPipelines.FileSystem.File;

namespace ModularPipelines.Build.Modules;

// [SkipIfNoGitHubToken]
// [SkipIfNoStandardGitHubToken]
[DependsOn<DownloadCodeCoverageFromOtherOperatingSystemBuildsModule>]
[DependsOn<RunUnitTestsModule>]
[DependsOn<NugetVersionGeneratorModule>]
[ResolveDependencies]
public class MergeCoverageModule : Module<File>
{
    /// <inheritdoc/>
    protected override async Task<File?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var packageVersion = await GetModule<NugetVersionGeneratorModule>();
        var coverageFilesFromThisRun = context.Git().RootDirectory
            .GetFiles(x => x.Name.Contains("cobertura") && x.Extension is ".xml").GroupBy(a => a.Folder)
            .Select(group => group.OrderByDescending(file => file.CreationTime).First())
            .ToList();

        var coverageFilesFromOtherSystems = await GetModule<DownloadCodeCoverageFromOtherOperatingSystemBuildsModule>();

        List<File> coverageFiles = [];
        if (!coverageFilesFromOtherSystems.SkipDecision.ShouldSkip)
        {
            if (coverageFilesFromOtherSystems.Value?.Count is null or < 1)
            {
                context.Logger.LogInformation("No code coverage found from other operating systems");
                return null;
            }

            coverageFiles = coverageFilesFromThisRun
               .Concat(coverageFilesFromOtherSystems.Value ?? [])
               .Distinct()
               .ToList();
        }
        else
        {
            coverageFiles.AddRange(coverageFilesFromThisRun);
        }

        var outputPath = context.Git().RootDirectory / "_buildOutput" / packageVersion.Value! / "MergedCoverage.xml";

        await context.Command.ExecuteCommandLineTool(new CommandLineToolOptions("dotnet")
        {
            Arguments = new[] { "dotnet-coverage", "merge", "--remove-input-files", "--output-format", "cobertura", "--output", outputPath.Path }.Concat(coverageFiles.Select(x => x.Path)),
        }, cancellationToken);

        return outputPath.Path;
    }
}