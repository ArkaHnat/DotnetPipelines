using System.Reflection;

namespace DotnetModularPipelines.Extensions;

internal static class AttributeHelpers
{
    public static IEnumerable<T> GetCustomAttributesIncludingBaseInterfaces<T>(this Type type)
        where T : Attribute
    {
        foreach (var result in type.GetInterfaces()
                     .SelectMany(i => i.GetCustomAttributes<T>(true)))
        {
            yield return result;
        }

        foreach (var customAttribute in type.GetCustomAttributes<T>(true))
        {
            yield return customAttribute;
        }
    }

    public static IEnumerable<T> GetCustomAttributesIncludingInherited<T>(this Type type)
        where T : Attribute
    {
        var attributes = new List<T>();

        // Traverse the inheritance hierarchy
        while (type != null && type != typeof(object))
        {
            var typeAttributes = type.GetCustomAttributes<T>(false);
            attributes.AddRange(typeAttributes);
            type = type.BaseType; // Move to the parent class
        }

        return attributes;
    }
}