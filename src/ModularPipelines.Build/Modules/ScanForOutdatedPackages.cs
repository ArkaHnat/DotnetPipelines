using Microsoft.Extensions.Logging;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using ModularPipelines.DotNet.Services.Tools.SonarScanner;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

public class ScanForOutdatedPackages : Module<CommandResult>
{
	private readonly string outputDirectory = "output";
	private readonly string outputFile = "outdated.json";
	public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

    /// <inheritdoc/>
    protected override async Task<CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var vulnerablePackages = await context.DotNet().List.Package(new DotNet.Options.DotNetListPackageOptions { WorkingDirectory = context.Git().RootDirectory, Outdated = true, Format = "json", IncludeTransitive = true, ProjectSolution = "ModularPipelines.Merged.sln" });
		
		var outputPath = context.Git().RootDirectory / outputDirectory;
		if (!Directory.Exists(outputDirectory))
		{
			_ = Directory.CreateDirectory(outputPath);
		}

		using (var fs = File.CreateText(outputPath / outputFile))
		{
			fs.Write(vulnerablePackages.StandardOutput);
		}
		return vulnerablePackages;
    }   
}