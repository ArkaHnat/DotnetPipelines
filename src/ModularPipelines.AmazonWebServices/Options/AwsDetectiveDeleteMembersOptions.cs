using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.AmazonWebServices.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("detective", "delete-members")]
public record AwsDetectiveDeleteMembersOptions(
[property: CommandSwitch("--graph-arn")] string GraphArn,
[property: CommandSwitch("--account-ids")] string[] AccountIds
) : AwsOptions
{
    [CommandSwitch("--generate-cli-skeleton")]
    public string? GenerateCliSkeleton { get; set; }
}