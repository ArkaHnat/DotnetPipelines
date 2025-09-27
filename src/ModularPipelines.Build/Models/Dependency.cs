namespace ModularPipelines.Build.Model;

public class Dependency
{
    public required string Name { get; set; }

    public required string ResolvedVersion { get; set; }

    public required string LatestVersion { get; set; }

    public required string UpgradeSeverity { get; set; }

    public bool Upgraded { get; set; }
}
