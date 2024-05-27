using System.Text;
using Microsoft.Extensions.Logging;
using ModularPipelines.Interfaces;
using ModularPipelines.Models;

namespace ModularPipelines.Engine;

internal class DependencyPrinter : IDependencyPrinter
{
    private readonly IDependencyChainProvider _dependencyChainProvider;
    private readonly ILogger<DependencyPrinter> _logger;
    private readonly IInternalCollapsableLogging _collapsableLogging;

    public DependencyPrinter(IDependencyChainProvider dependencyChainProvider,
        ILogger<DependencyPrinter> logger,
        IInternalCollapsableLogging collapsableLogging)
    {
        _dependencyChainProvider = dependencyChainProvider;
        _logger = logger;
        _collapsableLogging = collapsableLogging;
    }

    public void PrintDependencyChains()
    {
        var alreadyPrinted = new HashSet<ModuleDependencyModel>();

        var stringBuilder = new StringBuilder();

        foreach (var moduleDependencyModel in _dependencyChainProvider.ModuleDependencyModels.OrderBy(m => m.AllDescendantDependencies().Count()))
        {
            var internalStringBuilder = new StringBuilder();
            if (alreadyPrinted.Contains(moduleDependencyModel))
            {
                continue;
            }

            internalStringBuilder.AppendLine();
            Append(internalStringBuilder, moduleDependencyModel, 1, alreadyPrinted);
            var items = new List<string>(internalStringBuilder.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            items.Sort();
            var orderedString = string.Join("\r\n", items.ToArray().Reverse());
            
            stringBuilder.AppendLine(orderedString);

            stringBuilder.AppendLine("\r\n");
        }

        alreadyPrinted.Clear();
        
        Print(stringBuilder.ToString());
    }

    private void Print(string value)
    {
        Console.WriteLine();
        _collapsableLogging.StartConsoleLogGroupDirectToConsole("Dependency Chains");
        _logger.LogInformation("The following dependency chains have been detected:\r\n{Chain}", value);
        _collapsableLogging.EndConsoleLogGroupDirectToConsole("Dependency Chains");
        Console.WriteLine();
    }

    private void Append(StringBuilder stringBuilder, ModuleDependencyModel moduleDependencyModel, int dashCount, ISet<ModuleDependencyModel> alreadyPrinted)
    {
        alreadyPrinted.Add(moduleDependencyModel);

        stringBuilder.Append(new string('-', dashCount));
        stringBuilder.Append('>');
        stringBuilder.Append(' ');
        stringBuilder.AppendLine(moduleDependencyModel.Module.GetType()
            .Name);

        foreach (var dependencyModel in moduleDependencyModel.IsDependencyFor)
        {
            if (alreadyPrinted.Contains(dependencyModel))
            {
                continue;
            }

            Append(stringBuilder, dependencyModel, dashCount + 2, alreadyPrinted);
        }

        foreach (var dependencyModel in moduleDependencyModel.IsDependentOn)
        {
            if (alreadyPrinted.Contains(dependencyModel))
            {
                continue;
            }

            Append(stringBuilder, dependencyModel, dashCount - 2, alreadyPrinted);
        }

        foreach (var dependencyModel in moduleDependencyModel.IsTriggering)
        {
            if (alreadyPrinted.Contains(dependencyModel))
            {
                continue;
            }

            Append(stringBuilder, dependencyModel, dashCount + 2, alreadyPrinted);
        }

        foreach (var dependencyModel in moduleDependencyModel.IsTriggeredBy)
        {
            if (alreadyPrinted.Contains(dependencyModel))
            {
                continue;
            }

            if (dashCount < 2)
            {
                dashCount = 2;
            }

            Append(stringBuilder, dependencyModel, dashCount - 2, alreadyPrinted);
        }
    }
}