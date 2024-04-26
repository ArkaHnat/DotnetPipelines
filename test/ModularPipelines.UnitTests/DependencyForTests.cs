using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Exceptions;
using ModularPipelines.Extensions;
using ModularPipelines.Modules;
using ModularPipelines.TestHelpers;
using TUnit.Assertions.Extensions;
using Status = ModularPipelines.Enums.Status;

namespace ModularPipelines.UnitTests;

public class DependencyForTests : TestBase
{
    [DependencyFor<ReliantModule>]
    private class DependencyModule : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
    [DependencyFor<ReliantModule>(ResolveIfNotRegistered =true)]
    [SearchFor(SearchForReliants = true)]
    private class DependencyModuleWithResolveIndirectReliants : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }

    [DependencyFor<ReliantModule>(ResolveIfNotRegistered = true)]
    [SearchFor(SearchForDependencies = true)]
    private class DependencyModuleWithResolveIndirectDependencies : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
    [DependencyFor<ReliantModule>(ResolveIfNotRegistered = false)]
    private class DependencyModuleWithoutResolve : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
    private class ReliantModule : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }

    [DependencyFor<Module4>(IgnoreIfNotRegistered = true)]
    private class Module3 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
    private class Module4 : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            return await NothingAsync();
        }
    }
    [DependencyFor<DependencyModule>(IgnoreIfNotRegistered = true)]
    private class Module3WithGetIfRegistered : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            _ = GetModuleIfRegistered<DependencyModule>();
            return await NothingAsync();
        }
    }

    [DependencyFor<DependencyModule>(IgnoreIfNotRegistered = true)]
    private class Module3WithGet : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            _ = GetModule<DependencyModule>();
            return await NothingAsync();
        }
    }

    [DependencyFor<ReliesOnSelfModule>]
    private class ReliesOnSelfModule : Module
    {
        protected override async Task<IDictionary<string, object>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            _ = GetModule<DependencyModule>();
            return await NothingAsync();
        }
    }

    [Test]
    public async Task No_Exception_Thrown_When_Reliant_Module_Present()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<DependencyModule>()
            .AddModule<ReliantModule>()
            .ExecutePipelineAsync();
        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Reliant_Module_Present2()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module4>()
            .AddModule<Module3>()
            .ExecutePipelineAsync();
        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
    }

    [Test]
    public async Task Exception_Should_Not_Be_Thrown_When_Reliant_Module_Missing_And_No_Ignore_On_Attribute()
    {
        await TestPipelineHostBuilder.Create()
                .AddModule<DependencyModule>()
                .ExecutePipelineAsync();
    }

    [Test]
    public async Task No_Exception_Thrown_When_Reliant_Module_Missing_And_Ignore_On_Attribute()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module3>()
            .ExecutePipelineAsync();
        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Reliant_Module_Missing_And_Get_If_Registered_Called()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<Module3WithGetIfRegistered>()
            .ExecutePipelineAsync();
        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
    }

    [Test]
    public async Task Exception_Thrown_When_Reliant_Module_Missing_And_Get_Module_Called()
    {
        await Assert.That(async () => await TestPipelineHostBuilder.Create()
                .AddModule<Module3WithGet>()
                .ExecutePipelineAsync()).
            Throws.Exception().OfAnyType();
    }

    [Test]
    public async Task Dependency_For_Self_Module_Throws_Exception()
    {
        await Assert.That(async () => await TestPipelineHostBuilder.Create()
                .AddModule<ReliesOnSelfModule>()
                .ExecutePipelineAsync()).
            Throws.Exception().OfType<ModuleReferencingSelfException>();
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_Resolve_In_Parameter()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .ConfigureServices((context, collection) =>
            {
                collection.AddModule<DependencyModuleWithoutResolve>();
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
                collection.AddModule<ReliantModule>();
            }
            )
            .ExecutePipelineAsync();
        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);

        await Assert.That(pipelineSummary.Modules.Count).Is.GreaterThan(1);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_ResolveDependencies_On_Attribute()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<ReliantModule>()
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
        await Assert.That(pipelineSummary.Modules.Count).Is.GreaterThan(1);
    }

    [Test]
    public async Task No_Exception_Thrown_When_Dependent_Module_Missing_And_Resolve_On_Attribute()
    {
        var pipelineSummary = await TestPipelineHostBuilder.Create()
            .AddModule<DependencyModuleWithResolveIndirectReliants>()
            .ExecutePipelineAsync();

        await Assert.That(pipelineSummary.Status).Is.EqualTo(Status.Successful);
        await Assert.That(pipelineSummary.Modules.Count).Is.GreaterThan(1);
    }
}