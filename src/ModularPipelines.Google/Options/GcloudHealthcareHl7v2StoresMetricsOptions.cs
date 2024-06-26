using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Google.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("healthcare", "hl7v2-stores", "metrics")]
public record GcloudHealthcareHl7v2StoresMetricsOptions(
[property: PositionalArgument] string HL7V2Store,
[property: PositionalArgument] string Dataset,
[property: PositionalArgument] string Location
) : GcloudOptions;