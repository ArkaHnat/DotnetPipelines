using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Exceptions;
using ModularPipelines.Modules;
using ModularPipelines.TestHelpers;
using TUnit.Assertions.Extensions.Throws;

namespace ModularPipelines.UnitTests;

public class NestedCollisionTests
{
    [Test]
    public async Task Modules_Dependent_On_Each_Other_Throws_Exception()
    {
        await Assert.That(() => TestPipelineHostBuilder.Create()
                .AddModule<DependencyConflictModule1>()
                .AddModule<DependencyConflictModule2>()
                .AddModule<DependencyConflictModule3>()
                .AddModule<DependencyConflictModule4>()
                .AddModule<DependencyConflictModule5>()
                .ExecutePipelineAsync()).
            ThrowsException().OfType<DependencyCollisionException>()
                .And.ThrowsException().With.Message.EqualTo("Dependency collision detected: **DependencyConflictModule2** -> DependencyConflictModule3 -> DependencyConflictModule4 -> DependencyConflictModule5 -> **DependencyConflictModule2**");
    }

    [DependsOn<DependencyConflictModule2>]
    private class DependencyConflictModule1 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return null;
        }
    }

    [DependsOn<DependencyConflictModule3>]
    private class DependencyConflictModule2 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return null;
        }
    }

    [DependsOn<DependencyConflictModule4>]
    private class DependencyConflictModule3 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return null;
        }
    }

    [DependsOn<DependencyConflictModule5>]
    private class DependencyConflictModule4 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return null;
        }
    }

    [DependsOn<DependencyConflictModule2>]
    private class DependencyConflictModule5 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return null;
        }
    }
}