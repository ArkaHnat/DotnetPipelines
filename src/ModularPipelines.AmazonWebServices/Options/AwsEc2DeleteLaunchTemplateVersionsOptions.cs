using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.AmazonWebServices.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("ec2", "delete-launch-template-versions")]
public record AwsEc2DeleteLaunchTemplateVersionsOptions(
[property: CommandSwitch("--versions")] string[] Versions
) : AwsOptions
{
    [CommandSwitch("--launch-template-id")]
    public string? LaunchTemplateId { get; set; }

    [CommandSwitch("--launch-template-name")]
    public string? LaunchTemplateName { get; set; }

    [CommandSwitch("--generate-cli-skeleton")]
    public string? GenerateCliSkeleton { get; set; }
}