using System.Reflection;
using Initialization.Microsoft.Extensions.DependencyInjection;
using ModularPipelines.Attributes;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Engine;

internal class DependencyChainProvider : IDependencyChainProvider, IInitializer
{
    private readonly IModuleRetriever _moduleRetriever;
    
    public IReadOnlyList<ModuleDependencyModel> ModuleDependencyModels { get; private set; } = null!;

    public DependencyChainProvider(IModuleRetriever moduleRetriever)
    {
        _moduleRetriever = moduleRetriever;
    }

    public int Order => int.MaxValue;

    public async Task InitializeAsync()
    {
        var modules = await _moduleRetriever.GetOrganizedModules();
        ModuleDependencyModels = Detect(modules.AllModules.Where(a => a.GetType().IsAssignableTo(typeof(ModuleBase))).Select(a => a.ToModule).Select(x => new ModuleDependencyModel(x!)).ToList());
    }

    private List<ModuleDependencyModel> Detect(List<ModuleDependencyModel> allModules)
    {
        foreach (var moduleDependencyModel in allModules)
        {
            var dependencies = GetModuleDependencies(moduleDependencyModel, allModules).ToList();

            moduleDependencyModel.IsDependentOn.AddRange(dependencies);

            foreach (var dependencyModel in dependencies)
            {
                dependencyModel.IsDependencyFor.Add(moduleDependencyModel);
            }

            var dependants = GetModuleReliants(moduleDependencyModel, allModules).ToList();

            moduleDependencyModel.IsDependencyFor.AddRange(dependants);

            foreach (var dependencyModel in dependants)
            {
                dependencyModel.IsDependentOn.Add(moduleDependencyModel);
            }

            var targetsToTrigger = GetTargetsToTrigger(moduleDependencyModel, allModules).ToList();

            moduleDependencyModel.IsTriggering.AddRange(targetsToTrigger);

            foreach (var dependencyModel in targetsToTrigger)
            {
                dependencyModel.IsTriggeredBy.Add(moduleDependencyModel);
            }

            var triggeringTargets = GetTriggeringTargets(moduleDependencyModel, allModules).ToList();

            moduleDependencyModel.IsTriggeredBy.AddRange(triggeringTargets);

            foreach (var dependencyModel in triggeringTargets)
            {
                dependencyModel.IsTriggering.Add(moduleDependencyModel);
            }
        }

        return allModules;
    }

    private IEnumerable<ModuleDependencyModel> GetModuleDependencies(ModuleDependencyModel moduleDependencyModel, IReadOnlyCollection<ModuleDependencyModel> allModules)
    {
        var dependencies = moduleDependencyModel.Module.ToModule.GetModuleDependencies();

        foreach (var dependency in dependencies)
        {
            var dependencyModel = GetModuleDependencyModel(dependency.DependencyType, allModules);

            if (dependencyModel is not null)
            {
                yield return dependencyModel;
            }
        }
    }

    private IEnumerable<ModuleDependencyModel> GetModuleReliants(ModuleDependencyModel moduleDependencyModel, IReadOnlyCollection<ModuleDependencyModel> allModules)
    {
        var customAttributes = moduleDependencyModel.Module.GetType().GetCustomAttributes<DependencyForAttribute>(true);
        
        foreach (var dependencyForAttribute in customAttributes)
        {
            var dependency = GetModuleDependencyModel(dependencyForAttribute.Type, allModules);

            if (dependency is not null)
            {
                yield return dependency;
            }
        }
    }

    private IEnumerable<ModuleDependencyModel> GetTriggeringTargets(ModuleDependencyModel moduleDependencyModel, IReadOnlyCollection<ModuleDependencyModel> allModules)
    {
        var customAttributes = moduleDependencyModel.Module.GetType().GetCustomAttributes<TriggeredByAttribute>(true);

        foreach (var dependencyForAttribute in customAttributes)
        {
            var dependency = GetModuleDependencyModel(dependencyForAttribute.Type, allModules);

            if (dependency is not null)
            {
                yield return dependency;
            }
        }
    }

    private IEnumerable<ModuleDependencyModel> GetTargetsToTrigger(ModuleDependencyModel moduleDependencyModel, IReadOnlyCollection<ModuleDependencyModel> allModules)
    {
        var customAttributes = moduleDependencyModel.Module.GetType().GetCustomAttributes<TriggersAttribute>(true);

        foreach (var dependencyForAttribute in customAttributes)
        {
            var dependency = GetModuleDependencyModel(dependencyForAttribute.Type, allModules);

            if (dependency is not null)
            {
                yield return dependency;
            }
        }
    }

    private ModuleDependencyModel? GetModuleDependencyModel(Type type, IEnumerable<ModuleDependencyModel> allModules)
    {
        return allModules.FirstOrDefault(x => x.Module.GetType() == type);
    }
}