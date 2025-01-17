using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.DotNet.Options;

[ExcludeFromCodeCoverage]
public record DotNetToolSonarScannerInstallOptions : DotNetToolInstallOptions, IDotnetToolSonarScanner
{

	public DotNetToolSonarScannerInstallOptions() : base(IDotnetToolSonarScanner.PackageNameConst)
	{
	}
}