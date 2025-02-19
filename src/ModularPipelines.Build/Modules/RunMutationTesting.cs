using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Build.Modules;

public class RunMutationTesting : Module<CommandResult>
{
    /// <inheritdoc/>
    protected override async Task<CommandResult?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
    {
        var result = await context.DotNet().Tool.Custom(new DotNet.Options.DotnetCustomToolOptions("stryker")
        {
            Arguments = new[] { "--solution ModularPipelines.Merged.sln --log-to-file --dev-mode --output ./buildOutput/" },
        });

        return result;
    }

    /// <inheritdoc/>
    protected override async Task OnAfterExecute(IPipelineContext context)
    {
        var moduleResult = await this;
        context.Logger.LogInformation("Restored dotnet tools.");
    }
}