using ModularPipelines.Options;

namespace ModularPipelines.DotNet.Services.Tools.DotnetEntityFramework;

public record DotnetToolEntityFrameworkOptions : CommandLineToolOptions
{
	public DotnetToolEntityFrameworkOptions(string tool) : base(tool)
	{
	}

	public DotnetToolEntityFrameworkOptions(string tool, params string[]? arguments) : base(tool, arguments)
	{
	}

	protected DotnetToolEntityFrameworkOptions(CommandLineToolOptions original) : base(original)
	{
	}
	
}