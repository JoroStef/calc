using Slope.Attributes;
using Slope.Models;
using System.Reflection;

public static class MetadataGenerator
{
    public static MetadataFile Generate<TInput>()
    {
        var file = new MetadataFile();

        foreach (var property in typeof(TInput).GetProperties())
        {
            var collection =
                property.GetCustomAttribute<InputCollectionAttribute>();

            if (collection == null)
                continue;

            var itemType = GetCollectionItemType(property.PropertyType);

            var metadata = new CollectionMetadata
            {
                Title = collection.Title,
                ItemType = itemType.Name,
                Columns = GetFields(itemType)
            };

            file.Collections.Add(property.Name, metadata);
        }

        return file;
    }

    private static List<FieldMetadata> GetFields(Type type)
    {
        return type
            .GetProperties()
            .Select(p => new
            {
                Property = p,
                Attribute = p.GetCustomAttribute<InputFieldAttribute>()
            })
            .Where(x => x.Attribute != null)
            .OrderBy(x => x.Attribute!.Order)
            .Select(x => new FieldMetadata
            {
                Property = x.Property.Name,
                Label = x.Attribute!.Label,
                Type = GetHtmlType(x.Property.PropertyType)
            })
            .ToList();
    }

    private static Type GetCollectionItemType(Type type)
    {
        if (!type.IsGenericType)
            throw new InvalidOperationException(
                $"{type.Name} is not a generic collection.");

        return type.GetGenericArguments()[0];
    }

    private static string GetHtmlType(Type type)
    {
        if (type == typeof(string))
            return "text";

        if (type == typeof(bool))
            return "checkbox";

        if (type.IsEnum)
            return "enum";

        if (type == typeof(int) ||
            type == typeof(long) ||
            type == typeof(float) ||
            type == typeof(double) ||
            type == typeof(decimal))
            return "number";

        return "text";
    }
}