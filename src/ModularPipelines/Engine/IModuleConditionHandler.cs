using ModularPipelines.Attributes;
using ModularPipelines.Modules;

namespace ModularPipelines.Engine;

internal interface IModuleConditionHandler
{
    Task<bool> ShouldIgnore(ModuleBase module);

    void UnskipDependencies(IEnumerable<DependsOnAttribute> attributes, IEnumerable<ModuleBase> modules);
}