using Slope.Editors;
using Slope.Report;

namespace Slope.Models.ConsoleUI
{
    public class OpenReportCommand : MenuCommand
    {
        public OpenReportCommand() : base("Open report")
        { }

        public override void Execute(MenuContext context)
        {
            base.Execute(context);

            var report = new HtmlReportBuilder();

            report
                .H1("Input")
                .H2("Geometry")
                .Table(context.Input.Geometry)
                .H2("Soil layers")
                .Table(context.Input.Layers)
                .H2("Surface loads")
                .Table(context.Input.Loads);

            var reportPath = Path.Combine(context.ProjectFolder, "report.html");
            File.WriteAllText(reportPath, report.ToString());
        }

    }
}
