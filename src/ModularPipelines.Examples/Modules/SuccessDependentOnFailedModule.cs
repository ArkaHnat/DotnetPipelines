using Microsoft.Extensions.Logging;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Examples.Modules.FailedModules;
using ModularPipelines.Examples.Modules.Success;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Examples.Modules;

[DependsOn<FailedModuleWithException>]
public class SuccessDependentOnFailedModule : Module
{
    protected override Task<bool> ShouldIgnoreFailures(IPipelineContext context, Exception exception)
    {
        return Task.FromResult(true);
    }

    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        context.Logger.LogInformation("Some message");
        await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);
        return null;
    }
}