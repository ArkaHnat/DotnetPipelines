using System.Reflection;
using EnumerableAsyncProcessor.Extensions;
using Microsoft.Extensions.Options;
using ModularPipelines.Attributes;
using ModularPipelines.Models;
using ModularPipelines.Modules;
using ModularPipelines.Options;

namespace ModularPipelines.Engine;

internal class ModuleConditionHandler : IModuleConditionHandler
{
    private readonly IOptions<PipelineOptions> _pipelineOptions;

    public ModuleConditionHandler(IOptions<PipelineOptions> pipelineOptions)
    {
        _pipelineOptions = pipelineOptions;
    }

    public async Task<bool> ShouldIgnore(ModuleBase module)
    {
        if (IsIgnoreCategory(module))
        {
            await module.SkipHandler.SetSkipped("A category of this module has been ignored");
            return true;
        }

        if (!IsRunnableCategory(module))
        {
            await module.SkipHandler.SetSkipped("The module was not in a runnable category");
            return true;
        }

        if (!IsRunnableModule(module))
        {
            await module.SkipHandler.SetSkipped("The module was not in a runnable modules");
            return true;
        }

        return !await IsRunnableCondition(module);
    }

    public void UnskipDependencies(IEnumerable<IModuleRelation> attributes, IEnumerable<ModuleBase> modules)
    {
        attributes.ToList().ForEach(a=>UnskipDependencies(a, modules));
    }

    private IEnumerable<ModuleBase> UnskipDependencies(IModuleRelation attribute, IEnumerable<ModuleBase> modules)
    {
        var dependency = modules.FirstOrDefault(a => a.GetType() == attribute.Type);
        if (dependency != null)
        {
            dependency.SkipHandler.SetUnskip(SkipDecision.DoNotSkip);
            
            return dependency.DependentModules.SelectMany(a=>UnskipDependencies(a, modules)).ToList();
        }

        return Enumerable.Empty<ModuleBase>();
    }

    private bool IsRunnableCategory(ModuleBase module)
    {
        var runOnlyCategories = _pipelineOptions.Value.RunOnlyCategories?.ToArray();

        if (runOnlyCategories?.Any() != true)
        {
            return true;
        }

        var category = module.GetType().GetCustomAttribute<ModuleCategoryAttribute>();

        return category != null && runOnlyCategories.Contains(category.Category);
    }

    private bool IsRunnableModule(ModuleBase module)
    {
        var runOnlyModules = _pipelineOptions.Value.RunOnlyModules?.ToArray();

        if (runOnlyModules?.Any() != true)
        {
            return true;
        }

        return runOnlyModules.Contains(module.TypeName);
    }

    private bool IsIgnoreCategory(ModuleBase module)
    {
        var ignoreCategories = _pipelineOptions.Value.IgnoreCategories?.ToArray();

        if (ignoreCategories?.Any() != true)
        {
            return false;
        }

        var category = module.GetType().GetCustomAttribute<ModuleCategoryAttribute>();

        return category != null && ignoreCategories.Contains(category.Category);
    }

    private async Task<bool> IsRunnableCondition(ModuleBase module)
    {
        var mandatoryRunConditionAttributes = module.GetType().GetCustomAttributes<MandatoryRunConditionAttribute>(true).ToList();
        var runConditionAttributes = module.GetType().GetCustomAttributes<RunConditionAttribute>(true).Except(mandatoryRunConditionAttributes).ToList();

        var mandatoryConditionResults = await mandatoryRunConditionAttributes.ToAsyncProcessorBuilder()
            .SelectAsync(async runConditionAttribute => new RunnableConditionMet(await runConditionAttribute.Condition(module.Context), runConditionAttribute))
            .ProcessInParallel();

        var mandatoryCondition = mandatoryConditionResults.FirstOrDefault(result => !result.ConditionMet);

        if (mandatoryCondition != null)
        {
            await module.SkipHandler.SetSkipped($"A condition to run this module has not been met - {mandatoryCondition.RunConditionAttribute.GetType().Name}");
            return false;
        }

        if (!runConditionAttributes.Any())
        {
            return true;
        }

        var conditionResults = await runConditionAttributes.ToAsyncProcessorBuilder()
            .SelectAsync(async runConditionAttribute => new RunnableConditionMet(await runConditionAttribute.Condition(module.Context), runConditionAttribute))
            .ProcessInParallel();

        var runnableCondition = conditionResults.FirstOrDefault(result => result.ConditionMet);

        if (runnableCondition != null)
        {
            return true;
        }

        await module.SkipHandler.SetSkipped($"No run conditions were met: {string.Join(", ", runConditionAttributes.Select(x => x.GetType().Name.Replace("Attribute", string.Empty, StringComparison.OrdinalIgnoreCase)))}");

        return false;
    }

    private record RunnableConditionMet(bool ConditionMet, RunConditionAttribute RunConditionAttribute);
}