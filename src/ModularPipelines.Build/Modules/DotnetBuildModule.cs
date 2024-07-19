using EnumerableAsyncProcessor.Extensions;
using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Options;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;
using Octokit;
using File = ModularPipelines.FileSystem.File;

namespace ModularPipelines.Build.Modules;

[DependsOn<ChangedFilesInPullRequestModule>]
[DependsOn<FindProjectDependenciesModule>]
[DependsOn<DotnetRestoreModule>]
[ResolveDependencies]
public class DotnetBuildModule : Module<CommandResult[]>
{
    public static string BuildConfiguration = Configuration.Debug;

    /// <inheritdoc/>
    protected override async Task<CommandResult[]?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var changedFiles = await GetModule<ChangedFilesInPullRequestModule>();
        var projectFiles = await GetModule<FindProjectDependenciesModule>();

        var dependencies = await projectFiles.Value!.Dependencies
            .ToAsyncProcessorBuilder()
            .SelectAsync(async projectFile => await Build(context, cancellationToken, projectFile))
            .ProcessOneAtATime();

        var others = await projectFiles.Value!.Others
            .Where(x =>
            {
                if (changedFiles.SkipDecision.ShouldSkip)
                {
                    return true;
                }

                return ProjectHasChanged(x,
                    changedFiles.Value!, context);
            })
            .ToAsyncProcessorBuilder()
            .SelectAsync(async projectFile => await Build(context, cancellationToken, projectFile))
            .ProcessInParallel();

        return dependencies.Concat(others).ToArray();
    }

    private bool ProjectHasChanged(File projectFile, IEnumerable<File> changedFiles,
        IPipelineContext context)
    {
        var projectDirectory = projectFile.Folder!;

        if (!changedFiles.Any(x => x.Path.Contains(projectDirectory.Path)))
        {
            context.Logger.LogInformation("{Project} has not changed so not building it", projectFile.Name);
            return false;
        }

        context.Logger.LogInformation("{Project} has changed so building it", projectFile.Name);

        return true;
    }

    private async Task<CommandResult> Build(IPipelineContext context, CancellationToken cancellationToken, File projectFile)
    {
        return await context.DotNet().Build(new DotNetBuildOptions
        {
            NoRestore = true,
            Configuration = BuildConfiguration,
            ProjectSolution = projectFile,
            Properties = new KeyValue[]
                {
                    new("RunAnalyzersDuringBuild", "false"),
                    new("RunAnalyzers", "false"),
                },
        }, cancellationToken);
    }
}