using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using ModularPipelines.Attributes;
using ModularPipelines.Examples;
using ModularPipelines.Examples.Modules;
using ModularPipelines.Extensions;
using ModularPipelines.Host;
using ModularPipelines.Modules;
using ModularPipelines.Options;

await PipelineHostBuilder.Create()
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)
            .AddUserSecrets<Program>()
            .AddCommandLine(args)
            .AddEnvironmentVariables();
    })
    .ConfigurePipelineOptions((_, options) =>
    {
        options.ExecutionMode = ExecutionMode.StopOnFirstException;
        options.IgnoreCategories = new[] { "Ignore" };
    })
    .ConfigureServices((context, collection) =>
    {
        collection.Configure<MyOptions>(context.Configuration);

        // ModularPipelinesHelpers.InjectRequiredModulesAsync(collection, args).Wait();
        collection.InjectRequiredModules(args);
    })
    .ExecutePipelineAsync();
