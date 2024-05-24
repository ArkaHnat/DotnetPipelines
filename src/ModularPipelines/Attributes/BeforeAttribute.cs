using ModularPipelines.Modules;

namespace ModularPipelines.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
public class BeforeAttribute : Attribute, IModuleRelation
{
    public BeforeAttribute(Type type)
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

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
public class BeforeAttribute<TModule> : BeforeAttribute 
    where TModule : ModuleBase
{
    public BeforeAttribute() : base(typeof(TModule))
    {
    }
}