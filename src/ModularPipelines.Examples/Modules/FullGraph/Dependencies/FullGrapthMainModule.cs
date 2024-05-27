using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Modules;

namespace ModularPipelines.Examples.Modules.FullGraph.Dependencies;

[DependsOn<FullGrapthDependensOnModule1>]
[DependsOn<FullGrapthDependensOnModule2>]
[DependencyFor<FullGrapthReliantModule1>]
[DependencyFor<FullGrapthReliantModule2>]
public class FullGrapthMainModule : Module
{
    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(3000), cancellationToken);
        return null;
    }
}