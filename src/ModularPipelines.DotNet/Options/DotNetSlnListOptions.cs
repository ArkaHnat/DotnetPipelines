using ModularPipelines.Attributes;
using System.Diagnostics.CodeAnalysis;


namespace DotnetModularPipelines.DotNet.Options;

[ExcludeFromCodeCoverage]
[CommandFollowingArguments("list")]
public record DotNetSlnListOptions : DotNetSlnOptions
{
    public DotNetSlnListOptions(
        string solutionFile
    )
    {
        SolutionFile = solutionFile;
    }

    public DotNetSlnListOptions()
    {
        CommandParts = ["[<SOLUTION_FILE>]"];
    }

    [PositionalArgument(PlaceholderName = "[<SOLUTION_FILE>]")]
    public string? SolutionFile { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether display solution folder paths. [default: False].
    /// </summary>
    [BooleanCommandSwitch("--solution-folders")]
    public virtual bool SolutionFolders { get; set; }
}