using Slope.Attributes;

namespace Slope.Models.ConsoleUI
{
    public sealed class OpenProjectCommand : MenuCommand
    {
        public OpenProjectCommand() : base("Open project")
        { }

        public override void Execute(MenuContext context)
        {
            base.Execute(context);

            var objectEditor = new ObjectEditor<Project>();

            var projectObj = objectEditor.Create();

            var projectFolder = Path.Combine(projectObj.ParentFolder, projectObj.Name);

            if (!Directory.Exists(projectFolder))
            {
                Directory.CreateDirectory(projectFolder);
            }

            // Work with default folder for all projects - shall be well known
            // Create a new subfolder in it. Handle name duplications (conflicts with existing).
            // Create new input object and parse it 

            context.ProjectFolder = projectFolder;
            context.Input = new CalculationInput();
        }
    }
}