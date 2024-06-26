using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.AmazonWebServices.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("tnb", "get-sol-function-package-content")]
public record AwsTnbGetSolFunctionPackageContentOptions(
[property: CommandSwitch("--accept")] string Accept,
[property: CommandSwitch("--vnf-pkg-id")] string VnfPkgId
) : AwsOptions;