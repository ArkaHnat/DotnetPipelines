﻿using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ModularPipelines.Context;
using ModularPipelines.Engine;

namespace ModularPipelines.MicrosoftTeams.Extensions;

public static class MicrosoftTeamsExtensions
{
#pragma warning disable CA2255
    [ModuleInitializer]
#pragma warning restore CA2255
    public static void RegisterMicrosoftTeamsContext()
    {
        ServiceContextRegistry.RegisterContext(collection => RegisterMicrosoftTeamsContext(collection));
    }
    
    public static IServiceCollection RegisterMicrosoftTeamsContext(this IServiceCollection services)
    {
        services.TryAddSingleton<IMicrosoftTeams, MicrosoftTeams>();
        return services;
    }

    public static IMicrosoftTeams MicrosoftTeams(this IModuleContext context) => context.ServiceProvider.GetRequiredService<IMicrosoftTeams>();
}