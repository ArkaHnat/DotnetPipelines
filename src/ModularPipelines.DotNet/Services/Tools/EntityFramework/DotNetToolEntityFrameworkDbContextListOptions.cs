using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Options;
using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;
using ModularPipelines.Options;

namespace ModularPipelines.DotNet.Services.Tools.SonarScanner;

public record DotNetToolEntityFrameworkDbContextListOptions : DotNetOptions
{
	public DotNetToolEntityFrameworkDbContextListOptions() : base()
	{
		CommandParts = [IDotnetToolEntityFramework.PackageNameConst, "DbContext", "List"];
	}

}