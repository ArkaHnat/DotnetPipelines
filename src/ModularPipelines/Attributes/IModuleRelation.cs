using ModularPipelines.Modules;

namespace ModularPipelines.Attributes;

public interface IModuleRelation
{
    public Type Type { get; }

    public bool IgnoreIfNotRegistered { get; set; }

    public bool ResolveIfNotRegistered { get; set; }
}
