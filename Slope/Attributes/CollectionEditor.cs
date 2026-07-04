using System;
using System.Reflection;

namespace Slope.Attributes;

public sealed class CollectionEditor<T> where T : class, new()
{
    private readonly ObjectEditor<T> _objectEditor;
    List<(PropertyInfo Property, ColumnAttribute Column)> _columns;

    public CollectionEditor(ObjectEditor<T> objectEditor)
    {
        _objectEditor = objectEditor;

        _columns = typeof(T).GetProperties()
    .Select(p => (Property: p, Column: p.GetCustomAttribute<ColumnAttribute>()))
.Where(x => x.Column != null)
.OrderBy(x => x.Column!.Order)
.ToList();

    }

    public void Edit(IList<T> items)
    {
        while (true)
        {
            Draw(items);

            Console.WriteLine();
            Console.Write("[A]dd  [E]dit  [D]elete  [B]ack > ");

            var cmd =
                Console.ReadLine()?
                    .Trim()
                    .ToUpperInvariant();

            switch (cmd)
            {
                case "A":
                    Add(items);
                    break;

                case "E":
                    EditItem(items);
                    break;

                case "D":
                    Delete(items);
                    break;

                case "B":
                    return;
            }
        }
    }

    private void Add(IList<T> items)
    {
        Console.WriteLine();

        var item = _objectEditor.Create();

        items.Add(item);
    }

    private void EditItem(IList<T> items)
    {
        if (items.Count == 0)
            return;

        var index = ReadIndex(items.Count);

        _objectEditor.Edit(items[index]);
    }

    private void Delete(IList<T> items)
    {
        if (items.Count == 0)
            return;

        var index =
            ReadIndex(items.Count);

        items.RemoveAt(index);
    }

    private static int ReadIndex(int count)
    {
        while (true)
        {
            Console.Write($"Index (1-{count}): ");

            if (int.TryParse(Console.ReadLine(), out var index) &&
                index >= 1 &&
                index <= count)
            {
                return index - 1;
            }

            Console.WriteLine("Invalid index.");
        }
    }

    private void Draw(IList<T> items)
    {
        Console.Clear();

        PrintHeader();
        
        Console.WriteLine(new string('-', 60));

        if (items.Count == 0)
        {
            Console.WriteLine("<empty>");
            return;
        }

        PrintRows(items);
    }

    private void PrintHeader()
    {
        Console.Write("#".PadRight(3));

        foreach (var c in _columns)
        {
            Console.Write(c.Column!.Title.PadRight(c.Column.Width));
        }

        Console.WriteLine();
    }

    private void PrintRows(IList<T> items)
    {
        for (int row = 0; row < items.Count; row++)
        {
            Console.Write($"{row + 1}".PadRight(3));

            foreach (var c in _columns)
            {
                var value = c.Property.GetValue(items[row]);

                string text = Format(value, c.Column!);

                Console.Write(text.PadRight(c.Column.Width));
            }

            Console.WriteLine();
        }

    }

    private static string Format(object? value, ColumnAttribute column)
    {
        if (value == null)
            return "";

        if (value is IFormattable formattable &&
            !string.IsNullOrEmpty(column.Format))
        {
            return formattable.ToString(column.Format, null);
        }

        return value.ToString() ?? "";
    }
}
