using System.Diagnostics.CodeAnalysis;
using ModularPipelines.Attributes;

namespace ModularPipelines.DotNet.Options;

[ExcludeFromCodeCoverage]
public record DotNetToolOutdatedInstallOptions : DotNetToolInstallOptions, IDotnetToolOutdatedOptions
{

	public DotNetToolOutdatedInstallOptions() : base(IDotnetToolOutdatedOptions.PackageNameConst)
	{
	}
}