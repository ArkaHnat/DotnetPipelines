using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Exceptions;
using ModularPipelines.Extensions;
using ModularPipelines.Modules;
using ModularPipelines.TestHelpers;
using TUnit.Assertions.AssertConditions.Throws;
using Status = ModularPipelines.Enums.Status;

namespace ModularPipelines.UnitTests;

public class BeforeTests : TestBase
{
    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Present()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module1>()
            .AddModule<Module2>()
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status)
            .IsEqualTo(Status.Successful);

		var module1 = pipelineSummary.Modules.FirstOrDefault(a => a.GetType() == typeof(Module1));
        var module2 = pipelineSummary.Modules.FirstOrDefault(a => a.GetType() == typeof(Module2));
        await Assert.That(module2.EndTime).IsLessThan(module1.StartTime);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_Ignore_On_Attribute()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module3>()
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status)
            .IsEqualTo(Status.Successful);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_NoIgnore_On_Attribute()
    {
        await Assert.That(async () => await TestPipelineHostBuilder.Create()
            .AddModule<Module4>()
            .ExecutePipelineAsync())
            .Throws<ModuleNotRegisteredException>();
    }


    private class Module1 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }

    [Before<Module1>]
    private class Module2 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }

    [Before<Module1>(IgnoreIfNotRegistered = true)]
    private class Module3 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
    [Before<Module1>(IgnoreIfNotRegistered = false)]
    private class Module4 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
   

    [Before<DependsOnSelfModule>]
    private class DependsOnSelfModule : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            _ = GetModule<Module1>();
            return await NothingAsync();
        }
    }

}