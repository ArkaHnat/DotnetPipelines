using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Azure.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("keyvault", "certificate", "contact", "list")]
public record AzKeyvaultCertificateContactListOptions(
[property: CommandSwitch("--vault-name")] string VaultName
) : AzOptions;