using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Exceptions;
using ModularPipelines.Extensions;
using ModularPipelines.Modules;
using ModularPipelines.TestHelpers;
using Status = ModularPipelines.Enums.Status;

namespace ModularPipelines.UnitTests;

public class DependsOnTests : TestBase
{
    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Present()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module2>()
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status)
            .Is
            .EqualTo(Status.Successful);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Present2()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module1>()
            .AddModule<Module3>()
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status)
            .Is
            .EqualTo(Status.Successful);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_Ignore_On_Attribute()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module3>()
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status)
            .Is
            .EqualTo(Status.Successful);
    }
    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_ResolveIndirectReliants_On_Attribute()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .ConfigureServices((context, collection) => { collection.AddModule<ModuleWithResolveDependants>(); })
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status)
            .Is
            .EqualTo(Status.Successful);

        await Assert.That(pipelineSummary.Modules.Count)
            .Is
            .EqualTo(2);
    }


    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_Get_If_Registered_Called()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module3WithGetIfRegistered>()
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status)
            .Is
            .EqualTo(Status.Successful);
    }



    [Test]
    public async Task Depends_On_Self_Module_Throws_Exception()
    {
        await Assert.That(async () => await TestPipelineHostBuilder.Create()
                .AddModule<DependsOnSelfModule>()
                .ExecutePipelineAsync())
            .Throws
            .Exception()
            .OfType<ModuleReferencingSelfException>();
    }
    [Test]
    public async Task ExpectPipeline_ToSucces_IfOptionalDependencyFailed()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<ModuleWithFailingOptionalDependency>()
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status)
            .Is
            .EqualTo(Status.Successful);
    }
    private class Module1 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }

    [DependsOn<Module1>]
    [ResolveDependencies]
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

    [DependsOn<Module1>]
    private class Module3WithResolveDirectDependency : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }

    [DependsOn<ModuleWithResolveDependants>]
    private class ModuleWithDependsOnReliants : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }

    [ResolveIndirectDependants]
    private class ModuleWithResolveDependants : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
    private class AlwaysFailModule : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
    [DependsOn<AlwaysFailModule>(Optional = true)]
    [ResolveDependencies]
    private class ModuleWithFailingOptionalDependency : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
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

    [DependsOn<Module1>(IgnoreIfNotRegistered = true)]
    private class Module3WithGetIfRegistered : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            _ = GetModuleIfRegistered<Module1>();
            return await NothingAsync();
        }
    }
}