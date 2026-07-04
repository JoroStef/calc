using Slope.Attributes;
using Slope.Models.UiEditors;
using System.Reflection;

namespace Slope.Models.ConsoleUI;

public class EditLayersCommand : MenuCommand
{
    public EditLayersCommand() : base("Edit layers")
    { }

    public override void Execute(MenuContext context)
    {
        base.Execute(context);

        var layer = new SoilLayer();

        var collectionEditor = new CollectionEditor<SoilLayer>(new ObjectEditor<SoilLayer>());
        collectionEditor.Edit(context.Input.Layers);
    }
}
