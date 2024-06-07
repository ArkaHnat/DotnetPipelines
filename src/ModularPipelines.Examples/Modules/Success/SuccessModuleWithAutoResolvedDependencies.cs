using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Examples.Modules.Success;
using ModularPipelines.Modules;

namespace ModularPipelines.Examples.Modules.FailedModules;

[DependsOn<AutoLoadedDependencyModule>]
public class SuccessModuleWithAutoResolvedDependencies : Module
{
    protected override Task<bool> ShouldIgnoreFailures(IPipelineContext context, Exception exception) => Task.FromResult(true);

    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        return null;
    }
}

[DependencyFor<AutoLoadedReliantModule>]
public class SuccessModuleWithAutoResolvedReliants : Module
{
    protected override Task<bool> ShouldIgnoreFailures(IPipelineContext context, Exception exception) => Task.FromResult(true);

    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        return null;
    }
}

public class SuccessModuleWithAutoResolvedIndirectReliants : Module
{
    protected override Task<bool> ShouldIgnoreFailures(IPipelineContext context, Exception exception) => Task.FromResult(true);

    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        return null;
    }
}

public class SuccessModuleWithAutoResolvedIndirectDependencies : Module
{
    protected override Task<bool> ShouldIgnoreFailures(IPipelineContext context, Exception exception) => Task.FromResult(true);

    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        return null;
    }
}

public class SuccessModuleWithAutoResolvedIndirectRelations : Module
{
    protected override Task<bool> ShouldIgnoreFailures(IPipelineContext context, Exception exception) => Task.FromResult(true);

    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        return null;
    }
}
