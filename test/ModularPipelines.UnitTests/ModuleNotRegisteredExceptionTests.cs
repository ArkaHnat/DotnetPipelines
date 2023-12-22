using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Exceptions;
using ModularPipelines.Modules;

namespace ModularPipelines.UnitTests;

public class ModuleNotRegisteredExceptionTests : TestBase
{
    private class Module1 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }

    [DependsOn<Module1>]
    private class Module2 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await GetModule<Module1>();
            return await NothingAsync();
        }
    }

    [Test]
    public void Module_Getting_Non_Registered_Module_Throws_Exception()
    {
        var moduleFailedException = Assert.ThrowsAsync<ModuleFailedException>(() =>
            TestPipelineHostBuilder.Create()
                .AddModule<Module2>()
                .ExecutePipelineAsync()
        );

        Assert.That(moduleFailedException!.InnerException, Is.TypeOf<ModuleNotRegisteredException>());
    }

    [Test]
    public void Module_Getting_Registered_Module_Does_Not_Throw_Exception()
    {
        Assert.DoesNotThrowAsync(() =>
            TestPipelineHostBuilder.Create()
                .AddModule<Module1>()
                .AddModule<Module2>()
                .ExecutePipelineAsync()
        );
    }
}