﻿using ModularPipelines.Context;
using ModularPipelines.Docker.Extensions;
using ModularPipelines.FileSystem;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Models;
using ModularPipelines.Modules;

namespace ModularPipelines.UnitTests.Helpers;

public class DockerTests : TestBase
{
    private class DockerBuildModule : Module<CommandResult>
    {
        protected override async Task<ModuleResult<CommandResult>?> ExecuteAsync(IModuleContext context, CancellationToken cancellationToken)
        {
            var pretendPath = context.Git()
                .RootDirectory
                .GetFolder("src")
                .GetFolder("MyApp")
                .GetFile("Dockerfile");

            return await context.Docker().Build(new(pretendPath)
            {
                InternalDryRun = true,
                BuildArgs = new KeyValueVariables("=")
                {
                    ["Arg1"] = "Value1",
                    ["Arg2"] = "Value2",
                    ["Arg3"] = "Value3"
                },
                Tag = "mytaggedimage",
                Output = "type=local,dest=out",
                Target = "build-env"
            }, token: cancellationToken);
        }
    }

    [Test]
    public async Task DockerBuild_CorrectInputCommand()
    {
        var module = await RunModule<DockerBuildModule>();

        var result = await module;

        var dockerfilePath = new Folder(Environment.CurrentDirectory).Parent!.Parent!.Parent!.Parent!.Parent!.GetFolder("src")
            .GetFolder("MyApp").GetFile("Dockerfile").Path;

        Assert.That(result.Value!.CommandInput, Is.EqualTo($"docker build --build-arg Arg1=Value1 --build-arg Arg2=Value2 --build-arg Arg3=Value3 --tag mytaggedimage --target build-env --output type=local,dest=out {dockerfilePath}"));
    }
}