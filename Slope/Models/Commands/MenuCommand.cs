using Slope.Services;

public class MenuCommand
{
    protected MenuCommand(string title)
    {
        Title = title;
    }

    public string Key { get; set; } = string.Empty;

    public string Title { get; }

    public virtual bool CanExecute(MenuContext context) => true;

    public virtual void Execute(MenuContext context) 
    {
        if (context == null)
        {
            return;
        }
    }
}