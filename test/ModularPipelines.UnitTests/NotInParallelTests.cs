using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Modules;
using ModularPipelines.TestHelpers;
using TUnit.Assertions.Extensions;

namespace ModularPipelines.UnitTests;

public class NotInParallelTests
{
    [ModularPipelines.Attributes.NotInParallel]
    public class Module1 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            return GetType().Name;
        }
    }

    [ModularPipelines.Attributes.NotInParallel]
    public class Module2 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            return GetType().Name;
        }
    }

    [ModularPipelines.Attributes.NotInParallel]
    [DependsOn<ParallelDependency1>]
    public class NotParallelModuleWithParallelDependency1 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            return GetType().Name;
        }
    }
    public class ParallelDependency1 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            return GetType().Name;
        }
    }
    public class ParallelDependency2 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            return GetType().Name;
        }
    }
    [ModularPipelines.Attributes.NotInParallel]
    [DependsOn<ParallelDependency2>]
    public class NotParallelModuleWithParallelDependency2 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            return GetType().Name;
        }
    }
    [ModularPipelines.Attributes.NotInParallel]
    [DependsOn<NotParallelModuleWithParallelDependency1>]
    public class NotParallelModuleWithNonParallelDependency : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            return GetType().Name;
        }
    }

    [Test, Retry(3)]
    public async Task NotInParallel()
    {
        var results = await TestPipelineHostBuilder.Create()
            .AddModule<Module1>()
            .AddModule<Module2>()
            .ExecutePipelineAsync();

        var firstModule = results.Modules.MinBy(x => x.EndTime)!;
        var nextModule = results.Modules.MaxBy(x => x.EndTime)!;
        await Assert.That(nextModule.StartTime)
            .Is.EqualToWithTolerance(firstModule.StartTime + TimeSpan.FromSeconds(2),
                TimeSpan.FromMilliseconds(500));
    }

    [Test, Retry(3)]
    public async Task NotInParallel_With_ParallelDependency()
    {
        var results = await TestPipelineHostBuilder.Create()
            .AddModule<NotParallelModuleWithParallelDependency2>()
            .AddModule<ParallelDependency2>()
            .ExecutePipelineAsync();

        var firstModule = results.Modules.MinBy(x => x.EndTime)!;
        var nextModule = results.Modules.MaxBy(x => x.EndTime)!;
        await Assert.That(nextModule.StartTime)
            .Is.EqualToWithTolerance(firstModule.StartTime + TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(500));
    }

    [Test, Retry(3)]
    public async Task NotInParallel_With_NonParallelDependency()
    {
        var results = await TestPipelineHostBuilder.Create()
            .AddModule<ParallelDependency1>()
            .AddModule<NotParallelModuleWithNonParallelDependency>()
            .ExecutePipelineAsync();

        var firstModule = results.Modules.MinBy(x => x.EndTime)!;
        var nextModule = results.Modules.MaxBy(x => x.EndTime)!;

        var expectedStartTime = firstModule.StartTime + TimeSpan.FromSeconds(4);
        
        await Assert.That(nextModule.StartTime)
            .Is.EqualToWithTolerance(expectedStartTime, TimeSpan.FromMilliseconds(500));
    }
}