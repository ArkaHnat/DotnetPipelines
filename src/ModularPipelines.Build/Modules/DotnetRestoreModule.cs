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

[DependsOn<NugetVersionGeneratorModule>]
[DependsOn<DotnetCleanModule>]
[DependsOn<FindProjectDependenciesModule>]
[DependsOn<ChangedFilesInPullRequestModule>]
[ResolveDependencies]
public class DotnetRestoreModule : Module<CommandResult[]>
{
    /// <inheritdoc/>
    protected override async Task<CommandResult[]?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var packageVersion = await GetModule<NugetVersionGeneratorModule>();
        var projectFiles = await GetModule<FindProjectDependenciesModule>();
        var changedFiles = await GetModule<ChangedFilesInPullRequestModule>();
        var dependencies = await projectFiles.Value!.Dependencies
            .ToAsyncProcessorBuilder()
            .SelectAsync(async projectFile => await Restore(context, cancellationToken, projectFile))
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
            .SelectAsync(async projectFile => await Restore(context, cancellationToken, projectFile))
            .ProcessInParallel();

        return dependencies.Concat(others).ToArray();
    }

    private bool ProjectHasChanged(File projectFile, IEnumerable<File> changedFiles,
        IPipelineContext context)
    {
        var projectDirectory = projectFile.Folder!;

        if (!changedFiles.Any(x => x.Path.Contains(projectDirectory.Path)))
        {
            context.Logger.LogInformation("{Project} has not changed so not restoring it", projectFile.Name);
            return false;
        }

        context.Logger.LogInformation("{Project} has changed so restoring it", projectFile.Name);

        return true;
    }

    private static async Task<CommandResult> Restore(IPipelineContext context, CancellationToken cancellationToken, File projectFile)
    {
        return await context.DotNet().Restore(new DotNetRestoreOptions
        {
            Path = projectFile.Path,
        }, cancellationToken);
    }
}