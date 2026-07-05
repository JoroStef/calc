using Slope.Metadata.Attributes;
using System.Reflection;

namespace Slope.Metadata;

public static class ModelMetadata
{
    public static IReadOnlyList<FieldInfo> GetFields<T>()
    {
        return typeof(T)
            .GetProperties()
            .Select(p => new
            {
                Property = p,
                Attribute = p.GetCustomAttribute<TableFieldAttribute>()
            })
            .Where(x => x.Attribute != null)
            .OrderBy(x => x.Attribute!.Order)
            .Select(x => new FieldInfo(
                x.Property,
                x.Attribute!))
            .ToList();
    }
}
