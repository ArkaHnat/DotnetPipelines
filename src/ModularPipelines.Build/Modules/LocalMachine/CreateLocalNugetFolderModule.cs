using Microsoft.Extensions.Logging;
using ModularPipelines.Context;
using ModularPipelines.FileSystem;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules.LocalMachine;

public class CreateLocalNugetFolderModule : Module<Folder>
{
    /// <inheritdoc/>
    protected override async Task<Folder?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var localNugetRepositoryFolder = 
            context.Git().RootDirectory
            .GetFolder("_buildOutput")
            .GetFolder("ModularPipelines")
            .GetFolder("LocalNuget")
            .Create();

        await Task.Yield();

        context.Logger.LogInformation("Local NuGet Repository Path: {Path}", localNugetRepositoryFolder.Path);

        return localNugetRepositoryFolder;
    }
}