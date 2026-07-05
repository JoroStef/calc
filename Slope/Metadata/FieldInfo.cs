using Slope.Metadata.Attributes;
using System.Reflection;

namespace Slope.Metadata;

public record FieldInfo(
    PropertyInfo? Property,
    TableFieldAttribute? Attribute
    );
