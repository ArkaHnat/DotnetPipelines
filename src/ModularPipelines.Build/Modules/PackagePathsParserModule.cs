using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.Modules;
using File = ModularPipelines.FileSystem.File;

namespace ModularPipelines.Build.Modules;

[DependsOn<PackProjectsModule>]
[ResolveDependencies]
public class PackagePathsParserModule : Module<List<File>>
{
    /// <inheritdoc/>
    protected override async Task<List<File>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var packPackagesModuleResult = await GetModule<PackProjectsModule>();

		return packPackagesModuleResult.Value!
			.Where(x => !string.IsNullOrWhiteSpace(x.StandardOutput))
			.Select(x => x.StandardOutput)
            .Where(x=>x.Contains("Successfully created package "))
            .Select(x => x.Split("Successfully created package '")?[1])
            .Where(x=> x!= null)
			.Select(x => x.Split("'.")[0])
            .Select(x => new File(x))
            .ToList();
    }
}