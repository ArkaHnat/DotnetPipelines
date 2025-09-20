using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.DbContext;
/// <summary>
/// Generates a compiled version of the model used by the DbContext and precompiles queries.
/// </summary>
/// 
[CommandPrecedingArguments("optimize")]
public record DotNetToolEntityFrameworkDbContextOptimizeOptions : DotnetToolEntityFrameworkToolRunEFDbContextOptions
{
	public DotNetToolEntityFrameworkDbContextOptimizeOptions() : base()
	{
	}

	/// <summary>
	/// Gets or sets the directory to put files in. Paths are relative to the project directory.
	/// </summary>
	[CommandSwitch("--output-dir")]
	public virtual string OutputDir { get; set; }

	/// <summary>
	/// Gets or sets the namespace to use for all generated classes. Defaults to generated from the root namespace and the output directory plus CompiledModels.
	/// </summary>
	[CommandSwitch("--namespace")]
	public virtual string Namespace { get; set; }

	/// <summary>
	/// Gets or sets the suffix to attach to the name of all the generated files. E.g. .g could be used to indicate that these files contain generated code.
	/// </summary>
	[CommandSwitch("--suffix")]
	public virtual string Suffix { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether don't generate a compiled model. This is used when the compiled model has already been generated.
	/// </summary>
	[BooleanCommandSwitch("--no-scaffold")]
	public virtual bool NoScaffold { get; set; }

	/// <summary>
	///     Gets or sets generate precompiled queries. This is required for NativeAOT compilation if the target project contains any queries.
	/// </summary>
	[BooleanCommandSwitch("--precompile-queries")]
	public virtual string PrecompileQueries { get; set; }

	/// <summary>
	///     Gets or sets generate precompiled queries. This is required for NativeAOT compilation if the target project contains any queries.
	/// </summary>
	[BooleanCommandSwitch("--nativeaot")]
	public virtual string NativeAot { get; set; }
}