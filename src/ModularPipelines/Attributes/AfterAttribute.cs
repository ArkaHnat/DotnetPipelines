using ModularPipelines.Modules;

namespace ModularPipelines.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
public class AfterAttribute<TModule> : AfterAttribute
    where TModule : ModuleBase
{
    public AfterAttribute() : base(typeof(TModule))
    {
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
public class AfterAttribute : Attribute, IModuleRelation
{
    public AfterAttribute(Type type)
    {
        if (!type.IsAssignableTo(typeof(ModuleBase)))
        {
            throw new Exception($"{type.FullName} is not a Module class");
        }

        Type = type;
    }

    public Type Type { get; }

    public bool IgnoreIfNotRegistered { get; set; }
}