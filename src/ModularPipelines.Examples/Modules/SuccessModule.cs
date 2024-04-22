using ModularPipelines.Context;
using ModularPipelines.Modules;

namespace ModularPipelines.Examples.Modules;

public class SuccessModule : Module
{
    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        return null;
    }
}