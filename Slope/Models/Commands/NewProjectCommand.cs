using Slope.Editors;

namespace Slope.Models.ConsoleUI;

public class NewProjectCommand : MenuCommand
{
    public NewProjectCommand() : base("New project")
    { }

    public override void Execute(MenuContext context)
    {
        base.Execute(context);

        base.Execute(context);

        var objectEditor = new ObjectEditor<Project>();

        var projectObj = objectEditor.Create();

        var projectFolder = Path.Combine(projectObj.ParentFolder, projectObj.Name);

        if (!Directory.Exists(projectFolder))
        {
            Directory.CreateDirectory(projectFolder);
        }

        context.ProjectFolder = projectFolder;
        context.Input = new CalculationInput();
    }
}
