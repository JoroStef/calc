using Slope.Editors;
using Slope.Services;

namespace Slope.Models.ConsoleUI;

public class EditLayersCommand : MenuCommand
{
    public EditLayersCommand() : base("Edit layers")
    { }

    public override void Execute(MenuContext context)
    {
        base.Execute(context);

        var collectionEditor = new CollectionEditor<SoilLayer>(new ObjectEditor<SoilLayer>());

        collectionEditor.Edit(context.Input!.Layers);

        var projectService = ServiceFactory.Get<IProjectService>();
        projectService.SaveInput(context);
    }
}
