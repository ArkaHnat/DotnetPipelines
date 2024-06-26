using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Google.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("service-directory", "services", "describe")]
public record GcloudServiceDirectoryServicesDescribeOptions(
[property: PositionalArgument] string Service,
[property: PositionalArgument] string Location,
[property: PositionalArgument] string Namespace
) : GcloudOptions;