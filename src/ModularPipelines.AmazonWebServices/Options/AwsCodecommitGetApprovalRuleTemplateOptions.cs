using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.AmazonWebServices.Options;

[ExcludeFromCodeCoverage]
[CommandPrecedingArguments("codecommit", "get-approval-rule-template")]
public record AwsCodecommitGetApprovalRuleTemplateOptions(
[property: CommandSwitch("--approval-rule-template-name")] string ApprovalRuleTemplateName
) : AwsOptions
{
    [CommandSwitch("--generate-cli-skeleton")]
    public string? GenerateCliSkeleton { get; set; }
}