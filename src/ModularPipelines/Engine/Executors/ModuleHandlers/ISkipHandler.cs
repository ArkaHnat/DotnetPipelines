using ModularPipelines.Models;

namespace ModularPipelines.Engine.Executors.ModuleHandlers;

internal interface ISkipHandler
{
    Task CallbackTask { get; }

    Task SetSkipped(SkipDecision skipDecision);

    void SetUnskip(SkipDecision doNotSkip);

    Task<bool> HandleSkipped();
}