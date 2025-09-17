using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Options;
using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;
using ModularPipelines.Options;

namespace ModularPipelines.DotNet.Services.Tools.SonarScanner;

public record DotNetToolEntityFrameworkDbContextInfoOptions : DotNetOptions
{
	public DotNetToolEntityFrameworkDbContextInfoOptions() : base()
	{
		CommandParts = [IDotnetToolEntityFramework.PackageNameConst, "DbContext", "Info"];
	}

}