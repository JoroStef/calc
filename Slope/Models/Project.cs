using Slope.Metadata.Attributes;

namespace Slope.Models;

public record Project
{
    [Prompt("Id")]
    public string Id { get; init; } = "";

    [Prompt("Name")]
    public string Name { get; init; } = "";

    [Prompt("Parent Folder")]
    public string ParentFolder { get; init; } = "";
}
