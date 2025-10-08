namespace DotnetModularPipelines.DotNet.Models.DotnetOutdated;

public class Targetframework
{
    public required string Name { get; set; }

    public required Dependency[] Dependencies { get; set; }
}
