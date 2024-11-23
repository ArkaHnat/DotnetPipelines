using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Modules;
using ModularPipelines.TestHelpers;

namespace ModularPipelines.UnitTests;

[Retry(3)]
public class NotInParallelTests
{
    [ModularPipelines.Attributes.NotInParallel]
    public class Module1 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            return GetType().Name;
        }
    }

    [ModularPipelines.Attributes.NotInParallel]
    public class Module2 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            return GetType().Name;
        }
    }

    [ModularPipelines.Attributes.NotInParallel]
    [ModularPipelines.Attributes.DependsOn<ParallelDependency1>]
    public class NotParallelModuleWithParallelDependency1 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            return GetType().Name;
        }
    }
    public class ParallelDependency1 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            return GetType().Name;
        }
    }
    public class ParallelDependency2 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            return GetType().Name;
        }
    }
    [ModularPipelines.Attributes.NotInParallel]
    [ModularPipelines.Attributes.DependsOn<ParallelDependency2>]
    public class NotParallelModuleWithParallelDependency2 : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            return GetType().Name;
        }
    }
    [ModularPipelines.Attributes.NotInParallel]
    [ModularPipelines.Attributes.DependsOn<NotParallelModuleWithParallelDependency1>]
    public class NotParallelModuleWithNonParallelDependency : Module<string>
    {
        protected override async Task<string?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            return GetType().Name;
        }
    }

    [Test]
    public async Task NotInParallel()
    {
        var results = await TestPipelineHostBuilder.Create()
            .AddModule<Module1>()
            .AddModule<Module2>()
            .ExecutePipelineAsync();

        var firstModule = results.Modules.MinBy(x => x.EndTime)!;
        var nextModule = results.Modules.MaxBy(x => x.EndTime)!;
        await Assert.That(nextModule.StartTime)
            .IsGreaterThanOrEqualTo(firstModule.StartTime + TimeSpan.FromSeconds(5));
    }

    [Test]
    public async Task NotInParallel_With_ParallelDependency()
    {
        var results = await TestPipelineHostBuilder.Create()
            .AddModule<NotParallelModuleWithParallelDependency2>()
            .AddModule<ParallelDependency2>()
            .ExecutePipelineAsync();

        var firstModule = results.Modules.MinBy(x => x.EndTime)!;
        var nextModule = results.Modules.MaxBy(x => x.EndTime)!;
        await Assert.That(nextModule.StartTime)
            .IsGreaterThanOrEqualTo(firstModule.StartTime + TimeSpan.FromSeconds(5));
    }

    [Test]
    public async Task NotInParallel_With_NonParallelDependency()
    {
        var results = await TestPipelineHostBuilder.Create()
            .AddModule<ParallelDependency1>()
            .AddModule<NotParallelModuleWithParallelDependency1>()
            .AddModule<NotParallelModuleWithNonParallelDependency>()
            .ExecutePipelineAsync();

        var firstModule = results.Modules.MinBy(x => x.EndTime)!;
        var nextModule = results.Modules.MaxBy(x => x.EndTime)!;

        var expectedStartTime = firstModule.StartTime + TimeSpan.FromSeconds(10);
        Console.WriteLine("First " + firstModule.StartTime);
        Console.WriteLine("Last expected" + expectedStartTime);
        Console.WriteLine("Last performed" + nextModule.StartTime);
        await Assert.That(nextModule.StartTime)
            .IsGreaterThanOrEqualTo(expectedStartTime);
    }
}