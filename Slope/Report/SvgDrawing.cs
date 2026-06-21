using Slope.Report.Contracts;
using Slope.Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slope.Report
{
    public class SvgDrawing
    {
        private readonly List<ISvgElement> _elements = [];

        public double Width { get; set; } = 800;
        public double Height { get; set; } = 600;

        public void Add(ISvgElement element)
        {
            _elements.Add(element);
        }

        public string ToSvg()
        {
            var sb = new StringBuilder();

            var vb = GetViewBox();

            sb.AppendLine($"<svg xmlns='http://www.w3.org/2000/svg' width='{Width}' height='{Height}' viewBox='{vb.minX} {vb.minY} {vb.width} {vb.height}'>");

            sb.AppendLine($"<g transform='scale(1,-1) translate(0,{-1 * vb.height})'>");

            foreach (var element in _elements)
            {
                sb.AppendLine(element.ToSvg());
            }

            sb.AppendLine("</g>");
            sb.AppendLine("</svg>");

            return sb.ToString();
        }

        private SvgBounds ComputeBounds()
        {
            var bounds = SvgBounds.Empty;

            foreach (var e in _elements)
            {
                bounds = bounds.Expand(e.GetBounds());
            }

            return bounds;
        }

        private (double minX, double minY, double width, double height) GetViewBox(double margin = 2)
        {
            var b = ComputeBounds();

            double minX = b.MinX - margin;
            double minY = b.MinY + margin;

            double width = (b.MaxX - b.MinX) + 2 * margin;
            double height = (b.MaxY - b.MinY) + 2 * margin;

            return (minX, minY, width, height);
        }
    }
}
