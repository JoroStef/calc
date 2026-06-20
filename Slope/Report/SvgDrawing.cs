using Slope.Report.Contracts;
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

        public void Add(ISvgElement element)
        {
            _elements.Add(element);
        }

        public string ToSvg()
        {
            var sb = new StringBuilder();

            sb.AppendLine("<svg xmlns='http://www.w3.org/2000/svg'>");

            foreach (var element in _elements)
            {
                sb.AppendLine(element.ToSvg());
            }

            sb.AppendLine("</svg>");

            return sb.ToString();
        }
    }
}
