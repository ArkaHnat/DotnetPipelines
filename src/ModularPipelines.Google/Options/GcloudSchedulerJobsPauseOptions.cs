using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Google.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("scheduler", "jobs", "pause")]
public record GcloudSchedulerJobsPauseOptions(
[property: PositionalArgument] string Job,
[property: PositionalArgument] string Location
) : GcloudOptions;