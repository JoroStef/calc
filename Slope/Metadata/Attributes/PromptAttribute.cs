namespace Slope.Metadata.Attributes;

public sealed class PromptAttribute : Attribute
{
    public PromptAttribute(string text)
    {
        Text = text;
    }

    public string Text { get; }
}
