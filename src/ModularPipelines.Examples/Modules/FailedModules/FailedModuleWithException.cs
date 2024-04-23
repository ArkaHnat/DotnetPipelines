using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Modules;

namespace ModularPipelines.Examples.Modules.FailedModules;

public class FailedModuleWithException : Module
{
    protected override Task<bool> ShouldIgnoreFailures(IPipelineContext context, Exception exception)
    {
        return Task.FromResult(true);
    }

    protected override TimeSpan Timeout => TimeSpan.FromSeconds(10);

    /// <inheritdoc/>
    protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        context.Logger.LogToConsole($@"{typeof(FailedModuleWithException)}");
        await Task.Delay(TimeSpan.FromMilliseconds(500));
        throw new NotImplementedException();
    }
}