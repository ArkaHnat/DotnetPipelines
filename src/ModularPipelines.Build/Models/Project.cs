namespace ModularPipelines.Build.Model;

public class Project
{
    public required string Name { get; set; }

    public required string FilePath { get; set; }

    public required Targetframework[] TargetFrameworks { get; set; }
}
