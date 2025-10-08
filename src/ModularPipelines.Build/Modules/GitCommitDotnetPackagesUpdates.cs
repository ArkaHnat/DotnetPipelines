using DotnetModularPipelines.Git.Options;
using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

[DependsOn<DotNetToolUpdatePackagesModule>]
[ResolveDependencies]
public class GitCommitDotnetPackagesUpdates : Module<CommandResult>
{
    public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

	/// <inheritdoc/>
	protected override async Task<CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
	{
		var updatedTool = await GetModule<DotnetToolUpdateModule>();
		_ = await context.Git().Commands.Add(new GitAddOptions()
		{
			FilePattern = context.Git().RootDirectory / "Directory.Packages.props",
		});
		var commit = await context.Git().Commands.Commit(new Git.Options.GitCommitOptions()
		{
			Message = $"chore(deps): Updated dotnet package [{updatedTool.Value.Value.packageId}] to version [{updatedTool.Value.Value.toVersion}]",
		});
		return commit;
	}

    protected override async Task<SkipDecision> ShouldSkip(IPipelineContext context)
    {
        var updatedTool = await GetModule<DotnetToolUpdateModule>();
        return string.IsNullOrWhiteSpace(updatedTool.Value.Value.toVersion)
            ? SkipDecision.Skip("No tools updated skipping")
            : SkipDecision.DoNotSkip;
    }

    /// <inheritdoc/>
    protected override async Task OnAfterExecute(IPipelineContext context)
    {
        _ = await this;
        context.Logger.LogInformation("Restored dotnet tools.");
    }
}