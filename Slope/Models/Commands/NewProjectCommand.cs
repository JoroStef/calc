using Slope.Models.UiEditors;
using Slope.Services;

namespace Slope.Models.ConsoleUI
{
    public class NewProjectCommand : MenuCommand
    {
        public NewProjectCommand() : base("New project")
        { }

        public override void Execute(MenuContext context)
        {
            base.Execute(context);

            var inputFueld = InputFieldFactory.Get(typeof(string));

            var projectFolder = inputFueld.Read("Project folder", null) as string;

            if (!Directory.Exists(projectFolder))
            {
                Console.WriteLine("Directory doesn't exist.");
                return;
            }

            var projectService = ServiceFactory.Get<IProjectService>();
            CalculationInput input = projectService.LoadInput(projectFolder);

            context.ProjectFolder = projectFolder;
            context.Input = input;
        }
    }
}
