using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Services.Tools.SonarScanner;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Examples.Modules;

public class SonarCubeModule : Module<CommandResult>
{
	/// <inheritdoc/>
	protected override async Task<CommandResult> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
	{

		await context.DotNet().Tool.SonarCubeScanner.InstallToolAsync( new DotNetToolSonarScannerInstallOptions());
		await context.DotNet().Tool.SonarCubeScanner.BeginScanAsync(new DotNetToolSonarScannerBeginOptions("ProjectName","org"){  Verbose = true, Timeout=10 }, cancellationToken);
		return await context.DotNet().Tool.SonarCubeScanner.EndScanAsync(new DotNetToolSonarScannerEndOptions() { }, cancellationToken);
	}
}