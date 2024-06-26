using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Google.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("container", "fleet", "fleetobservability", "enable")]
public record GcloudContainerFleetFleetobservabilityEnableOptions : GcloudOptions;