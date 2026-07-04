using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slope.Models.UiEditors;

public sealed class DoubleField : IInputField
{
    public static readonly DoubleField Instance = new();

    public object Read(string caption, object? currentValue)
    {
        while (true)
        {
            Console.Write($"{caption}");

            if (currentValue != null)
                Console.Write($" [{currentValue}]");

            Console.Write(": ");

            var text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text))
                return currentValue!;

            if (double.TryParse(text, out var value))
                return value;

            Console.WriteLine("Invalid number.");
        }
    }
}
