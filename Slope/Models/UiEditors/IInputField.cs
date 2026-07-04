namespace Slope.Models.UiEditors;

public interface IInputField
{
    object? Read(string caption, object? currentValue);
}
