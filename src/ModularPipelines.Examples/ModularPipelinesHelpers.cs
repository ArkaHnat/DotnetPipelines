using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using ModularPipelines.Attributes;
using ModularPipelines.Extensions;
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

    public static void InjectRequiredModules(this IServiceCollection collection, string[] moduleNames)
    {
        foreach (var moduleName in moduleNames)
        {
            var types = AppDomain.CurrentDomain
                        .GetAssemblies()
                        .SelectMany(a => a.GetTypes())
                        .Where(t => t.IsAssignableTo(typeof(ModuleBase)))
                        .Where(t => !t.IsAbstract)
                        .ToArray();
            var moduleType = GetTypeByName(moduleName);
            collection.ActivateDependencies(moduleType, types);
        }
    }
}
