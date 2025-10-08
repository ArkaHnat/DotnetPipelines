using DotnetModularPipelines.Git.Options;
using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

[DependsOn<DotnetToolUpdateModule>]
[ResolveDependencies]
public class GitCommitDotnetToolUpdates : Module<CommandResult>
{
    public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

    /// <inheritdoc/>
    protected override async Task<CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var updatedTool = await GetModule<DotnetToolUpdateModule>();
        
        var status = await context.Git().Commands.Add(new GitAddOptions()
        {
            FilePattern = context.Git().RootDirectory / ".config/dotnet-tools.json",
        });
        var commit = await context.Git().Commands.Commit(new Git.Options.GitCommitOptions()
        {
            Message = $"chore(deps): Updated dotnet tool [{updatedTool.Value.Value.packageId}] from version [{updatedTool.Value.Value.fromVersion}] to version [{updatedTool.Value.Value.toVersion}]",
        });
        return commit;
    }
	protected override async Task<SkipDecision> ShouldSkip(IPipelineContext context)
	{

		var updatedTool = await GetModule<DotnetToolUpdateModule>();
        if (string.IsNullOrWhiteSpace(updatedTool.Value.Value.toVersion)){
            return SkipDecision.Skip("No tools updated skipping");
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