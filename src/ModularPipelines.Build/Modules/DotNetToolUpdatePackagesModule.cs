using DotnetModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework;
using DotnetModularPipelines.Git.Options;
using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Build.Model;
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
public class DotNetToolUpdatePackagesModule : Module<CommandResult>
{
    public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

    /// <inheritdoc/>
    protected override async Task<CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        _ = Directory.GetCurrentDirectory();
        var dotnetOutdatedOutputJson = new FileSystem.File(context.Git().RootDirectory / "_buildOutput/dotnet-outdated-output.json");
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
