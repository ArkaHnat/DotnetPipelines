using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Modules;

namespace ModularPipelines.Examples.Modules.ResolveModules;

[Triggers<FullGraphTiggeredByModule>]
[TriggeredBy<FullGrapthTriggeringModule>]
public class FullGraphMainTriggerModule : Module
{
    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(5000), cancellationToken);
        return null;
    }
}