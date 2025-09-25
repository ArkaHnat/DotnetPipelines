using DotnetModularPipelines.DotNet.Options;
using Microsoft.Extensions.Logging;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

public class DotnetToolUpdateModule : Module<(string packageId, string version)?>
{
    public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

    /// <inheritdoc/>
    protected override async Task<(string packageId, string version)?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        _ = Directory.GetCurrentDirectory();
        var list = await context.DotNet().Tool.List(new DotNetToolListOptions
        {
            Format = DotNetToolListOptions.DotnetToolListFormat.Json,
        });

        foreach (var item in list.data)
        {
            var result = await context.DotNet().Tool.Update(new DotNet.Options.DotNetToolUpdateOptions(item.packageId)
            {
                Local = true,
            });
            if (result.StandardOutput.Contains("is up to date", StringComparison.InvariantCultureIgnoreCase))
            {
                continue;
            }

            return (item.packageId, item.version);
        }

        return null;
    }

    /// <inheritdoc/>
    protected override async Task OnAfterExecute(IPipelineContext context)
    {
        _ = await this;
        context.Logger.LogInformation("Restored dotnet tools.");
    }
}
