using Slope.Metadata;
using Slope.Metadata.Attributes;
using System.Text;

namespace Slope.Rendering;

public class HtmlTableRenderer<T>
{
    private readonly IReadOnlyList<FieldInfo> _fields = ModelMetadata.GetFields<T>();

    public string Render(IEnumerable<T> items)
    {
        var sb = new StringBuilder();

        sb.AppendLine("<table>");

        sb.AppendLine("<thead>");
        sb.AppendLine("<tr>");

        foreach (var field in _fields)
        {
            sb.Append($"<th style=\"width:{field.Attribute.HtmlWidth}\">")
              .Append(field.Attribute.Caption)
              .AppendLine("</th>");
        }

        sb.AppendLine("</tr>");
        sb.AppendLine("</thead>");

        sb.AppendLine("<tbody>");

        foreach (var item in items)
        {
            sb.AppendLine("<tr>");

            foreach (var field in _fields)
            {
                var value = field.Property.GetValue(item);


                sb.Append("<td>")
                  .Append(Format(value, field.Attribute))
                  .AppendLine("</td>");
            }

            sb.AppendLine("</tr>");
        }

        sb.AppendLine("</tbody>");
        sb.AppendLine("</table>");

        return sb.ToString();
    }

    private static string Format(
        object? value,
        TableFieldAttribute field)
    {
        if (value == null)
            return "";

        if (value is IFormattable f &&
            field.Format != null)
            return f.ToString(field.Format, null);

        return value.ToString() ?? "";
    }
}
