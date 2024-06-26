using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("maintenance", "configuration", "create")]
public record AzMaintenanceConfigurationCreateOptions(
[property: CommandSwitch("--name")] string Name,
[property: CommandSwitch("--resource-group")] string ResourceGroup
) : AzOptions
{
    [CommandSwitch("--duration")]
    public string? Duration { get; set; }

    [CommandSwitch("--expiration-date-time")]
    public string? ExpirationDateTime { get; set; }

    [CommandSwitch("--extension-properties")]
    public string? ExtensionProperties { get; set; }

    [CommandSwitch("--install-patches-linux-parameters")]
    public string? InstallPatchesLinuxParameters { get; set; }

    [CommandSwitch("--install-patches-windows-parameters")]
    public string? InstallPatchesWindowsParameters { get; set; }

    [CommandSwitch("--location")]
    public string? Location { get; set; }

    [CommandSwitch("--maintenance-scope")]
    public string? MaintenanceScope { get; set; }

    [CommandSwitch("--maintenance-window-recur-every")]
    public string? MaintenanceWindowRecurEvery { get; set; }

    [CommandSwitch("--maintenance-window-start-date-time")]
    public string? MaintenanceWindowStartDateTime { get; set; }

    [CommandSwitch("--maintenance-window-time-zone")]
    public string? MaintenanceWindowTimeZone { get; set; }

    [CommandSwitch("--namespace")]
    public string? Namespace { get; set; }

    [CommandSwitch("--reboot-setting")]
    public string? RebootSetting { get; set; }

    [CommandSwitch("--tags")]
    public string? Tags { get; set; }

    [CommandSwitch("--visibility")]
    public string? Visibility { get; set; }
}