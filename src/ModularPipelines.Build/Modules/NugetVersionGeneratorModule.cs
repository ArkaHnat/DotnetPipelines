using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModularPipelines.Attributes;
using ModularPipelines.Build.Settings;
using ModularPipelines.Context;
using ModularPipelines.Extensions;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

[DependsOn<GitCommitDotnetPackagesUpdates>]
[ResolveDependencies]
public class NugetVersionGeneratorModule : Module<string>
{
    public NugetVersionGeneratorModule()
    {
    }

    /// <inheritdoc/>
    protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var gitVersionInformation = await context.Git().Versioning.GetGitVersioningInformation();

        var version = gitVersionInformation.FullSemVer;

        context.LogOnPipelineEnd($"Generated Version Number: {version}");

        return version;
    }
    /// <inheritdoc/>
    protected override async Task OnAfterExecute(IPipelineContext context)
    {
        var moduleResult = await this;
        context.Logger.LogInformation("NuGet Version to Package: {Version}", moduleResult.Value);
    }
}
