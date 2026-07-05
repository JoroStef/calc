using Slope.Editors;
using Slope.Services;

namespace Slope.Models.ConsoleUI;

public sealed class EditGeometryCommand : MenuCommand
{
    public EditGeometryCommand() : base("Edit geometry")
    { }

    public override void Execute(MenuContext context)
    {
        base.Execute(context);

        var collectionEditor = new CollectionEditor<Point2D>(new ObjectEditor<Point2D>());

        collectionEditor.Edit(context.Input!.Geometry);

        var projectService = ServiceFactory.Get<IProjectService>();
        projectService.SaveInput(context);
    }
}