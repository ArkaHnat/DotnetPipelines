namespace ModularPipelines.Attributes;

public class Resolve : Attribute
{
    public bool Reliants { get; set; }

    public bool Dependencies { get; set; }

    public bool IndirectReliants { get; set; }

    public bool IndirectDependency { get; set; }

    public bool TriggeringModules { get; set; }

    public bool TriggeredByModules { get; set; }
}