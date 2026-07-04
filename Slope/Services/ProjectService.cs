using Slope.Models;
using System.Text.Json;

namespace Slope.Services;

public class ProjectService : IProjectService
{
    public CalculationInput LoadInput(string projectFolder)
    {
        CalculationInput input = new CalculationInput();
        var inputFile = Path.Combine(projectFolder, "input.json");
        if (!File.Exists(inputFile))
        {
            var inputText = JsonSerializer.Serialize(input);

            File.WriteAllText(inputFile, inputText);
        }
        else
        {
            input = JsonSerializer.Deserialize<CalculationInput>(File.ReadAllText(inputFile));
        }

        return input;
    }

    public void SaveInput(MenuContext context)
    {
        if (!Directory.Exists(context.ProjectFolder))
        {
            Directory.CreateDirectory(context.ProjectFolder);
        }

        var inputString = JsonSerializer.Serialize(context.Input);

        var filePath = Path.Combine(context.ProjectFolder, "input.json");

        File.WriteAllText(filePath, inputString);
    }
}
