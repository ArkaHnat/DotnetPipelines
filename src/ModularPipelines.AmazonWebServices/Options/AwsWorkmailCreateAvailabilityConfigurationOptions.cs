using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.AmazonWebServices.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("workmail", "create-availability-configuration")]
public record AwsWorkmailCreateAvailabilityConfigurationOptions(
[property: CommandSwitch("--organization-id")] string OrganizationId,
[property: CommandSwitch("--domain-name")] string DomainName
) : AwsOptions
{
    [CommandSwitch("--client-token")]
    public string? ClientToken { get; set; }

    [CommandSwitch("--ews-provider")]
    public string? EwsProvider { get; set; }

    [CommandSwitch("--lambda-provider")]
    public string? LambdaProvider { get; set; }

    [CommandSwitch("--generate-cli-skeleton")]
    public string? GenerateCliSkeleton { get; set; }
}