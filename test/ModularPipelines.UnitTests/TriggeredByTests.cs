using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Exceptions;
using ModularPipelines.Modules;
using ModularPipelines.TestHelpers;
using TUnit.Assertions.Extensions;
using Status = ModularPipelines.Enums.Status;

namespace ModularPipelines.UnitTests;

public class TriggeredByTests : TestBase
{
    [Test]
    public async Task No_Exception_Thrown_When_Triggered_Module_Present()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<TriggeringModule>()
            .AddModule<TriggeredModule>()
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status)
            .Is
            .EqualTo(Status.Successful);
    }

    [Triggers<TriggeredModule>]
    private class TriggeringModule : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            
            return await NothingAsync();
        }
    }
    private class TriggeredModule : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }

    [Triggers<TriggersSelfModule>]
    private class TriggersSelfModule : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
}