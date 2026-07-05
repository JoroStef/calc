using Slope.Metadata;
using Slope.Metadata.Attributes;

namespace Slope.Rendering;

public class ConsoleTableRenderer<T>
{
    private readonly IReadOnlyList<FieldInfo> _fields = ModelMetadata.GetFields<T>();

    public void Render(IEnumerable<T> items)
    {
        //
        // Header
        //

        Console.Write("#".PadRight(4));

        foreach (var field in _fields)
        {
            Console.Write(field.Attribute.Caption.PadRight(field.Attribute.Width));
        }   

        Console.WriteLine();

        Console.WriteLine(new string('-', 60));

        //
        // Rows
        //

        int index = 1;

        foreach (var item in items)
        {
            Console.Write(index++.ToString().PadRight(4));

            foreach (var field in _fields)
            {
                var value = field.Property.GetValue(item);

                Console.Write(Format(value, field.Attribute).PadRight(field.Attribute.Width));
            }

            Console.WriteLine();
        }
    }

    private static string Format(
        object? value,
        TableFieldAttribute field)
    {
        if (value == null)
            return "";

        if (value is IFormattable f &&
            field.Format != null)
            return f.ToString(field.Format, null);

        return value.ToString() ?? "";
    }
}
