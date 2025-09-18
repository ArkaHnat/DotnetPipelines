
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Json;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;
using ModularPipelines.TestHelpers;
using Shouldly;
using Status = ModularPipelines.Enums.Status;
namespace ModularPipelines.UnitTests;

public class DotnetEfTests : TestBase
{
    public class MyListModule : Module<List<DotnetEfDbContextListElement>>
    {
        protected override async Task<List<DotnetEfDbContextListElement>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
        {
            var options = new DotNetToolEntityFrameworkDbContextListOptions()
            {
                Json = true,
                NoBuild = true,
                Project = context.Git().RootDirectory + "\\test\\ModularPipelines.EFForTests\\ModularPipelines.EFForTests.csproj"
            };
			var result = await context.DotNet().Tool.EntityFramework.DbContext.List(options);

            return result;
        }
    }

	[ModularPipelines.Attributes.DependsOn<MyListModule>]
	[ResolveDependencies]
	public class MyInfoModule : Module<DotnetEfDbContextInfoElement>
	{
		protected override async Task<DotnetEfDbContextInfoElement?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
		{
			var contexts = await GetModule<MyListModule>();
			var options = new DotNetToolEntityFrameworkDbContextInfoOptions()
			{
				Json = true,
				NoBuild = true,
				Context = contexts.Value!.FirstOrDefault().Name,
				Project = context.Git().RootDirectory + "\\test\\ModularPipelines.EFForTests\\ModularPipelines.EFForTests.csproj"
			};
			var result = await context.DotNet().Tool.EntityFramework.DbContext.Info(options);

			return result;
		}
	}

	[Test]
    public async Task ShouldGetTwoContexts()
    {
        var myModule1= await RunModule<MyListModule>();
        myModule1.Result.Value!.Count.ShouldBe(2);
	}
	[Test]
	public async Task InfoTest()
	{
		var myModule1 = await RunModule<MyInfoModule>();
		myModule1.Result.Value!.ShouldNotBeNull();
	}
}