using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;
/// <summary>
/// Adds new migration.
/// </summary>
/// 

[CommandPrecedingArguments("add")]
public record DotNetToolEntityFrameworkMigrationsAddOptions : DotNetToolEntityFrameworkMigrationsOptions
{
	public DotNetToolEntityFrameworkMigrationsAddOptions() : base()
	{
		CommandParts = ["<NAME>"];
	}

	/// <summary>
	///     Gets or sets the file to write the result to.
	/// </summary>
	[CommandSwitch("--output-dir")]
	public virtual string? OutputDir { get; set; }

	/// <summary>
	/// Gets or sets the name of the migration.
	/// </summary>
	[PositionalArgument(PlaceholderName = "<NAME>")]
	public string? Name { get; set; }

	/// <summary>
	///  Gets or sets the namespace to use for the generated classes. Defaults to generated from the output directory.
	/// </summary>
	[CommandSwitch("--namespace")]
	public virtual string? Namespace { get; set; }
}