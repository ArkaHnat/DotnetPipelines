using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using ModularPipelines.Attributes;
using ModularPipelines.Modules;

namespace ModularPipelines.Examples;

public static class ModularPipelinesHelpers
{
    public static Type[] Types => Assembly.GetAssembly(typeof(Program))!.GetTypes().Where(a => a.IsAssignableTo(typeof(ModuleBase))).ToArray();

    public static Type GetTypeByName(string name)
    {
        var moduleDictionary = Types.ToDictionary(x => x.FullName!.Split(".").Last(), x => x);
        return moduleDictionary[name];
    }

    public static async Task InjectRequiredModulesAsync(this IServiceCollection collection, string[] moduleNames)
    {
        await Parallel.ForEachAsync(moduleNames, async (moduleName, _) =>
        {
            var moduleType = GetTypeByName(moduleName);
            await ActivateDependenciesAsync(collection, moduleType);
        });
    }
#pragma warning restore CS8321 // Local function is declared but never used

    public static void InjectRequiredModules(this IServiceCollection collection, string[] moduleNames)
    {
        foreach (var moduleName in moduleNames)
        {
            var moduleType = GetTypeByName(moduleName);
            ActivateDependencies(collection, moduleType, true);
        }
    }

    private static void ActivateDependencies(IServiceCollection collection, Type typeToActivate, bool isRequiredModule = false)
    {
        var activatedType = Activator.CreateInstance(typeToActivate) as IModule;
        if (activatedType == null)
        {
            return;
        }

        var aT = activatedType as ModuleBase;
        if (collection.All(a => a.ImplementationType != typeToActivate) && collection.All(a => a.ImplementationInstance?.GetType() != typeToActivate))
        {
            collection.AddSingleton<IModule>(activatedType);

        // Load OptionalDependency (dependsfor)
            var relatedTypes = Types
                .Where(a => a.GetCustomAttributes<DependencyForAttribute>(true).Any(a => a.Type == typeToActivate) && !collection.Any(x => x.ServiceType == a));

            // loadOnlyDependencies
            // IModuleRelation[] relations = [.. activatedType.DependentModules];

            // Load Dependencies and Reliants
            IModuleRelation[] relations = [.. aT!.DependentModules, .. aT.ReliantModules];

            foreach (var relatedType in relatedTypes)
            {
                if (collection.All(x => x.ServiceType != relatedType) && collection.All(a =>a.ImplementationInstance?.GetType() != relatedType))
                {
                    ActivateDependencies(collection, relatedType);
                }
            }

            foreach (var relation in relations)
            {
                if (collection.All(x => x.ServiceType != relation.Type) && collection.All(a=>a.ImplementationInstance?.GetType()!=relation.Type))
                {
                    ActivateDependencies(collection, relation.Type);
                }
            }
        }
    }

    private static async Task ActivateDependenciesAsync(IServiceCollection collection, Type typeToActivate)
    {
        var activatedType = Activator.CreateInstance(typeToActivate);
        if (activatedType == null)
        {
            return;
        }

        var aT = activatedType as ModuleBase;
        collection.AddSingleton(aT!);

        IModuleRelation[] relations = [.. aT!.DependentModules, .. aT.ReliantModules];
        await Parallel.ForEachAsync(relations, async (relation, _) =>
        {
            if (!collection.Any(x => x.ServiceType == relation.Type))
            {
                await ActivateDependenciesAsync(collection, relation.Type);
            }
        });
    }
}
