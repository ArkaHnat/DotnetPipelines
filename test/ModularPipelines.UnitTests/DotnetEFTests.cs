
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Json;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
using DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;
using ModularPipelines.Attributes;
using ModularPipelines.Context;
using ModularPipelines.DotNet.Extensions;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Modules;
using ModularPipelines.TestHelpers;
using Shouldly;
namespace ModularPipelines.UnitTests;

public class DotnetEfTests : TestBase
{
	public class DbContetextListModule : Module<List<DotnetEfDbContextListElement>>
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
	public class MigrationsListModule : Module<List<DotnetEfMigrationsListElement>>
	{
		protected override async Task<List<DotnetEfMigrationsListElement>?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
		{
			var dbContextListOptions = new DotNetToolEntityFrameworkDbContextListOptions()
			{
				Json = true,
				NoBuild = true,
				Project = context.Git().RootDirectory + "\\test\\ModularPipelines.EFForTests\\ModularPipelines.EFForTests.csproj"
			};
			var contexts = await context.DotNet().Tool.EntityFramework.DbContext.List(dbContextListOptions);

			var migrationListOptions = new DotNetToolEntityFrameworkMigrationsListOptions()
			{
				Json = true,
				NoBuild = true,
				Context = contexts.FirstOrDefault().Name,
				Project = context.Git().RootDirectory + "\\test\\ModularPipelines.EFForTests\\ModularPipelines.EFForTests.csproj"
			};
			var result = await context.DotNet().Tool.EntityFramework.Migrations.List(migrationListOptions);
			return result;
		}
	}


			[ModularPipelines.Attributes.DependsOn<DbContetextListModule>]
	[ResolveDependencies]
	public class MyInfoModule : Module<DotnetEfDbContextInfoElement>
	{
		protected override async Task<DotnetEfDbContextInfoElement?> ExecuteAsync(IPipelineContext context, CancellationToken cancellationToken)
		{
			var contexts = await GetModule<DbContetextListModule>();
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

	[Skip("Temporarly disable due to failure on GithubActions")]
	[Test]
	public async Task ShouldGetTwoContexts()
	{
		var myModule1 = await RunModule<DbContetextListModule>();
		myModule1.Result.Value!.Count.ShouldBe(2);
	}

	[Skip("Temporarly disable due to failure on GithubActions")]
	[Test]
	public async Task InfoTest()
	{
		var myModule1 = await RunModule<MyInfoModule>();
		myModule1.Result.Value!.ShouldNotBeNull();
	}
	[Skip("Temporarly disable due to failure on GithubActions")]
	[Test]
	public async Task MigrationListTest()
	{
		var myModule1 = await RunModule<MigrationsListModule>();
		myModule1.Result.Value!.Count.ShouldBe(2);
	}
}