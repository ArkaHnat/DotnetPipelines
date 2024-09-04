using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Context;
using ModularPipelines.Enums;

namespace ModularPipelines.Attributes;

[ExcludeFromCodeCoverage]
public class RunOnlyOnSpecificBuildSystem : RunConditionAttribute
{
    public RunOnlyOnSpecificBuildSystem(BuildSystem buildSystem)
    {
        BuildSystem = buildSystem;
    }

    public BuildSystem BuildSystem { get; }

    /// <inheritdoc/>
    public override Task<bool> Condition(IPipelineHookContext pipelineContext)
    {
        return Task.FromResult(this.BuildSystem == BuildSystem.Unknown);
    }
}