﻿using EnumerableAsyncProcessor.Extensions;
using Microsoft.Extensions.Logging;
using ModularPipelines.Enums;
using ModularPipelines.Exceptions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.Engine.Executors.ModuleHandlers;

internal class WaitHandler<T> : BaseHandler<T>, IWaitHandler
{
    public WaitHandler(Module<T> module) : base(module)
    {
    }

    public async Task<WaitResult> WaitForModuleDependencies()
    {
        try
        {
            await WaitForDependencies();
        }
        catch when (EngineCancellationToken.IsCancellationRequested && Module.ModuleRunType == ModuleRunType.OnSuccessfulDependencies)
        {
            // The Engine has requested a cancellation due to failures - So fail fast and don't repeat exceptions thrown by other modules.
            Context.Logger.LogDebug("The pipeline has been cancelled before this module started");

            ModuleResultTaskCompletionSource.TrySetCanceled();
            return WaitResult.Abort;
        }

        return WaitResult.Continue;
    }

    private async Task WaitForDependencies()
    {
        if (!Module.DependentModules.Any())
        {
            Context.Logger.LogDebug("No dependent modules - Nothing to wait for");
            return;
        }

        try
        {
            await Module.DependentModules
                .ToAsyncProcessorBuilder()
                .ForEachAsync(dependsOnAttribute =>
                {
                    var module = Context.GetModule(dependsOnAttribute.Type);

                    if (dependsOnAttribute.IgnoreIfNotRegistered && module is null)
                    {
                        Context.Logger.LogDebug("{Module} was not registered so not waiting", dependsOnAttribute.Type.Name);
                        return Task.CompletedTask;
                    }

                    if (module is null)
                    {
                        throw new ModuleNotRegisteredException(
                            $"The module {dependsOnAttribute.Type.Name} has not been registered", null);
                    }

                    Context.Logger.LogDebug("Waiting for {Module}", dependsOnAttribute.Type.Name);

                    return module.ResultTaskInternal;
                })
                .ProcessInParallel();
        }
        catch (Exception e) when (Module.ModuleRunType == ModuleRunType.AlwaysRun)
        {
            Context.Logger.LogError(e, "Ignoring Exception due to 'AlwaysRun' set");
        }
    }
}