using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("billing", "role-assignment", "list")]
public record AzBillingRoleAssignmentListOptions(
[property: CommandSwitch("--account-name")] int AccountName
) : AzOptions
{
    [CommandSwitch("--invoice-section-name")]
    public string? InvoiceSectionName { get; set; }

    [CommandSwitch("--profile-name")]
    public string? ProfileName { get; set; }
}