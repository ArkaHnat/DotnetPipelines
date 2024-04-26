
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Exceptions;
using ModularPipelines.Modules;
using ModularPipelines.TestHelpers;

namespace ModularPipelines.Tests;

public class TestsWhichNeedsToBeInSeparateAssembly
{
    private class Module1 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }

    [DependencyFor(typeof(ModuleFailedException))]
    private class ReliesOnNonModule : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            //_ = GetModule<DependencyModule>();
            return await NothingAsync();
        }
    }

    [DependsOn(typeof(ModuleFailedException))]
    private class DependsOnNonModule : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            //_ = GetModule<Module1>();
            return await NothingAsync();
        }
    }

    [Test]
    public async Task Depends_On_Non_Module_Throws_Exception()
    {
        await TUnit.Assertions.Assert.That(async () => await TestPipelineHostBuilder.Create()
                .AddModule<ReliesOnNonModule>()
                .ExecutePipelineAsync()).
            Throws.Exception().With.Message.EqualTo("ModularPipelines.Exceptions.ModuleFailedException is not a Module class");
    }

    [Test]
    public async Task Relies_On_Non_Module_Throws_Exception2()
    {
        await TUnit.Assertions.Assert.That(async () => await TestPipelineHostBuilder.Create()
                .AddModule<ReliesOnNonModule>()
                .ExecutePipelineAsync()).
            Throws.Exception().With.Message.EqualTo("ModularPipelines.Exceptions.ModuleFailedException is not a Module class");
    }
}