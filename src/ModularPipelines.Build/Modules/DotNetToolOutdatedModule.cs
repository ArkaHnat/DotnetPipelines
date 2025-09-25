using DotnetModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using DotnetModularPipelines.Git.Options;
using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

[DependsOn<DotnetToolUpdateModule>]
[ResolveDependencies]
public class DotNetToolOutdatedModule : Module<CommandResult>
{
    public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

    /// <inheritdoc/>
    protected override async Task<CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        _ = Directory.GetCurrentDirectory();

        var options = new DotnetToolOutdatedRunOptions(context.Git().RootDirectory / "ModularPipelines.Merged.sln")
        {
            UpdateOnlySinglePackage = true,
            OutputFilename = "dotnet-outdated-output",
            OutputFormat = DotnetToolUpdateOutputFormat.Json,
        };
        var result = await context.DotNet().Tool.DotnetOutdated.Run(options);

        return result;
    }

    protected override async Task<SkipDecision> ShouldSkip(IPipelineContext context)
    {
        var result = await GetModule<DotnetToolUpdateModule>();
        if (result.HasValue)
        {
            return SkipDecision.Skip("DotnetToolUpdateModule already updated files");

        }
        return SkipDecision.DoNotSkip;
    }
	/// <inheritdoc/>
	protected override async Task OnAfterExecute(IPipelineContext context)
    {
        _ = await this;
        context.Logger.LogInformation("Restored dotnet tools.");
    }
}
