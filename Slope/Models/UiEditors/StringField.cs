namespace Slope.Models.UiEditors;

public sealed class StringField : IInputField
{
    public static readonly StringField Instance = new();

    public object Read(string caption, object? currentValue)
    {
        Console.Write($"{caption}");

        if (currentValue != null)
            Console.Write($" [{currentValue}]");

        Console.Write(": ");

        var text = Console.ReadLine();

        return string.IsNullOrWhiteSpace(text)
            ? currentValue!
            : text;
    }
}
