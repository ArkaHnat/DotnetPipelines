using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Options;

namespace ModularPipelines.DotNet.Services.Tools.SonarScanner;

[ExcludeFromCodeCoverage]
public record DotNetToolSonarScannerInstallOptions : DotNetToolInstallOptions, IDotnetToolSonarScanner
{

	public DotNetToolSonarScannerInstallOptions() : base(IDotnetToolSonarScanner.PackageNameConst)
	{
	}
}