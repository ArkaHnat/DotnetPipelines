using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("iot", "central", "organization", "delete")]
public record AzIotCentralOrganizationDeleteOptions(
[property: CommandSwitch("--app-id")] string AppId,
[property: CommandSwitch("--org-id")] string OrgId
) : AzOptions
{
    [CommandSwitch("--central-api-uri")]
    public string? CentralApiUri { get; set; }

    [CommandSwitch("--token")]
    public string? Token { get; set; }
}