using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Examples.Modules.Success;
using ModularPipelines.Modules;

namespace ModularPipelines.Examples.Modules.FailedModules;

[DependsOn<AutoLoadedModule>(ResolveIfNotRegistered = true)]
public class SuccessModuleWithAutoResolvedDependencies : Module
{
    protected override Task<bool> ShouldIgnoreFailures(IPipelineContext context, Exception exception) => Task.FromResult(true);

    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(9), cancellationToken);
        return null;
    }
}