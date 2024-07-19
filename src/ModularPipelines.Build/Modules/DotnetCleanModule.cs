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

[DependsOn<FindProjectDependenciesModule>]
[ResolveDependencies]
public class DotnetCleanModule : Module<CommandResult[]>
{
    /// <inheritdoc/>
    protected override async Task<CommandResult[]?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var projectFiles = await GetModule<FindProjectDependenciesModule>();

        var dependencies = await projectFiles.Value!.Dependencies
            .ToAsyncProcessorBuilder()
            .SelectAsync(async projectFile => await Restore(context, cancellationToken, projectFile))
            .ProcessOneAtATime();

        return dependencies.ToArray();
    }

    private static async Task<CommandResult> Restore(IPipelineContext context, CancellationToken cancellationToken, File projectFile)
    {
        return await context.DotNet().Clean(new DotNetCleanOptions
        {
            ProjectSolution = projectFile,
        }, cancellationToken);
    }
}