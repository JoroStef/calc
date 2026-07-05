namespace Slope.Editors.UiEditors;

public interface IInputField
{
    object? Read(string caption, object? currentValue);
}
