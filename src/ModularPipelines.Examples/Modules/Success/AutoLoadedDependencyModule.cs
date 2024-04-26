using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Examples.Modules.FailedModules;
using ModularPipelines.Modules;

namespace ModularPipelines.Examples.Modules.Success;

public class AutoLoadedDependencyModule : Module
{
    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

        return null;
    }
}

public class AutoLoadedReliantModule : Module
{
    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

        return null;
    }
}

[DependencyFor<SuccessModuleWithAutoResolvedIndirectReliants>]
public class AutoLoadedIndirectReliantModule : Module
{
    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

        return null;
    }
}

[DependsOn<SuccessModuleWithAutoResolvedIndirectDependencies>]
public class AutoLoadedIndirectDependencyModule : Module
{
    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

        return null;
    }
}

[DependencyFor<SuccessModuleWithAutoResolvedIndirectRelations>]
public class AutoLoadedIndirectReliantModule2 : Module
{
    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

        return null;
    }
}

[DependsOn<SuccessModuleWithAutoResolvedIndirectRelations>]
public class AutoLoadedIndirectDependencyModule2 : Module
{
    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

        return null;
    }
}
