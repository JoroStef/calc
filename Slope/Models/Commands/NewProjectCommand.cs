using Slope.Helpers;

namespace Slope.Models.ConsoleUI
{
    public class NewProjectCommand : MenuCommand
    {
        public NewProjectCommand() : base("New project")
        { }

        public override void Execute(MenuContext context)
        {
            base.Execute(context);

            var projName = ConsoleInput.ReadString("Enter project name");

            // Work with default folder for all projects - shall be well known
            // Create a new subfolder in it. Handle name duplications (conflicts with existing).
            // Create new input object and parse it 

            context.ProjectFolder = projName;
            context.Input = new CalculationInput();
        }
    }
}
