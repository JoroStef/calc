namespace Slope.Models.ConsoleUI
{
    public class ExitCommand : MenuCommand
    {
        public ExitCommand() : base("Exit")
        { }

        public override void Execute(MenuContext context)
        {
            base.Execute(context);

            context.IsRunning = false;
        }
    }
}