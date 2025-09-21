using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Options;

namespace DotnetModularPipelines.DotNet.Services.Tools.EntityFramework.Options;

public record DotnetToolEntityFrameworkOptions : DotNetOptions
{
	public DotnetToolEntityFrameworkOptions() : base()
	{
	}

	/// <summary>
	/// Gets or sets show JSON output.
	/// </summary>
	[BooleanCommandSwitch("--json")]
	public virtual bool? Json { get; set; }

	/// <summary>
	/// Gets or sets the DbContext class to use. Class name only or fully qualified with namespaces. If this option is omitted, EF Core will find the context class. If there are multiple context classes, this option is required.
	/// </summary>
	[CommandSwitch("--context")]
	public virtual string Context { get; set; }

	/// <summary>
	/// Gets or sets relative path to the project folder of the target project. Default value is the current folder.
	/// </summary>
	[CommandSwitch("--project")]
	public virtual string Project { get; set; }

	/// <summary>
	/// Gets or sets relative path to the project folder of the startup project. Default value is the current folder.
	/// </summary>
	[CommandSwitch("--startup-project")]
	public virtual string StartupProject { get; set; }

	/// <summary>
	/// Gets or sets the Target Framework Moniker for the target framework. Use when the project file specifies multiple target frameworks, and you want to select one of them.
	/// </summary>
	[CommandSwitch("--framework ")]
	public virtual string Framework { get; set; }

	/// <summary>
	/// Gets or sets the build configuration, for example: Debug or Release.
	/// </summary>
	[CommandSwitch("--configuration")]
	public virtual string Configuration { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the target runtime to restore packages for. For a list of Runtime Identifiers (RIDs), see the RID catalog.
	/// </summary>
	[CommandSwitch("--runtime ")]
	public virtual string Runtime { get; set; }

	/// <summary>
	/// Gets or sets don't build the project. Intended to be used when the build is up-to-date.
	/// </summary>
	[BooleanCommandSwitch("--no-build")]
	public virtual bool? NoBuild { get; set; }

	/// <summary>
	/// Gets or sets show help information.
	/// </summary>
	[BooleanCommandSwitch("--help")]
	public virtual bool? Help { get; set; }

	/// <summary>
	/// Gets or sets don't colorize output.
	/// </summary>
	[BooleanCommandSwitch("--no-color")]
	public virtual bool? NoColor { get; set; }

	/// <summary>
	/// Gets or sets show verbose output.
	/// </summary>
	[BooleanCommandSwitch("--verbose")]
	public virtual bool? Verbose { get; set; }

	/// <summary>
	/// Gets or sets prefix output with level.
	/// </summary>
	[BooleanCommandSwitch("--prefix-output")]
	public virtual bool? PrefixOutput { get; set; }
}