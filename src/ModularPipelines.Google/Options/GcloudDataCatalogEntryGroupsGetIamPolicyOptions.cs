using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.Google.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("data-catalog", "entry-groups", "get-iam-policy")]
public record GcloudDataCatalogEntryGroupsGetIamPolicyOptions(
[property: PositionalArgument] string EntryGroup,
[property: PositionalArgument] string Location
) : GcloudOptions;