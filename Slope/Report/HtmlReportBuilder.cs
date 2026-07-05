using Slope.Rendering;
using System.Text;

namespace Slope.Report;

public class HtmlReportBuilder
{
    StringBuilder _builder = new StringBuilder();

    public HtmlReportBuilder()
    {
        _builder.AppendLine("""
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset="utf-8"/>
                <title>Calculation Report</title>
                <style>
                    td { text-align: center; }
                    table, th, td { 
                        border: 1px solid black;
                        border-collapse: collapse;
                    }
                </style>
            </head>
            <body>
            """);
    }

    public HtmlReportBuilder H1(string text)
    {
        _builder.Append("<h1>")
                .Append(text)
                .AppendLine("</h1>");

        return this;
    }

    public HtmlReportBuilder H2(string text)
    {
        _builder.Append("<h2>")
                .Append(text)
                .AppendLine("</h2>");

        return this;
    }

    public HtmlReportBuilder Paragraph(string text)
    {
        _builder.Append("<p>")
                .Append(text)
                .AppendLine("</p>");

        return this;
    }

    public HtmlReportBuilder Raw(string html)
    {
        _builder.AppendLine(html);

        return this;
    }

    public HtmlReportBuilder Table<T>(IEnumerable<T> items)
    {
        var renderer = new HtmlTableRenderer<T>();

        _builder.Append(renderer.Render(items));

        return this;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append(_builder);

        sb.AppendLine("""
            </body>
            </html>
            """);

        return sb.ToString();
    }
}
