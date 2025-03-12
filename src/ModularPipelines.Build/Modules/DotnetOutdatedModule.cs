using Microsoft.Extensions.Logging;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Services.Tools.DotnetOutdated;
using ModularPipelines.DotNet.Services.Tools.SonarScanner;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

public class DotnetOutdatedToolModule : Module<CommandResult>
{
    public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

    /// <inheritdoc/>
    protected override async Task<CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var currentDIrectorty = Directory.GetCurrentDirectory();
        
        var options = new DotnetToolOutdatedRunOptions(context.Git().RootDirectory / "ModularPipelines.Merged.sln");
        options.Upgrade = true;
		var result = await context.DotNet().Tool.DotnetOutdated.Run(options);

        return result;
    }
    

    /// <inheritdoc/>
    protected override async Task OnAfterExecute(IPipelineContext context)
    {
        var moduleResult = await this;
        context.Logger.LogInformation("Restored dotnet tools.");
    }
}