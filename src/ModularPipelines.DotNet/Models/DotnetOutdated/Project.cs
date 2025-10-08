namespace DotnetModularPipelines.DotNet.Models.DotnetOutdated;

public class Project
{
    public required string Name { get; set; }

    public required string FilePath { get; set; }

    public required Targetframework[] TargetFrameworks { get; set; }
}
