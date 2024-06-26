using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.AmazonWebServices.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("elb", "wait", "any-instance-in-service")]
public record AwsElbWaitAnyInstanceInServiceOptions(
[property: CommandSwitch("--load-balancer-name")] string LoadBalancerName
) : AwsOptions
{
    [CommandSwitch("--instances")]
    public string[]? Instances { get; set; }

    [CommandSwitch("--generate-cli-skeleton")]
    public string? GenerateCliSkeleton { get; set; }
}