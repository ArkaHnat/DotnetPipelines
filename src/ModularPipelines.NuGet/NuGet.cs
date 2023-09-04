﻿using ModularPipelines.Models;
using ModularPipelines.Context;
using ModularPipelines.Extensions;
using ModularPipelines.NuGet.Options;
using TomLonghurst.EnumerableAsyncProcessor.Extensions;

namespace ModularPipelines.NuGet;

internal class NuGet : INuGet
{
    private readonly IModuleContext _context;

    public NuGet(IModuleContext context)
    {
        _context = context;
    }

    public async Task<CommandResult[]> UploadPackages(NuGetUploadOptions options)
    {
        return await options.PackagePaths.ToAsyncProcessorBuilder()
            .SelectAsync(async packagePath => 
                await _context.Command.ExecuteCommandLineTool(options.WithArguments(packagePath)))
            .ProcessOneAtATime();
    }

    public async Task<CommandResult> AddSource(NuGetSourceOptions options)
    {
        return await _context.Command.ExecuteCommandLineTool(options);
    }
}
