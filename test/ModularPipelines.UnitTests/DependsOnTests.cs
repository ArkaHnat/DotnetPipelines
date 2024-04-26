using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Exceptions;
using ModularPipelines.Extensions;
using ModularPipelines.Modules;
using ModularPipelines.TestHelpers;
using TUnit.Assertions.Extensions;
using Status = ModularPipelines.Enums.Status;

namespace ModularPipelines.UnitTests;

public class DependsOnTests : TestBase
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
            return await NothingAsync();
        }
    }

    [DependsOn<Module1>(IgnoreIfNotRegistered = true)]
    private class Module3 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
    [DependsOn<Module1>(ResolveIfNotRegistered = true)]
    private class Module3WithResolveDependency : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
    [DependsOn<Module1>(ResolveIfNotRegistered = false)]
    private class Module3WithoutResolveDependency : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
    [DependsOn<Module1>(IgnoreIfNotRegistered = true)]
    private class Module3WithGetIfRegistered : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            _ = GetModuleIfRegistered<Module1>();
            return await NothingAsync();
        }
    }

    [DependsOn<Module1>(IgnoreIfNotRegistered = true)]
    private class Module3WithGet : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            _ = GetModule<Module1>();
            return await NothingAsync();
        }
    }

    [DependsOn<DependsOnSelfModule>]
    private class DependsOnSelfModule : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            _ = GetModule<Module1>();
            return await NothingAsync();
        }
    }
   

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Present()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module1>()
            .AddModule<Module2>()
            .ExecutePipelineAsync();
        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Present2()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module1>()
            .AddModule<Module3>()
            .ExecutePipelineAsync();
        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
    }

    [Test]
    public async Task Exception_Thrown_When_Dependent_Module_Missing_And_No_Ignore_On_Attribute()
    {
        await Assert.That(async () => await TestPipelineHostBuilder.Create()
                .AddModule<Module2>()
                .ExecutePipelineAsync())
            .Throws.Exception().OfAnyType();
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_Ignore_On_Attribute()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module3>()
            .ExecutePipelineAsync();
        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
    }
    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_Resolve_On_Attribute()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .ConfigureServices((context, collection) =>
            {
                collection.AddModule<Module3WithResolveDependency>();
            }
            )
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
        await Assert.That(pipelineSummary.Modules.Count).Is.EqualTo(2);
    }
    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_Resolve_On_Attribute2()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .ConfigureServices((context, collection) =>
            {
                collection.AddModule<Module1>();
            }
            )
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
        await Assert.That(pipelineSummary.Modules.Count).Is.EqualTo(2);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_Resolve_In_Parameter()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .ConfigureServices((context, collection) =>
                {
                    collection.AddModule<Module3WithoutResolveDependency>();
                }
            )
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
        await Assert.That(pipelineSummary.Modules.Count).Is.EqualTo(2);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_Resolve_In_Parameter2()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .ConfigureServices((context, collection) =>
            {
                collection.AddModule<Module1>();
            }
            )
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
        await Assert.That(pipelineSummary.Modules.Count).Is.EqualTo(7);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_Get_If_Registered_Called()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module3WithGetIfRegistered>()
            .ExecutePipelineAsync();
        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
    }

    [Test]
    public async Task Exception_Thrown_When_Dependent_Module_Missing_And_Get_Module_Called()
    {
        await Assert.That(async () => await TestPipelineHostBuilder.Create()
                .AddModule<Module3WithGet>()
                .ExecutePipelineAsync()).
            Throws.Exception().OfAnyType();
    }

    [Test]
    public async Task Depends_On_Self_Module_Throws_Exception()
    {
        await Assert.That(async () => await TestPipelineHostBuilder.Create()
                .AddModule<DependsOnSelfModule>()
                .ExecutePipelineAsync()).
            Throws.Exception().OfType<ModuleReferencingSelfException>();
    }
}