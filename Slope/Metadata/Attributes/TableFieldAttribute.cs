namespace Slope.Metadata.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class TableFieldAttribute : Attribute
{
    public TableFieldAttribute(int order, string title)
    {
        Order = order;
        Caption = title;
    }

    public TableFieldAttribute(int order, string caption, string unit)
    {
        Order = order;
        Caption = caption;
        Unit = unit;
    }

    public int Order { get; }

    public string Caption { get; }

    public string? Unit { get; }

    public int Width { get; init; } = 12;

    public string HtmlWidth { get; set; } = "80px";

    public string? Format { get; init; }

    public bool Editable { get; init; } = true;
}
