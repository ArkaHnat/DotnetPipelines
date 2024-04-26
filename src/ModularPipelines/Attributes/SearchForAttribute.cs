namespace ModularPipelines.Attributes;

public class SearchForAttribute : Attribute
{
    public bool SearchForReliants { get; set; }

    public bool SearchForDependencies { get; set; }

    public bool SearchForIndirectReliants { get; set; }

    public bool SearchForIndirectDependencies { get; set; }
}