namespace ModularPipelines.Build.Model;

public class Targetframework
{
    public required string Name { get; set; }

    public required Dependency[] Dependencies { get; set; }
}
