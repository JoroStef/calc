namespace Slope.Metadata.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class InputCollectionAttribute : Attribute
{
    public string Title { get; }

    public int Order { get; }

    public InputCollectionAttribute(string title, int order)
    {
        Title = title;
        Order = order;
    }
}
