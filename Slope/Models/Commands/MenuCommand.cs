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
        Console.WriteLine($"Execuiting '{Title}' ...");
        Console.ReadLine();
    }
}