namespace Slope.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ColumnAttribute : Attribute
{
    public ColumnAttribute(int order, string title)
    {
        Order = order;
        Title = title;
    }

    public ColumnAttribute(int order, string title, string unit)
    {
        Order = order;
        Title = title;
        Unit = unit;
    }

    public int Order { get; }

    public string Title { get; }

    public string? Unit { get; }

    public int Width { get; init; } = 12;

    public string? Format { get; init; }

    public bool Editable { get; init; } = true;
}
