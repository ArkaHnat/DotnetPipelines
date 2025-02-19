using ModularPipelines.Options;

namespace ModularPipelines.DotNet.Services.Tools;

public record DotnetToolStrykerOptions : CommandLineToolOptions
{
	public DotnetToolStrykerOptions(string tool) : base(tool)
	{
	}

	public DotnetToolStrykerOptions(string tool, params string[]? arguments) : base(tool, arguments)
	{
	}

	protected DotnetToolStrykerOptions(CommandLineToolOptions original) : base(original)
	{
	}
}