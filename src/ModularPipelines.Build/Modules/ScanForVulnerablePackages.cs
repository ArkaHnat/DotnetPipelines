using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

public class ScanForVulnerablePackages : Module<Models.CommandResult>
{
	private readonly string outputDirectory = "output";
	private readonly string outputFile = "vulnerable.json";

	public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

	/// <inheritdoc/>
	protected override async Task<Models.CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
	{
		var outputPath = context.Git().RootDirectory / outputDirectory;
		var options = new DotNet.Options.DotNetListPackageOptions 
		{ 
			WorkingDirectory = context.Git().RootDirectory, 
			Vulnerable = true, 
			Format = "json", 
			IncludeTransitive = true, 
			ProjectSolution = "ModularPipelines.Merged.sln", 
		};
		
		var vulnerablePackages = await context.DotNet().List.Package(options);


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