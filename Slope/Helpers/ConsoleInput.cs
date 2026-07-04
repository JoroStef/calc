namespace Slope.Helpers;

public static class ConsoleInput
{
    public static T Read<T>(
        string prompt,
        Func<string, (bool Success, T Value)> parser,
        Func<T, bool>? validator = null,
        string validationMessage = "Invalid value.")
    {
        while (true)
        {
            Console.Write($"{prompt}: ");

            var text = Console.ReadLine() ?? "";

            var result = parser(text);

            if (!result.Success)
            {
                Console.WriteLine("Invalid format.");
                continue;
            }

            if (validator != null &&
                !validator(result.Value))
            {
                Console.WriteLine(validationMessage);
                continue;
            }

            return result.Value;
        }
    }

    public static string ReadString(string prompt)
    {
        return Read(
            prompt, 
            s => (true, s),
            s => !string.IsNullOrWhiteSpace(s),
            validationMessage: "Can not be empty");
    }

    public static int ReadInt(string prompt)
    {
        return Read(prompt, s =>
        {
            var ok = int.TryParse(s, out var value);

            return (ok, value);
        });
    }

    public static double ReadDouble(string prompt)
    {
        return Read(prompt, s =>
        {
            var ok = double.TryParse(
                s,
                out var value);

            return (ok, value);
        });
    }

    public static TEnum ReadEnum<TEnum>(string prompt)
    where TEnum : struct, Enum
    {
        return Read(prompt, s =>
        {
            var ok =
                Enum.TryParse<TEnum>(s, true, out var value);

            return (ok, value);
        });
    }
}
