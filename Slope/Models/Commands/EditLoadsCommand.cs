using Slope.Editors;
using Slope.Services;

namespace Slope.Models.ConsoleUI
{
    public class EditLoadsCommand : MenuCommand
    {
        public EditLoadsCommand() : base("Edit loads")
        { }

        public override void Execute(MenuContext context)
        {
            base.Execute(context);

            var collectionEditor = new CollectionEditor<SurfaceLoad>(new ObjectEditor<SurfaceLoad>());

            collectionEditor.Edit(context.Input!.Loads);

            var projectService = ServiceFactory.Get<IProjectService>();
            projectService.SaveInput(context);
        }
    }
}
