using ModularPipelines.Attributes;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options.Migrations;

public record DotNetToolEntityFrameworkMigrationsBundleOptions : DotNetToolEntityFrameworkMigrationsOptions
{
	public DotNetToolEntityFrameworkMigrationsBundleOptions() : base()
	{
		CommandParts = ["Bundle"];
	}

	/// <summary>
	/// Gets or sets the path of executable file to create.
	/// </summary>
	[CommandSwitch("--output")]
	public virtual string? Output { get; set; }

	/// <summary>
	/// Gets or sets the target runtime to bundle for.
	/// </summary>
	[CommandSwitch("--target-runtime ")]
	public virtual string? TargetRuntime { get; set; }

	/// <summary>
	/// Gets or sets overwrite existing files.
	/// </summary>
	[BooleanCommandSwitch("--force")]
	public virtual bool? Force { get; set; }

	/// <summary>
	/// Gets or sets overwrite existing files.
	/// </summary>
	[BooleanCommandSwitch("--self-contained")]
	public virtual bool? SelfContained { get; set; }
}