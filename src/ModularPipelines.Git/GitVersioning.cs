using System.Text.Json;
using Microsoft.Extensions.Logging;
using ModularPipelines.Context;
using ModularPipelines.FileSystem;
using ModularPipelines.Git.Models;
using ModularPipelines.Logging;
using ModularPipelines.Options;
using Polly;
using File = ModularPipelines.FileSystem.File;

namespace ModularPipelines.Git;

internal class GitVersioning : IGitVersioning
{
    private readonly IGitInformation _gitInformation;
    private readonly ICommand _command;
    private readonly IModuleLoggerProvider _moduleLoggerProvider;

    private readonly Folder _temporaryFolder;

    private static readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
    private static GitVersionInformation? _prefetchedGitVersionInformation;

    public string DotnetToolName 
    { 
        get
        {
            return "GitVersion.Tool";
        } 
    }

    public GitVersioning(IFileSystemContext fileSystemContext, IGitInformation gitInformation, ICommand command, IModuleLoggerProvider moduleLoggerProvider)
    {
        _gitInformation = gitInformation;
        _command = command;
        _moduleLoggerProvider = moduleLoggerProvider;
        _temporaryFolder = fileSystemContext.CreateTemporaryFolder();
    }

    public async Task<GitVersionInformation> GetGitVersioningInformation()
    {
        await _semaphoreSlim.WaitAsync();

        try
        {
            if (_prefetchedGitVersionInformation != null)
            {
                return _prefetchedGitVersionInformation;
            }

            await TryWriteConfigurationFile();

            var gitVersionOutput = await _command.ExecuteCommandLineTool(
                new CommandLineToolOptions("dotnet")
                {
                    WorkingDirectory = _gitInformation.Root.Path,
                    Arguments =
                    [
                        "gitversion", "/output", "json"
                    ],
                });

            return _prefetchedGitVersionInformation ??=
                JsonSerializer.Deserialize<GitVersionInformation>(gitVersionOutput.StandardOutput)!;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    private async Task TryWriteConfigurationFile()
    {
        try
        {
            var file = new File(Path.Combine(_gitInformation.Root.Path, "GitVersion.yml"));
            
            if (!file.Exists)
            {
                await file.WriteAsync(
                    """
                    mode: ContinuousDeployment
                    strategies:
                      - Mainline
                    """
                );
            }
        }
        catch (Exception e)
        {
            _moduleLoggerProvider.GetLogger().LogWarning(e, "Error defining GitVersion.yml configuration");
        }
    }
}