using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.AmazonWebServices.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("sagemaker", "list-hub-contents")]
public record AwsSagemakerListHubContentsOptions(
[property: CommandSwitch("--hub-name")] string HubName,
[property: CommandSwitch("--hub-content-type")] string HubContentType
) : AwsOptions
{
    [CommandSwitch("--name-contains")]
    public string? NameContains { get; set; }

    [CommandSwitch("--max-schema-version")]
    public string? MaxSchemaVersion { get; set; }

    [CommandSwitch("--creation-time-before")]
    public long? CreationTimeBefore { get; set; }

    [CommandSwitch("--creation-time-after")]
    public long? CreationTimeAfter { get; set; }

    [CommandSwitch("--sort-by")]
    public string? SortBy { get; set; }

    [CommandSwitch("--sort-order")]
    public string? SortOrder { get; set; }

    [CommandSwitch("--max-results")]
    public int? MaxResults { get; set; }

    [CommandSwitch("--next-token")]
    public string? NextToken { get; set; }

    [CommandSwitch("--generate-cli-skeleton")]
    public string? GenerateCliSkeleton { get; set; }
}