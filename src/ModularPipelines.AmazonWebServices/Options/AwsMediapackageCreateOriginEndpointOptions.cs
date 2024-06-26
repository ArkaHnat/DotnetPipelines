using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;
using ModularPipelines.Models;

namespace ModularPipelines.AmazonWebServices.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("mediapackage", "create-origin-endpoint")]
public record AwsMediapackageCreateOriginEndpointOptions(
[property: CommandSwitch("--channel-id")] string ChannelId,
[property: CommandSwitch("--id")] string Id
) : AwsOptions
{
    [CommandSwitch("--authorization")]
    public string? Authorization { get; set; }

    [CommandSwitch("--cmaf-package")]
    public string? CmafPackage { get; set; }

    [CommandSwitch("--dash-package")]
    public string? DashPackage { get; set; }

    [CommandSwitch("--description")]
    public string? Description { get; set; }

    [CommandSwitch("--hls-package")]
    public string? HlsPackage { get; set; }

    [CommandSwitch("--manifest-name")]
    public string? ManifestName { get; set; }

    [CommandSwitch("--mss-package")]
    public string? MssPackage { get; set; }

    [CommandSwitch("--origination")]
    public string? Origination { get; set; }

    [CommandSwitch("--startover-window-seconds")]
    public int? StartoverWindowSeconds { get; set; }

    [CommandSwitch("--tags")]
    public IEnumerable<KeyValue>? Tags { get; set; }

    [CommandSwitch("--time-delay-seconds")]
    public int? TimeDelaySeconds { get; set; }

    [CommandSwitch("--whitelist")]
    public string[]? Whitelist { get; set; }

    [CommandSwitch("--generate-cli-skeleton")]
    public string? GenerateCliSkeleton { get; set; }
}