using Slope.Attributes;

namespace Slope.Models;

public record Project
{
    [InputField("Id", 0)]
    [Prompt("Id")]
    public string Id { get; init; } = "";

    [InputField("Name", 0)]
    [Prompt("Project name")]
    public string Name { get; init; } = "";

    [InputField("Parent Folder", 0)]
    [Prompt("Parent folder")]
    public string ParentFolder { get; init; } = "";
}
