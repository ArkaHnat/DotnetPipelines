using EnumerableAsyncProcessor.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModularPipelines.Attributes;
using ModularPipelines.Build.Settings;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Options;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

[DependsOn<RunUnitTestsModule>(Optional = true)]
[DependsOn<NugetVersionGeneratorModule>]
[DependsOn<UploadPackagesToNugetModule>]
[DependsOn<FindProjectsModule>]
[DependsOn<PackagePathsParserModule>]
[RunOnlyOnSpecificBuildSystem(Enums.BuildSystem.Unknown)]
[ResolveDependencies]
public class UnlistsPublishedNugetPackageModule : Module<CommandResult[]>
{
    private readonly List<string> ignoredPackages = new List<string>() { "DotnetModularPipelines.Analyzers.Package" };
    private readonly IOptions<NuGetSettings> _nugetSettings;
    private readonly IOptions<PublishSettings> _publishSettings;
    
    protected override Task<bool> ShouldIgnoreFailures(IPipelineContext context, Exception exception)
    {
        return Task.FromResult(true);
    }

    public UnlistsPublishedNugetPackageModule(IOptions<NuGetSettings> nugetSettings, IOptions<PublishSettings> publishSettings)
    {
        _nugetSettings = nugetSettings;
        _publishSettings = publishSettings;
    }

    /// <inheritdoc/>
    protected override async Task OnBeforeExecute(IPipelineContext context)
    {
        var projects = await GetModule<FindProjectsModule>();

        foreach (var project in projects.Value!)
        {
            context.Logger.LogInformation("Unlisting {File}", project);
        }

        await base.OnBeforeExecute(context);
    }

    /// <inheritdoc/>
    protected override Task<SkipDecision> ShouldSkip(IPipelineContext context)
    {
        return Task.FromResult<SkipDecision>(!_publishSettings.Value.ShouldPublish);
    }

    /// <inheritdoc/>
    protected override async Task<CommandResult[]?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(_nugetSettings.Value.ApiKey);

        var projects = await GetModule<FindProjectsModule>();
        var packageVersion = await GetModule<NugetVersionGeneratorModule>();
        var res = await projects.Value!.Where(a=>ignoredPackages.Any(b=>a.Name.Contains(b)))
            .SelectAsync(async project => await context.DotNet().Nuget.Delete(new DotNetNugetDeleteOptions
            {
                NonInteractive = true,
                Source = "https://api.nuget.org/v3/index.json",
                ApiKey = _nugetSettings.Value.ApiKey!,
                Arguments = ["Dotnet" + project.NameWithoutExtension, packageVersion.Value!],
            }, cancellationToken), cancellationToken: cancellationToken)
            .ProcessOneAtATime();
        return res;
    }
}