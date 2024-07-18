using Microsoft.Extensions.Logging;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

public class DotnetToolRestoreModule : Module<CommandResult>
{
    public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

    /// <inheritdoc/>
    protected override async Task<CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var result = await context.DotNet().Tool.Restore();

        return result;
    }

    /// <inheritdoc/>
    protected override async Task OnAfterExecute(IPipelineContext context)
    {
        var moduleResult = await this;
        context.Logger.LogInformation("Restored dotnet tools.");
    }
}