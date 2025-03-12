using ModularPipelines.Attributes;
using ModularPipelines.DotNet.Options;

namespace ModularPipelines.DotNet.Services.Tools.DotnetOutdated;

public record DotnetToolOutdatedRunOptions : DotNetOptions, IDotnetToolOutdatedOptions
{
	public DotnetToolOutdatedRunOptions(string projectKey)
	{
		CommandParts = ["tool", "run", IDotnetToolOutdatedOptions.PackageNameConst, projectKey];
	}

	/// <summary>
	/// Gets or sets include-auto-references=.<boolean>"
	/// Specifies whether to include auto-referenced packages.
	/// </summary>
	[BooleanCommandSwitch("--include-auto-references")]
	public bool? IncludeAutoReferences { get; set; }

	/// <summary>
	/// Gets or sets transitive"
	/// Specifies whether it should detect transitive dependencies.
	/// </summary>
	[BooleanCommandSwitch("--transitive")]
	public bool? Transitive { get; set; }

	/// <summary>
	/// Gets or sets transitive depth"
	/// Defines how many levels deep transitive dependencies should be analyzed. Integer value (default = 1).
	/// </summary>
	[CommandSwitch("--transitive-depth")]
	public int? TransitiveDepth { get; set; }

	// <summary>

	// Gets or sets upgrade"
	// Specifies whether it should detect upgrade.
	// </summary>
	[BooleanCommandSwitch("--upgrade")]
	public bool? Upgrade { get; set; }

	// <summary>

	// Gets or sets fail-on-updates "
	// Specifies whether it should return a non-zero exit code when updates are found.
	// </summary>
	[BooleanCommandSwitch("--fail-on-updates")]
	public bool? FailOnUpdates { get; set; }

	// <summary>

	// Gets or sets IncludeFilter"
	// Specifies to only look at packages where the name contains the provided string. Culture and case insensitive. If provided multiple times, a single match is enough to include a package..
	// </summary>
	[CommandSwitch("--include","=")]
	public string? IncludeFilter { get; set; }

	// <summary>

	// Gets or sets ExcludeFilter "
	// Specifies to only look at packages where the name does not contain the provided string. Culture and case insensitive. If provided multiple times, a single match is enough to exclude a package.
	// </summary>
	[CommandSwitch("--exclude ")]
	public string? ExcludeFilter { get; set; }

	// <summary>

	// Gets or sets output "
	// Specifies the filename for a generated report. (Use the -of|--output-format option to specify the format. JSON by default.)
	// </summary>
	[CommandSwitch("--output ")]
	public string? OutputFilename { get; set; }

	// <summary>

	// Gets or sets output-format "
	// pecifies the output format for the generated report. Possible values: json (default), csv, or markdown.
	// Allowed values are: Json, Csv, Markdown. 
	// Default value is: Json.
	// </summary>
	[CommandSwitch("--output-format")]
	public DotnetToolUpdateOutputFormat? OutputFormat { get; set; }

	// <summary>

	// Gets or sets older-than "
	// SOnly include package versions that are older than the specified number of days.
	// Default value is: 0.
	// </summary>
	[CommandSwitch("--older-than ")]
	public int? OlderThan { get; set; }

	// <summary>

	// Gets or sets no-restore"
	// Specifies whether it should return a non-zero exit code when updates are found.
	// </summary>
	[CommandSwitch("--no-restore")]
	public bool? NoRestore { get; set; }

	// <summary>

	// Gets or sets recursive "
	// Specifies whether it should return a non-zero exit code when updates are found.
	// </summary>
	[CommandSwitch("--recursive")]
	public bool? Recursive { get; set; }

	// <summary>

	// Gets or sets ignore-failed-sources"
	// Specifies whether it should return a non-zero exit code when updates are found.
	// </summary>
	[CommandSwitch("--ignore-failed-sources ")]
	public bool? IgnoreFailedSources { get; set; }

	// <summary>

	// Gets or sets fail-on-updates "
	// Specifies whether it should return a non-zero exit code when updates are found.
	// </summary>
	[CommandSwitch("--fail-on-updates ")]
	public bool? IncludeUpToDate { get; set; }

	// <summary>
	// Gets or sets runtime "
	// Specifies whether it should return a non-zero exit code when updates are found.
	// </summary>
	[CommandSwitch("--runtime ")]
	public string? Runtime { get; set; }
}
