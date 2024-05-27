using ModularPipelines.Attributes;
using ModularPipelines.Extensions;
using ModularPipelines.Helpers;
using ModularPipelines.Modules;

namespace ModularPipelines.Engine;

internal class DependencyDetector : IDependencyDetector
{
    private readonly IUnusedModuleDetector _unusedModuleDetector;
    private readonly IDependencyCollisionDetector _dependencyCollisionDetector;
    private readonly IDependencyPrinter _dependencyPrinter;
    private readonly IEnumerable<IModule> modules;

    public DependencyDetector(IUnusedModuleDetector unusedModuleDetector,
        IDependencyCollisionDetector dependencyCollisionDetector,
        IDependencyPrinter dependencyPrinter,
        IEnumerable<IModule> modules)
    {
        _unusedModuleDetector = unusedModuleDetector;
        _dependencyCollisionDetector = dependencyCollisionDetector;
        _dependencyPrinter = dependencyPrinter;
        this.modules = modules;
    }

    public void Check()
    {
        _unusedModuleDetector.Log();
        _dependencyCollisionDetector.CheckCollisions();
        _dependencyPrinter.PrintDependencyChains();
    }

    public void ResolveRelations()
    {
        foreach (var module in modules)
        {
            var moduleType = module.GetType();
            foreach (var relatedModule in moduleType.GetCustomAttributesIncludingBaseInterfaces<DependsOnAttribute>())
            {
                module.ToModule.DependentModules.Add(relatedModule);
                var resolvedModule = modules.FirstOrDefault(a => a.ToModule.GetType() == relatedModule.Type);

                resolvedModule?.ToModule.ReliantModules.Add(new DependencyForAttribute(moduleType));
            }

            foreach (var relatedModule in moduleType.GetCustomAttributesIncludingBaseInterfaces<DependencyForAttribute>())
            {
                module.ToModule.ReliantModules.Add(relatedModule);

                var resolvedModule = modules.FirstOrDefault(a => a.ToModule.GetType() == relatedModule.Type);

                resolvedModule?.ToModule.DependentModules.Add(new DependsOnAttribute(moduleType));
            }

            foreach (var relatedModule in moduleType.GetCustomAttributesIncludingBaseInterfaces<TriggersAttribute>())
            {
                module.ToModule.TriggersModules.Add(relatedModule);
                var resolvedModule = modules.FirstOrDefault(a => a.ToModule.GetType() == relatedModule.Type);

                resolvedModule?.ToModule.TriggeredByModules.Add(new TriggersAttribute(moduleType));
            }

            foreach (var relatedModule in moduleType.GetCustomAttributesIncludingBaseInterfaces<TriggeredByAttribute>())
            {
                module.ToModule.TriggeredByModules.Add(relatedModule);

                var resolvedModule = modules.FirstOrDefault(a => a.ToModule.GetType() == relatedModule.Type);

                resolvedModule?.ToModule.TriggersModules.Add(new TriggeredByAttribute(moduleType));
            }
        }
    }
}