namespace Slope.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class InputFieldAttribute : Attribute
    {
        public string Label { get; }

        public int Order { get; }

        public InputFieldAttribute(string label, int order)
        {
            Label = label;
            Order = order;
        }
    }
}
