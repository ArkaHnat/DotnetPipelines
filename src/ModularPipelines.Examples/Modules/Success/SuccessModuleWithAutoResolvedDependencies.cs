using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Examples.Modules.Success;
using ModularPipelines.Modules;

namespace ModularPipelines.Examples.Modules.FailedModules;

[DependsOn<AutoLoadedDependencyModule>]
[SearchFor(SearchForDependencies = true, SearchForReliants = false, SearchForIndirectDependencies = false, SearchForIndirectReliants = false)]
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
[SearchFor(SearchForDependencies = false, SearchForReliants = true, SearchForIndirectDependencies = false, SearchForIndirectReliants = false)]
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

[SearchFor(SearchForDependencies = false, SearchForReliants = false, SearchForIndirectDependencies = false, SearchForIndirectReliants = true)]
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

[SearchFor(SearchForDependencies = false, SearchForReliants = false, SearchForIndirectDependencies = true, SearchForIndirectReliants = false)]
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

[SearchFor(SearchForDependencies = false, SearchForReliants = false, SearchForIndirectDependencies = true, SearchForIndirectReliants = true)]
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
