using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModularPipelines.Interfaces;
using ModularPipelines.Models;
using ModularPipelines.Options;

namespace ModularPipelines.Engine;

internal class DependencyPrinter : IDependencyPrinter
{
    private readonly IDependencyChainProvider _dependencyChainProvider;
    private readonly ILogger<DependencyPrinter> _logger;
    private readonly IInternalCollapsableLogging _collapsableLogging;
    private readonly IOptions<PipelineOptions> _options;

    public DependencyPrinter(IDependencyChainProvider dependencyChainProvider,
        ILogger<DependencyPrinter> logger,
        IInternalCollapsableLogging collapsableLogging,
        IOptions<PipelineOptions> options)
    {
        _dependencyChainProvider = dependencyChainProvider;
        _logger = logger;
        _collapsableLogging = collapsableLogging;
        _options = options;
    }

    public void PrintDependencyChains()
    {
        if (!_options.Value.PrintDependencyChains)
        {
            return;
        }
        
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
            items.ToArray().Reverse();
            var orderedString = string.Join("\r\n", items);

            stringBuilder.AppendLine(orderedString);
            stringBuilder.AppendLine("\r\n");
        }

        alreadyPrinted.Clear();
        
        Print(stringBuilder.ToString());
    }

    private void Print(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }
        
        _logger.LogDebug("\n");
        _collapsableLogging.StartConsoleLogGroupDirectToConsole("Dependency Chains", LogLevel.Debug);
        _logger.LogDebug("The following dependency chains have been detected:\r\n{Chain}", value);
        _collapsableLogging.EndConsoleLogGroupDirectToConsole("Dependency Chains", LogLevel.Debug);
        _logger.LogDebug("\n");
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

            if (dashCount < 2)
            {
                dashCount = 3;
                stringBuilder.Replace("-> ", "---> ");
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
                dashCount = 3;
                stringBuilder.Replace("-> ", "---> ");
            }

            Append(stringBuilder, dependencyModel, dashCount - 2, alreadyPrinted);
        }
    }
}