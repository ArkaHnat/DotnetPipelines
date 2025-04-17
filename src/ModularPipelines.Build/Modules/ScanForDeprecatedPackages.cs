using Microsoft.Extensions.Logging;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using ModularPipelines.DotNet.Services.Tools.SonarScanner;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

public class ScanForDeprecatedPackages : Module<CommandResult>
{
	private readonly string outputDirectory = "output";
	private readonly string outputFile = "deprecated.json";
	public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

    /// <inheritdoc/>
    protected override async Task<CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var options = new DotNet.Options.DotNetListPackageOptions { WorkingDirectory = context.Git().RootDirectory, Deprecated = true, Format = "json", IncludeTransitive = true, ProjectSolution = "ModularPipelines.Merged.sln" };

		var vulnerablePackages = await context.DotNet().List.Package(options);

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