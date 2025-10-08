using System.Text.RegularExpressions;
using DotnetModularPipelines.DotNet.Options;
using Microsoft.Extensions.Logging;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

public class DotnetToolUpdateModule : Module<(string packageId, string fromVersion, string toVersion)?>
{
    public override ModuleRunType ModuleRunType => ModuleRunType.BeforePipeline;

    /// <inheritdoc/>
    protected override async Task<(string packageId, string fromVersion, string toVersion)?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
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

            var pattern = @"Tool '(?<package>[^']+)' was successfully updated from version '(?<oldVersion>[^']+)' to version '(?<newVersion>[^']+)'";

            var matches = Regex.Matches(result.StandardOutput, pattern);

            var oldVersion = string.Empty;
            var newVersion= string.Empty;
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    var package = match.Groups["package"].Value;
                    oldVersion = match.Groups["oldVersion"].Value;
                    newVersion = match.Groups["newVersion"].Value;
                }
            }

            return (item.packageId, oldVersion, newVersion);
        }

        return ("", "", "");
    }

    /// <inheritdoc/>
    protected override async Task OnAfterExecute(IPipelineContext context)
    {
        _ = await this;
        context.Logger.LogInformation("Restored dotnet tools.");
    }
}
