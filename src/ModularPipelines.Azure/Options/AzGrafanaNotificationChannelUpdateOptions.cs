using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("grafana", "notification-channel", "update")]
public record AzGrafanaNotificationChannelUpdateOptions(
[property: CommandSwitch("--definition")] string Definition,
[property: CommandSwitch("--name")] string Name,
[property: CommandSwitch("--notification-channel")] string NotificationChannel
) : AzOptions
{
    [CommandSwitch("--api-key")]
    public string? ApiKey { get; set; }

    [CommandSwitch("--resource-group")]
    public string? ResourceGroup { get; set; }
}