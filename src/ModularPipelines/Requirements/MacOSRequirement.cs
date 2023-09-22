﻿using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Context;

namespace ModularPipelines.Requirements;

[ExcludeFromCodeCoverage]
public class MacOSRequirement : IPipelineRequirement
{
    public Task<bool> MustAsync(IPipelineContext context)
    {
        return Task.FromResult(context.Environment.OperatingSystem == OperatingSystemIdentifier.MacOS);
    }
}