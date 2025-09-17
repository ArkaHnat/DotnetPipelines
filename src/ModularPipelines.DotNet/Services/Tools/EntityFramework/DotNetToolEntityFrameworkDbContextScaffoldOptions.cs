using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Options;
using ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;
using ModularPipelines.Options;

namespace ModularPipelines.DotNet.Services.Tools.SonarScanner;

public record DotNetToolEntityFrameworkDbContextScaffoldOptions : DotNetOptions
{
	public DotNetToolEntityFrameworkDbContextScaffoldOptions() : base()
	{
		CommandParts = [IDotnetToolEntityFramework.PackageNameConst, "DbContext", "Scaffold"];
	}

}